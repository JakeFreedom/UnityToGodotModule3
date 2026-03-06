using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class Room3 : Node3D
{

	//Room Data
	[Export] Room3ResourceData Room3Data;


	//Spawn Timer
	Timer spawnTimer;

	List<SpinningTarget> targets;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		spawnTimer = new Timer();
		spawnTimer.WaitTime = Room3Data.SpawnTimerDelay;
		spawnTimer.Timeout += SpawnTimerTimeOutHandler;
		spawnTimer.Autostart = true;
		AddChild(spawnTimer);
		spawnTimer.Start();

		targets = new List<SpinningTarget>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//Target to spawn that isn't a Godot object, pass in delta and have them control their own movement.
		//I am done for the day.
	}




	private void SpawnTimerTimeOutHandler()
	{

		//Get the random tier, if we can spawn great,others skip that spawn tick and move on with our lives.

		//I don't even want to instantiate the target if we can't add it.
		if(GameConfig.Instance.GetRng().RandfRange(0, 1) <= .57)
			AddToTier1();
		else
			AddToTier2();
	}

	private void AddToTier1()
	{
		int count = 0;
		//Check Tier Count
		foreach (SpinningTarget t in targets)
		{
            if (t.Tier == 1)
			{
				count += 1;
			}
			else
				continue;
		}
		if (count < 2)
			AddToScene(-5f, 1);
	}

	private void  AddToTier2()
	{
        int count = 0;
		foreach (SpinningTarget t in targets)
		{
            if (t.Tier == 2)
			{
				count += 1;
			}
			else
				continue;
		}
		if (count < 2)
			AddToScene(-12, 2);
	}

	private void AddToScene(float tierPosition, int tier)
	{
        SpinningTarget target = Room3Data.TargetScene.Instantiate<SpinningTarget>();
        target.Position = new Vector3(GameConfig.Instance.GetRng().RandfRange(Room3Data.MinLateralPOS, Room3Data.MaxLateralPOS), 4.2f, tierPosition);
		target.RadialTargets = Room3Data.RadialTargetsToSpawn;
		target.Tier = tier;
		target.TargetRPM = Room3Data.RadialRPMs;
		targets.Add(target);
        GetNode<Node3D>("Targets").AddChild(target);

    }
}
