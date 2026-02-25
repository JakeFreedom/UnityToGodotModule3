using Godot;
public partial class Player : Node2D
{

	[Export] int playerMovmentSpeed = 500;

	CharacterBody2D player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() => player = GetNode<CharacterBody2D>("PlayerSprite");
}
