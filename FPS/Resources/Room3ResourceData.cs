using Godot;
using System;

[GlobalClass]
public partial class Room3ResourceData : Resource
{
    private const int tier1 = -5;
    private const int tier2 = -12;


    //Target Prefabs/Scenes
    [Export] PackedScene SpinningTargetScene;
    [Export] int spawnTimerDelay;


    [Export] int targetLifeTimer; //<-- This will come later
    [Export] int radialTargetsToSpawn;
    [Export] float radialRPMs = 1;

    int spawnLateralPOSMin = -13;
    int spawnLateralPOSMax = 0;

    public Room3ResourceData()
    {
       
    }

    //Spawning Data
    //What Tier, How many targets around the hub to spawn

    //RPM Data

    //Movement Speed Data



    public int SpawnTimerDelay => spawnTimerDelay;

    public PackedScene TargetScene => SpinningTargetScene;

    public int MaxLateralPOS => spawnLateralPOSMax;
    public int MinLateralPOS => spawnLateralPOSMin;
    public int RadialTargetsToSpawn => radialTargetsToSpawn;
    public float RadialRPMs => radialRPMs;
}
