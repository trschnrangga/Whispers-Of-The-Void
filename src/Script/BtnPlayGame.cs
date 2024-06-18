using Godot;
using System;

public partial class BtnPlayGame : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	//public override void _Process(double delta)
	//{
	//}
	
	private void _On_Button_Play_Pressed()
	{
		GetTree().ChangeSceneToFile("res://src/Scene/map_selection_menu.tscn");
	}
}



