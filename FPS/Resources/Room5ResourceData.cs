using Godot;
namespace FPS.Room5
{
    [GlobalClass]
    public partial class Room5ResourceData : Resource
    {
        [Export] PackedScene[] TargetScenes;
        [Export] int MaxPopupDelay;
        [Export] int MaxVerticleSpeed;
        [Export] int MaxTargetLifeTime;
        [Export] PackedScene shotParticle;

        public PackedScene[] TargetToSpawn { get => TargetScenes;}
        public int PopupDelay {  get => MaxPopupDelay;}
        public int VerticleSpeed { get => MaxVerticleSpeed;}
        public int TargetLifeTime { get => MaxTargetLifeTime;}
        public PackedScene ShotParticle { get => shotParticle;}

    }
}
