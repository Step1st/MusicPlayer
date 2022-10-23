using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terminal.Gui;

namespace MusicPlayer.Views;
internal static class PlayingView
{
    public static FrameView Playing
    {
        get; private set;
    }

    static PlayingView()
    {
        Playing = new FrameView("Playing")
        {
            X = Pos.Percent(50f),
            Y = Pos.Percent(70f),
            Width = Dim.Percent(50f),
            Height = Dim.Percent(32f)
        }; ;
    }

}
