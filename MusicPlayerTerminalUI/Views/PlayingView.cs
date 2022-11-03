﻿using System;
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
        };

        var label = new Label("Track Name")
        {
            X = 0,
            Y = Pos.Percent(20f),
            Width = Dim.Fill(),
            Height = 1
        };

        Playing.Add(label);

        var progress = new ProgressBar()
        {
            X = 0,
            Y = Pos.Percent(75f) - 1,
            Width = Dim.Fill() - 13,
            Height = 1,
            ProgressBarFormat = ProgressBarFormat.Framed
        };

        Playing.Add(progress);

        var timelabel = new Label("00:00/00:00")
        {
            X = Pos.Right(progress) + 1,
            Y = Pos.Percent(75f),
            Width = Dim.Percent(50f),
            Height = 1
        };

        Playing.Add(timelabel);
    }

}