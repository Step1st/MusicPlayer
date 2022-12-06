using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerCoreUnitTests;
public class MetadataExtractorTests
{

    public string path = @"../../../../Songs/testMetadata.mp3";
    [Theory]
    [InlineData("../../../../Songs/testMetadata.mp3", "TestTitle")]
    [InlineData("../../../../Songs/testNoMetadata.mp3", "testNoMetadata.mp3")]
    public void GetTitle(string path, string expectedTitle)
    {
        var actualTitle = MetadataExtractor.GetTitle(path);
        Assert.Equal(expectedTitle, actualTitle);
    }

    [Theory]
    [InlineData("../../../../Songs/testMetadata.mp3", "TestArtist")]
    [InlineData("../../../../Songs/testNoMetadata.mp3", "-")]
    public void GetArtist(string path, string expectedArtist)
    {
        var actualArtist = MetadataExtractor.GetArtist(path);
        Assert.Equal(expectedArtist, actualArtist);
    }

    [Theory]
    [InlineData("../../../../Songs/testMetadata.mp3", "TestAlbum")]
    [InlineData("../../../../Songs/testNoMetadata.mp3", "-")]
    public void GetAlbum(string path, string expectedAlbum)
    {
        var actualAlbum = MetadataExtractor.GetAlbum(path);
        Assert.Equal(expectedAlbum, actualAlbum);
    }

    [Theory]
    [InlineData("../../../../Songs/testMetadata.mp3", "TestGenre")]
    [InlineData("../../../../Songs/testNoMetadata.mp3", "-")]
    public void GetGenre(string path, string expectedGenre)
    {
        var actualGenre = MetadataExtractor.GetGenre(path);
        Assert.Equal(expectedGenre, actualGenre);
    }

    [Theory]
    [InlineData("../../../../Songs/testMetadata.mp3", "2022")]
    [InlineData("../../../../Songs/testNoMetadata.mp3", "-")]
    public void GetYear(string path, string expectedYear)
    {
        var actualYear = MetadataExtractor.GetYear(path);
        Assert.Equal(expectedYear, actualYear);
    }
}
