using Godot;
using System;

internal class Target : iTarget
{
    public event EventHandler LifeTimeTimerElapsed;
    int targetFallingSpeed;
    Vector3 positionalData;
    public Target(PackedScene target, 
                        int targetFallSpeed, 
                        int targetValue, 
                        int targetLifeTime,
                        Vector3 positionalData) 
    {
        this.positionalData = positionalData;
        this.TargetScene = target;
        targetFallingSpeed = targetFallSpeed;
        Value = targetValue;
        System.Timers.Timer timer = new System.Timers.Timer();
        timer.Interval = targetLifeTime * 1000;
        timer.Elapsed += Timer_Elapsed;
        timer.Start();
    }

    private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) => LifeTimeTimerElapsed?.Invoke(this, e);
    void iTarget.Update(double delta) => this.TargetNode.Position = new Vector3(TargetNode.Position.X, TargetNode.Position.Y - targetFallingSpeed*(float)delta, TargetNode.Position.Z);
    Node3D iTarget.Instantiate(Node3D parentNode)
    {
        this.TargetNode = TargetScene.Instantiate<Node3D>();
        this.TargetNode.Position = positionalData;
        parentNode.GetNode<Node3D>("Targets").AddChild(this.TargetNode);
        return this.TargetNode;
    }



    public int Value { get; set; }
    public Node3D TargetNode { get; set; }
    public PackedScene TargetScene { get;  set; }
}

