using FPS.Room5;
using Godot;
using System.Reflection;

public partial class GameConfig : Node
{
    private static  RandomNumberGenerator _rng;
    private static IEventBus<GameEvent> bus;
    private static IEventBus<UIEvent> uiBus;
    public GameConfig()
    {
        Instance = this;
        bus = new EventBus<GameEvent>();
        uiBus = new EventBus<UIEvent>();
    }
    public static GameConfig Instance { get; private set; }

    public RandomNumberGenerator GetRng()
    {
        if (_rng != null)
            return _rng;
        _rng = new RandomNumberGenerator();
        return _rng;
    }

    public IEventBus<GameEvent> GetBus() => bus;
    public IEventBus<UIEvent> GetUIBus => uiBus;
}
