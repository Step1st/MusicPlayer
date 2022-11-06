using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Terminal.Gui;
using MusicPlayerCore;

namespace MusicPlayer.Views;

internal class MusicPlayerView
{
    private readonly IPlayer player;

    private Window Main;
    private FrameView trackList;
    private FrameView playback;
    private FrameView playing;
    private ProgressBar progressBar;

    private List<string> tracks = new();

    private object mainLoopTimeout = null!;

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
            //ColorScheme = Colors.Menu

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

        list.OpenSelectedItem += (args) =>
        {
            player.Start(args.Value.ToString()!);
            UpdateTrackName(args.Value.ToString()!);
            progressBar.Fraction = 0f;
            UpdateProgressBar();
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


        var seekF = new Button("<< Seek")
        {
            X = Pos.Center(),
            Y = Pos.Center() - 1 - 3,
            Width = 10,
            Height = 1
        };
        seekF.Clicked += player.SeekBackward;
        playback.Add(seekF);

        // add playback controls
        var playPause = new Button("Play/Pause")
        {
            X = Pos.Center(),
            Y = Pos.Center() - 1 - 1,
            Width = 10,
            Height = 1
        };

        playPause.Clicked += () => player.PlayPause();
        playback.Add(playPause);


        var seekB = new Button(">> Seek")
        {
            X = Pos.Center(),
            Y = Pos.Center() - 2 + 2,
            Width = 10,
            Height = 1
        };

        seekB.Clicked += player.SeekForward;
        playback.Add(seekB);

        //add volume controls

        var volumeUp = new Button("+ Volume")
        {
            X = Pos.Center(),
            Y = Pos.Center() + 3 - 1,
            Width = 10,
            Height = 1
        };

        volumeUp.Clicked += player.VolumeUp;
        playback.Add(volumeUp);


        var volumeDown = new Button("- Volume")
        {
            X = Pos.Center(),
            Y = Pos.Center() + 3 + 1,
            Width = 10,
            Height = 1
        };

        volumeDown.Clicked += player.VolumeDown;
        playback.Add(volumeDown);
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

        //var color = new

        progressBar = new ProgressBar()
        {
            X = 0,
            Y = Pos.Percent(75f) - 1,
            Width = Dim.Fill() - 17,
            Height = 1,
            ProgressBarStyle = ProgressBarStyle.MarqueeContinuous,
            ColorScheme = Colors.ColorSchemes["Base"],
            ProgressBarFormat = ProgressBarFormat.Framed
        };

        playing.Add(progressBar);

        var timelabel = new Label("00:00 / 00:00")
        {
            X = Pos.Right(progressBar) + 1,
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
        var fileFormats = new string[] { ".mp3", ".wav", ".flac" };
        var dialog = new OpenDialog("Open File", "Open")
        {
            AllowsMultipleSelection = true,
            CanChooseDirectories = false,
            CanChooseFiles = true,
            DirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            AllowedFileTypes = fileFormats
        };
        Application.Run(dialog);

        if (!dialog.Canceled)
        {
            var file = dialog.FilePath;
            //var filename = System.IO.Path.GetFileName(file.ToString());
            tracks.Add(file.ToString()!);
            player.Start(file.ToString()!);
            Application.Refresh();
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
            var folder = dialog.FilePath;
            foreach (var fileExtention in new string[]{ "*.mp3", "*.wav", "*.flac"} )
            {
                Debug.WriteLine(fileExtention);
                foreach (var file in Directory.EnumerateFiles(folder.ToString()!, fileExtention))
                {
                    Debug.WriteLine(file);
                    tracks.Add(file);
                }
            }
            Application.Refresh();
        }
    }

    private void UpdateTrackName(string trackName)
    {
        var track = new Label(trackName)
        {
            X = 0,
            Y = Pos.Percent(20f),
            Width = Dim.Fill(),
            Height = 1
        };

        playing.Add(track);
    }

    private void UpdateTime(string time)
    {
        //playing.Subviews[2].Text = time;
        var timelabel = new Label(time)
        {
            X = Pos.Right(progressBar) + 1,
            Y = Pos.Percent(75f),
            Width = Dim.Percent(50f),
            Height = 1
        };

        playing.Add(timelabel);
    }

    private void UpdateProgressBar()
    {
        mainLoopTimeout = Application.MainLoop.AddTimeout(TimeSpan.FromSeconds(1), (updateTimer) =>
        {
            while (player.CurrentTime().Seconds < player.TotalTime().TotalSeconds && player.PlaybackState is not PlaybackState.Stopped)
            {
                progressBar.Fraction = (float)(player.CurrentTime().TotalSeconds / player.TotalTime().TotalSeconds);
                var timePlayed = player.CurrentTime().ToString(@"mm\:ss");
                var trackLength = player.TotalTime().ToString(@"mm\:ss");
                UpdateTime($"{timePlayed} / {trackLength}");

                return true;
            }

            return false;
        });
    }
}
