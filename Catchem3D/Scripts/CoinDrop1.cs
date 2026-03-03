using Godot;
using System;
using System.Reflection;

public partial class CoinDrop1 : Node3D
{
	[Export] private float rotationSpeed = 10;
	[Export(PropertyHint.Range, ".5, 3,.1")] private float fallingSpeed = 3;
	[Export(PropertyHint.Range, "1,5,1")] private int pointValue = 1;
	public override void _Ready()
	{
		GetNode<Area3D>("Area3D").Name = "Drop 1";
		this.Name = "Drop 1";
	}
}
