using Godot;
using System;

public partial class objDespawner : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//this.BodyEntered += ObjectEntered;
		this.AreaEntered += ObjectEntered;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	private void ObjectEntered(Node2D otherBody) {
		otherBody.Owner.QueueFree();
	}
}
