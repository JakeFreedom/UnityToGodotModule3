using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPS.Room4
{
    internal class WalkingTarget : iTarget
    {
        public event EventHandler LifeTimeTimerElapsed;

        public Node3D Instantiate(Node3D parentNode)
        {
            return new Node3D();
        }

        public void Update(double delta)
        {
            
        }

        Node3D iTarget.Instantiate(Node3D parentNode)
        {
            throw new NotImplementedException();
        }
        int iTarget.Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        Node3D iTarget.TargetNode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        PackedScene iTarget.TargetScene { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
