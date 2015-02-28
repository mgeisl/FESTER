using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Totem.Engine;
using Totem.Video;
using Totem.Common;
using Totem.Input;
using Totem.Audio;

namespace Fester
{
    class MenuState : GameState
    {
        private Sprite BackGround;
        private Sprite startButton;
        private Sprite helpButton; 
        private Sprite exitButton;
        private Sound music;

        public override void Enter()
        { 
            music = Engine.Mixer.CreateSound("Dark Techno (Game Mix).wav");
            music.Play();
            var scene = Engine.Display.CreateScene();
            scene.Width = 320;
            scene.Height = 240;
            BackGround = scene.CreateSprite("Images/MenuBackground.png");
            BackGround.Order = -10;
            startButton = scene.CreateSprite("StartButton.xml");   //declares startbutton animation
            startButton.Position = new Vector(110, 50);
            helpButton = scene.CreateSprite("HelpButton.xml");     //declares helpbutton animation //the x coordinates are modified slightly because the image is off
            helpButton.Position = new Vector(107, 105);
            exitButton = scene.CreateSprite("ExitButton.xml");     //declares exitbutton animation
            exitButton.Position = new Vector(110, 160);
        }
        public override void Update()
        {
            int mousex = (int)(Engine.Mouse.Position.X * (Engine.Display.Scene.Width / (float)Engine.Display.Mode.Width));
            int mousey = (int)(Engine.Mouse.Position.Y * (Engine.Display.Scene.Height / (float)Engine.Display.Mode.Height));
            
            startButton.FrameIndex = 0;
            if (startButton.Bounds.Intersects(new Rectangle(mousex, mousey, 1, 1)))
            {
                startButton.FrameIndex = 1;
                if (Engine.Mouse.LeftDown)
                {
                    Engine.PushState(new MainState()); 
                    music.Stop();
                }
            }
            helpButton.FrameIndex = 0;
            if (helpButton.Bounds.Intersects(new Rectangle(mousex, mousey, 1, 1)))
            {
                helpButton.FrameIndex = 1;
                if (Engine.Mouse.LeftDown)
                {
                    //Engine.PushState(new HelpState());
                }
            }
            exitButton.FrameIndex = 0;
            if (exitButton.Bounds.Intersects(new Rectangle(mousex, mousey, 1, 1)))
            {
                exitButton.FrameIndex = 1;
                if (Engine.Mouse.LeftDown)
                {
                    music.Stop();
                    Engine.PopState();
                }
            }
        }
        public override void Exit()
        {
            
        }
    }
}
