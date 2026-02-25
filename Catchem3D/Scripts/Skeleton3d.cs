using Godot;
using System;

public partial class Skeleton3d : Skeleton3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AnimationPlayer ap = GetNode<AnimationPlayer>("../AnimationPlayer");
		ap.Play("mixamo_com");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
