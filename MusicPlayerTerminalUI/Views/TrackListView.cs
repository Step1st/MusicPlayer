using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace MusicPlayer.Views;
internal class TrackListView
{
    public static FrameView TrackList
    {
        get; private set;
    }

    private static List<string> tracks = new();

    static TrackListView()
    {
        TrackList = new FrameView("Track List")
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Percent(70f)
        };

        var list = new ListView(new List<string>() { "Track 1", "Track 2", "Track 3" })
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };

        TrackList.Add(list);
    }
}
