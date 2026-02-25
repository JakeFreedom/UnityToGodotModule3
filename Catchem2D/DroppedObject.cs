using Godot;
using System;

public partial class DroppedObject : Node2D
{


	[Export]
	public float fallingSpeed = 100;

	private RandomNumberGenerator rng = new RandomNumberGenerator();
	public int ballHealth;
	Label ballHealthLabel;
	Area2D hitBox;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		rng.Randomize();
		fallingSpeed = rng.RandfRange(2, 10);
		this.Name = $"Speed{fallingSpeed}";
		hitBox = GetNode<Area2D>("RigidBody2D/Area2D");
		hitBox.AreaEntered += AreaEnteredHandler;
		//GetNode<Area2D>("RigidBody2D/Area2D").AreaEntered += AreaEnteredHandler;
		ballHealth = rng.RandiRange(5, 15);
		ballHealthLabel = GetNode<Label>("RigidBody2D/Label");
		ballHealthLabel.Text = ballHealth.ToString();
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		//Position += new Vector2(0, fallingSpeed);
		GetNode<RigidBody2D>("RigidBody2D").ConstantForce = new Vector2(0, 250);

		
	}

	public void SetSprite(Sprite2D sprite)
	{
		GetNode<Sprite2D>("RigidBody2D/Sprite2D").Texture = sprite.Texture;
		GetNode<Sprite2D>("RigidBody2D/Sprite2D").Scale = new Vector2(.75f, .75f);

    }

	private void AreaEnteredHandler(Area2D otherArea) {
		ballHealth -= 1;
		if (ballHealth <= 0)
			QueueFree();
		ballHealthLabel.Text = ballHealth.ToString();
	}
}
