using FPS.Room5.Events;
using Godot;
using System;

public partial class MoleTarget : Node3D
{
	private bool isDead = false;
	public event EventHandler Hit;

	public override void _Ready()
	{
		GetNode<Area3D>("Area3D").AreaEntered += AreaEnteredHandler;
		GetNode<Area3D>("Area3D").CollisionLayer = 0;
		GetNode<Area3D>("Area3D").CollisionLayer = 1 << 9;
		GetNode<Area3D>("Area3D").CollisionMask = 0;
		GetNode<Area3D>("Area3D").CollisionMask = 1 << 9; ;
        GameConfig.Instance.GetBus().Subscribe<DoShakeaEndedEvent>(OnDoShakeEnded);
	}

	private void OnDoShakeEnded(DoShakeaEndedEvent e)
	{
		GetNode<Area3D>("Area3D").CollisionLayer = 1 << 0;
        GetNode<Area3D>("Area3D").CollisionMask = 1 << 0; 
    }
	private void AreaEnteredHandler(Area3D otherArea)
	{
		if (!otherArea.IsInGroup("LaunchDetector"))
		{
			Hit?.Invoke(this, new EventArgs());
			isDead = true;
		}
	}

    public override void _Process(double delta)
    {
		if (isDead && this.Scale.X > 0)
			this.Scale = new Vector3(this.Scale.X - (float)delta*5, this.Scale.Y - (float)delta*5, this.Scale.Z - (float)delta*5);
    }
}
