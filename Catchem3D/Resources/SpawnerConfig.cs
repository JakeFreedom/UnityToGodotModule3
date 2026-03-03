using Godot;
using System;
[GlobalClass]
public partial class SpawnerConfig : Resource
{

    //What does our spawn have
    /*
     *
     * 1) Spawn Position
     * 2) Spawn Rate
     * 
     * SpawnCenter  uid://b5i0oubdq05uu
     * 
     */

    [Export] PackedScene dropScene;
    [Export] Mesh[] meshes;
    [Export] Vector3 position;
    [Export] int maxFallingSpeed;
    [Export] int maxRotationalSpeed;
    [Export] Vector3 scale;
    [Export] int maxSpawnDelay;
    [Export] int burstSpawnThreshold;
    [Export] int burstSpawnAmount;
    [Export] int spawnRadius = 8;
    [Export] Vector3 spawnCenter;

    Vector3 spawnPosition;

    public int SpawnRadius => spawnRadius;
    public int BurstSpawnThreshold => burstSpawnThreshold;
    public int BurstSpawnAmount => burstSpawnAmount;
    public int MaxSpawnDelay => maxSpawnDelay;
    public int MaxFallingSpeed => maxFallingSpeed;
    public int MaxRotationalSpeed => maxRotationalSpeed;
    public PackedScene GenericDropScene => dropScene;
    public Mesh[] GenericDropMeshes => meshes;

    RandomNumberGenerator rng;
    public SpawnerConfig() { 
        rng = new RandomNumberGenerator();
    }

    public new Vector3 GetPosition()
    {
        Vector2 randDirection = Vector2.Right.Rotated(rng.RandfRange(0, Mathf.Tau));
        return spawnPosition = Vector3.Zero + (new Vector3(randDirection.X, 0, randDirection.Y) * rng.RandfRange(-this.SpawnRadius, this.SpawnRadius));
    }
}
