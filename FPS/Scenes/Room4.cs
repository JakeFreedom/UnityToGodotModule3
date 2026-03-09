using Godot;
using System;
using System.Collections.Generic;

public partial class Room4 : Node3D
{
	[Export] Room4ResourceData Room4Data;

	Timer spawnTimer;
	List<iTarget> targets = new List<iTarget>();
	bool isRoomActive = false;
	int spawnCount;
	public override void _Ready()
	{
		spawnTimer = new Timer();
		AddChild(spawnTimer);
		spawnTimer.WaitTime = GenerateSpawnDelay();
		spawnTimer.Autostart = true;
		spawnTimer.Timeout += SpawnTimerTimeOut;
		spawnTimer.Start();
		spawnTimer.Paused = !isRoomActive;
		ConfigureLightSwitch();
		
	}

	public override void _Process(double delta)
	{
		if (targets == null || targets.Count <=0)
			return;
			
		foreach (iTarget target in targets) {
			if(target != null)
				target.Update(delta);
		}
	}
	private void SpawnTimerTimeOut() 
	{
		SpawnTarget(true);
		spawnTimer.Start(GenerateSpawnDelay());
	}

	private void SpawnTarget(bool DoSpawnCountUpdate)
	{
		iTarget node = new Target(Room4Data.TargetScene,
														GenerateFallingSpeed(),
														GenerateTargetValue(),
														GenerateTargetLifeTime(), 
														GeneratePositionalData());
		node.Instantiate(this);
		node.LifeTimeTimerElapsed += TargetLifeTimeElapsed;
		targets.Add(node);
		if(DoSpawnCountUpdate)
			UpdateSpawnCount();
		
	}

	private void TargetLifeTimeElapsed(object sender, EventArgs e)
	{
		targets.Remove((iTarget)sender);
		((iTarget)sender).TargetNode.QueueFree();
	}
	private int GenerateSpawnDelay() =>  GameConfig.Instance.GetRng().RandiRange(1, Room4Data.MaxSpawnDelay);
	private int GenerateTargetValue() =>  GameConfig.Instance.GetRng().RandiRange(1, 10);
	private int GenerateTargetLifeTime() =>  Room4Data.TargetLifeTime;
	private int GenerateFallingSpeed() =>  GameConfig.Instance.GetRng().RandiRange(5, Room4Data.MaxTargetFallSpeed);
	private Vector3 GeneratePositionalData() =>  new Vector3(GameConfig.Instance.GetRng().RandfRange(Room4Data.SpawnMinX, Room4Data.SpawnMaxX), 7, GetSpawnTier());
    private void ConfigureLightSwitch() => GetNode<SwitchLever>("LightSwitch").LightSwitchLever += LightSwitchHandler;
	private float GetSpawnTier() => Room4Data.SpawnTier[GameConfig.Instance.GetRng().RandiRange(0, 1)];
	private void LightSwitchHandler(bool isOn)
	{
		isRoomActive = isOn;
		spawnTimer.Paused = !isOn;
	}
	private void UpdateSpawnCount()
	{
		spawnCount++;
		if (spawnCount >= Room4Data.BurstThreshold)
		{
			for (int x = 0; x <= Room4Data.BurstAmount; x++)
			{
				SpawnTarget(false);
			}
				spawnCount = 0;
		}
	}
}
