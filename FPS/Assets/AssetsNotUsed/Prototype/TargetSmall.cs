using Godot;
using System;
using System.Security.AccessControl;

public partial class TargetSmall : Node3D
{

	double t;
	int leftSide = -34;
	int rightSide = -21;
	int targetPosition;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		targetPosition = rightSide;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	//public override void _Process(double delta)
	//{
	//	if (Position.X <= targetPosition)
	//	{
	//		//GD.Print("if");
	//		targetPosition = rightSide;
	//		Position = new Vector3(Position.X + (float)delta, Position.Y, Position.Z);
	//	}
	//	else
	//	{
	//		//GD.Print("else");
	//			targetPosition = leftSide;
	//			Position = new Vector3(Position.X - (float)delta, Position.Y, Position.Z);
	//	}
 //   }
}
