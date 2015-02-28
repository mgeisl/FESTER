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
    class Player
    {
        public Player(Engine engine)
        {
            sprite = engine.Display.Scene.CreateSprite("PlayerSprite.xml");
            sprite.Order = 10;
            sprite.AnimationName = "WalkRight";
            body = engine.Physics.World.CreateRectangularBody(sprite, 3.0f);   
        }

        //public override void Update()
        //{
        //}
        private Body body;
        private Sprite sprite;
        private float health = 100.0f;
        public Body Body
        {
            get { return body; }
        }

        public Sprite Sprite
        {
            get { return sprite; }
        }
        public float Health
        {
            get { return health; }
            set { health = value; }
        }

    }
}
