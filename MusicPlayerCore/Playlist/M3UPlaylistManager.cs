using PlaylistsNET.Models;
using PlaylistsNET.Content;


namespace MusicPlayerCore.Playlist;

public static class M3UPlaylistManager
{

    public static void CreatePlaylist(string path, List<string> tracks)
    {
        M3uPlaylist playlist = new();
        M3uContent content = new();

        if (!path.EndsWith(".m3u"))
        {
            path += ".m3u";
        }

        foreach (var track in tracks)
        {
            playlist.PlaylistEntries.Add(new M3uPlaylistEntry()
            {
                Path = track
            });
        }
        var m3uContent = content.ToText(playlist);

        File.WriteAllText(path, m3uContent);
    }

    public static List<string> LoadPlaylist(string path)
    {

        M3uContent content = new();
        
        var playlist = content.GetFromStream(File.OpenRead(path));

        return playlist.GetTracksPaths();
    }

}
