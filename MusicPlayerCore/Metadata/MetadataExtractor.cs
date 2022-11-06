using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerCore.Metadata;
public static class MetadataExtractor
{
    public static string GetTitle(string path)
    {
        var file = TagLib.File.Create(path);
        return file.Tag.Title;
    }
    public static string GetArtist(string path)
    {
        var file = TagLib.File.Create(path);
        return file.Tag.FirstPerformer;
    }
    public static string GetAlbum(string path)
    {
        var file = TagLib.File.Create(path);
        return file.Tag.Album;
    }
    public static string GetGenre(string path)
    {
        var file = TagLib.File.Create(path);
        return file.Tag.FirstGenre;
    }
    public static string GetYear(string path)
    {
        var file = TagLib.File.Create(path);
        return file.Tag.Year.ToString();
    }
}
