using Terminal.Gui;
using MusicPlayer.Views;
using MusicPlayerCore;

namespace MusicPlayer;

internal class Program
{
    private static void Main(string[] args)
    {
        //MusicPlayerView musicPlayer = new(new MockPlayer());
        //musicPlayer.Init();

        IPlayer player = new WindowsPlayer();
        string path = @"";
        //string path = @"C:\Users\stolo\test.mp3";
        player.Start(path);
        while (true)
        {
            Console.WriteLine("Press any key to play/pause");
            Console.ReadKey();
            player.PlayPause();
        }
    }
}

public class MockPlayer : IPlayer
{
    public void ChangeVolume(double volume) => throw new NotImplementedException();
    public TimeSpan CurrentTime() => throw new NotImplementedException();
    public void Next() => throw new NotImplementedException();
    public void PlayPause() => throw new NotImplementedException();
    public void Previous() => throw new NotImplementedException();
    public void SeekBackward() => throw new NotImplementedException();
    public void SeekForward() => throw new NotImplementedException();
    public void Start(string path) => throw new NotImplementedException();
    public void Status() => throw new NotImplementedException();
    public TimeSpan TotalTime() => throw new NotImplementedException();
}
