using Godot;
using System;

public partial class ScoreManager : Node
{
	private int _score = 0;
	private ScoreText _scoreText;
	public override void _Ready()
	{
		_scoreText = GetNode<ScoreText>("/root/main/CanvasLayer/ScoreText");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		UpdateScoreText();
	}

	public void AddScore(int points)
	{
		_score += points;

	}

	public void UpdateScoreText()
	{
		_scoreText.UpdateScore(_score.ToString());
	}
}
