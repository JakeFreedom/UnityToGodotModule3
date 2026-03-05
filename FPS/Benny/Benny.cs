using Godot;
using System;
using System.Reflection;

public partial class Benny : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	AnimationPlayer ap;
	Palyer player;
	BennyHitBox hitBox;
	Boolean isDead = false;
	public override void _Ready()
    {
		ap = GetNode<AnimationPlayer>("AnimationPlayer");
		ap.Play("Walk");

		//See if we can find the player node

		player = GetTree().Root.GetNode<Node3D>("Root").GetNode<Palyer>("Player");
		
		//Get hit box
		hitBox = GetNode<BennyHitBox>("HitBox");
		hitBox.RegisterHit += RegisterHitEventHandler;
    }


	double t;
    public override void _Process(double delta)
    {
		if(! isDead) 
		{ 
			t += delta;
			LookAt(player.Position, Vector3.Up, true);
			this.GlobalPosition = this.GlobalPosition.MoveToward(player.Position, (float)delta);
		}

		
    }

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		//// Add the gravity.
		//if (!IsOnFloor())
		//{
		//	velocity += GetGravity() * (float)delta;
		//}

		//// Handle Jump.
		//if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		//{
		//	velocity.Y = JumpVelocity;
		//}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		//Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		//Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		//if (direction != Vector3.Zero)
		//{
		//	velocity.X = direction.X * Speed;
		//	velocity.Z = direction.Z * Speed;
		//}
		//else
		//{
		//	velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		//	velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		//}

		//Velocity = velocity;
		//MoveAndSlide();
	}

	private void RegisterHitEventHandler()
	{
		isDead = true;
		ap.Stop();
		ap.Play("DeathAnimation");
		GetTree().CreateTimer(4, true, true, false).Timeout += RemoveObject;
		hitBox.RegisterHit -= RegisterHitEventHandler;
        
	
	}

	private void RemoveObject() {
		QueueFree();
	}
}
