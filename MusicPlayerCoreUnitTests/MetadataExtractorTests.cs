using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerCoreUnitTests;
public class MetadataExtractorTests
{
    [Fact]
    public void GetTitle()
    {
        var path = @"C:\Users\stolo\Music\testMetadata.mp3";
        var title = MetadataExtractor.GetTitle(path);
        Assert.Equal("TestTitle", title);
    }

    [Fact]
    public void GetArtist()
    {
        var path = @"C:\Users\stolo\Music\testMetadata.mp3";
        var artist = MetadataExtractor.GetArtist(path);
        Assert.Equal("TestArtist", artist);
    }

    [Fact]
    public void GetAlbum()
    {
        var path = @"C:\Users\stolo\Music\testMetadata.mp3";
        var album = MetadataExtractor.GetAlbum(path);
        Assert.Equal("TestAlbum", album);
    }

    [Fact]
    public void GetGenre()
    {
        var path = @"C:\Users\stolo\Music\testMetadata.mp3";
        var genre = MetadataExtractor.GetGenre(path);
        Assert.Equal("TestGenre", genre);
    }

    [Fact]
    public void GetYear()
    {
        var path = @"C:\Users\stolo\Music\testMetadata.mp3";
        var year = MetadataExtractor.GetYear(path);
        Assert.Equal("2022", year);
    }
}
