using Godot;
using System;

public partial class TestTargetStuff : RigidBody3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//ApplyForce(new Vector3((float)delta, (float)-delta, (float)-.01f) ,null);
		if (Input.IsActionJustPressed("fire"))
		{
			ApplyMyForce();
		}
		
	}

	private void ApplyMyForce()
	{
		//this.ApplyImpulse(new Vector3(.01f, .0f, -.01f));
		this.ApplyCentralImpulse(new Vector3(.01f, 0.01f, -.01f));
	}
}
