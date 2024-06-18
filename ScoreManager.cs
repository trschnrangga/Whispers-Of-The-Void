using Godot;
using System;

public partial class ScoreManager : Node
{
	private int _score = 0;
	private ScoreText _scoreText;
	private Node sceneRoot;
	public override void _Ready()
	{
		sceneRoot = GetTree().CurrentScene;
		_scoreText = sceneRoot.GetNode<ScoreText>("CanvasLayer/ScoreText");
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
