namespace FPS.Room5
{
    public class MoleKilledEvent : GameEvent
    {
        public string EnemyType { get; set; }
        public string KilledBy { get; set; }   
        public int XpReward { get; set; }
    }
}
