using Godot;
using System;
using System.Diagnostics;

public partial class main : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// // Get a reference to the spawner Node2D (replace "Spawner" with your spawner node name)
		// var spawner = GetNode<Spawners>("Spawners");

		// if (spawner != null)
		// {
		//     // Call the spawner's method to spawn a character
		//     spawner.SpawnCharacter();
		// }
		// else
		// {
		//     Debug.Print("Spawner node not found!");
		//}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	
	public void _on_timer_timeout()
	{
		// Get a reference to the spawner Node2D (replace "Spawner" with your spawner node name)
		var spawner = GetNode<Spawners>("Spawners");
		
		if (spawner != null)
		{
			// Call the spawner's method to spawn a character
			spawner.SpawnCharacter();
		}
		else
		{
			Debug.Print("Spawner node not found!");
		}
	}
	
}


