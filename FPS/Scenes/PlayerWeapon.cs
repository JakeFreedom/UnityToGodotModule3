using Godot;
using System;

public partial class PlayerWeapon : Node3D
{

	Node3D crossHair;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		crossHair = GetNode<Node3D>("../GunPointer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//LookAt(crossHair.GlobalPosition);
	}
}
