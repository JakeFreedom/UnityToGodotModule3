using Godot;

public partial class GameConfig : Node
{
    private static  RandomNumberGenerator _rng;
    public GameConfig()
    {
        Instance = this;
    }
    public static GameConfig Instance { get; private set; }

    public RandomNumberGenerator GetRng()
    {
        if (_rng != null)
            return _rng;
        _rng = new RandomNumberGenerator();
        return _rng;
    }
}
