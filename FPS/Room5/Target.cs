using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPS.Room5
{

    //Mole Target
    public class Target : GameEvent, ITarget
    {
        private PackedScene targetScene;
        private IEventBus<GameEvent> eventBus;

        public Target(IEventBus<GameEvent> bus, PackedScene moleScene)
        {
            targetScene = moleScene;
            eventBus = bus; //Get a ref to the bus so we can publish events when needed
        }

        public void Update(double delta) 
        {
            this.TargetNode.Position = new Vector3(this.TargetNode.Position.X, this.TargetNode.Position.Y + (float)delta, this.TargetNode.Position.Z);
        }

        public Node3D Instantiate()
        {
            this.TargetNode = targetScene.Instantiate<Node3D>();
            ((MoleTarget)TargetNode).Hit += Target_Hit;

            return this.TargetNode;
        }

        private void Target_Hit(object sender, EventArgs e)
        {
            eventBus.Publish(new MoleKilledEvent { EnemyType = "Mole" }); // I was killed, publish that even on the bus to notify everyone who has subscribed.
        }

        public Node3D TargetNode { get; set; }
    }
}
