using Godot;
using System;

public partial class ScoreText : Label
{
	Label scoreNum;
	public override void _Ready()
	{
		scoreNum = GetNode<Label>("ScoreNumber");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UpdateScore(string score)
	{
		scoreNum.Text = score;
	}
}
