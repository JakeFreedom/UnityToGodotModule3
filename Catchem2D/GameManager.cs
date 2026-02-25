using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node3D
{
    [Export] PackedScene obstacleScene;
    [Export]int spawnDelay;
    [Export(PropertyHint.Range, "5,15,1")] int obstaclesToSpawn = 5;


    Timer spawnTimer;
    RandomNumberGenerator rng = new RandomNumberGenerator();

    //Spawn obstacle at random locations
    //Have how many configurable in the inspector
    //Grab a random rotation on the z axis
    //Create a random color 

    public override void _Ready()
    {
        //We don't really need a timer to do this.
        //You really need to start following the program and have everything laid on in writting before getting to any programming.
        spawnTimer = new Timer();
        spawnTimer.WaitTime = (spawnDelay == 0 ? 1 : spawnDelay);
        //We could use this timer to change the locations of the obstacles

        //But for now let's just spawn 5 random obstacles.
        //We can spawn them with in the viewport rect minus a margin on the side of 150 pixels/uints
        Vector2 screenSize = GetViewport().GetVisibleRect().Size;
        screenSize.X -= 150;
        screenSize.Y -= 150;
        
        List<Vector2> spawnPoints = new List<Vector2>();
        bool checkForOverLap = true;
        for (int i = 0; i < obstaclesToSpawn; i++)
        {
            checkForOverLap = true;
            //Make 5 random spawnpoints
            Vector2 spawnPoint = new Vector2(rng.RandiRange(150, (int)screenSize.X), rng.RandiRange(150, (int)screenSize.Y));
            //We only want to add the new spawn point to the list if it's not over lapping... Sprite is 64x64
            while (checkForOverLap)
            {
                if (CheckForOverLap(spawnPoint, spawnPoints))
                {
                    //gen a new spawn point
                    spawnPoint = new Vector2(rng.RandiRange(150, (int)screenSize.X), rng.RandiRange(150, (int)screenSize.Y));
                }
                else
                {
                    checkForOverLap = false;
                    spawnPoints.Add(spawnPoint);
                }
            }

            if(spawnPoints.Count == 0) 
                spawnPoints.Add(spawnPoint);
        }


        //Add them to the scene
        foreach(Vector2 spawnPoint in spawnPoints)
        {
            Obstacles obstacle = obstacleScene.Instantiate<Obstacles>() as Obstacles;
            obstacle.Position = spawnPoint;
            
            GenerateColor(obstacle);
            GiveRandomZRotation(obstacle);
            
            AddChild(obstacle);
        }

    }

    /// <summary>
    /// returns true if there is overlap
    /// </summary>
    /// <param name="spawnPoint"></param>
    /// <param name="spawnPoints"></param>
    /// <returns></returns>
    private bool CheckForOverLap(Vector2 spawnPoint, List<Vector2> spawnPoints) {
        foreach (Vector2 goodPoints in spawnPoints)
        {
            //GD.Print($"Good Point: {goodPoints.X},{goodPoints.Y}");
            //GD.Print($"Spawn Point:{spawnPoint.X}, {spawnPoint.Y}");
            if (spawnPoint.X >= goodPoints.X && spawnPoint.X+64 <= goodPoints.X + 64)
                return true;
            else
                return  false;

        }
            //If something goes wrong we will error on the side that there is overlap and return true.
            return false;
    }
    private void GenerateColor(Obstacles obj) {
        obj.color = new Vector3(rng.RandfRange(0, 1), rng.RandfRange(0, 1), rng.RandfRange(0, 1));
    
    }
    private void GiveRandomZRotation(Obstacles obj) {

        obj.Rotate(Mathf.DegToRad(rng.RandiRange(-15, 15)));
    
    }
}
