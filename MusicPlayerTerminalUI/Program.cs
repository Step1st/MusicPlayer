using Terminal.Gui;
using MusicPlayer.Views;

namespace MusicPlayer;

internal class Program
{
    private static void Main(string[] args)
    {
        Application.Init();
        MainView.Init();
        Application.Run();
    }
}
