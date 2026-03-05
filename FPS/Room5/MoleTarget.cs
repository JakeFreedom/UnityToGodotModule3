using Godot;

public partial class MoleTarget : Node3D
{

	[Signal]
	public delegate void MoleTargerQueFreeEventHandler(MoleTarget mt);

	public float traveled = 0f;
	public int direction = 1;
	public int MaxHeight { get; set; }

	private MeshInstance3D normalMesh;
	private Node3D explodingTarget;

	public MoleTube myTube;

    public override void _Ready()
    {
		GetNode<Area3D>("Area3D").AreaEntered += AreaEnteredHandler;
		normalMesh = GetNode<MeshInstance3D>("MeshInstance3D");
		explodingTarget = GetNode<Node3D>("ExplodingTarget");
    }
	private void AreaEnteredHandler(Area3D otherArea)
	{
		GD.Print("Bullet");
		AnimationPlayer ap = GetNode<AnimationPlayer>("ExplodingTarget/AnimationPlayer");
		if (ap != null) {
			normalMesh.Visible = false;
			explodingTarget.Visible = true;
			ap.Play("Explode");
			
		}

		
		EmitSignal(SignalName.MoleTargerQueFree, this);


	}
	
	public void StartDeathTimer()
	{
		GetTree().CreateTimer(1,true,true).Timeout += TimerTimeout;
	}
	private void TimerTimeout() => QueueFree();
}
