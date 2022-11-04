using Terminal.Gui;
using MusicPlayer.Views;
using MusicPlayerCore;

namespace MusicPlayer;

internal class Program
{
    private static void Main(string[] args)
    {
        MusicPlayerView musicPlayer = new(new MockPlayer());
        musicPlayer.Init();
    }
}

public class MockPlayer : IPlayer
{
}
