using Godot;
using System;

public partial class ObjectSpawner : Node2D
{
	[Export] Sprite2D[] sprites;
	[Export] ObjectSpawnerConfig config;

	Timer spawnTimer;
	int spawnedObjects = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		spawnTimer = new Timer();
		spawnTimer.WaitTime = config.DelayBetweenSpawns;//Should we just make a timer config??
		spawnTimer.Autostart = true;
		spawnTimer.Timeout += TimeOutHandler;
		AddChild(spawnTimer);
		spawnTimer.Start();
	}

	private void TimeOutHandler() {
		spawnedObjects += 1;

		if (spawnedObjects >= config.spawnedObjectThresholdForBurst)
			for (int x = 0; x <= config.objectsToBurstSpawn; x++)
				CreateSpawnedObject();

			spawnedObjects = 0;

		CreateSpawnedObject();
		spawnTimer.Start();//This needs to be a random range
	}

	private void CreateSpawnedObject()
	{
        //Spawn Range is 1920/4 = 0 -- 480 -- 960 -- 1440 -- 1920
        DroppedObject droppedObject = config.spawnObject.Instantiate<DroppedObject>();
        droppedObject.SetSprite((Sprite2D)sprites.GetValue(GameManager.GetRandI(0, sprites.Length - 1)));
        //Get a random number in the range player has set
        int spawnPosition = new Random().Next(config.spawnLocations+3);
		//Convert to pixels
        int spawnInPixels = (spawnPosition - 1) * ((int)(GetViewport().GetVisibleRect().Size.X -40) / config.spawnLocations);
        if (spawnInPixels == 0)
            spawnInPixels += 40;
        droppedObject.Position = new Vector2(spawnInPixels, 0);
        AddChild(droppedObject);
    }
}
