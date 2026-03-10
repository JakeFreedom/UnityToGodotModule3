using Godot;

namespace FPS.Room5
{
    [GlobalClass]
    public partial class Room5ResourceData : Resource
    {
        [Export] PackedScene MoleTargetScene;



        public PackedScene MoleTarget { get; protected set; }
    }
}
