using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terminal.Gui;

namespace MusicPlayer.Views;
internal static class PlaybackView
{
    public static FrameView Playback
    {
        get; private set;
    }

    static PlaybackView()
    {
        Playback = new FrameView("Playback")
        {
            X = 0,
            Y = Pos.Percent(70f),
            Width = Dim.Percent(50f),
            Height = Dim.Percent(32f)
        };
    }
}
