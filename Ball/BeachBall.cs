using Godot;
using System;

public partial class BeachBall : Node3D
{
    public float moveSpeed;
    public float startingXPos;
    public float maxHeight;
    public bool moveUp;
    public int rotationalSpeed;
    public int rotationDirection; //1 counter clockwise -1 clockwise
    public float maxScale;
    public bool scaleUp;
    public int scaleSpeed;
}
