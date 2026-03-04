using Godot;
using Godot.Collections;
using System;

public partial class TargetController : Node3D
{

	Array<Node> targets;

    int leftSide = -34;
    int rightSide = -20;
    int targetPosition;
	float targetSpeed = 5.0f;
	Dictionary<Node3D, int> targetNodes = new Dictionary<Node3D, int>();


    public override void _Ready()
	{
		targets = GetChildren(false);
		targetPosition = rightSide;
		foreach (Node3D node in targets) {
			targetNodes.Add(node, targetPosition);
		}
	}
		
	public override void _Process(double delta)
	{
		//foreach(Node3D node in targets)
		//{
		//	targetPosition = targetNodes[node];
		//	if (node.Position.X <= targetPosition)
		//	{
  //              targetNodes[node] = rightSide;
		//		node.Position = new Vector3(node.Position.X  +( (float)delta* targetSpeed), node.Position.Y, node.Position.Z);
		//	}
		//	else
		//	{
		//		targetNodes[node] = leftSide;
		//		node.Position = new Vector3(node.Position.X - ((float)delta*targetSpeed), node.Position.Y, node.Position.Z);
		//	}
		//}
	}
}
