using Godot;
using System;

public partial class DummyOnHitSmoke : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<GpuParticles3D>("GPUParticles3D").Emitting = true;
		GetNode<GpuParticles3D>("GPUParticles3D").Scale = new Vector3(1.5f, 1.5f, 1.5f);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
