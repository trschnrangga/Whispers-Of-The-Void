using Godot;
using System;

public partial class BtnMap2 : Button
{
	//private TextureRect _background;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//background = GD.Load<TextureRect>("res://Assets/UI/map_selection_menu/map-desert.png");
		//_background = GetNode<TextureRect>("background");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	//public override void _Process(double delta)
	//{
		//
	//}
	
	//private void OnMouseEntered()
	//{
		//GetNode<TextureRect>("map_selection_menu/background").Texture = texture;
		//background.Texture = texture;
	//}
	
	private void OnBtnMap2Pressed()
	{
		GetTree().ChangeSceneToFile("res://src/Scene/desert_map.tscn");
	}

	//private void _on_mouse_entered()
	//{
		//_background.Texture = GD.Load<Texture2D>("res://Assets/UI/map_selection_menu/map-desert.png");
	//}
}





