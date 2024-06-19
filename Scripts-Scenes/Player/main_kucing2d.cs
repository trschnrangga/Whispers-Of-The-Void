using Godot;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;


public partial class main_kucing2d : CharacterBody2D, IHittable
{
	[Export] private float _catHealth = 100;
	[Export] private float _catSpeed = 70.0f;
	[Export] private double DashCooldown = 1.5;
	[Export] private double ShootCooldown = 0.75;
	[Export] private double SlashCooldown = 0.35;

	public float MaxCatHealth { get {return _catHealth;}}
	public float CatHealth { get {return _catHealth; } set { _catHealth = value; } }
	public float CatSpeed { get {return _catSpeed;} set { _catSpeed = value; } }

	private float flashDuration = 0.1f;
	private bool canShoot = true;
	private bool canDash = true;
	private bool canSlash = true;
	private bool isDead = false;
	private bool healthbarRemoved = false;
	private Color catOriginalColor;
	private AnimatedSprite2D catSprite;
	private healthBar healthbar;
	private Node sceneRoot;
	public enum Direction {right, left, idle}
	private Direction lastDirection = Direction.idle;
	private Vector2 headleft, headright;
	private gameover gameOver;

    public override void _Ready()
    {
		headleft = new Vector2(-1, 1);
		headright = new Vector2(1, 1);
		sceneRoot = GetTree().CurrentScene;
        catSprite = GetNode<AnimatedSprite2D>("Cat");
		catOriginalColor = catSprite.Modulate;
		healthbar = GetNode<healthBar>("healthBar");
        healthbar.InitHealth(CatHealth);
		gameOver = sceneRoot.GetNode<gameover>("GAMEOVER");

    }
	public void Directionals()
	{
		AnimatedSprite2D v_sprite = GetNode<AnimatedSprite2D>("Cat");
		CharacterBody2D kucing = GetNode<CharacterBody2D>(".");


		if (!isDead)
		{
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
		else
		{
			v_sprite.Animation = "DEAD";
		}
	}
    public async void GetInput()
    {
		if (!isDead)
		{
			Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
			Velocity = inputDirection * CatSpeed;
			Velocity.Normalized();

			if (Input.IsActionPressed("dash"))
			{
				if (canDash && lastDirection != Direction.idle)
				{
					canDash = false;
					Dash();
					await ToSignal(GetTree().CreateTimer(DashCooldown), SceneTreeTimer.SignalName.Timeout);
					canDash = true;
				}
			}

			if (canShoot)
			{
				canShoot = false;
				if (Input.IsActionPressed("m2"))
				{
					ShootWave();
					await ToSignal(GetTree().CreateTimer(ShootCooldown), SceneTreeTimer.SignalName.Timeout);
				}
				canShoot = true;
			}

			if (canSlash)
			{
				canSlash = false;
				if (Input.IsActionPressed("m1"))
				{
					Slash();
					await ToSignal(GetTree().CreateTimer(SlashCooldown), SceneTreeTimer.SignalName.Timeout);
				}
				canSlash = true;
			}
		}
		else
		{
			Vector2 notMoving = new Vector2(0,0);
			Velocity = notMoving;
		}
    }
    public override void _PhysicsProcess(double delta)
    {
        GetInput();
        MoveAndSlide();
		Directionals();
		//GD.Print(CatHealth);
    }


	public async void Dash()
	{
		CatSpeed = 400;
		await ToSignal(GetTree().CreateTimer(0.125), SceneTreeTimer.SignalName.Timeout);
		CatSpeed = 70.0f;
	}
	
	public void ShootWave()
	{
		PackedScene soundWave = ResourceLoader.Load<PackedScene>("res://Scenes/projectile_test.tscn");
		var newSoundWave = soundWave.Instantiate<Node2D>();
		var player = sceneRoot.GetNode<CharacterBody2D>("Entities/MainCat");
		newSoundWave.GlobalPosition = player.GlobalPosition;
		sceneRoot.GetNode(".").AddChild(newSoundWave);
	}

	public void Slash()
	{
		PackedScene slash = ResourceLoader.Load<PackedScene>("res://Scenes/slash_test.tscn");
		var newSlash = slash.Instantiate<Node2D>();
		var player = sceneRoot.GetNode<CharacterBody2D>("Entities/MainCat");
		newSlash.GlobalPosition = Vector2.Zero;
		sceneRoot.GetNode("Entities/MainCat").AddChild(newSlash);
		
	}	
	public async void TakeDamage(int damage)
	{
		if (!healthbarRemoved)
		{
			CatHealth -= damage;
			if (CatHealth >= 0)
			{
				
				Flash();
				healthbar.SetHealth(CatHealth);	
			

				//game over/death animation
			}
			else
			{
				isDead = true;
				healthbar.Remove();
				healthbarRemoved = true;
				await ToSignal(GetTree().CreateTimer(2.5), SceneTreeTimer.SignalName.Timeout);
				gameOver.Enabled();
				
			}
		}
	}

	public async void Flash()
	{
		catSprite.Modulate = new Color(255, 255, 255);
		await ToSignal(GetTree().CreateTimer(flashDuration), SceneTreeTimer.SignalName.Timeout);
		catSprite.Modulate = catOriginalColor;
	}

	
	
}