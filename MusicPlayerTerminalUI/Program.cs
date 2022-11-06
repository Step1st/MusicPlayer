using MusicPlayer.Views;
using MusicPlayerCore.Player;
using System.Runtime.InteropServices;
using System.Numerics;

namespace MusicPlayer;

internal class Program
{
    private static void Main(string[] args)
    {
        IPlayer player = new WindowsPlayer();
        //if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        //{
        //    player = new WindowsPlayer();
        //}
        //else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        //{
        //    player = new LinuxPlayer();
        //}
        //else
        //{
        //    player = new MacPlayer();
        //}
        MusicPlayerView musicPlayer = new(player);
        musicPlayer.Init();
    }
}
