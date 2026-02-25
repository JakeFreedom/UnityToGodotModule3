using Godot;
using System;
using System.Collections.Generic;

public partial class SpawnerRing : Node3D
{
	[Export] private int maxSpawnDelay = 5; //We will pick a random number between 0 and 5 to set the delay for the next drop
	[Export(PropertyHint.Range, "1.0,8.0,.1")] private float spawnRadius = 8;
	[Export] private int BurstSpawnThreshold = 10;
	[Export] private int BurstSpawnAmount = 8;
	[Export(PropertyHint.Range, "1,5,1")] private int minFallingSpeed = 1;
	[Export(PropertyHint.Range, "5, 8, 1")] private int maxFallingSpeed = 5;
	[Export(PropertyHint.Range, "1,5,1")] private int minRotationSpeed = 1;
	[Export(PropertyHint.Range,"5,8,1")] private int maxRotationSpeed = 5;

	[Export] private PackedScene genericDropScene;
	[Export] private Mesh[] meshes;

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
			node.Position += new Vector3(0, -node.fallingSpeed * (float)delta, 0);
			node.Rotation += new Vector3(0, (float)delta * node.rotationalSpeed, 0);
        }
    }

	private void CreateDrop()
	{
		if (spawnedDrops >= BurstSpawnThreshold)
		{
			for(int i = 0; i < BurstSpawnAmount; i++)
			{
                spawnPosition = GetPosition();
                SpawnDrop(spawnPosition);
				spawnedDrops = 0;
            }
		}

		spawnPosition = GetPosition();
		SpawnDrop(spawnPosition);
		
		//We set the random time here.
		t.Start(rng.RandiRange(0, maxSpawnDelay));
	}

	private new Vector3 GetPosition()
	{
        Vector2 randDirection = Vector2.Right.Rotated(rng.RandfRange(0, Mathf.Tau));
        spawnPosition = GetNode<CsgCylinder3D>("SpawnCenter").GlobalPosition + (new Vector3(randDirection.X, 0, randDirection.Y) * rng.RandfRange(-spawnRadius, spawnRadius));
		return spawnPosition;
    }

	private void SpawnDrop(Vector3 spawnPosition)
	{
		GenericDrop spawnedObject = genericDropScene.Instantiate() as GenericDrop;
		spawnedObject.Position = spawnPosition;
		spawnedObject.genericMesh = meshes[rng.RandiRange(0, meshes.Length -1)];

		//here is where we need to give its falling speed and rotational speed
		spawnedObject.fallingSpeed = rng.RandiRange(minFallingSpeed, maxFallingSpeed);
		spawnedObject.rotationalSpeed = rng.RandiRange(minRotationSpeed, maxRotationSpeed);
		spawnedObject.Scale = new Vector3(.5f, .5f, .5f);
		spawnedObject.IHaveBeenCaught += RemoveFromList;
        AddChild(spawnedObject);
		spawnedDrops += 1;
		spawnedDropList.Add(spawnedObject);
    }

	private void RemoveFromList(GenericDrop gd)
	{
		spawnedDropList.Remove(gd);
	}
}
