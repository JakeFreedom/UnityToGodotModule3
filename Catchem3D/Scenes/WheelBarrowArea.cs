using Godot;
using System;

public partial class WheelBarrowArea : Area3D
{

	[Signal]
	public delegate void CollectableCapturedEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Name = "WheelBarrowArea";
		this.AreaEntered += AreaEnteredHandler;
		//caughtSoundsEffect = GetNode<AudioStreamPlayer3D>("/CharacterBody3D/AudioStreamPlayer3D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	private void AreaEnteredHandler(Area3D otherArea)
	{		
		if (otherArea.Owner.IsInGroup("Collectable"))
		{
			//GD.Print("This is a collectable");
			//caughtSoundsEffect.Play();
			EmitSignal(SignalName.CollectableCaptured);
			otherArea.Owner.QueueFree();
		}
	}
}
