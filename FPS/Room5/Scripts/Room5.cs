using FPS.Room5.Events;
using Godot;
using System.Collections.Generic;

namespace FPS.Room5
{
    public partial class Room5 : Node3D
    {
        [Export] Room5ResourceData Room5Data;
        List<Node3D> activeTubes = new List<Node3D>();
        List<ITarget> targets = new List<ITarget>();
        Timer t;
        IEventBus<GameEvent> bus;
        bool isRoomActive = false;
        private readonly object threadlock = new();
        public override void _Ready()
        {
            t = new Timer();
            t.WaitTime = 2;
            t.Timeout += T_Timeout;
            AddChild(t);
            t.Start();
            t.Paused = !isRoomActive;
            bus = GameConfig.Instance.GetBus();
            SubscribeToEvents();
        }

        private void OnLightSwitch(LightSwitchEvent e)
        {
            //Make room active
            isRoomActive = e.isOn;
            t.Paused = !e.isOn;
        }
        private void OnTubeLaunch(TubeLaunchEvent e)
        {
            Node3D nodeToRemove = null;
            foreach(Node3D n in activeTubes)
            {
                if (n.Name == e.TubeName)
                {
                    nodeToRemove = n;
                    break;
                }
            }
            if(nodeToRemove != null) 
                activeTubes.Remove(nodeToRemove);
        }

        private void T_Timeout() => SpawnMoleTarget();
        public override void _Process(double delta)
        {
             foreach(ITarget t in targets)
                t.Update(delta);
        }
        private void SpawnMoleTarget()
        {
            //A mole target is Target.cs that we pass a packed scene into
            ITarget node = new Target(GameConfig.Instance.GetBus(), 
                                                        ConfigureTargetToSpawn(), 
                                                        ConfigurePopupDelay(), 
                                                        ConfigureTargetVerticleSpeed(), 
                                                        Room5Data.ShotParticle,
                                                        ConfigureTargetLifeTime());
            
            Node3D target = node.Instantiate();
            target.Position = new Vector3(target.Position.X, .5f, target.Position.Z);
            //Here is where we will have to pick what tube we want to spawn in. But it has to be a tube that isn't active.
            Node3D moleTubes = GetNode<Node3D>("MoleTubes/NewMoleTubes");
            Godot.Collections.Array<Node> tubes = moleTubes.GetChildren();
            Node3D spawnTube = tubes[GameConfig.Instance.GetRng().RandiRange(0, tubes.Count-1)] as Node3D;
            spawnTube.AddChild(target);
            activeTubes.Add(spawnTube);
            targets.Add(node);
        }

        private int ConfigureTargetLifeTime() => GameConfig.Instance.GetRng().RandiRange(5, Room5Data.TargetLifeTime);
        private int ConfigurePopupDelay() => GameConfig.Instance.GetRng().RandiRange(1, Room5Data.PopupDelay);
        private int ConfigureTargetVerticleSpeed() => GameConfig.Instance.GetRng().RandiRange(1, Room5Data.VerticleSpeed);
        private PackedScene ConfigureTargetToSpawn()  =>  Room5Data.TargetToSpawn[GameConfig.Instance.GetRng().RandiRange(0, Room5Data.TargetToSpawn.Length-1)];
        private void SubscribeToEvents()
        {
            bus.Subscribe<TubeLaunchEvent>(OnTubeLaunch);
            bus.Subscribe<LightSwitchEvent>(OnLightSwitch);
        }
    }
}