using Godot;
using System;

public partial class QueFreeer : Area3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        this.AreaEntered += AreaEnteredHandler;
	}

	public void AreaEnteredHandler(Area3D otherArea)
	{
		//GD.Print(otherArea.Name);
		//otherArea.Owner.QueueFree();
	}
}
