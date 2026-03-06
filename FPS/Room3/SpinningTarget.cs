using Godot;
using System;
using System.Security;

public partial class SpinningTarget : Node3D
{
	[Export] PackedScene radialTargetScene;
	[Export] float maxLeftDistance = -13;
	[Export] float maxRightDistance = -0.3f;
	[Export] float targetMoveSpeed = 5.0f;
	



	float targetRPMs = 1;
	int radialTargetsToSpawn = 1;
	float degreesPerFrame;
	Node3D centerHub;
	float targetPosition;
	int tier;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		centerHub = GetNode<Node3D>("Hub");
		targetPosition = maxRightDistance;
		degreesPerFrame = ((targetRPMs * 360) / 60) / 60;

		SpawnRadialTargets();
	}

	public override void _Process(double delta)
	{
		MoveTargets(delta);

    }

    public override void _PhysicsProcess(double delta)
    {
        //Rotate
        centerHub.RotateZ(Mathf.DegToRad(degreesPerFrame));
    }

	private void SpawnRadialTargets()
	{
		for (int i = 0; i < radialTargetsToSpawn; i++)
		{
			Node3D target = radialTargetScene.Instantiate<Node3D>();
			float radialRotation = Mathf.RadToDeg(i*(Mathf.Tau / radialTargetsToSpawn));
			target.Position = new Vector3(0, 0, .2f);
			target.Scale = new Vector3(1.3f, 1.3f, 1.3f);
			target.RotateZ(Mathf.DegToRad(radialRotation));
			target.RotateY(Mathf.RadToDeg(181));
			centerHub.AddChild(target);
		}
	}
    private void MoveTargets(double delta)
	{
        if (Position.X <= targetPosition)
        {
            targetPosition = maxRightDistance;
            Position = new Vector3(Position.X + ((float)delta * targetMoveSpeed), Position.Y, Position.Z);
        }
        else
        {
            targetPosition = maxLeftDistance;
            Position = new Vector3(Position.X - ((float)delta * targetMoveSpeed), Position.Y, Position.Z);
        }
    }


	public int RadialTargets
	{
		set => radialTargetsToSpawn = value;
	}

	public int Tier
	{
		set
		{
			if (value < 1 || value > 2)
			{ tier = 1; }
			else { tier = value; }
		}//If tier is out of bounds, default to tier 1 <-- This would need to change if there are more tiers of course.
		get => tier;
	}

	public float TargetRPM { set => targetRPMs = value; } 
}
