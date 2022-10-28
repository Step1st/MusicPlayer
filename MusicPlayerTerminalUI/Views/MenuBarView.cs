using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terminal.Gui;

namespace MusicPlayer.Views;

internal class MenuBarView
{
    public MenuBar MenuBar { get; private set; }

    private MenuBarView()
    {
        MenuBar = new(new MenuBarItem[]
        {
            new MenuBarItem("_File", new MenuItem[]
            {
                new MenuItem("_Open", "Open a music file", () => Console.Write("")),

                new MenuItem("Open Pla_ylist", "Load a playlist", () => Console.Write("")),

                new MenuItem("_Quit", "Exit Music Player", () => Application.RequestStop()),
            }),

            new MenuBarItem("_Help", new MenuItem[]
            {
                new MenuItem("_About", "", () => Console.Write("")),
            })
        });
    }

    public static MenuBarView GetMenuBarViewInstance { get; } = new();
}
