using Godot;
using System;
using System.Collections.Generic;

namespace FPS.Room5
{
    public partial class Room5 : Node3D
    {
        [Export] Room5ResourceData Room5Data;
        List<Node3D> activeTubes = new List<Node3D>();
        List<ITarget> targets = new List<ITarget>();

        Timer t;
        public override void _Ready()
        {
            t = new Timer();
            t.WaitTime = 2;
            t.Timeout += T_Timeout;
            AddChild(t);
            t.Start();
            //Right now the room cares about subing to the mole killed event
            GameConfig.Instance.GetBus().Subscribe<MoleKilledEvent>(OnMoleKilled);
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
            ITarget node = new Target(GameConfig.Instance.GetBus(), Room5Data.MoleTarget);
            
            Node3D target = node.Instantiate();
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
    }
}