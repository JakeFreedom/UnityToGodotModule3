using System;
using System.Runtime.CompilerServices;
using Godot;

namespace FPS.Room3
{
    //This is the main target class, which will hold a copy of the target that is spawned around the radius of the hub
    internal class RadialTargetHub
    {
        PackedScene radialTargetScene;
        PackedScene hubScene;
        float maxLeftDistance = -13;
        float maxRightDistance = -0.3f;
        float targetMoveSpeed;
        int spawnTier;
        int radialTargetsToSpawn = 0;
        float degreesPerFrame = 0;
        Node3D centerHub;
        Node3D targetSupport;
        float targetPosition;
        int radialTargetRotation = 1;

        public event EventHandler LifeTimeTimerExpired;

        public RadialTargetHub(PackedScene hubScene, PackedScene radialTargetScene, int tier, int radialTargetRPM, int radialTargetsToSpawn = 1, int lifeTime = 5, float targetMovementSpeed = 5f)
        {
            this.hubScene = hubScene;
            this.radialTargetScene = radialTargetScene;
            this.spawnTier = tier;
            this.radialTargetsToSpawn = radialTargetsToSpawn;
            degreesPerFrame = ((radialTargetRPM * 360) / 60) / 60;
            targetPosition = maxRightDistance;
            targetMoveSpeed = targetMovementSpeed;

            //Create life time timer
            System.Timers.Timer lifeTimeTimer = new System.Timers.Timer();
            lifeTimeTimer.Interval = lifeTime * 1000;
            lifeTimeTimer.Elapsed += LifeTimeTimer_Elapsed;
            lifeTimeTimer.Enabled = true;
            radialTargetRotation = (GameConfig.Instance.GetRng().RandiRange(-1, 1) == 0 ? -1:1);
            
        }

        private void LifeTimeTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //Destroy base target
            targetSupport.QueueFree();
            LifeTimeTimerExpired?.Invoke(this, new EventArgs());
        }

        #region Public Methods
        public Node3D Instantiate() {

            targetSupport = hubScene.Instantiate<Node3D>();
            centerHub = targetSupport.GetNode<Node3D>("Hub");
            SpawnRadialTargets(targetSupport);
            return targetSupport;
        }

        public void Update(double delta) {
            MoveTargets(delta);
        }

        public void PhysicsUpdate(double delta) => centerHub.RotateZ(Mathf.DegToRad(degreesPerFrame * radialTargetRotation) );
        
        #endregion
        #region Private Methods
        private void MoveTargets(double delta)
        {
            if (targetSupport.Position.X <= targetPosition)
            {
                targetPosition = maxRightDistance;
                targetSupport.Position = new Vector3(targetSupport.Position.X + ((float)delta * targetMoveSpeed), targetSupport.Position.Y, targetSupport.Position.Z);
            }
            else
            {
                targetPosition = maxLeftDistance;
                targetSupport.Position = new Vector3(targetSupport.Position.X - ((float)delta * targetMoveSpeed), targetSupport.Position.Y, targetSupport.Position.Z);
            }
        }

        private void SpawnRadialTargets(Node3D hub)
        {
            for (int i = 0; i < radialTargetsToSpawn; i++)
            {
                Node3D target = radialTargetScene.Instantiate<Node3D>();
                float radialRotation = Mathf.RadToDeg(i * (Mathf.Tau / radialTargetsToSpawn));
                target.Position = new Vector3(0, 0, .2f);
                target.Scale = new Vector3(1.3f, 1.3f, 1.3f);
                target.RotateZ(Mathf.DegToRad(radialRotation));
                target.RotateY(Mathf.RadToDeg(181));
                centerHub.AddChild(target);
            }
        }
        #endregion

        #region Public Properties
        public int Tier => spawnTier;
        #endregion
    }
}
