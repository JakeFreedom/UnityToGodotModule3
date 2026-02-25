using Godot;
using System;

public partial class ControlPlayer : CharacterBody3D
{

	[Signal]
	public delegate void CollectableCapturedEventHandler();

	public const float Speed = 3.5f;
	public const float JumpVelocity = 4.5f;

	[Export(PropertyHint.Range,".3, 2.2, .1")]
	public float speedBoost = 10.5f;


	AnimationPlayer apWalking;

	AudioStreamPlayer3D caughtSoundEffect;

	public float RotationSpeed = 3.0f;
    public override void _Ready()
    {
		GetNode<CatchBarrel>("Rig_Medium/Skeleton3D/Dummy_ArmRight/CatchBarrel").CollectableCaptured += CollectableCapturedHandler;
		apWalking = GetNode<AnimationPlayer>("AnimationPlayer");
		caughtSoundEffect = GetNode<AudioStreamPlayer3D>("Pop");
    }
	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector3 direction = (new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			// Calculate the target direction based on input
			Vector3 targetDirection = direction;// new Vector3(inputDir.X, 0, inputDir.Y).Normalized();

            //Get character to turn over time towards the direction of movement.
            // Calculate the target rotation in radians
            float targetRotation = Mathf.Atan2(targetDirection.X, targetDirection.Z);

            // Interpolate the current rotation towards the target rotation for smooth turning
            float newRotation = Mathf.LerpAngle(Rotation.Y, targetRotation, (float)delta * RotationSpeed);
            Rotation = new Vector3(0, newRotation, 0);

			if (Input.IsActionPressed("Speed_Boost"))
			{
				// Apply movement
				velocity.X = (targetDirection.X * Speed) * speedBoost;
				velocity.Z = (targetDirection.Z * Speed) * speedBoost;
				//If we have time we will implement the running animation.
	

			}
			else
			{
                // Apply movement
                velocity.X = targetDirection.X * Speed;
                velocity.Z = targetDirection.Z * Speed;

            }

			apWalking.Play("WalkingArmOpenInFront");

		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
			apWalking.Stop();
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private void CollectableCapturedHandler() {

		caughtSoundEffect.Play();
		EmitSignal(SignalName.CollectableCaptured);
	}
}
