using Godot;

public partial class WheelBarrowArea : Area3D
{

	[Signal]
	public delegate void CollectableCapturedEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Name = "WheelBarrowArea";
		this.AreaEntered += AreaEnteredHandler;
	}

	private void AreaEnteredHandler(Area3D otherArea)
	{		
		if (otherArea.Owner.IsInGroup("Collectable"))
		{
			EmitSignal(SignalName.CollectableCaptured);
			otherArea.Owner.CallDeferred("QueueFree");
		}
	}
}
