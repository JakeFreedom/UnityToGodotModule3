	using Godot;
using System;

public partial class Obstacles : StaticBody2D
{
	public Vector3 color;

	private int HitCount = 0;
	private Label hitCountLabel;
	private AudioStreamPlayer2D collisionPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Sprite2D>("Sprite2D").Modulate = new Color(color.X , color.Y, color.Z);
		GetNode<Area2D>("HitDetector").AreaEntered += AreaEnteredHandler;
		hitCountLabel = GetNode<Label>("HitCount");
		hitCountLabel.Text = HitCount.ToString();
		//hitCountLabel.Rotation = GlobalRotationDegrees - 90	;
		collisionPlayer = GetNode<AudioStreamPlayer2D>("CollisionPlayer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	private void AreaEnteredHandler(Area2D otherArea)
	{
		HitCount++; hitCountLabel.Text = HitCount.ToString();
		if (otherArea.Owner.IsInGroup("Ball"))
		{
			collisionPlayer.PitchScale = .1f;
			collisionPlayer.Play();
		}
	}
}
