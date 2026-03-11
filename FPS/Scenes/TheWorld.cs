using FPS.Room5;
using FPS.Room5.Events;
using Godot;
using System;
using System.Reflection;

public partial class TheWorld : Node3D
{
	Label totalTargetSpawnCountLabel;
	Label bulletsFiredCountLabel;
	Label missedTargetCountLabel;

	int bulletsFired;
	int spawnCount;
	int missedTargets;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		totalTargetSpawnCountLabel = GetNode<Label>("EnemiesSpawnedLabel/Count");
		bulletsFiredCountLabel = GetNode<Label>("BulletsFiredLabel/Count");
		missedTargetCountLabel = GetNode<Label>("MissedTargets/Count");
		GameConfig.Instance.GetBus().Subscribe<MoleKilledEvent>(OnTargetKilled);
		GameConfig.Instance.GetBus().Subscribe<MoleSpawnedEvent>(OnTargetSpawn);
		GameConfig.Instance.GetBus().Subscribe<BulletFiredEvent>(OnBulletFired);
        GameConfig.Instance.GetBus().Subscribe<MissedTargetEvent>(OnMissedTarget);
    }


	private void OnMissedTarget(MissedTargetEvent e)
	{
		missedTargets++;
		missedTargetCountLabel.Text = missedTargets.ToString();
	}
	private void OnBulletFired(BulletFiredEvent e)
	{
		//GD.Print("A Bullet was fired");
		bulletsFired++;
		bulletsFiredCountLabel.Text = bulletsFired.ToString();
	}
	private void OnTargetKilled(MoleKilledEvent e)
	{
	
	}

	private void OnTargetSpawn(MoleSpawnedEvent e)
	{
		spawnCount++;
		totalTargetSpawnCountLabel.Text = spawnCount.ToString();
	}
}
