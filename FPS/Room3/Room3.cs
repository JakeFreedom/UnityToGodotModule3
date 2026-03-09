using FPS.Room3;
using Godot;
using System;
using System.Collections.Generic;

public partial class Room3 : Node3D
{
	//Room Data
	[Export] Room3ResourceData Room3Data;

	//Spawn Timer
	Timer spawnTimer;
	bool isRoomActive = false;

	List<RadialTargetHub> radialTargets;
	
	public override void _Ready()
	{
		spawnTimer = new Timer();
		spawnTimer.WaitTime = Room3Data.SpawnTimerDelay;
		spawnTimer.Timeout += SpawnTimerTimeOutHandler;
		spawnTimer.Autostart = true;
		AddChild(spawnTimer);
		spawnTimer.Start();
		spawnTimer.Paused = !isRoomActive;

		SwitchLever lightSwitch = GetNode<SwitchLever>("LightSwitch");
        lightSwitch.LightSwitchLever += LightSwitch_LightSwitchLever;
		radialTargets = new List<RadialTargetHub>();
	}


    public override void _Process(double delta)
	{
		foreach(RadialTargetHub target in radialTargets)
			target.Update(delta);
	}

    public override void _PhysicsProcess(double delta)
    {
        foreach (RadialTargetHub target in radialTargets)
            target.PhysicsUpdate(delta);
    }

    private void LightSwitch_LightSwitchLever(bool isOn)
    {
		isRoomActive = isOn;
		spawnTimer.Paused = !isOn;
    }

	private void SpawnTimerTimeOutHandler() =>
		CanWeSpawnOnTier(GameConfig.Instance.GetRng().RandiRange(Room3Data.TierPositions.Length-Room3Data.TierPositions.Length+1, Room3Data.TierPositions.Length));

	private void CanWeSpawnOnTier(int tier)
	{
		int count = 0;
		foreach(RadialTargetHub target in radialTargets)
			if (target.Tier == tier)
				count++;
		if (count < 2)
			AddToScene(Room3Data.TierPositions[tier - 1], tier);
	}

	private void AddToScene(float tierPosition, int tierToSpawnOn)
	{
		//This will spawn the Spinning Target Scene
		RadialTargetHub node = new (
															Room3Data.TargetHubScene,
															Room3Data.RadialScene[GameConfig.Instance.GetRng().RandiRange(0,1)], 
															tierToSpawnOn, 
															GameConfig.Instance.GetRng().RandiRange(10,Room3Data.RadialRPMs), 
															Room3Data.RadialTargetsToSpawn, 
															Room3Data.TargetLifeTimer, 
															GameConfig.Instance.GetRng().RandfRange(1,Room3Data.TargetMovementSpeed));

		Node3D target = node.Instantiate();
        //Get the signal for Life Time so we can remove the target from the radialTargets List
        node.LifeTimeTimerExpired += TargetLifeTimerExpired;
		target.Position = new Vector3(GameConfig.Instance.GetRng().RandfRange(Room3Data.MinLateralPOS, Room3Data.MaxLateralPOS), 4.2f, tierPosition);
		GetNode<Node3D>("Targets").AddChild(target);
		radialTargets.Add(node);
    }

    private void TargetLifeTimerExpired(object sender, EventArgs e) => radialTargets.Remove((RadialTargetHub)sender);
}
