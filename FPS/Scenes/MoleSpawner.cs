using Godot;
using System;
using System.Collections.Generic;

public partial class MoleSpawner : Node3D
{

	[Export]
	public int spawnDelay = 5;
	[Export]
	public PackedScene moleTarget;

	Godot.RandomNumberGenerator rng;
	Godot.Collections.Array<Node>  moleTubes;
	Godot.Timer spawnTimer;
	public override void _Ready()
	{
		moleTubes = GetChildren(false);
		rng = new Godot.RandomNumberGenerator();

		

		spawnTimer = new Timer();
		spawnTimer.WaitTime = spawnDelay;
		spawnTimer.Autostart = true;
		AddChild(spawnTimer);
		spawnTimer.Timeout += SpawnMole;
		spawnTimer.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void SpawnMole()
	{
		//Get a random Tube
		Node3D tubeToSpawnIn = moleTubes[rng.RandiRange(0, moleTubes.Count - 1)] as Node3D;
		
		Node3D spawnedTarget = moleTarget.Instantiate() as Node3D;
		//spawnedTarget.Position = tubeToSpawnIn.Position;
		//GD.Print($"Mole Position {spawnedTarget.Position}");
		tubeToSpawnIn.AddChild(spawnedTarget);


	}
}
