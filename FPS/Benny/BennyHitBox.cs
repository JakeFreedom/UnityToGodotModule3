using Godot;
using System;

public partial class BennyHitBox : Area3D
{
	[Signal]
	public delegate void RegisterHitEventHandler();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AreaEntered += AreaEnteredHandler;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	private void AreaEnteredHandler(Area3D otherArea)
	{

		if (otherArea.IsInGroup("Bullet")) {
			EmitSignal(SignalName.RegisterHit);
		}
	}
}
