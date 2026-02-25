using Godot;
using System;

public partial class ItemDropper : Node3D
{

	[Export]
	public MeshInstance3D[] meshes;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
