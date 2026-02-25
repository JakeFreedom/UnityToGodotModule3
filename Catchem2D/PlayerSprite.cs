using Godot;

public partial class PlayerSprite : CharacterBody2D
{
	[Export] float BaseSpeed = 900.0f;
	[Export] float BoostSpeed = 4000.0f;

	[Signal] public delegate void ItemCaughtEventHandler(int score);
    public override void _Ready() => GetNode<Area2D>("Area2D").AreaEntered += ObjectEntered;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		float Speed = BaseSpeed;

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			Speed = CheckForSpeedBoost(Speed);
			velocity.X = direction.X * Speed;
		}
		else
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);

		Velocity = velocity;
		MoveAndSlide();
	}

	private void ObjectEntered(Area2D otherObject)
	{
		EmitSignal(SignalName.ItemCaught, ((DroppedObject)otherObject.Owner).CurrentHealth+1);
		otherObject.Owner.QueueFree();
	}

	private float CheckForSpeedBoost(float Speed) {
		if (Input.IsKeyPressed(Key.Shift))
			Speed = BoostSpeed;
		else
			Speed = BaseSpeed;

		return Speed;
    }
}
