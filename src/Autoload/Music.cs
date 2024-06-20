using Godot;
using System;

public partial class Music : AudioStreamPlayer
{	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Stream = GD.Load<AudioStream>("res://src/bgm/Super Smash Bros. Ultimate - I'll Face Myself [8-bit; VRC6] (ft. sen-pi).wav");
		VolumeDb = -20;
		Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
