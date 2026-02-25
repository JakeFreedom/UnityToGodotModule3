using Godot;
using System;

public partial class CatchBarrel : Node3D
{

    [Signal]
    public delegate void CollectableCapturedEventHandler();
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		GetNode<Area3D>("Area3D").AreaEntered += AreaEnteredHandler;
	}

	private void AreaEnteredHandler(Area3D otherArea) {

		//GD.Print("Something entered the barrel");
        if (otherArea.Owner.IsInGroup("Collectable"))
        {
            EmitSignal(SignalName.CollectableCaptured);
            ((GenericDrop)otherArea.Owner).YouHaveBeenCaptures();
            otherArea.Owner.QueueFree();
        }
    }
}
