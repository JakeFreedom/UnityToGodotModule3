using FPS.Room5.Events;
using Godot;
using System;

namespace FPS.Room5
{
    //Mole Target
    public class Target : GameEvent, ITarget
    {
        PackedScene targetScene;
        IEventBus<GameEvent> eventBus;
        bool isActive = false;
        float maxRight = .2f;
        float maxLeft = -.2f;
        float targetPosition = 0;
        float shakeWait = 0;
        float popupDelay;
        int verticleSpeed = 0;
        PackedScene shotParticle;
        bool lifeOver = false;
        Vector3 moveDirection;
        public Target(IEventBus<GameEvent> bus, PackedScene moleScene, int maxPopupDelay, int maxVerticleSpeed, PackedScene shotParticleSystem, int targetLifeTime)
        {            
            targetScene = moleScene;
            eventBus = bus; //Get a ref to the bus so we can publish events when needed
            targetPosition = maxRight;
            popupDelay = maxPopupDelay;
            verticleSpeed = maxVerticleSpeed;
            shotParticle = shotParticleSystem;

            //Put it out on the line that I have become alive.<--See what I did there.
            bus.Publish<MoleSpawnedEvent>(new MoleSpawnedEvent());
        }

        public void Update(double delta) 
        {
            if (isActive)
                this.TargetNode.Position = new Vector3(this.TargetNode.Position.X, this.TargetNode.Position.Y + ((float)delta * verticleSpeed), this.TargetNode.Position.Z);

            if (!isActive)
                DoShake(delta);
        }

        public Node3D Instantiate()
        {
            this.TargetNode = targetScene.Instantiate<Node3D>();
            ((MoleTarget)TargetNode).Hit += Target_Hit;

            return this.TargetNode;
        }

        private void Target_Hit(object sender, EventArgs e)
        {
            if (isActive)
            {
                Node3D t = shotParticle.Instantiate<Node3D>();
                this.TargetNode.AddChild(t);
                eventBus.Publish(new MoleKilledEvent { EnemyType = "Mole" }); // I was killed, publish that event on the bus to notify everyone who has subscribed.
            }
        }
        
        private void DoShake(double delta)
        {
            shakeWait += (float)delta;
            if (shakeWait > popupDelay)
            {
                isActive = true;
                this.TargetNode.Position = Vector3.Zero;
                //Publish event so target node knows to enable it's area 3d
                eventBus.Publish<DoShakeaEndedEvent>(new DoShakeaEndedEvent());
            }
            if(this.TargetNode.Position.X < targetPosition)
            {
                targetPosition = maxRight;
                moveDirection = new Vector3(this.TargetNode.Position.X + (float)delta*9, this.TargetNode.Position.Y, this.TargetNode.Position.Z);
            }
            else
            {
                targetPosition = maxLeft;
                moveDirection = new Vector3(this.TargetNode.Position.X - (float)delta*9, this.TargetNode.Position.Y, this.TargetNode.Position.Z);
            }
            this.TargetNode.Position = moveDirection;
        }

        public Node3D TargetNode { get; set; }
    }
}
