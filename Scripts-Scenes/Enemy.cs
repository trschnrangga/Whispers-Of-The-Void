using Godot;
using System;

public partial class Enemy : Node2D
{
	public float Speed {get; set;}

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public override void _PhysicsProcess(double delta)
	{

	}
}

public interface IScoreable
{
	int GetScoreValue();
}
