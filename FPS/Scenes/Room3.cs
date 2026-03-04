using Godot;
using System;

public partial class Room3 : Node3D
{
	[Export] PackedScene target;
	[Export] TierSpawnRange SpawnRange;	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void SpawnTarget()
	{

	}


	private Node3D CreateTarget(Vector3 positionalData)
	{
		return new Node3D();
	}
}
