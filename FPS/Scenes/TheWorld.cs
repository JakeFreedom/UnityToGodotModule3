using Godot;
using System;

public partial class TheWorld : Node3D
{

	Label l;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		l = GetNode<Label>("Label");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		l.Text = Engine.GetFramesPerSecond().ToString();
	}
}
