using Godot;
using System;

public partial class CoinDrop3 : Node3D
{
    [Export] public float rotationSpeed = 10;
    [Export(PropertyHint.Range, ".5, 3,.1")] public float fallingSpeed = 3;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready() => this.Name = "Drop 2";
}
