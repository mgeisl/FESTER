using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Totem.Engine;
using System.IO;
using Totem.Common;
using Totem.Input;
using Totem.Video;
using Totem.Physics;
using Totem.Utilities;

namespace Fester
{
    class MainState : GameState
    {
        private int playerStartX;
        private int playerStartY;
        private Label healthLabel;
        int mapWidth;
        int mapHeight;
        int tile_type;
        int playerKills = 0;
        int playerKeys = 0;
        int enem_num;
        int key_num;
        Player player;
        Boolean playerDead = false;
        Body lvlExit;
        List<Tile> tiles = new List<Tile>();
        List<Sprite> spikeList = new List<Sprite>();
        List<Zombie> zombies = new List<Zombie>();
        List<Sprite> zombieSpawns = new List<Sprite>();
        public int levelCount = 0;
        private Scene scene;
        List<Gore> guts = new List<Gore>();
        public override void Enter()
        {
            scene = Engine.Display.CreateScene();
            Engine.Physics.CreateWorld(new Vector(0.0f, 200.0f));
            scene.Width = 320;
            scene.Height = 240;
            LoadLevel();
        }
        public void LoadLevel()
        {
            healthLabel = scene.CreateLabel("HP:100%", new FontDescription("Courier" , 16),new Color(242,46,46));
            healthLabel.IsHud = true;
           // BinaryReader reader = new BinaryReader(File.OpenRead("Level" + levelCount.ToString() + ".fm"));
            BinaryReader reader = new BinaryReader(File.OpenRead("testlevel1again.fm"));
            mapWidth = reader.ReadInt32();
            mapHeight = reader.ReadInt32();
            tile_type = reader.ReadInt32();
            for (int i = 0; i < mapWidth / 20; i++)
            {
                for (int j = 0; j < mapHeight / 20; j++)
                {
                    if (reader.ReadBoolean())
                    {
                        var tile = new Tile(Engine);
                        tile.Body.Position = new Vector((i * 20), (j * 20));
                        tiles.Add(tile);
                        tile.Body.Friction = 1.2f;
                    }
                }
            }
            int zombieSpawnCount = reader.ReadInt32();
            int zombieSpawnType = reader.ReadInt32();
            for (int i = 0; i < zombieSpawnCount; i++)
            {
                int zSpawnX = reader.ReadInt32();
                int zSpawnY = reader.ReadInt32();
                Sprite zombieSpawn = null;
                zombieSpawn.Position = new Vector(zSpawnX, zSpawnY);
                zombieSpawns.Add(zombieSpawn);
                switch (zombieSpawnType)
                {
                    case 1:
                        zombieSpawn = Engine.Display.Scene.CreateSprite("ZombieSpawnDoor.xml");
                        break;
                    case 2:
                        zombieSpawn = Engine.Display.Scene.CreateSprite("Images/ZombieSpawn.png");
                        break;
                }
            }
           
            int ZombieCount = reader.ReadInt32();
            for (int i = 0; i < ZombieCount; i++)
            {
                Zombie zombie = new Zombie(Engine);
                int x = reader.ReadInt32();
                int y = reader.ReadInt32();
                 zombie.Body.Position = new Vector(x, y);
                 zombies.Add(zombie);

            }
            int keyCount = reader.ReadInt32();
            for (int i = 0; i < keyCount; i++)
            {
                int x = reader.ReadInt32();
                int y = reader.ReadInt32();
                var key = scene.CreateSprite("Images/KeyPickup.png");
                key.Position = new Vector(x, y);
            }
            var x1 = reader.ReadInt32();
            var y1 = reader.ReadInt32();
            playerStartX = x1;
            playerStartY = y1;
            player = new Player(Engine);
            player.Body.Position = new Vector(x1, y1);
            player.Body.Bounce = 0.0f;

            int door_Type = reader.ReadInt32();  //reads in whether the door is open, 
            key_num = reader.ReadInt32();
            enem_num = reader.ReadInt32();
            x1 = reader.ReadInt32();
            y1 = reader.ReadInt32();
            Sprite exit = null;
            switch (door_Type)
            {
                case 1:
                    exit = scene.CreateSprite("Images/DoorOpen.png");
                    exit.Position = new Vector(x1, y1);
                    break;
                case 2:
                    exit = scene.CreateSprite("Images/KeyDoorClosed.png");
                    exit.Position = new Vector(x1, y1);
                    break;
                case 3:
                    exit = scene.CreateSprite("Images/EnemyDoorClosed.png");
                    exit.Position = new Vector(x1, y1);
                    break;
            }

            int weaponCount = reader.ReadInt32();
            for (int i = 0; i < weaponCount; i++)
            {
                int x = reader.ReadInt32();
                int y = reader.ReadInt32();

                int weaponNumber = RandomUtility.Integer(1,2);
                Sprite weapon = null;
                switch (weaponNumber)
                {
                    case 1:
                        weapon = scene.CreateSprite("Images/CrowbarPickup.png");
                        break;
                    case 2:
                        weapon = scene.CreateSprite("Images/AxePickup.png");
                        break;
                    case 3:
                        break;
                }
                weapon.Position = new Vector(x, y);
            }
            String backgroundImage = reader.ReadString();
            Sprite background = Engine.Display.Scene.CreateSprite(backgroundImage);
            background.Position = new Vector(0, 0);
        }

