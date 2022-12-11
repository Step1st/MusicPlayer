using Terminal.Gui;
using MusicPlayerCore.Player;
using MusicPlayerCore.Metadata;
using MusicPlayerCore.Playlist;
using PlaybackState = MusicPlayerCore.Player.PlaybackState;

namespace MusicPlayer.Views;

internal class MusicPlayerView
{
    private readonly IPlayer player;

    private Window Main;
    private FrameView trackList;
    private FrameView playing;
    private FrameView playback;
    
    private Label track;
    private Label artist;
    private Label album;
    private Label genre;
    
    private Button playPause;
    private Label nowPlaying;

    private Label volume;
    private Label time;
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
            Height = Dim.Fill(),
            
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
        playback = new FrameView("Playback")
        {
            X = Pos.Percent(85f),
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Percent(65f)
        };


        // add playback controls
        playPause = new Button("Play")
        {
            X = Pos.Center(),
            Y = Pos.Percent(35f),
            Width = 10,
            Height = 1
        };

        var seekB = new Button("<< Seek")
        {
            X = Pos.Center(),
            Y = Pos.Top(playPause) - 2,
            Width = 10,
            Height = 1
        };
        seekB.Clicked += player.SeekBackward;

        playPause.Clicked += () =>
        {
            player.PlayPause();
            UpdatePlayPauseButton();
        };

        var seekF = new Button(">> Seek")
        {
            X = Pos.Center(),
            Y = Pos.Bottom(playPause) + 1,
            Width = 10,
            Height = 1
        };
        seekF.Clicked += player.SeekForward;

        //add volume controls
        var volumeUp = new Button("+ Volume")
        {
            X = Pos.Center(),
            Y = Pos.Percent(65f),
            Width = 10,
            Height = 1
        };
        
        volumeUp.Clicked += VolumeUp;

        var volumeDown = new Button("- Volume")
        {
            X = Pos.Center(),
            Y = Pos.Bottom(volumeUp) + 1,
            Width = 10,
            Height = 1
        };
    
        volumeDown.Clicked += VolumeDown;

        playback.Add(seekB, playPause, seekF, volumeUp, volumeDown);

        Main.Add(playback);
    }

    private void SetupPlayback()
    {
        playing = new FrameView("Playback")
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

        progressBar = new ProgressBar()
        {
            X = 0,
            Y = Pos.Percent(80f),
            Width = Dim.Fill() - 17,
            Height = 1,
            ProgressBarStyle = ProgressBarStyle.Continuous,
        };

        time = new Label("00:00 / 00:00")
        {
            X = Pos.Right(progressBar) + 1,
            Y = Pos.Percent(80f),
            Width = Dim.Percent(50f),
            Height = 1
        };

        volume = new Label($"Volume: {player.Volume}")
        {
            X = Pos.Right(progressBar) + 1,
            Y = Pos.Percent(50f),
            Width = 10,
            Height = 1
        };

        playing.Add(time);

        track = new Label("Track: -")
        {
            X = 0,
            Y = Pos.Percent(50f),
            Width = Dim.Percent(15f) + 7,
            Height = 1
        };

        // add playback controls
        artist = new Label("Artist: -")
        {
            X = Pos.Right(track) + Pos.Percent(2f),
            Y = Pos.Percent(50f),
            Width = Dim.Percent(15f) + 8,
            Height = 1,
            
        };

        album = new Label("Album: -")
        {
            X = Pos.Right(artist) + Pos.Percent(2f),
            Y = Pos.Percent(50f),
            Width = Dim.Percent(15f) + 7,
            Height = 1
        };

        //add volume controls

        genre = new Label("Genre: -")
        {
            X = Pos.Right(album) + Pos.Percent(4f),
            Y = Pos.Percent(50f),
            Width = Dim.Percent(15f) + 7,
            Height = 1
        };

        playing.Add(nowPlaying, progressBar, volume, track, artist, album, genre);
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
        SetupMetadata();
        SetupPlayback();

        Main.Add(trackList);
        Main.Add(playing);

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
            PlayTrack(tracks.FirstOrDefault()!);
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
            var firstFile = files.FirstOrDefault()!;
            
            foreach (var file in files)
            {
                tracks.Add(file.ToString()!);
            }
            
            PlayTrack(firstFile);
            Application.Refresh();
        }
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

            var firstFile = files.FirstOrDefault()!;

            foreach (var file in files)
            {
                tracks.Add(file);
            }
            
            PlayTrack(firstFile);
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

    private void UpdateMetadataName(string path)
    {
        track.Text = $"Track: {MetadataExtractor.GetTitle(path)}";
        artist.Text = $"Artist: {MetadataExtractor.GetArtist(path)}";
        album.Text = $"Album: {MetadataExtractor.GetAlbum(path)}";
        genre.Text = $"Genre: {MetadataExtractor.GetGenre(path)}";
    }

    private void UpdateNowPlaying(string track) => nowPlaying.Text = $"Now Playing: {track}";

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
