using Godot;

public partial class Main : Node3D
{
	Ux userUX;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<ControlPlayer>("Player").CollectableCaptured += CollectableCapturedHandler;
		userUX = GetNode<Ux>("UX");
	}

	private void CollectableCapturedHandler() => userUX.UpdatePlayerScore(1);
}
