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

        trackList = new FrameView("Track List")
        {
            X = 0,
            Y = 0,
            Width = Dim.Percent(85f),
            Height = Dim.Percent(70f)
        };

        var list = new ListView(new List<string>() { "Track 1", "Track 2", "Track 3" })
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        
        trackList.Add(list);

        playback = new FrameView("Playback")
        {
            X = Pos.Percent(85f),
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Percent(70f)
        };

        playback.Add(new Button("<< Seek")
        {
            X = Pos.Center(),
            Y = Pos.Center()-3 - 3,
            Width = 10,
            Height = 1
        });

        // add playback controls
        playback.Add(new Button("Play")
        {
            X = Pos.Center(),
            Y = Pos.Center() -3 - 1,
            Width = 10,
            Height = 1
        });

        playback.Add(new Button("Pause")
        {
            X = Pos.Center(),
            Y = Pos.Center() -3 + 1,
            Width = 10,
            Height = 1
        });

        playback.Add(new Button(">> Seek")
        {
            X = Pos.Center(),
            Y = Pos.Center() -3 + 3,
            Width = 10,
            Height = 1
        });

        //add volume controls
        playback.Add(new Button("+ Volume")
        {
            X = Pos.Center(),
            Y = Pos.Center() +4 -1,
            Width = 10,
            Height = 1
        });

        playback.Add(new Button("- Volume")
        {
            X = Pos.Center(),
            Y = Pos.Center() +4 + 1,
            Width = 10,
            Height = 1
        });


        playing = new FrameView("Playing")
        {
            X = 0,
            Y = Pos.Percent(70f),
            Width = Dim.Fill(),
            Height = Dim.Percent(32f)
        };

        var label = new Label("Track Name")
        {
            X = 0,
            Y = Pos.Percent(20f),
            Width = Dim.Fill(),
            Height = 1
        };

        playing.Add(label);

        var progress = new ProgressBar()
        {
            X = 0,
            Y = Pos.Percent(75f) - 1,
            Width = Dim.Fill() - 13,
            Height = 1,
            ProgressBarFormat = ProgressBarFormat.Framed
        };

        playing.Add(progress);

        var timelabel = new Label("00:00/00:00")
        {
            X = Pos.Right(progress) + 1,
            Y = Pos.Percent(75f),
            Width = Dim.Percent(50f),
            Height = 1
        };

        playing.Add(timelabel);

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
