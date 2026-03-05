using Godot;
using System;

public partial class VerticleMovingTarget : Node3D
{

	[Export(PropertyHint.Range,".5, 3, .1")]
	public float fallingSpeed = 3.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//Position = new Vector3(Position.X, Position.Y - ((float)delta * fallingSpeed), Position.Z);
	}
}
