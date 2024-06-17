using Godot;
using System;
using System.Collections.Generic;

public partial class main_enemytest1 : CharacterBody2D, IHittable
{
    [Export] private int _eyeHealth = 15;
    [Export] public float _eyeSpeed = 50.0f;
    [Export] private int _eyeDamage = 5;

    public int EyeHealth { get { return _eyeHealth; } set { _eyeHealth = value; } }
    public float EyeSpeed { get { return _eyeSpeed; } set { _eyeSpeed = value; } }
    public int EyeDamage { get { return _eyeDamage; } set { _eyeDamage = value; } }

	private float flashDuration = 0.15f;
    private Color _originalColor;
    private AnimatedSprite2D _eyeSprite;
    private Area2D _hurtBox;
    private NodePath _playerPath = "/root/main/Entities/MainCat";
    private healthBar healthbar;
	private bool bodyEntered = false;
	private bool canDamage = true;

    public override void _Ready()
    {
        _eyeSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _originalColor = _eyeSprite.Modulate;
        _hurtBox = GetNode<Area2D>("HurtBox"); // Assume your Area2D node is named "HurtBox"
        healthbar = GetNode<healthBar>("healthBar");
        healthbar.InitHealth(EyeHealth);
    }

    public override async void _PhysicsProcess(double delta)
    {
        // Move towards the player
        if (GetNode(_playerPath) is CharacterBody2D player)
        {
            Velocity = Position.DirectionTo(player.Position) * EyeSpeed;
            MoveAndSlide();
        }

        if (bodyEntered && canDamage)
		{
			if (canDamage)
			{
				canDamage = false;
				CheckOverlappingBodies();
				await ToSignal(GetTree().CreateTimer(1.25), SceneTreeTimer.SignalName.Timeout);
				canDamage = true;
			}
		}
		
		//GD.Print(_health);
    }

    private void CheckOverlappingBodies()
    {
        var overlappingBodies = _hurtBox.GetOverlappingBodies();
        foreach (var body in overlappingBodies)
        {
            // Assuming 'main_kucing2d' is the player's class and implements 'IHittable'
            if (body is main_kucing2d kucing)
            {
                kucing.TakeDamage(_eyeDamage);
				GD.Print("overlapping body");
                
            }
        }
    }

    public async void TakeDamage(int bulletDamage)
    {
        EyeHealth -= bulletDamage;  
        Flash();
        if (EyeHealth <= 0)
        {
            await ToSignal(GetTree().CreateTimer(0.2), SceneTreeTimer.SignalName.Timeout);
            QueueFree();
        }
        if (healthbar != null)
        {
            healthbar.SetHealth(EyeHealth);
        }
    }

    public async void Flash()
    {
        _eyeSprite.Modulate = new Color(255, 255, 255); // Set to white
        await ToSignal(GetTree().CreateTimer(flashDuration), SceneTreeTimer.SignalName.Timeout);
        _eyeSprite.Modulate = _originalColor; // Revert to original color
    }

	private async void _on_hurt_box_body_entered(Node body)
    {
        // Check if the body is the player and apply damage
        if (body is main_kucing2d kucing)
        {
            kucing.TakeDamage(_eyeDamage);
			GD.Print("on body entered");
			await ToSignal(GetTree().CreateTimer(1), SceneTreeTimer.SignalName.Timeout);
			bodyEntered = true;
        }
    }
}