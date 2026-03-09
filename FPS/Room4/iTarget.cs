using System;
using Godot;


internal interface iTarget
{
    event EventHandler LifeTimeTimerElapsed;

    void Update(double delta);
    Node3D Instantiate(Node3D parentNode);
        
    int Value { get; protected set; }
    Node3D TargetNode { get; protected set; }
    PackedScene TargetScene { get; protected set; }
}

