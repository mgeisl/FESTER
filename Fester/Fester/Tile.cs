using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Totem.Engine;
using Totem.Common;
using Totem.Physics;
using Totem.Utilities;
using Totem.Video;
using Totem.Input;


namespace Fester
{
    class Tile
    {
        public Tile(Engine engine)
        {
            sprite = engine.Display.Scene.CreateSprite("Images/tile1.png");
            sprite.Order = 1;
            body = engine.Physics.World.CreateRectangularBody(sprite, 1.0f);
            body.IsStatic = true;
        }
        private Body body;
        private Sprite sprite;
        public Body Body
        {
            get { return body; }
        }

        public Sprite Sprite
        {
            get { return sprite; }
        }
    }
}
