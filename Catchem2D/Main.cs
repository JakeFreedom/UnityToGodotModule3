using Godot;
using System;

public partial class Main : Node2D
{
	[Export]
	Label score;

	int playerScore;
	PlayerSprite player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetNode<PlayerSprite>("Player/PlayerSprite");
		player.ItemCaught += ChangeScore;
		playerScore = 0;
		score.Text = playerScore.ToString();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void ChangeScore(int scoreToAdd) {

		playerScore += scoreToAdd;
		score.Text = playerScore.ToString();

	}
}
