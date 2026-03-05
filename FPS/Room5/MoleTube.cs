using Godot;
using System;

public partial class MoleTube : Node3D
{
	[Export] public int MaxHeight = 6;

	private PackedScene moleTargetScene;
	private Boolean isActive = false;
	RandomNumberGenerator rng;
	public override void _Ready()
	{
		rng = new RandomNumberGenerator();
		GetNode<Area3D>("Area3D").AreaEntered += AreaEnteredHandler;
	}

	public void SetActive() =>	isActive = true;
	public void Deactivate() => isActive = false;
	public void SetMoleTargetScene(PackedScene scene) =>  moleTargetScene = scene; 
	public Boolean IsActive { get=>isActive; }
	private void AreaEnteredHandler(Area3D otherArea) => isActive = false;
}
