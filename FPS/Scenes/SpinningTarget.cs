using Godot;
using System;

public partial class SpinningTarget : Node3D
{
	[Export]
	public float maxLeftDistance = -13;
	[Export]
	public float maxRightDistance = -0.3f;
	[Export]
	public float targetMoveSpeed = 5.0f;
	[Export]
	public float targetRPMs = 1;

	float degreesPerFrame;
	Node3D centerHub;
	float targetPosition;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		centerHub = GetNode<Node3D>("Hub");
		targetPosition = maxRightDistance;
		degreesPerFrame = ((targetRPMs * 360) / 60) / 60;
		
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
}
