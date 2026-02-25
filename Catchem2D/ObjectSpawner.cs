using Godot;
using System;

public partial class ObjectSpawner : Node2D
{

	[Export]
	public PackedScene spawneObject;
	[Export(PropertyHint.Range, "1,5,1")]
	public int timeBetweenDrops;
	[Export(PropertyHint.Range, "1, 15, 1")]
	public int spawnLocations;
	[Export]
	public Sprite2D[] sprites;
	[Export] int spawnedObjectThresholdForBurst;
	[Export] int objectsToBurstSpawn=  0;

	Timer spawnTimer;
	RandomNumberGenerator rng;
	int spawnedObjects = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		spawnTimer = new Timer();
		spawnTimer.WaitTime = .5;//timeBetweenDrops;
		spawnTimer.Autostart = true;
		spawnTimer.Timeout += SpawnObject;
		AddChild(spawnTimer);
		spawnTimer.Start();
		rng = new RandomNumberGenerator();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SpawnObject() {
		spawnedObjects += 1;

		if (spawnedObjects >= spawnedObjectThresholdForBurst)
		{
			for (int x = 0; x <= objectsToBurstSpawn; x++)
				CreateSpawnedObject();

			spawnedObjects = 0;
		}

		CreateSpawnedObject();

	
		spawnTimer.Start();//This needs to be a random range
	}

	private void CreateSpawnedObject()
	{
        //Once x objects have spawned we will spawn in a burst x object config in the inspector.
        //Spawn Range is 1920/4 = 0 -- 480 -- 960 -- 1440 -- 1920
        DroppedObject droppedObject = spawneObject.Instantiate<DroppedObject>();
        droppedObject.SetSprite((Sprite2D)sprites.GetValue(rng.RandiRange(0, sprites.Length - 1)));


        //Get a random number in the range player has set
        int spawnPosition = new Random().Next(spawnLocations - 1);
        //Convert to pixels
        int spawnInPixels = (spawnPosition - 1) * ((int)(GetViewport().GetVisibleRect().Size.X - 40) / spawnLocations);
        if (spawnInPixels == 0)
            spawnInPixels += 40;
        droppedObject.Position = new Vector2(spawnInPixels, 0);
        AddChild(droppedObject);
    }
}
