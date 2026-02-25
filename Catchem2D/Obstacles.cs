using Godot;

public partial class Obstacles : StaticBody2D
{
	//private Vector3 color;

	private int HitCount = 0;
	private Label hitCountLabel;
	private AudioStreamPlayer2D collisionPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Area2D>("HitDetector").AreaEntered += AreaEnteredHandler;
		hitCountLabel = GetNode<Label>("HitCount");
		hitCountLabel.Text = HitCount.ToString();
		collisionPlayer = GetNode<AudioStreamPlayer2D>("CollisionPlayer");
	}

	public void SetColor(Vector3 color) =>	GetNode<Sprite2D>("Sprite2D").Modulate = new Color(color.X, color.Y, color.Z);
	private void AreaEnteredHandler(Area2D otherArea)
	{
		HitCount++; hitCountLabel.Text = HitCount.ToString();
		if (otherArea.Owner.IsInGroup("Ball"))
		{
			collisionPlayer.PitchScale = .1f;
			collisionPlayer.Play();
		}
	}
}
