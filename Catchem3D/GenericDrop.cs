using Godot;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

public partial class GenericDrop : Node3D
{
	Mesh genericMesh;
	float fallingSpeed = 10;
	float rotationalSpeed = 5;

	[Signal] public delegate void IHaveBeenCaughtEventHandler(GenericDrop me);
	
	public override void _Ready() =>	
		GetNode<MeshInstance3D>("Texture").Mesh = genericMesh;

	public void YouHaveBeenCaptures() =>	
		EmitSignal(SignalName.IHaveBeenCaught,this);


	public float FallingSpeed { set => fallingSpeed = value; get => fallingSpeed; }
	public float RotationalSpeed { set => rotationalSpeed = value; get => rotationalSpeed; }
	public Mesh GenericMesh { set => genericMesh = value; get => genericMesh; }

}
