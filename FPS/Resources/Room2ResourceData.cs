using Godot;


[GlobalClass]
public partial class Room2ResourceData : Resource
{
    [Export] PackedScene[] AvilableTargetsToSpawn;

    [Export] float tier1MaxMoveSpeed;
    [Export] float tier2MaxMoveSpeed;
    [Export] float tier3MaxMoveSpeed;

    [Export] int RoomLeftWallBounds;
    [Export] int RoomRightWallBounds;

    [Export] float targetLifeTime;

    
    public PackedScene GetTargetToSpawn() => AvilableTargetsToSpawn[GameConfig.Instance.GetRng().RandiRange(0, AvilableTargetsToSpawn.Length -1)];
    public int GetSpawnPOSX() => GameConfig.Instance.GetRng().RandiRange(RoomLeftWallBounds, RoomRightWallBounds);

    public float Tier1MaxMoveSpeed => tier1MaxMoveSpeed;
    public float Tier2MaxMoveSpeed => tier2MaxMoveSpeed;
    public float Tier3MaxMoveSpeed => tier3MaxMoveSpeed;
    public float TargetLifeTime => targetLifeTime;
    public int LeftWallBounds => RoomLeftWallBounds;
    public int RightWallBounds => RoomRightWallBounds; 


}
