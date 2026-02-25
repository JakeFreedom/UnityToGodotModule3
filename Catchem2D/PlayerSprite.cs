using Godot;
using System;

public partial class PlayerSprite : CharacterBody2D
{
	[Export]
	public float BaseSpeed = 900.0f;
	[Export]
	public float BoostSpeed = 4000.0f;

	[Signal]
	public delegate void ItemCaughtEventHandler(int score);
    public override void _Ready()
    {
		GetNode<Area2D>("Area2D").AreaEntered += ObjectEntered;
    }

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		float Speed = BaseSpeed;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			if (Input.IsKeyPressed(Key.Shift))
			{
				Speed = BoostSpeed;
			}
			else
				Speed = BaseSpeed;

			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private void ObjectEntered(Area2D otherObject)
	{
		GD.Print(((DroppedObject)otherObject.Owner).ballHealth);
		EmitSignal(SignalName.ItemCaught, ((DroppedObject)otherObject.Owner).ballHealth+1);
		otherObject.Owner.QueueFree();
	}
}
