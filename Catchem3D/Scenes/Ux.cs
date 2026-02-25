using Godot;
using System;

public partial class Ux : Control
{

	private int playerScore;
	private Label playerScoreLabel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playerScoreLabel = GetNode<Label>("MarginContainer/HBoxContainer/Score");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	public void UpdatePlayerScore(int newScore)
	{
		playerScore += newScore;
		playerScoreLabel.Text = playerScore.ToString();
	}
}
