using Godot;
using System;

public partial class Music : AudioStreamPlayer
{	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Stream = GD.Load<AudioStream>("res://src/bgm/2019-08-25_-_8bit-Smooth_Presentation_-_David_Fesliyan.mp3");
		Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
