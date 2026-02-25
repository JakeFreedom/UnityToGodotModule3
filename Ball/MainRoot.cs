using Godot;
using System.Collections.Generic;

public partial class MainRoot : Node3D
{
	[Export] int beachBalls;
	[Export] int beachBallSpacing; //Beach ball is 2 meters wide, so if user says spacing 1, that means 1 beach ball width,2 2 balls width so on...
	[Export] PackedScene beachBallScene;
	[Export] int maxHeightRange;
	[Export] int rotationalSpeedRange;
	[Export] float maxScaleRange;
	[Export] int scaleSpeedRange;

	List<Node3D> beachBallList;
	Node3D[] beachBallArray;
	RandomNumberGenerator rng;
	public override void _Ready()
	{
		beachBallList = new List<Node3D>();
		beachBallArray = new Node3D[beachBalls];
		rng = new RandomNumberGenerator();
		var degreesOfRotation = rng.RandfRange(1, 90);
		int startingPosition = ((beachBalls - 1) * beachBallSpacing) * -1;
		for (int i = 0; i < beachBalls; i++)
		{
			BeachBall beachBall = beachBallScene.Instantiate<BeachBall>();
			beachBall.Position = new Vector3(startingPosition, rng.RandiRange(-2, 2), 0);//stating position is calculated base on the number of beach balls, the y position is purely random between -2 and 2

			//Give each ball a randon rotation on the y axis.
			beachBall.Rotate(new Vector3(0, 1, 0), Mathf.DegToRad(degreesOfRotation));
			beachBall.moveSpeed = rng.RandfRange(.2f, 4.5f);
			beachBall.startingXPos = beachBall.Position.X;
			beachBall.maxHeight = rng.RandiRange(1, maxHeightRange);
			beachBall.moveUp = true;
			beachBall.rotationalSpeed = rng.RandiRange(1, rotationalSpeedRange);
			beachBall.rotationDirection = rng.RandiRange(-1, 1);
			beachBall.maxScale = rng.RandfRange(1, maxScaleRange);
			beachBall.scaleUp = true;
			beachBall.scaleSpeed = rng.RandiRange(1, scaleSpeedRange);

			startingPosition += (beachBallSpacing * 2); // this is basically our spacing 2 is the default 1 full beachball(or touching)
			
			AddChild(beachBall);
			beachBallList.Add(beachBall);
			beachBallArray = beachBallList.ToArray();

		}
	}

	public override void _Process(double delta)
	{
		foreach (BeachBall ball in beachBallList)
			Rotate(ball, (float)delta);

		for(int i = 0; i < beachBallArray.Length - 1; i++)
		{
			BeachBall ball= beachBallArray[i] as BeachBall;
			Scale(ball, (float)delta);
		}

		int whileLoopIndex = 0;
		while (whileLoopIndex < beachBallArray.Length)
		{
			BeachBall ball = beachBallArray[whileLoopIndex] as BeachBall;
            TranslateUpDown(ball, (float)delta);
			whileLoopIndex++;
        }
	}
	private void Rotate(BeachBall node, float delta) => node.Rotate(Vector3.Up, ((float)delta * node.rotationalSpeed) * node.rotationDirection);

	private void TranslateUpDown(BeachBall node, float delta)
	{
		if (node.Position.Y <= node.maxHeight && node.moveUp)
			node.Position = new Vector3(node.Position.X, node.Position.Y + (float)delta * node.moveSpeed, node.Position.Z);
		else
		{
			node.moveUp = false;
			node.Position = new Vector3(node.Position.X, node.Position.Y - ((float)delta * node.moveSpeed), node.Position.Z);
			if (node.Position.Y <= 0)
				node.moveUp = true;
		}
	}

	private void Scale(BeachBall node, float delta)
	{
		if(node.Scale.X <= node.maxScale && node.scaleUp)
			node.Scale = new Vector3(node.Scale.X+delta*node.scaleSpeed, node.Scale.Y + delta*node.scaleSpeed, node.Scale.Z+delta*node.scaleSpeed);
		else
		{
			node.scaleUp = false;
            node.Scale = new Vector3(node.Scale.X - delta * node.scaleSpeed, node.Scale.Y - delta * node.scaleSpeed, node.Scale.Z - delta * node.scaleSpeed);
			if(node.Scale.X <= 1)
				node.scaleUp = true;
        }
	}
}
