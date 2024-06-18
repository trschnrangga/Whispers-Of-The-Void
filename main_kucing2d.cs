using Godot;
using System;
using System.Diagnostics;

public partial class main_kucing2d : CharacterBody2D
{
	public  float Speed = 125.0f;
	public enum Direction {right, left, idle}
	private Direction lastDirection = Direction.idle;
	public void GetInput()
	{

		Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
		Velocity = inputDirection * Speed;
		Velocity.Normalized();

		if (Input.IsActionPressed("dash"))
		{
			// Dash();
		}
	}
	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		MoveAndSlide();
		Directionals();

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
		Speed = 400;
		await ToSignal(GetTree().CreateTimer(0.15), SceneTreeTimer.SignalName.Timeout);
		Speed = 125.0f;
	}
	 
}
