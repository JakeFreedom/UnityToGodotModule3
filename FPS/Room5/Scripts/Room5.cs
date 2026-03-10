using FPS.Room5.Events;
using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FPS.Room5
{
    public partial class Room5 : Node3D
    {
        [Export] Room5ResourceData Room5Data;
        List<Node3D> activeTubes = new List<Node3D>();
        List<ITarget> targets = new List<ITarget>();
        Timer t;
        IEventBus<GameEvent> bus;
        public override void _Ready()
        {
            t = new Timer();
            t.WaitTime = 2;
            t.Timeout += T_Timeout;
            AddChild(t);
            t.Start();
            bus = GameConfig.Instance.GetBus();
            SubscribeToEvents();
        }


        private void OnTubeLaunch(TubeLaunchEvent e)
        {
            Node3D nodeToRemove = null;
            //GD.Print(e.TubeName);
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
        private void OnMoleSpawned(MoleSpawnedEvent e)
        {
        }
        //Here is where we handle the mole killed event
        private void OnMoleKilled(MoleKilledEvent e)
        {
            GD.Print(e.EnemyType);
        }

        private void T_Timeout()
        {
            SpawnMoleTarget();
        }

        public override void _Process(double delta)
        {
            //This is where we will call the moles update methods

            foreach(ITarget t in targets)
            {
                t.Update(delta);
            }
        }


        private void SpawnMoleTarget()
        {
            //A mole target is Target.cs that we pass a packed scene into
            ITarget node = new Target(GameConfig.Instance.GetBus(), ConfigureTargetToSpawn(), ConfigurePopupDelay(), ConfigureTargetVerticleSpeed());
            
            Node3D target = node.Instantiate();
            target.Position = new Vector3(target.Position.X, .5f, target.Position.Z);
            //Place the node in the scene,
            //Here is where we will have to pick what tube we want to spawn in. But it has to be a tube that isn't active.
            Node3D moleTubes = GetNode<Node3D>("MoleTubes/NewMoleTubes");
            Godot.Collections.Array<Node> tubes = moleTubes.GetChildren();
            Node3D spawnTube = tubes[GameConfig.Instance.GetRng().RandiRange(0, tubes.Count-1)] as Node3D;
            if (activeTubes.Contains(spawnTube))
                return;
            spawnTube.AddChild(target);
            activeTubes.Add(spawnTube);
            targets.Add(node);
        }

        private int ConfigurePopupDelay()
        {
            return GameConfig.Instance.GetRng().RandiRange(1, Room5Data.PopupDelay);
        }

        private int ConfigureTargetVerticleSpeed()
        {
            return GameConfig.Instance.GetRng().RandiRange(1, Room5Data.VerticleSpeed);
        }

        private PackedScene ConfigureTargetToSpawn()
        {
            return Room5Data.TargetToSpawn[GameConfig.Instance.GetRng().RandiRange(0, Room5Data.TargetToSpawn.Length-1)];
        }
        private void SubscribeToEvents()
        {
            bus.Subscribe<MoleKilledEvent>(OnMoleKilled);
            bus.Subscribe<MoleSpawnedEvent>(OnMoleSpawned);
            bus.Subscribe<TubeLaunchEvent>(OnTubeLaunch);
        }
    }
}