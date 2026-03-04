using Godot;
using System;

public partial class BulletSpawnPoint : Node3D
{

	[Export]
	public PackedScene projectile;
	[Export]
	Camera3D camera;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		if (Input.IsActionJustPressed("Fire"))
			FireBullet();
	}

	private void FireBullet()
	{
		var viewPortCenter = GetViewport().GetVisibleRect().Size / 2;
        // Raycast from camera through viewport center
        var from = camera.ProjectRayOrigin(viewPortCenter);
        var to = from + camera.ProjectRayNormal(viewPortCenter) * 100;

		var spaceState = GetWorld3D().DirectSpaceState;
		var query = PhysicsRayQueryParameters3D.Create(from, to);
        query.CollideWithAreas = true;
        var result = spaceState.IntersectRay(query);

		//GD.Print(result);	
		// Determine target point
		Vector3 targetPoint;
		if (result.Count > 0)
			targetPoint = (Vector3)result["position"];
		else
			targetPoint = to; // If no hit, aim at max distance


		var direction = (targetPoint - this.GlobalPosition).Normalized();

		Bullet spawnedProjectile = projectile.Instantiate() as Bullet;
		GetTree().Root.AddChild(spawnedProjectile);
		spawnedProjectile.GlobalPosition = this.GlobalPosition;

		spawnedProjectile.LookAt(this.GlobalPosition + direction, Vector3.Up);
		spawnedProjectile.velocity = direction * 75;//this.GlobalTransform.Basis.Z * 15;
	}
}
