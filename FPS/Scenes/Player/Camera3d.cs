using Godot;
using System;

public partial class Camera3d : Camera3D
{
    // Bob parameters
    [Export] public float BobFrequency = 9.0f;  // How fast the bob cycles
    [Export] public float BobAmplitude = 90.00f;  // How much vertical movement
    [Export] public float BobHorizontalAmplitude = 0.1f;  // Side-to-side sway

    // Internal state
    private float _timePassed = 0.0f;
    private bool _isMoving = false;

    // Store the original position
    private Vector3 _initialPosition;

    public override void _Ready()
    {
        _initialPosition = Position;
    }

    public override void _Process(double delta)
    {
        // Check if player is moving
        _isMoving = CheckMovement();

        if (_isMoving)
        {

            _timePassed += (float)delta * BobFrequency / BobAmplitude ;

            // Vertical bob (up and down)
            float verticalOffset = Mathf.Sin(_timePassed) * BobAmplitude;

            // Horizontal sway (side to side) - slower than vertical
            float horizontalOffset = Mathf.Cos(_timePassed * 0.5f) * BobHorizontalAmplitude;

            // Apply the offsets
            var newPosition = Position;
            newPosition.Y = _initialPosition.Y + verticalOffset;
            newPosition.X = _initialPosition.X + horizontalOffset;
            Position = newPosition;
        }
        else
        {
            // Smoothly return to initial position when not moving
            _timePassed = 0.0f;
            Position = Position.Lerp(_initialPosition, (float)delta * 5.0f);
        }
    }

    private bool CheckMovement()
    {
        // Replace this with your actual movement detection
        // Example for WASD movement:
        Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        return inputDir.Length() > 0.0f;
    }
}
