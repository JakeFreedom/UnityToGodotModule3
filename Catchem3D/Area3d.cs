using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Area3d : Area3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.AreaEntered += AreaEnteredHandler;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}


	private void AreaEnteredHandler(Area3D otherArea) {

		//GD.Print(otherArea.Name);
	}
}
