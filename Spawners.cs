using Godot;
using Microsoft.VisualBasic;
using System;
using System.Diagnostics;

public partial class Spawners : Node2D
{
	public PackedScene scene;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		string currSceneRootName = GetTree().Root.GetChild(1).Name;
        string rootNodePath = currSceneRootName == "main" ? "res://Scenes/main_enemytest1.tscn" : "res://Scenes/desert_insect_enemy.tscn";
        scene = ResourceLoader.Load<PackedScene>(rootNodePath);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        GD.Print(GetTree().Root.GetChild(1).Name);
    }

	public void SpawnCharacter()
	{
		Node sceneRoot = GetTree().CurrentScene; //Get reference to current scene tree
		Vector2 vector2 = new Vector2(500, 0);
		RandomNumberGenerator randnum = new RandomNumberGenerator();
		var randz = randnum.RandfRange(0, 2*(float)Math.PI);
		var instance = scene.Instantiate<CharacterBody2D>();
		AddChild(instance);
		instance.Position = sceneRoot.GetNode<CharacterBody2D>("Entities/MainCat").Position + vector2.Rotated(randz);
		
	}
}
