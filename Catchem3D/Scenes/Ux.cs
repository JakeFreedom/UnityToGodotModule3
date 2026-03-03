using Godot;

public partial class Ux : Control
{

	private int playerScore;
	private Label playerScoreLabel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() => playerScoreLabel = GetNode<Label>("MarginContainer/HBoxContainer/Score");

	public void UpdatePlayerScore(int newScore)
	{
		playerScore += newScore;
		playerScoreLabel.Text = playerScore.ToString();
	}
}
