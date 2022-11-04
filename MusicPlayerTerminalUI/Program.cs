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
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.P:
                    player.PlayPause();
                    break;
                case ConsoleKey.F:
                    player.SeekForward();
                    break;
                case ConsoleKey.B:
                    player.SeekBackward();
                    break;
                case ConsoleKey.V:
                    Console.WriteLine("Enter volume");
                    player.ChangeVolume(double.Parse(Console.ReadLine()));
                    break;
                case ConsoleKey.S:
                    player.Status();
                    break;
                case ConsoleKey.C:
                    Console.WriteLine(player.CurrentTime());
                    break;
                case ConsoleKey.T:
                    Console.WriteLine(player.TotalTime());
                    break;
                case ConsoleKey.Q:
                    return;
                default:
                    break;
            }
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
    public string Status() => throw new NotImplementedException();
    public TimeSpan TotalTime() => throw new NotImplementedException();
}
