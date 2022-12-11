﻿using System.Diagnostics;
using Terminal.Gui;
using MusicPlayerCore.Player;
using MusicPlayerCore.Metadata;
using MusicPlayerCore.Playlist;

namespace MusicPlayer.Views;

internal class MusicPlayerView
{
    private readonly IPlayer player;

    private Window Main;
    private FrameView trackList;
    private FrameView playback;
    private Label nowPlaying;
    private ProgressBar progressBar;

    private List<string> tracks = new();

    private object mainLoopTimeout = null!;

    private Label track;
    private Label artist;
    private Label album;
    private Label genre;
    private Button playPause;
    private Label volume;
    private Label time;

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
            Height = Dim.Percent(65f)
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
            var track = args.Value.ToString()!;
            PlayTrack(track);
        };

        trackList.Add(list);
    }

    private void SetupMetadata()
    {

       var trackView = new FrameView("Track")
        {
            X = Pos.Percent(85f),
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Percent(16.25f)
        };
        
        track = new Label("-")
        {
            X = 0,
            Y = Pos.Center(),
            Width = trackView.Width - 2,
            Height = 1
        };

        trackView.Add(track);

        var artistView = new FrameView("Artist")
        {
            X = Pos.Percent(85f),
            Y = Pos.Percent(16.25f),
            Width = Dim.Fill(),
            Height = Dim.Percent(16.25f)
        };

        artist = new Label("-")
        {
            X = 0,
            Y = Pos.Center(),
            Width = artistView.Width - 2,
            Height = 1
        };

        artistView.Add(artist);

        var albumView = new FrameView("Album")
        {
            X = Pos.Percent(85f),
            Y = Pos.Percent(33f),
            Width = Dim.Fill(),
            Height = Dim.Percent(16.25f)
        };

        album = new Label("-")
        {
            X = 0,
            Y = Pos.Center(),
            Width = albumView.Width - 2,
            Height = 1
        };

        albumView.Add(album);

        var genreView = new FrameView("Genre")
        {
            X = Pos.Percent(85f),
            Y = Pos.Top(playback) - Pos.Percent(17f), //Pos.Percent(54f),
            Width = Dim.Fill(),
            Height = Dim.Percent(17f)
        };

        genre = new Label("-")
        {
            X = 0,
            Y = Pos.Center(),
            Width = genreView.Width - 2,
            Height = 1
        };

        genreView.Add(genre);

        Main.Add(trackView, artistView, albumView, genreView);
    }

    private void SetupPlayback()
    {
        playback = new FrameView("Playback")
        {
            X = 0,
            Y = Pos.Percent(65f),
            Width = Dim.Fill(),
            Height = Dim.Percent(38f)
        };

        nowPlaying = new Label("Now Playing: ")
        {
            X = 0,
            Y = Pos.Percent(20f),
            Width = Dim.Percent(50f),
            Height = 1
        };

        playback.Add(nowPlaying);

        progressBar = new ProgressBar()
        {
            X = 0,
            Y = Pos.Percent(80f),
            Width = Dim.Fill() - 17,
            Height = 1,
            ProgressBarStyle = ProgressBarStyle.Continuous,
            //ColorScheme = Colors.ColorSchemes["Base"],
            //ProgressBarFormat = ProgressBarFormat.Framed,
        };

        playback.Add(progressBar);

        time = new Label("00:00 / 00:00")
        {
            X = Pos.Right(progressBar) + 1,
            Y = Pos.Percent(80f),
            Width = Dim.Percent(50f),
            Height = 1
        };

        playback.Add(time);

        var seekF = new Button("<< Seek")
        {
            X = 0,
            Y = Pos.Percent(50f),
            Width = 10,
            Height = 1
        };
        seekF.Clicked += player.SeekBackward;
        playback.Add(seekF);

        // add playback controls
        playPause = new Button("Play")
        {
            X = Pos.Right(seekF) + Pos.Percent(2f),
            Y = Pos.Percent(50f),
            Width = 10,
            Height = 1
        };

        playPause.Clicked += () =>
        {
            player.PlayPause();
            UpdatePlayPauseButton();
        };
        playback.Add(playPause);


        var seekB = new Button(">> Seek")
        {
            X = Pos.Right(playPause) + Pos.Percent(2f),
            Y = Pos.Percent(50f),
            Width = 10,
            Height = 1
        };

        seekB.Clicked += player.SeekForward;
        playback.Add(seekB);

        //add volume controls

        var volumeUp = new Button("+ Volume")
        {
            X = Pos.Right(seekB) + Pos.Percent(4f),
            Y = Pos.Percent(50f),
            Width = 10,
            Height = 1
        };

        volumeUp.Clicked += VolumeUp;
        playback.Add(volumeUp);


        var volumeDown = new Button("- Volume")
        {
            X = Pos.Right(volumeUp) + Pos.Percent(2f),
            Y = Pos.Percent(50f),
            Width = 10,
            Height = 1
        };

        volumeDown.Clicked += VolumeDown;

        playback.Add(volumeDown);


        volume = new Label($"Volume: {player.Volume}")
        {
            X = Pos.Right(volumeDown) + Pos.Percent(3f),
            Y = Pos.Percent(50f),
            Width = 10,
            Height = 1
        };

        playback.Add(volume);
    }

    private void VolumeDown()
    {
        player.VolumeDown();
        UpdateVolume();
    }

    private void VolumeUp()
    {
        player.VolumeUp();
        UpdateVolume();
    }

    public void Init()
    {
        Application.Init();

        SetupMain();
        SetupPlayback();
        SetupMetadata();

        Main.Add(trackList);
        Main.Add(playback);

        var top = Application.Top;

        var menu = new MenuBar(new MenuBarItem[]
        {
            new MenuBarItem("_File", new MenuItem[]
            {
                new MenuItem("_Open file", "Open a music files", () => OpenFile()),

                new MenuItem("Open _folder", "Open a folder of music files", () => OpenFolder()),

                new MenuItem("Open Pla_ylist", "Load a playlist", () => OpenPlaylist()),
                
                new MenuItem("_Create Playlist", "Create a m3u playlist", () => CreatePlaylist()),

                new MenuItem("_Quit", "Exit Music Player", () => Application.RequestStop()),
            })
        });

        top.Add(Main, menu);

        Application.Run();
    }

    private void CreatePlaylist()
    {
        var dialog = new SaveDialog("Create Playlist", "Create a playlist")
        {
            AllowedFileTypes = new string[] { "m3u" },
            DirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        };
        Application.Run(dialog);

        if (!dialog.Canceled)
        {
            var playlistPath = dialog.FilePath.ToString()!;
            M3UPlaylistManager.CreatePlaylist(playlistPath, tracks);
        }
    }

    private void OpenPlaylist()
    {
        var dialog = new OpenDialog("Open Playlist", "Select playlist to open")
        {
            AllowsMultipleSelection = false,
            CanChooseDirectories = false,
            CanChooseFiles = true,
            AllowedFileTypes = new[] { ".m3u" },
            DirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        };
        Application.Run(dialog);
        
        if (!dialog.Canceled)
        {
            var playlist = dialog.FilePath.ToString()!;
            tracks.Clear();
            tracks.AddRange(M3UPlaylistManager.LoadPlaylist(playlist));
            Debug.WriteLine(tracks.Count);
            //PlayTrack(tracks.FirstOrDefault()!);
            Application.Refresh();
        }
    }

    public void OpenFile()
    {
        var fileFormats = new string[] { ".mp3", ".wav", ".flac" };
        var dialog = new OpenDialog("Open Files", "Select files to open")
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
            var files = dialog.FilePaths;
            var firstFile = files.FirstOrDefault();
            foreach (var file in files)
            {
                tracks.Add(file.ToString()!);
            }
            if (firstFile != null)
            {
                PlayTrack(firstFile);
            }
            Application.Refresh();
        }
    }
    
    private void PlayTrack(string firstFile)
    {
        player.Start(firstFile.ToString()!);
        UpdateUI(firstFile);
    }

    private void UpdateUI(string file)
    {
        UpdateMetadataName(file);
        UpdateNowPlaying(file);
        progressBar.Fraction = 0f;
        UpdateProgressBar();
        UpdatePlayPauseButton();
    }

    public void OpenFolder()
    {
        var dialog = new OpenDialog("Open Folder", "Select folder to open")
        {
            AllowsMultipleSelection = false,
            CanChooseDirectories = true,
            CanChooseFiles = false,
            DirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        };
        Application.Run(dialog);

        if (!dialog.Canceled)
        {
            var dirs = Directory.EnumerateDirectories(dialog.FilePath.ToString()!);

            var files = Directory.EnumerateFiles(dialog.FilePath.ToString()!)
                .Where(f => f.EndsWith(".mp3") || f.EndsWith(".wav") || f.EndsWith(".flac"));

            foreach (var file in files)
            {
                tracks.Add(file);
            }
            Application.Refresh();
        }
    }

    private void UpdateMetadataName(string path)
    {
        track.Text = MetadataExtractor.GetTitle(path);
        artist.Text = MetadataExtractor.GetArtist(path);
        album.Text = MetadataExtractor.GetAlbum(path);
        genre.Text = MetadataExtractor.GetGenre(path);
    }

    private void UpdateNowPlaying(string track) => nowPlaying.Text = $"Now Playing: {track}";

    //private void UpdateTime(string time)
    //{
    //    var timelabel = new Label(time)
    //    {
    //        X = Pos.Right(progressBar) + 1,
    //        Y = Pos.Percent(75f),
    //        Width = Dim.Percent(50f),
    //        Height = 1
    //    };

    //    playback.Add(timelabel);
    //}

    private void UpdatePlayPauseButton()
    {
        if (player.PlaybackState == PlaybackState.Playing)
        {
            playPause.Text = "Pause";
        }
        else
        {
            playPause.Text = "Play";
        }
    }

    private void UpdateProgressBar()
    {
        mainLoopTimeout = Application.MainLoop.AddTimeout(TimeSpan.FromSeconds(1), (updateTimer) =>
        {
            while (player.CurrentTime().TotalSeconds <= player.TotalTime().TotalSeconds && player.PlaybackState is not PlaybackState.Stopped)
            {
                progressBar.Fraction = (float)(player.CurrentTime().TotalSeconds / player.TotalTime().TotalSeconds);
                var timePlayed = player.CurrentTime().ToString(@"mm\:ss");
                var trackLength = player.TotalTime().ToString(@"mm\:ss");
                time.Text = $"{timePlayed} / {trackLength}";

                return true;
            }

            return false;
        });
    }

    private void UpdateVolume() => volume.Text = $"Volume: {player.Volume}";
}
