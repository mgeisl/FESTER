using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Totem.Audio;
using Totem.Common;
using Totem.Engine;
using Totem.Input;
using Totem.Physics;
using Totem.Utilities;
using Totem.Video;

namespace Fester
{
    class Zombie
    {
        private bool isAngry = false;
        public Zombie(Engine engine)
        {
            sprite = engine.Display.Scene.CreateSprite("ZombieWalk.xml");
            sprite.Order = 10;
            sprite.AnimationName = "WalkRight";
            body = engine.Physics.World.CreateRectangularBody(sprite, 3.0f);
            body.Friction = 0.0001f;
        }
        public void Update(Engine engine, Player player)
        {
            float distance = (player.Body.Position - body.Position).Length;
            if (distance > 200.0f)
            {
                isAngry = true;
            }
            if (body.Bounds.Intersects(player.Body.Bounds))
            {
                //thrash animation
                //decrease in health over a certain time period
            }
            if (isAngry == true)
            {
                if (player.Body.Position.X < body.Position.X)
                {
                    body.LinearVelocity = new Vector(-20.0f, body.LinearVelocity.Y);
                }
                if (player.Body.Position.X > body.Position.X)
                {
                    body.LinearVelocity = new Vector(20.0f, body.LinearVelocity.Y);
                }
            }
            else
            {
                int value = RandomUtility.Integer(0, 100);
                if (value == 25)
                {
                    body.LinearVelocity = new Vector(25.0f, 0.0f);
                }
                else if (value == 50)
                {
                    body.LinearVelocity = new Vector(-25.0f, 0.0f);
                }
            }
            if (body.LinearVelocity.X < 0.1)
            {
                sprite.AnimationName = "WalkRight";
            }
            if (body.LinearVelocity.X > 0.1)
            {
                sprite.AnimationName = "WalkLeft";
            }
            body.Rotation = 0.0f;
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
