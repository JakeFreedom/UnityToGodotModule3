using Godot;
using System;

public partial class CoinDrop2 : Node3D
{
    [Export]
    public float rotationSpeed = 10;
    [Export(PropertyHint.Range, ".5, 3,.1")]
    public float fallingSpeed = 3;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		this.Name = "Drop 2";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        //this.Position += new Vector3(0, -fallingSpeed * (float)delta, 0);
        //this.Rotation += new Vector3(0, (float)delta * rotationSpeed, 0);
    }
}
