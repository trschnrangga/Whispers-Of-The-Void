using Godot;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;


public partial class main_kucing2d : CharacterBody2D, IHittable
{
	[Export] private int _catHealth = 100;
	[Export] private float _catSpeed = 70.0f;

	public int CatHealth { get {return _catHealth; } set { _catHealth = value; } }
	public float CatSpeed { get {return _catSpeed;} set { _catSpeed = value; } }

	private float flashDuration = 0.1f;
	private bool canShoot = true;
	private bool canDash = true;
	private Color catOriginalColor;
	private AnimatedSprite2D catSprite;

	public enum Direction {right, left, idle}
	private Direction lastDirection = Direction.idle;

    public override void _Ready()
    {
        catSprite = GetNode<AnimatedSprite2D>("Cat");
		catOriginalColor = catSprite.Modulate;

    }
    public async void GetInput()
    {

        Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
        Velocity = inputDirection * CatSpeed;
		Velocity.Normalized();

		if (Input.IsActionPressed("dash"))
		{
			if (canDash)
			{
				canDash = false;
				Dash();
				await ToSignal(GetTree().CreateTimer(1.5), SceneTreeTimer.SignalName.Timeout);
				canDash = true;
			}
		}

		if (canShoot)
		{
			canShoot = false;
			if (Input.IsActionPressed("click"))
			{
				ShootWave();
				await ToSignal(GetTree().CreateTimer(0.5), SceneTreeTimer.SignalName.Timeout);
			}
			canShoot = true;
		}
    }
    public override void _PhysicsProcess(double delta)
    {
        GetInput();
        MoveAndSlide();
		Directionals();
		//GD.Print(CatHealth);

    }

	public void Directionals()
	{
		AnimatedSprite2D v_sprite = GetNode<AnimatedSprite2D>("Cat");
		//Move Animation
		if (Velocity.X > 1 || Velocity.X < -1 || Velocity.Y > 1 || Velocity.Y < -1){
			v_sprite.Animation = "jalan";
		}
		else
		{
			v_sprite.Animation = "IDLE";
		}

		//Direction following last direction
		if (Velocity.X < 0){
			v_sprite.FlipH = true;
			lastDirection = Direction.left;
		}
		else if (Velocity.X > 0){
			v_sprite.FlipH = false;
			lastDirection = Direction.right;
		}
		else if (Velocity.X == 0){
			if (lastDirection == Direction.left){
				v_sprite.FlipH = true;
			}
			else if (lastDirection == Direction.left){
				v_sprite.FlipH = false;
			}
		}
	}

	public async void Dash()
	{
		CatSpeed = 400;
		await ToSignal(GetTree().CreateTimer(0.125), SceneTreeTimer.SignalName.Timeout);
		CatSpeed = 70.0f;
	}
	
	public void ShootWave()
	{
		PackedScene soundWave = ResourceLoader.Load<PackedScene>("res://projectile_test.tscn");
		var newSoundWave = soundWave.Instantiate<Node2D>();
		var player = GetNode<CharacterBody2D>("/root/main/Entities/MainCat");
		newSoundWave.GlobalPosition = player.GlobalPosition;
		GetTree().Root.GetNode("main/Entities").AddChild(newSoundWave);
	}	
	public void TakeDamage(int damage)
	{
		Flash();
		CatHealth -= damage;
		if (CatHealth <= 0)
		{
			//game over/death animation
		}
	}

	public async void Flash()
	{
		catSprite.Modulate = new Color(255, 255, 255);
		await ToSignal(GetTree().CreateTimer(flashDuration), SceneTreeTimer.SignalName.Timeout);
		catSprite.Modulate = catOriginalColor;
	}
	
}