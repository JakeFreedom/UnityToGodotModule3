using Godot;
using System;

public partial class Player : Node2D
{

	[Export]
	public int playerMovmentSpeed = 500;

	CharacterBody2D player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetNode<CharacterBody2D>("PlayerSprite");
		//We are going to need a ref to the sprite
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		////We will limit bounds with a collider. 
		//if(Input.IsActionPressed("translate_left"))
		//{
		//	player.Position -= new Vector2(playerMovmentSpeed * (float)delta, 0);
		//	vel
		//}

		//if(Input.IsActionPressed("translate_right"))
		//{
  //          player.Position += new Vector2(playerMovmentSpeed * (float)delta, 0);
  //      }
	}
}
