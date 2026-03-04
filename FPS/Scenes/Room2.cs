using Godot;
using System;
using System.Collections.Generic;

public partial class Room2 : Node3D
{
	[Export] PackedScene targetScene;
	//Tier 1 -5 to -6

	//Tier 2 -9 to -10

	//Tier 2 -12 to -13

	[Export(PropertyHint.Range, "-5, -6, .1")] double Tier1SpawnDepth;
	[Export(PropertyHint.Range, "1,2,.1")] double Tier1SpawnHeight;

	[Export] TierSpawnRange SpawnRange;


    [Export(PropertyHint.Range, "-8.5, -9.5, .1")] double Tier2SpawnDepth;
    [Export(PropertyHint.Range, "2.5,3.5,.1")] double Tier2SpawnHeight;


    [Export(PropertyHint.Range, "-12, -13, .1")] double Tier3SpawnDepth;
    [Export(PropertyHint.Range, "3.5,4.5,.1")] double Tier3SpawnHeight;

	[Export] int targetsToSpawnPerTier = 2;
	[Export] float timeBetweenSpanws = 2.0f;

	Timer spawnTimer;
	RandomNumberGenerator rng;
	List<Node3D> targets;

    int leftSide = -14;
    int rightSide = 0;
    int targetPosition;
    Dictionary<Node3D, int> targetNodes = new Dictionary<Node3D, int>();
	Boolean isRoomActive = false;
    public override void _Ready()
	{
		spawnTimer = new Timer();
		spawnTimer.WaitTime = timeBetweenSpanws;
		spawnTimer.Timeout += SpawnTarget;
		spawnTimer.Autostart = true;
		AddChild(spawnTimer);
		spawnTimer.Paused = !isRoomActive;

		rng = new RandomNumberGenerator();
		targets = new List<Node3D>();
		targetPosition = rightSide;


		//Find the light switch
		SwitchLever lightSwitch = GetNode<SwitchLever>("LightSwitch");
		lightSwitch.LightSwitchLever += SwitchLeverHandler;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (isRoomActive)
		{
			if (targets.Count > 0)
			{
				//Move targets back and forth
				foreach (Node3D node in targets)
				{

					targetPosition = targetNodes[node];
					if (node.Position.X <= targetPosition)
					{
						targetNodes[node] = rightSide;
						node.Position = new Vector3(node.Position.X + ((float)delta * 5f), node.Position.Y, node.Position.Z);
					}
					else
					{
						targetNodes[node] = leftSide;
						node.Position = new Vector3(node.Position.X - ((float)delta * 5f), node.Position.Y, node.Position.Z);
					}
				}
			}
		}
	}

	private void SpawnTarget()
	{
		Vector3 positionData;
		//Pick a random tier
		switch(rng.RandiRange(1,3))
		{
			case 1:
				positionData = new Vector3(rng.RandiRange(SpawnRange.MinSpawnRange, SpawnRange.MaxSpawnRange), rng.RandfRange(1.0f, 2.0f), rng.RandfRange(-5f, -6f));
				InstantiateTarget(positionData);
				break;
			case 2:
                positionData = new Vector3(rng.RandiRange(SpawnRange.MinSpawnRange, SpawnRange.MaxSpawnRange), rng.RandfRange(2.5f, 3.5f), rng.RandfRange(-8.5f, -9.5f));
                InstantiateTarget(positionData);
                break;
			case 3:
                positionData = new Vector3(rng.RandiRange(SpawnRange.MinSpawnRange, SpawnRange.MaxSpawnRange), rng.RandfRange(3.5f, 4.5f), rng.RandfRange(-12.5f, -13.5f));
                InstantiateTarget(positionData);
                break;

			default:
				break;
			
		}
	}

	private Node3D InstantiateTarget(Vector3 positionalData)
	{
		Node3D target = targetScene.Instantiate<Node3D>();
		target.Position = positionalData;
		target.Scale = new Vector3(2, 2, 2);
        GetNode<Node3D>("Targets/SpawnOrigin").AddChild(target);
        targets.Add(target);
        targetNodes.Add(target, targetPosition);
        //GD.Print(target.Position);


        return target;
    }

	private void SwitchLeverHandler(bool isOn)
	{
		isRoomActive = isOn;
		spawnTimer.Paused = !isOn;
	}
}
