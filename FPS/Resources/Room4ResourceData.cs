using Godot;

[GlobalClass]
public partial class Room4ResourceData : Resource
{
    [Export] PackedScene targetScene;
    [Export] int spawnMin = 0;
    [Export] int spawnMax = -13;
    [Export] int maxSpawDelay = 5;
    [Export] int burstThreshold = 15;
    [Export] int burstAmount = 10;
    [Export] int maxTargetFallSpeed = 5;
    [Export] float[] spawnTier;
    [Export] int targetLifeTime = 5;
    

    public PackedScene TargetScene => targetScene;
    public int MaxSpawnDelay => maxSpawDelay;
    public int SpawnMinX => spawnMin;
    public int SpawnMaxX => spawnMax;
    public int MaxTargetFallSpeed => maxTargetFallSpeed;
    public int TargetLifeTime => targetLifeTime;
    public float[] SpawnTier => spawnTier;
    public int BurstThreshold => burstThreshold;
    public int BurstAmount => burstAmount;
}

