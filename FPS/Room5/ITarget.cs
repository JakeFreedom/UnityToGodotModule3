using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPS.Room5
{
    public  interface ITarget
    {
        Node3D Instantiate();
        void Update(double delta);
    }
}
