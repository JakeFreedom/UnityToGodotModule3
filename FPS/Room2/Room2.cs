using FPS.Room2;
using Godot;
using System;
using System.Collections.Generic;

public partial class Room2 : Node3D
{
	[Export] Room2ResourceData RoomConfigData;

	[Export] int targetsToSpawnPerTier = 2;
	[Export] float timeBetweenSpanws = 2.0f;

	Timer spawnTimer;
	List<SpawnedTarget> targets;

	Boolean isRoomActive = false;
    public override void _Ready()
	{
		spawnTimer = new Timer();
		spawnTimer.WaitTime = timeBetweenSpanws;
		spawnTimer.Timeout += SpawnTarget;
		spawnTimer.Autostart = true;
		AddChild(spawnTimer);
		spawnTimer.Paused = !isRoomActive;
		targets = new List<SpawnedTarget>();
	
		//Find the light switch
		SwitchLever lightSwitch = GetNode<SwitchLever>("LightSwitch");
		lightSwitch.LightSwitchLever += SwitchLeverHandler;

	}
		
	public override void _Process(double delta)
	{
		if (!isRoomActive)
			return;

		if (targets.Count <= 0)
			return;

		//Move targets back and forth
		foreach (SpawnedTarget node in targets)
			node.Update(delta);
	}

	/// <summary>
	/// Timer TimeOut Handler
	/// </summary>
	private void SpawnTarget()
	{
		int spawnPOSX = RoomConfigData.GetSpawnPOSX();
		Vector3 positionData;
		//Pick a random tier
		switch(GameConfig.Instance.GetRng().RandiRange(1,3))
		{

			case 1:
				positionData = new Vector3(spawnPOSX, GameConfig.Instance.GetRng().RandfRange(1.0f, 2.0f), GameConfig.Instance.GetRng().RandfRange(-5f, -6f));
				InstantiateTarget(positionData, GameConfig.Instance.GetRng().RandfRange(1, RoomConfigData.Tier1MaxMoveSpeed));
				break;
			case 2:
                positionData = new Vector3(spawnPOSX, GameConfig.Instance.GetRng().RandfRange(2.5f, 3.5f), GameConfig.Instance.GetRng().RandfRange(-8.5f, -9.5f));
                InstantiateTarget(positionData, GameConfig.Instance.GetRng().RandfRange(1, RoomConfigData.Tier2MaxMoveSpeed));
                break;
			case 3:
                positionData = new Vector3(spawnPOSX, GameConfig.Instance.GetRng().RandfRange(3.5f, 4.5f), GameConfig.Instance.GetRng().RandfRange(-12.5f, -13.5f));
                InstantiateTarget(positionData, GameConfig.Instance.GetRng().RandfRange(1, RoomConfigData.Tier3MaxMoveSpeed));
                break;

			default:
				break;
			
		}
	}

	private void InstantiateTarget(Vector3 positionalData,float moveSpeed)
	{
		SpawnedTarget node = new SpawnedTarget(RoomConfigData.TargetLifeTime,RoomConfigData.LeftWallBounds, RoomConfigData.RightWallBounds);
		node.Target = RoomConfigData.GetTargetToSpawn().Instantiate<Node3D>(); // Pick one at random from the Resource File
        node.Target.Position = positionalData;
		node.Target.Scale = new Vector3(2, 2, 2); //Why is this here.
		node.LifeTimeTimeOut += DestroyTarget; // Register c# event
        GetNode<Node3D>("Targets/SpawnOrigin").AddChild(node.Target);
        targets.Add(node);
		node.MoveSpeed = moveSpeed;
		
    }

	private void SwitchLeverHandler(bool isOn)
	{
		isRoomActive = isOn;
		spawnTimer.Paused = !isOn;
	}

	private void DestroyTarget(SpawnedTarget n) => targets.Remove(n);
}
