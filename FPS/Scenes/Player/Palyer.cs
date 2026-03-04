using Godot;
using System;

public partial class Palyer : CharacterBody3D
{


	public const float Speed = 7.0f;
	public const float JumpVelocity = 4.5f;

    public override void _Ready()
    {
		Input.MouseMode = Input.MouseModeEnum.Captured;
    }
    public override void _Input(InputEvent inputEvent)
    {
        if(inputEvent is InputEventMouseMotion mouseMotion)
		{
            var camera = GetNode<Camera3D>("Camera3D");
            // Calculate new rotation
            float newRotationX = camera.Rotation.X - mouseMotion.Relative.Y * .002f;

            // Clamp BEFORE applying
            newRotationX = Mathf.Clamp(newRotationX, -Mathf.Pi / 2, Mathf.Pi / 2);

            // Apply the clamped rotation
            camera.Rotation = camera.Rotation with { X = newRotationX };

  
            RotateY(-mouseMotion.Relative.X * .002f);
        }
    }

    public override void _UnhandledInput(InputEvent inputEvent)
    {
		if (Input.IsActionJustPressed("ExitMouseCapture"))
			if (Input.MouseMode == Input.MouseModeEnum.Captured)
				Input.MouseMode = Input.MouseModeEnum.Visible;
			else
				Input.MouseMode = Input.MouseModeEnum.Captured;
    }
	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
			
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
