using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPS.Room5
{
    public class MoleKilledEvent : GameEvent
    {
        public string EnemyType { get; set; }
        public string KilledBy { get; set; }   
        public int XpReward { get; set; }
    }
}
