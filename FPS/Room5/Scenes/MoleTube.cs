using FPS.Room5;
using FPS.Room5.Events;
using Godot;
using System;

public partial class MoleTube : Node3D
{
    private IEventBus<GameEvent> bus;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        bus = GameConfig.Instance.GetBus();
        GetNode<Area3D>("Area3D2").AreaEntered += MoleTube_AreaEntered;
	}

    private void MoleTube_AreaEntered(Area3D area)
    {
        GD.Print("Mole Tube Launch");
        bus.Publish<TubeLaunchEvent>(new TubeLaunchEvent { TubeName = this.Name });
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
