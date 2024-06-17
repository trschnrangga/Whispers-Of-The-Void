using Godot;
using System;
using System.Collections.Generic;

public partial class main_enemytest1 : CharacterBody2D, IHittable
{
    [Export] private int _health = 15;
    [Export] public float Speed = 50.0f;
    [Export] private int _eyeDamage = 5;

	private float flashDuration = 0.1f;
    private Color _originalColor;
    private AnimatedSprite2D _eyeSprite;
    private Area2D _hurtBox;
    private NodePath _playerPath = "/root/main/Entities/MainCat"; // Use NodePath for better maintainability
	private bool bodyEntered = false;
	private bool canDamage = true;

    public override void _Ready()
    {
        _eyeSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _originalColor = _eyeSprite.Modulate;
        _hurtBox = GetNode<Area2D>("HurtBox"); // Assume your Area2D node is named "HurtBox"
    }

    public override async void _PhysicsProcess(double delta)
    {
        // Move towards the player
        if (GetNode(_playerPath) is CharacterBody2D player)
        {
            Velocity = Position.DirectionTo(player.Position) * Speed;
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
        _health -= bulletDamage;
        Flash();
        if (_health <= 0)
        {
            await ToSignal(GetTree().CreateTimer(flashDuration), SceneTreeTimer.SignalName.Timeout);
            QueueFree();
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