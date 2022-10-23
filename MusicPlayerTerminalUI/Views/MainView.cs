using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace MusicPlayer.Views;
internal static class MainView
{
    private static Window Main;
    
    static MainView()
    {
        Main = new Window
        {
            X = 0,
            Y = 1, // Leave one row for the toplevel menu

            // By using Dim.Fill(), it will automatically resize without manual intervention
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            Border = new Border()
        };
    }

    public static void Init()
    {
        var trackList = TrackListView.TrackList;

        var playback = PlaybackView.Playback;

        var playing = PlayingView.Playing;

        Main.Add(trackList);
        Main.Add(playback);
        Main.Add(playing);
        var top = Application.Top;

        var menu = MenuBarView.MenuBar;
        top.Add(Main, menu);
    }
}
