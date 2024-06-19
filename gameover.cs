using Godot;
using System;

public partial class gameover : CanvasLayer
{
	private Button retryButton;
	private gameover gameOverScreen;
	private Node sceneRoot;
	public override void _Ready()
	{
		gameOverScreen = GetNode<gameover>(".");
		gameOverScreen.Visible = false;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_retry_button_pressed()
	{
		GetTree().ReloadCurrentScene();
	}

	public void _on_menu_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://src/Scene/main_menu.tscn");
	}

	public void Enabled()
	{
		gameOverScreen.Visible = true;
	}

	
}
