using Godot;
using System;


public partial class healthBar : ProgressBar
{
	// Called when the node enters the scene tree for the first time.
	double health = 0;
	ProgressBar damageBar;
	public override void _Ready()
	{
		damageBar = GetNode<ProgressBar>("damageBar");

        // Connect the SlashCompleted signal to the OnSlashCompleted method
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public async void SetHealth(double new_health)
	{
		var prev_health = health;
		health = min(MaxValue, new_health);
		Value = health;


		if (health <= 0)
		{
			QueueFree();
		}

		if (health < prev_health)
		{
			await ToSignal(GetTree().CreateTimer(0.35), SceneTreeTimer.SignalName.Timeout);
			damageBar.Value = health;
		}
		else
		{
			damageBar.Value = health;
		}
	}

	public void InitHealth(double _health)
	{
		health = _health;
		MaxValue = health;
		Value = health;
		damageBar.MaxValue = health;
		damageBar.Value = health;
	}

	public double min(double a, double b)
	{
		if (a > b)
		{
			return b;
		}
		else
		{
			return a;
		}
	}

}
