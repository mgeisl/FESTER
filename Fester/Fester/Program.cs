using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Totem.Engine;
using Totem.Video;

namespace Fester
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var engine = new Engine();
            engine.Display.Mode = new DisplayMode(800, 600, 0, false, true);
            engine.Run(new IntroState());
        }
    }
}
