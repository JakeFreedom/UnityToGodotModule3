using Godot;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;


public partial class MoleSpawnController : Node3D
{
	[Export] PackedScene moleScene;
	[Export] float timeBetweenSpawns = 2;

	List<MoleTube> moleTubeList;
	List<MoleTarget> moleTargetsToTrack;
	MoleTube activeTube;
	Timer spawnTimer;
	bool isRoomActive = false;
	RandomNumberGenerator rng;
	
	public override void _Ready()
	{
		moleTargetsToTrack = new List<MoleTarget>();
		rng = new RandomNumberGenerator();
		spawnTimer = new Timer();
		spawnTimer.Autostart = true;
		spawnTimer.WaitTime = timeBetweenSpawns;
		spawnTimer.Timeout += SpawnTimer_TimeOut;
		spawnTimer.Paused = !isRoomActive;
		spawnTimer.Start();
		moleTubeList = new List<MoleTube>();
		//Get a list of all the Tubes in the scene
		for(int i = 0; i < GetChildCount(); i++)
			moleTubeList.Add(GetChild<MoleTube>(i));


		SwitchLever t = GetNode<SwitchLever>("../../LightSwitch");
		t.LightSwitchLever += LightSwitchLeverHandler;
		AddChild(spawnTimer);
	}

	public override void _Process(double delta)
	{
		if (isRoomActive)
		{
			foreach (MoleTarget mole in moleTargetsToTrack)
			{
				float moveAmount = rng.RandfRange(.5f, 3f) * (float)delta * mole.direction;
				mole.Position += new Vector3(0, moveAmount, 0);
				mole.traveled += Math.Abs(moveAmount);

				// Reverse direction when limit is reached
				if (mole.traveled >= mole.MaxHeight)
				{
					mole.direction *= -1;
					mole.traveled = -2;//<--This is how far down we will go.

				}
				else if (mole.Position.Y < -1) { moleTargetsToTrack.Remove(mole); }
			}
		}
	}

	private void LightSwitchLeverHandler(bool isRoomActive) { this.isRoomActive = isRoomActive; spawnTimer.Paused = !isRoomActive; }
	private void SpawnTimer_TimeOut() 
	{
		//Get a random tube that isn't in use
		List<MoleTube> availTubes = moleTubeList.Where(tube => tube.IsActive == false).ToList();
		if (availTubes.Count > 0)
		{
			activeTube = availTubes[rng.RandiRange(0, availTubes.Count - 1)];
			MoleTarget moleTarget = moleScene.Instantiate<MoleTarget>();
			activeTube.AddChild(moleTarget);
			activeTube.SetActive();
			moleTarget.Position = new Vector3(0, 0, 0);
			moleTarget.MoleTargerQueFree += MoleTargetQueFreeHandler;
			moleTarget.MaxHeight = activeTube.MaxHeight;
			moleTarget.myTube = activeTube;
			moleTargetsToTrack.Add(moleTarget);
		}
	}

	private void MoleTargetQueFreeHandler(MoleTarget mt) 
	{
		mt.StartDeathTimer();
		mt.myTube.Deactivate();
		moleTargetsToTrack.Remove(mt); 
	
	}
}
