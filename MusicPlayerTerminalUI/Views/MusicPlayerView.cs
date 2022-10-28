using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using MusicPlayerCore;

namespace MusicPlayer.Views;

internal class MusicPlayerView
{
    
    private IPlayer player;

    private Window Main;
    private FrameView trackList;
    private FrameView playback;
    private FrameView playing;

    public MusicPlayerView(IPlayer player) => this.player = player;

    public void Init()
    {
        Application.Init();

        Main = new Window("Music Player")
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            Border = new Border()
        };

        Main.Add(TrackListView.TrackList);
        Main.Add(PlaybackView.Playback);
        Main.Add(PlayingView.Playing);

        trackList = TrackListView.TrackList;

        playback = PlaybackView.Playback;

        playing = PlayingView.Playing;

        Main.Add(trackList);
        Main.Add(playback);
        Main.Add(playing);
        var top = Application.Top;

        var menu = new MenuBar(new MenuBarItem[]
        {
            new MenuBarItem("_File", new MenuItem[]
            {
                new MenuItem("_Open", "Open a music file", () => OpenFile()),

                new MenuItem("Open Pla_ylist", "Load a playlist", () => OpenFolder()),

                new MenuItem("_Quit", "Exit Music Player", () => Application.RequestStop()),
            }),

            new MenuBarItem("_Help", new MenuItem[]
            {
                new MenuItem("_About", "", () => Console.Write("")),
            })
        });
        
        top.Add(Main, menu);

        Application.Run();
    }

    public void OpenFile()
    {
    }

    public void OpenFolder()
    {
    }
}
