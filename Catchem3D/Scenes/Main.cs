using Godot;
using System;

public partial class Main : Node3D
{
	Ux userUX;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<ControlPlayer>("Player").CollectableCaptured += CollectableCapturedHandler;
		userUX = GetNode<Ux>("UX");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	private void CollectableCapturedHandler()
	{

		
		userUX.UpdatePlayerScore(1);

	}
}
