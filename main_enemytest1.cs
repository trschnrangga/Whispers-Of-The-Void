using System.Data.Common;
using Godot;
using System;

public partial class main_enemytest1 : CharacterBody2D
{
	public  float Speed = 50.0f;
	public override void _PhysicsProcess(double delta)
	{

		var player = GetNode<CharacterBody2D>("/root/desert_map/Entities/MainCat");
		Velocity = Position.DirectionTo(player.Position) * Speed;
		MoveAndSlide();
	}
}
