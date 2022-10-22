using Terminal.Gui;

namespace MusicPlayerTerminalUI;

internal class Program
{
    private static void Main(string[] args)
    {
        Application.Init();
        var top = Application.Top;
        top.ColorScheme = Colors.Base;
        //var topwindow = new Window 
        //{
        //    X = 0,
        //    Y = 1, // Leave one row for the toplevel menu

        //    // By using Dim.Fill(), it will automatically resize without manual intervention
        //    Width = Dim.Fill(),
        //    Height = Dim.Fill()
        //};

        var TrackList = new FrameView("Tracks")
        {
            X = 0,
            Y = 1, // Leave one row for the toplevel menu

            // By using Dim.Fill(), it will automatically resize without manual intervention
            Width = Dim.Fill(),
            Height = Dim.Percent(68f)
        };

        var controls = new FrameView("Playback")
        {
            X = 0,
            Y = Pos.Percent(70f),
            Width = Dim.Percent(50f),
            Height = Dim.Percent(30f)
        };

        var playingInfo = new FrameView("Playing")
        {
            X = Pos.Percent(50f),
            Y = Pos.Percent(70f),
            Width = Dim.Percent(50f),
            Height = Dim.Percent(30f)
        };

        //topwindow.Add(TrackList, playingInfo, controls);

        top.Add(TrackList, playingInfo, controls);

        // Run the application
        Application.Run();
    }
}
