using Godot;
using System;
using System.Collections.Generic;

public partial class SpawnerRing : Node3D
{
	[Export] SpawnerConfig config;

	//Private Globals
	RandomNumberGenerator rng;
	Timer t;
	List<Node3D> spawnedDropList;
	Vector3 spawnPosition = Vector3.Zero;
	int spawnedDrops;
	
	public override void _Ready()
	{
		spawnedDropList = new List<Node3D>();
		rng = new RandomNumberGenerator();
		t = new Timer();
		t.WaitTime = 1;//First drop we want someone predictable 
		t.Autostart = true;
		t.Timeout += CreateDrop;
		AddChild(t);
		t.Start();
	}


    public override void _Process(double delta)
    {
		//Iterate the list and give positional data and rotational data to each generic in the list
		foreach (GenericDrop node in spawnedDropList) {
			node.Position += new Vector3(0, -node.FallingSpeed * (float)delta, 0); 
			node.Rotation += new Vector3(0, (float)delta * node.RotationalSpeed, 0);
        }
    }

	private void CreateDrop()
	{
		if (spawnedDrops >= config.BurstSpawnThreshold)
			for(int i = 0; i < config.BurstSpawnAmount; i++)
			{
                spawnPosition = config.GetPosition();
                SpawnDrop(spawnPosition);
				spawnedDrops = 0;
            }

		spawnPosition = config.GetPosition();
		SpawnDrop(spawnPosition);
		
		//We set the random time here.
		t.Start(rng.RandiRange(0, config.MaxSpawnDelay));
	}

	private void SpawnDrop(Vector3 spawnPosition)
	{
		GenericDrop spawnedObject = config.GenericDropScene.Instantiate() as GenericDrop;
		spawnedObject.Position = spawnPosition;
		spawnedObject.GenericMesh = config.GenericDropMeshes[rng.RandiRange(0, config.GenericDropMeshes.Length -1)];

		//here is where we need to give its falling speed and rotational speed
		spawnedObject.FallingSpeed = rng.RandiRange(1, config.MaxFallingSpeed);
		spawnedObject.RotationalSpeed = rng.RandiRange(1, config.MaxRotationalSpeed);
		spawnedObject.Scale = new Vector3(.5f, .5f, .5f);
		spawnedObject.IHaveBeenCaught += RemoveFromList;
        AddChild(spawnedObject);
		spawnedDrops += 1;
		spawnedDropList.Add(spawnedObject);
    }

	private void RemoveFromList(GenericDrop gd)  => spawnedDropList.Remove(gd);
}