using Godot;
using System;

public partial class Bullet : MeshInstance3D
{

	public Vector3 velocity = Vector3.Zero;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//
		//GlobalPosition = new Vector3(GlobalPosition.X, GlobalPosition.Y, GlobalPosition.Z - (float)delta * 5);

		//Position += Vector3.Forward;
		GlobalPosition += velocity * (float)delta;
	}
}
