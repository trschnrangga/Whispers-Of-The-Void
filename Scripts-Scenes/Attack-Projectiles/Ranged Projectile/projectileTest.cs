using Godot;
using System;

public partial class projectileTest : Area2D
{
	[Export] public float speed = 120.0f; // Speed of the projectile
	[Export] public int waveDamage = 5; // Damage dealt by the projectile
	private const double RANGE = 500.0; // Maximum travel range
	private float distanceTraveled = 0.0f; // Distance traveled by the projectile
	private int pierceHealth = 3;
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
		Position += direction * (float)(speed * delta);

		// Update the distance traveled
		distanceTraveled += speed * (float)delta;

		// Check if the projectile has exceeded its range
		if (distanceTraveled > RANGE)
		{
			QueueFree(); // Remove the projectile from the scene
		}


		
	}

	// This function is called when the projectile collides with another body
	public void _on_body_entered(Node body)
	{
		// Check if the body implements the IHittable interface and is not the main player
		if (body is IHittable hittable && !(body is main_kucing2d))
		{
			hittable.TakeDamage(waveDamage); // Apply damage to the hit object
			pierceHealth--;
		}

		if (pierceHealth <= 0)
		{
			// QueueFree();
		}

	}

}
