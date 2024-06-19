using Godot;
using System;
using System.Collections;

public partial class slashTEST : Area2D
{
	[Export] public float speed = 100.0f; // Speed of the projectile
	[Export] public int slashDamage = 5; // Damage dealt by the projectile
	private const double RANGE = 1200.0; // Maximum travel range
	private float distanceTraveled = 0.0f; // Distance traveled by the projectile
	private Vector2 direction = Vector2.Zero; // Direction of the projectile's movement

	public override void _Ready()
	{
		// Get the mouse position in the world
		Vector2 mousePosition = GetGlobalMousePosition();

		// Calculate the direction vector from the current position to the mouse position
		direction = GlobalPosition.DirectionTo(mousePosition).Normalized();

		// Calculate the angle in radians for rotation
		float angleInRadians = direction.Angle();

		// Rotate the projectile to face the direction
		Rotation = angleInRadians;
	}

	public override void _PhysicsProcess(double delta)
	{
		 // Move the projectile in the calculated direction at the given speed
		//Position += direction * (float)(speed * delta);
	}

	// This function is called when the projectile collides with another body
	public async void _on_body_entered(Node body)
	{
		// Check if the body implements the IHittable interface and is not the main player
		if (body is IHittable hittable && !(body is main_kucing2d))
		{
			hittable.TakeDamage(slashDamage); // Apply damage to the hit object
		}
		await ToSignal(GetTree().CreateTimer(0.35), SceneTreeTimer.SignalName.Timeout);
		QueueFree();


	}
}