        public void NextLevel()
        {
            levelCount++;
            Engine.Physics.World.Destroy();
            Engine.Display.Scene.Destroy();
            scene = Engine.Display.Scene = Engine.Display.CreateScene();
            scene.Width = 320;
            scene.Height = 240;
            tiles.Clear();
            spikeList.Clear();
            
            Engine.Physics.World = Engine.Physics.CreateWorld(new Vector(0.0f, 200.0f));
            LoadLevel();
        }

        public override void Update()
        {
            int health = (int)player.Health;
            healthLabel.Text = "HP: " + health.ToString() + "%";
            float playX = player.Body.Position.X;
            float playY = player.Body.Position.Y;
            if (playX < 0)
            {
                playX = 0.0f;
            }
            if (playX > mapWidth)
            {
                playX = mapWidth;
            }
            if (playY > mapHeight)
            {
                playY = mapHeight;
            }
            if (playY < 0)
            {
                playY = 0;
            }
            player.Body.Position = new Vector(playX, playY);

            if ((player.Body.Bounds.Intersects(lvlExit.Bounds)) && playerDead == false)
            {
                if ((playerKills >= enem_num) && (playerKeys >= key_num))
                {
                    NextLevel();
                }
            }
            if (player.Health <= 0 && playerDead == false)
            {
                playerDead = true;
                    for (int i = 0; i < 60; i++)
                    {
                        Gore blood = new Gore(Engine, "Images/bloodParticle.png", false, .2f);
                        blood.Body.Position = new Vector(player.Body.Position.X + RandomUtility.Integer(5, 10), player.Body.Position.Y + RandomUtility.Integer(5, 10));
                        blood.Body.ApplyImpulse(new Vector(RandomUtility.Decimal(-5.0f, 5.0f), RandomUtility.Decimal(-10.0f, -50.0f)));
                        guts.Add(blood);
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        Gore gib = new Gore(Engine, "Images/gibletParticle.png", false, .50f);
                        gib.Body.Position = new Vector(player.Body.Position.X + RandomUtility.Integer(5, 10), player.Body.Position.Y + RandomUtility.Integer(5, 10));
                        gib.Body.ApplyImpulse(new Vector(RandomUtility.Decimal(-5.0f, 5.0f), RandomUtility.Decimal(-10.0f, -50.0f)));
                        guts.Add(gib);
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        Gore bone = new Gore(Engine, "Images/boneParticle.png", true, .75f);
                        bone.Body.Position = new Vector(player.Body.Position.X + RandomUtility.Integer(5, 10), player.Body.Position.Y + RandomUtility.Integer(5, 10));
                        bone.Body.ApplyImpulse(new Vector(RandomUtility.Decimal(-5.0f, 5.0f), RandomUtility.Decimal(-30.0f, -70.0f)));
                        guts.Add(bone);
                    }

            }
            foreach (Gore g in guts)
            {
                g.GoreTimer += Engine.FrameTimeMillis;
            }

            for (int i = 0; i < guts.Count; i++)
            {
                Gore g = guts[i];
                if (g.GoreTimer > 3500.0f)
                {
                    g.Destroy();
                    guts.RemoveAt(i);
                }
            }
            if (playerDead == false)
            {
                player.Sprite.IsHidden = false;
                Engine.Display.Scene.Camera = player.Body.Position - new Vector((Engine.Display.Scene.Width / 2), (Engine.Display.Scene.Height / 2));
                float tempx = Engine.Display.Scene.Camera.X;
                float tempy = Engine.Display.Scene.Camera.Y;
                if (tempx < 0)
                {
                    tempx = 0;
                }
                else if (tempx > 320)
                {
                    tempx = 320;
                }
                if (tempy < 0)
                {
                    tempy = 0;
                }
                else if (tempy > 240)
                {
                    tempy = 240;
                }
                Engine.Display.Scene.Camera = new Vector(tempx, tempy);
            }
            else if (playerDead == true)
            {
                Engine.Display.Scene.Camera = new Vector(scene.Camera.X, scene.Camera.Y);
                player.Sprite.IsHidden = true;
                player.Body.LinearVelocity = new Vector(0.0f, 0.0f);
                player.Body.Position = new Vector(0.0f, 0.0f);
            }
            foreach (Zombie z in zombies)
            {
                z.Update(Engine, player);
                if (player.Sprite.Bounds.Intersects(z.Sprite.Bounds))
                {
                    if (z.Sprite.AnimationName == "WalkLeft")
                    {
                        z.Sprite.AnimationName = "thrashLeft";
                    }
                    if (z.Sprite.AnimationName == "WalkRight")
                    {
                        z.Sprite.AnimationName = "thrashRight"; 
                    }
                    player.Health -= Engine.FrameTimeSeconds * 8;
                }
            }
            foreach (Sprite s in spikeList)
            {
                if (player.Sprite.Bounds.Intersects(s.Bounds))
                {
                    playerDead = true;
                    for (int i = 0; i < 60; i++)
                    {
                        Gore blood = new Gore(Engine, "Images/bloodParticle.png", false, .2f);
                        blood.Body.Position = new Vector(player.Body.Position.X + RandomUtility.Integer(5, 10), player.Body.Position.Y + RandomUtility.Integer(5, 10));
                        blood.Body.ApplyImpulse(new Vector(RandomUtility.Decimal(-5.0f, 5.0f), RandomUtility.Decimal(-10.0f, -50.0f)));
                        guts.Add(blood);
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        Gore gib = new Gore(Engine, "Images/gibletParticle.png", false, .50f);
                        gib.Body.Position = new Vector(player.Body.Position.X + RandomUtility.Integer(5, 10), player.Body.Position.Y + RandomUtility.Integer(5, 10));
                        gib.Body.ApplyImpulse(new Vector(RandomUtility.Decimal(-5.0f, 5.0f), RandomUtility.Decimal(-10.0f, -50.0f)));
                        guts.Add(gib);
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        Gore bone = new Gore(Engine, "Images/boneParticle.png", true, .75f);
                        bone.Body.Position = new Vector(player.Body.Position.X + RandomUtility.Integer(5, 10), player.Body.Position.Y + RandomUtility.Integer(5, 10));
                        bone.Body.ApplyImpulse(new Vector(RandomUtility.Decimal(-5.0f, 5.0f), RandomUtility.Decimal(-30.0f, -70.0f)));
                        guts.Add(bone);
                    }
                }
            }
            
            if (Engine.Keyboard.IsKeyPressed(Keys.D))
            {
                player.Body.LinearVelocity = new Vector(50.0f, player.Body.LinearVelocity.Y);
                player.Body.Rotation = 0.0f;
                player.Sprite.AnimationName = "WalkRight";
            }
            else if (Engine.Keyboard.IsKeyPressed(Keys.A))
            {
                player.Body.LinearVelocity = new Vector(-50.0f, player.Body.LinearVelocity.Y);
                player.Body.Rotation = 0.0f;
                player.Sprite.AnimationName = "WalkLeft";
            }
            else
            {
                player.Sprite.FrameIndex = 0;
                player.Body.Rotation = 0.0f;
            }

            if (Engine.Keyboard.WasKeyPressed(Keys.W))
            {
                if (player.Body.TouchBottom)
                {
                    player.Body.LinearVelocity = new Vector(player.Body.LinearVelocity.X, -200.0f);
                    player.Body.Rotation = 0.0f;
                }

            }
            if (Engine.Keyboard.WasKeyPressed(Keys.R))
            {
                player.Body.Position = new Vector(playerStartX, playerStartY);
                playerDead = false;
                player.Health = 100.0f;
                Engine.Display.Scene.Camera = player.Body.Position - new Vector((Engine.Display.Scene.Width / 2), (Engine.Display.Scene.Height / 2));
            }
            if (Engine.Keyboard.WasKeyPressed(Keys.Escape))
            {
                Engine.PopState();
            }

        }
        public override void Exit()
        {

        }
    }
}