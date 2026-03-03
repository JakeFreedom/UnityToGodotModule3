using Godot;
using System;

public partial class CoinDrop2 : Node3D
{
    [Export] float rotationSpeed = 10;
    [Export(PropertyHint.Range, ".5, 3,.1")] float fallingSpeed = 3;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready() => this.Name = "Drop 2";
}
