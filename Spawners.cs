using Godot;
using Microsoft.VisualBasic;
using System;
using System.Diagnostics;

public partial class Spawners : Node2D
{
	public PackedScene scene = ResourceLoader.Load<PackedScene>("res://main_enemytest1.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void SpawnCharacter()
	{
		Vector2 vector2 = new Vector2(1000, 0);
		RandomNumberGenerator randnum = new RandomNumberGenerator();
		var randz = randnum.RandfRange(0, 2*(float)Math.PI);
		var instance = scene.Instantiate<CharacterBody2D>();
		AddChild(instance);
		instance.Position = GetNode<CharacterBody2D>("/root/desert_map/Entities/MainCat").Position + vector2.Rotated(randz);
		
	}
}
