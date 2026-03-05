using Godot;
namespace FPS.Room2
{
    internal class SpawnedTarget 
    {
        //The event to register to remove this from overall list.
        //Sinde this doesn't derive from a game object, have to user c# events system.
        public delegate void LifeTimeTimeOutEventHandler(SpawnedTarget n);
        public event LifeTimeTimeOutEventHandler LifeTimeTimeOut;

        int leftSide;
        int rightSide;
        int targetPosition;
        System.Timers.Timer lifeTimeTimer;
        public SpawnedTarget(float timeToLive, int leftSideBounds, int rightSideBounds)
        {
            leftSide = leftSideBounds; ;
            rightSide = rightSideBounds;
            TargetPosition = (GameConfig.Instance.GetRng().RandfRange(0,1) <= .45 ? rightSide : leftSide);
            lifeTimeTimer = new System.Timers.Timer();
            lifeTimeTimer.Interval = timeToLive * 1000;
            lifeTimeTimer.Elapsed += LifeTimeTimer_Elapsed;
            lifeTimeTimer.Start();
        }

        public void Update(double delta) {

            if(Target.Position.X <= targetPosition)
            {
                targetPosition = rightSide;
                Target.Position = new Vector3(Target.Position.X + ((float)delta * MoveSpeed), Target.Position.Y, Target.Position.Z);
            }
            else
            {
                targetPosition = leftSide;
                Target.Position = new Vector3(Target.Position.X - ((float)delta * MoveSpeed), Target.Position.Y, Target.Position.Z);
            }
        }
        private void LifeTimeTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Target.QueueFree();
            LifeTimeTimeOut?.Invoke(this);
        }

        public Node3D Target { get; set; }

        public float MoveSpeed { get; set; }

        public int TargetPosition { get; set; }
    }
}
