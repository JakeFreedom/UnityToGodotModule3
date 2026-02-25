using Godot;
using System;
using System.Reflection;

public partial class CoinDrop1 : Node3D
{
	[Export]
	public float rotationSpeed = 10;
	[Export(PropertyHint.Range, ".5, 3,.1")]
	public float fallingSpeed = 3;
	[Export(PropertyHint.Range, "1,5,1")]
	public int pointValue = 1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Area3D>("Area3D").Name = "Drop 1";
		this.Name = "Drop 1";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//this.Position += new Vector3(0, -fallingSpeed * (float)delta, 0);
		//this.Rotation += new Vector3(0, (float)delta * rotationSpeed, 0);
	}
}
