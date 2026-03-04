using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class BeenySpawnPoints : Node3D
{
	List<Node3D> spawnPoints;
	Timer bennySpawnTimer;
	[Export]PackedScene bennyScene;


	RandomNumberGenerator rng;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		spawnPoints = new List<Node3D>();
		var children = GetChildren();
		foreach(Node3D n in children)
			spawnPoints.Add(n);

		bennySpawnTimer = new Timer();
		bennySpawnTimer.WaitTime = 3;
		bennySpawnTimer.Autostart = true;
		bennySpawnTimer.Timeout += SpawnBenny;
		bennySpawnTimer.Start();
		//AddChild(bennySpawnTimer);

		rng = new RandomNumberGenerator();


	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void SpawnBenny() { 
		Node3D rngSpawnPoint = spawnPoints[rng.RandiRange(0, spawnPoints.Count-1)];
		Benny benny = bennyScene.Instantiate() as Benny;
		rngSpawnPoint.AddChild(benny);
		bennySpawnTimer.Start(rng.RandiRange(3, 10)) ;
	}
}
