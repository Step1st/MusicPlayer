using Terminal.Gui;

namespace MusicPlayerTerminalUI;

internal class Program
{
    private static void Main(string[] args)
    {
        Application.Init();
        var top = Application.Top;
        //top.ColorScheme = Colors.Base;
        var topwindow = new Window
        {
            X = 0,
            Y = 1, // Leave one row for the toplevel menu

            // By using Dim.Fill(), it will automatically resize without manual intervention
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            Border = new Border()
        };

        var TrackList = new FrameView("Tracks")
        {
            X = 0,
            Y = 0, // Leave one row for the toplevel menu

            // By using Dim.Fill(), it will automatically resize without manual intervention
            Width = Dim.Fill(),
            Height = Dim.Percent(70f)
        };

        var controls = new FrameView("Playback")
        {
            X = 0,
            Y = Pos.Percent(70f),
            Width = Dim.Percent(50f),
            Height = Dim.Percent(32f)
        };

        var playingInfo = new FrameView("Playing")
        {
            X = Pos.Percent(50f),
            Y = Pos.Percent(70f),
            Width = Dim.Percent(50f),
            Height = Dim.Percent(32f)
        };

        var menu = new MenuBar(new MenuBarItem[]
{
            new MenuBarItem("_File", new MenuItem[]
            {
                new MenuItem("_Open", "Open a music file", () => Console.Write("")),

                new MenuItem("Open Pla_ylist", "Load a playlist", () => Console.Write("")),

                new MenuItem("_Quit", "Exit Music Player", () => Application.RequestStop()),
            }),

            new MenuBarItem("_Help", new MenuItem[]
            {
                new MenuItem("_About Music Player", string.Empty, () =>
                {
                    MessageBox.Query("Music Player", "Bruh", "Close");
                }),
            }),
});

        topwindow.Add(TrackList, playingInfo, controls);

        top.Add(topwindow, menu);

        // Run the application
        Application.Run();
    }
}
