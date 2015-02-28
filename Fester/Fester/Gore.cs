using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Totem.Video;
using Totem.Engine;
using Totem.Input; 
using Totem.Utilities;
using Totem.Common;
using Totem.Physics;

namespace Fester
{
    class Gore
    { 
        private Body body;
        private Sprite sprite;
        private float goreTimer;
        
        public Body Body
        {
            get { return body; }
        }

        public float GoreTimer
        {
            get { return goreTimer; }
            set { goreTimer = value; }
        }

        public void Destroy()
        {
            body.Destroy();
            sprite.Destroy();
        }
        public Sprite Sprite
        {
            get { return sprite; }
        }

        public Gore(Engine engine, string filename, Boolean isRectangular, float weight)
        {
            if (isRectangular == true)
            {
                sprite = engine.Display.Scene.CreateSprite(filename);
                sprite.Order = 8;
                body = engine.Physics.World.CreateRectangularBody(sprite, weight);
            }
            else
            {
                sprite = engine.Display.Scene.CreateSprite(filename);
                sprite.Order = 8;
                body = engine.Physics.World.CreateCircularBody(sprite, weight,10);
            }
            goreTimer = 0.0f; 

        }
        
    }
}
