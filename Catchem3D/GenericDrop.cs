using Godot;

public partial class GenericDrop : Node3D
{
	public Mesh genericMesh;
	public float fallingSpeed = 10;
	public float rotationalSpeed = 5;

	[Signal]
	public delegate void IHaveBeenCaughtEventHandler(GenericDrop me);
	
	public override void _Ready() =>	
		GetNode<MeshInstance3D>("Texture").Mesh = genericMesh;

	public void YouHaveBeenCaptures() =>	
		EmitSignal(SignalName.IHaveBeenCaught,this);

}
