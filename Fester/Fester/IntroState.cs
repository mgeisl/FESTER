using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Totem.Engine;
using Totem.Video;
using Totem.Input;
using Totem.Common;

namespace Fester
{
    class IntroState : GameState
    {
        private Sprite Background;
        private float opacity;
        private float fade;
        public override void Enter()
        {
            var scene = Engine.Display.CreateScene();
            scene.Width = 320;
            scene.Height = 240;

            Background = scene.CreateSprite("Images/IntroScreen.png");
            opacity = 0.0f;
            fade = 75.0f;
            Engine.Display.Title = "Fester v1.0";
        }
        public override void Update()
        {
            opacity += fade * Engine.FrameTimeSeconds;
            Background.Color = new Color(255, 255, 255,(byte)opacity);
            if (opacity > 255)
                fade = -fade;
            if (opacity < 0 || Engine.Keyboard.WasKeyPressed(Keys.Space))
            {
                Engine.PopState();
                Engine.PushState(new MenuState());
                }
        }
        public override void Exit()
        {
            
        }

    }
}
