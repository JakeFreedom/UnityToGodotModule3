using Godot;

    [Tool]
    public partial class TestTool : Button
    {

        public override void _Ready()
        {
            GD.Print("hello");
        }


        public override void _Process(double delta)
        {
            GD.Print(delta);
        }
    }

