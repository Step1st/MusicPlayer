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

    private List<string> tracks = new List<string>();

    public MusicPlayerView(IPlayer player) => this.player = player;

    private void SetupMain()
    {

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

        var list = new ListView(tracks)
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };

        trackList.Add(list);
    }

    private void SetupPlayback()
    {
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
            Y = Pos.Center() - 1 - 3,
            Width = 10,
            Height = 1
        });

        // add playback controls
        playback.Add(new Button("Play/Pause")
        {
            X = Pos.Center(),
            Y = Pos.Center() - 1 - 1,
            Width = 10,
            Height = 1
        });

        //playback.Add(new Button("Pause")
        //{
        //    X = Pos.Center(),
        //    Y = Pos.Center() - 3 + 1,
        //    Width = 10,
        //    Height = 1
        //});

        playback.Add(new Button(">> Seek")
        {
            X = Pos.Center(),
            Y = Pos.Center() - 2 + 2,
            Width = 10,
            Height = 1
        });

        //add volume controls
        playback.Add(new Button("+ Volume")
        {
            X = Pos.Center(),
            Y = Pos.Center() + 3 - 1,
            Width = 10,
            Height = 1
        });

        playback.Add(new Button("- Volume")
        {
            X = Pos.Center(),
            Y = Pos.Center() + 3 + 1,
            Width = 10,
            Height = 1
        });
    }

    private void SetupPlaying()
    {
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
    }

    public void Init()
    {
        Application.Init();

        SetupMain();

        SetupPlayback();

        SetupPlaying();

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
        var fileFormats = new string[] {".mp3", ".wav", ".flac" };
        var dialog = new OpenDialog("Open File", "Open")
        {
            AllowsMultipleSelection = false,
            CanChooseDirectories = false,
            CanChooseFiles = true,
            DirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            AllowedFileTypes = fileFormats
        };
        Application.Run(dialog);

        if (!dialog.Canceled)
        {
            // TODO: Add file to playlist
        }
    }
    
    public void OpenFolder()
    {
        var dialog = new OpenDialog("Open Folder", "Open")
        {
            AllowsMultipleSelection = false,
            CanChooseDirectories = true,
            CanChooseFiles = false,
            DirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
        };
        Application.Run(dialog);

        if (!dialog.Canceled)
        {
            // TODO: Load playlist
        }
    }
