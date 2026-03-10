using Godot;
using System;

public partial class MoleTarget : Node3D
{

		//[Signal]
		//public delegate void MoleTargerQueFreeEventHandler(MoleTarget mt);
		public event EventHandler Hit;

		//public float traveled = 0f;
		//public int direction = 1;
		//public int MaxHeight { get; set; }

		//private MeshInstance3D normalMesh;
		//private Node3D explodingTarget;

		//public MoleTube myTube;

		public override void _Ready()
		{
			GetNode<Area3D>("Area3D").AreaEntered += AreaEnteredHandler;
		}
		private void AreaEnteredHandler(Area3D otherArea)
		{
			Hit?.Invoke(this, new EventArgs());
		}
	}
