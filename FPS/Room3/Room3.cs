using Godot;

public partial class Room3 : Node3D
{
	[Export] PackedScene target;
	[Export] TierSpawnRange SpawnRange;


	//Packed scene for the spinning target
	[Export] PackedScene spinningTarget;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//Target to spawn that isn't a Godot object, pass in delta and have them control their own movement.
		//I am done for the day.
	}
}
