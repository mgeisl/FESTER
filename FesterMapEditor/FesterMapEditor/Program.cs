using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Totem.Video;
using Totem.Engine;
using Totem.Common;
using Totem.Input;
using Totem.Physics;
using Totem.Utilities;

namespace FesterMapEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            StartupDialog sd = new StartupDialog();

            if (sd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Engine engine = new Engine();
            FesterCraft fc = new FesterCraft();
            fc.LevelWidth = sd.LevelWidth;
            fc.LevelHeight = sd.LevelHeight;
            engine.Run(fc);

        }
    }
}
