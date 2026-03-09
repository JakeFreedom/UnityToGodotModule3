using Godot;

[GlobalClass]
public partial class Room3ResourceData : Resource
{
    int spawnLateralPOSMin = -13;
    int spawnLateralPOSMax = 0;


    //Target Prefabs/Scenes
    [Export] PackedScene HubTargetScene;
    [Export] PackedScene[] RadialTargetScene;
    [Export] int spawnTimerDelay;
    [Export] int targetLifeTimer; //<-- This will come later
    [Export] int radialTargetsToSpawn;
    [Export] int radialRPMs = 1;
    [Export] float[] tierPositions;
    [Export] float targetMoveSpeed = 5f;



    public int SpawnTimerDelay => spawnTimerDelay;
    public PackedScene TargetHubScene => HubTargetScene;
    public PackedScene[] RadialScene => RadialTargetScene;
    public int MaxLateralPOS => spawnLateralPOSMax;
    public int MinLateralPOS => spawnLateralPOSMin;
    public int RadialTargetsToSpawn => radialTargetsToSpawn;
    public float TargetMovementSpeed => targetMoveSpeed;
    public int RadialRPMs => radialRPMs;
    public float[] TierPositions => tierPositions;
    public int TargetLifeTimer => targetLifeTimer;

}

