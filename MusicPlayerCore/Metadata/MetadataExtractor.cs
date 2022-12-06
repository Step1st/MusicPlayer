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
        var file = GetFile(path);

        if (file?.Tag.Title != null)
        {
            return file.Tag.Title;
        }
        else
        {
            return path.Split('\\').Last();
        }
    }
    public static string GetArtist(string path)
    {
        var file = GetFile(path);

        if (file?.Tag.FirstPerformer != null)
        {
            return file.Tag.FirstPerformer;
        }
        else
        {
            return "-";
        }
    }
    public static string GetAlbum(string path)
    {
        var file = GetFile(path);

        if (file?.Tag.Album != null)
        {
            return file.Tag.Album;
        }
        else
        {
            return "-";
        }

    }
    public static string GetGenre(string path)
    {
        var file = GetFile(path);

        if (file?.Tag.FirstGenre != null)
        {
            return file.Tag.FirstGenre;
        }
        else
        {
            return "-";
        }
    }
    public static string GetYear(string path)
    {
        var file = GetFile(path);

        if (file?.Tag.Year != 0)
        {
            return file.Tag.Year.ToString();
        }
        else
        {
            return "-";
        }
    }

    public static TagLib.File? GetFile(string path)
    {
        try
        {
            return TagLib.File.Create(path);
        }
        catch (TagLib.CorruptFileException)
        {
            return null;
        }
    }
}
