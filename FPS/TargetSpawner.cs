using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

public partial class TargetSpawner : Node3D
{
	[Export]
	public PackedScene sceneToSpawn;
	[Export(PropertyHint.Range, "-13, 0 ,1")]
	public int SpawnRangeMin = 0;
    [Export(PropertyHint.Range, "-13, 0 ,1")]
    public int SpawnRangeMax = -13;
	[Export] float spawnDelay = 3;
	[Export] int spawnedObjectTillBurst = 15;
	[Export] int targetsToBurstSpawn = 10;
	// We have to lock the y, since height should not matteer, we will have to lock the z to 2 locations. Front/Back, x can be anything between
	//the edges of the room -- Which seems clunky, but we are not going
	//going to get in the weeds trying to come up with something clever.


	Godot.RandomNumberGenerator rng;
    Godot.Timer spawnTimer;
	int targetsSpawned = 0;
	bool isRoomActive = false;
	List<Node3D> targets;
	public override void _Ready()
	{
		spawnTimer = new Godot.Timer();
		spawnTimer.WaitTime = spawnDelay;
		spawnTimer.Autostart = true;
		spawnTimer.Timeout += SpawnTarget;
		AddChild(spawnTimer);
		spawnTimer.Start();
		spawnTimer.Paused = !isRoomActive;

		rng = new Godot.RandomNumberGenerator();

		SwitchLever lever = GetNode<SwitchLever>("../../LightSwitch");
		lever.LightSwitchLever += LightSwitchLeverHandler;
		targets = new List<Node3D>();
	}

	public override void _Process(double delta)
	{
		if(isRoomActive)
		{
			foreach(Node3D n in targets)
			{
				n.Position = new Vector3(n.Position.X, n.Position.Y - ((float)delta * 5), n.Position.Z);
            }
		}
	}


	private void SpawnTarget() {
		//What can we do here so these don't spawn on top of eachother.
		//Can we keep track of the say past 10 spawn points and if we have that again, we just move it a little bit.
		//We will come back to this at a later date.
		if (targetsSpawned < spawnedObjectTillBurst)
		{
			Node3D spawnedScene = sceneToSpawn.Instantiate() as Node3D;
			spawnedScene.Position = new Vector3(rng.RandiRange(SpawnRangeMin, SpawnRangeMax), 7, rng.RandfRange(-13f, -5.5f));
			AddChild(spawnedScene);
			targets.Add(spawnedScene);
			targetsSpawned++;
		}
		else
		{
			for(int i = 0; i<=targetsToBurstSpawn; i++)
			{
                Node3D spawnedScene = sceneToSpawn.Instantiate() as Node3D;
                spawnedScene.Position = new Vector3(rng.RandiRange(SpawnRangeMin, SpawnRangeMax), rng.RandiRange(7,9), rng.RandfRange(-13f, -5.5f));
                AddChild(spawnedScene);
                targets.Add(spawnedScene);
            }
			targetsSpawned = 0;
		}
	}

	private void LightSwitchLeverHandler(Boolean isOn)
	{
		spawnTimer.Paused = !isOn;
		isRoomActive = true;
	}
}
