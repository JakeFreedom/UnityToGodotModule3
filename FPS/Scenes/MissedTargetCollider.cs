using FPS.Room5.Events;
using Godot;

public partial class MissedTargetCollider : Area3D
{
    public override void _Ready()
    {
        this.AreaEntered += AreaEntered_Handler;
    }

    private void AreaEntered_Handler(Area3D otherArea)
    {
        if (otherArea.IsInGroup("MoleTarget"))
            GameConfig.Instance.GetBus().Publish<MissedTargetEvent>(new MissedTargetEvent());
    }

}
