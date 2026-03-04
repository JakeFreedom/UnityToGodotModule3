using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class SwitchLever : Node3D
{

	[Signal]
	public delegate void LightSwitchLeverEventHandler(bool isOn);

	[Export] Node3D roomLights;

	Area3D switchHitBox;
	bool switchLocation = false;
	AnimationPlayer switchPlayer;

	List<SpotLight3D> lights;
	Boolean turnLightsOn = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		switchHitBox = GetNode<Area3D>("HitBox");
		switchHitBox.AreaEntered += AreaEnteredHandler;
		switchPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

		lights = new List<SpotLight3D>();
		if(roomLights != null)
			FindRoomLights(roomLights);

	}

	float delayTime;
	int lightIndex = 0;
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		delayTime += (float)delta;
		if(turnLightsOn && lightIndex < 16)
			if(delayTime>=1)
			{
				delayTime = 0;
				lightIndex += 1;
			}
	}


	private void AreaEnteredHandler(Area3D otherArea)
	{
		if(switchLocation == false)
		{
			switchPlayer.Play("TurnOn");
			switchLocation = true;
			foreach (SpotLight3D light in lights)
			{
				light.Visible = true;
				turnLightsOn = true;
			}

			EmitSignal(SignalName.LightSwitchLever, true);
			
		}
		else
		{
			switchPlayer.Play("TurnOff");
			switchLocation = false;
            
			foreach (SpotLight3D light in lights)
                light.Visible = false;

            EmitSignal(SignalName.LightSwitchLever, false);
        }

	}


	private void FindRoomLights(Node3D node)
	{
		foreach(Node3D n in node.GetChildren())
			FindRoomLights(n);

		if (node.GetType() == typeof(SpotLight3D))
		{
			node.Visible = false;
			lights.Add((SpotLight3D)node);
		}
	}
}
