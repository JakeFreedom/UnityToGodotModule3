using Godot;

[GlobalClass]
public partial class ObjectSpawnerConfig : Resource
{
    [Export] public PackedScene spawnObject;
    [Export(PropertyHint.Range, "1, 15, 1")] public int spawnLocations;
    [Export] public int spawnedObjectThresholdForBurst;
    [Export] public int objectsToBurstSpawn = 0;
    [Export] public int DelayBetweenSpawns = 1;

}
