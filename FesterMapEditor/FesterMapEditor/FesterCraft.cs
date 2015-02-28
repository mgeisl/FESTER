using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Totem.Engine;
using Totem.Input;
using Totem.Common;
using Totem.Physics;
using Totem.Video;
using Totem.Utilities;
using Totem.Xml;
using System.Windows.Forms;
using System.IO;

namespace FesterMapEditor
{
    class FesterCraft : GameState
    {
        private Sprite player;
        private Sprite doorPosition;
        private int[,] tiles;
        private Sprite[,] tileSprites;
        private List<Sprite> keys;
        private List<Sprite> zombieSpawns;
        private List<Sprite> zombieEnemies;
        private List<Sprite> weapons;
        private int doorType;
        private int keyRequired;
        private int enemyRequired;
        private String backGroundImage;
        private Sprite background;
        private int levelWidth;
        private int levelHeight;
        private int tileType;
        private int zombieSpawnType;
        private int mode;


        public int LevelWidth
        {
            get { return levelWidth; }
            set { levelWidth = value; }
        }
        public int LevelHeight
        {
            get { return levelHeight; }
            set { levelHeight = value; }
        }

        public String BackGroundImage
        {
            get { return backGroundImage; }
            set { backGroundImage = value; }
        }

        public void OpenLevel(string file)
        {
            BinaryReader reader = new BinaryReader(File.OpenRead(file));
            levelWidth = reader.ReadInt32();
            levelHeight = reader.ReadInt32();
            tileType = reader.ReadInt32();

            for (int i = 0; i < levelWidth / 20; i++)
            {
                for (int j = 0; j < levelHeight / 20; j++)
                {
                    tiles[i, j] = reader.ReadInt32();
                }
            }
            var zombieSpawnCount = reader.ReadInt32();
            for (int i = 0; i < zombieSpawnCount; i++)
            {
                int zSpawnX = reader.ReadInt32();
                int zSpawnY = reader.ReadInt32();
            }

            int zombieCount = reader.ReadInt32();
            for (int i = 0; i < zombieCount; i++)
            {
                int zombieX = reader.ReadInt32();
                int zombieY = reader.ReadInt32();
                Sprite zombieSprite = Engine.Display.Scene.CreateSprite("Images/Zombie2.png");
                zombieSprite.Position = new Vector(zombieX, zombieY);
            }

            int keyPickups = reader.ReadInt32();
            for (int i = 0; i < keyPickups; i++)
            {
                int keyPickupX = reader.ReadInt32();
                int keyPickupY = reader.ReadInt32();
                Sprite keyPickup = Engine.Display.Scene.CreateSprite("Images/KeyPickup.png");
                keyPickup.Position = new Vector(keyPickupX, keyPickupY);
            }

            int PlayerStartX = reader.ReadInt32();
            int PlayerStartY = reader.ReadInt32();
            player.Position = new Vector(PlayerStartX, PlayerStartY);

            doorType = reader.ReadInt32();
            keyRequired = reader.ReadInt32();
            enemyRequired = reader.ReadInt32();
            float exitX = reader.ReadSingle();
            float exitY = reader.ReadSingle();
            switch (doorType)
            {
                case 1:
                    doorPosition = Engine.Display.Scene.CreateSprite("Images/DoorClosed.png");
                    doorPosition.Position = new Vector(exitX, exitY);
                    break;
                case 2:
                    doorPosition = Engine.Display.Scene.CreateSprite("Images/KeyDoorClosed.png");
                    doorPosition.Position = new Vector(exitX, exitY);
                    break;
                case 3:
                    doorPosition = Engine.Display.Scene.CreateSprite("Images/EnemyDoorClosed.png");
                    doorPosition.Position = new Vector(exitX, exitY);
                    break;
            }

            int weapons = reader.ReadInt32();
            for (int i = 0; i < weapons; i++)
            {
                int weaponX = reader.ReadInt32();
                int weaponY = reader.ReadInt32();
                Sprite weaponSprite = Engine.Display.Scene.CreateSprite("Images/AxePickup.png");
                weaponSprite.Position = new Vector(weaponX, weaponY);
            }

            String backgroundImage = reader.ReadString();
            Sprite background = Engine.Display.Scene.CreateSprite(backGroundImage);
            reader.Close();
        }
        private void OpenLevel()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            OpenLevel(dialog.FileName);
        }
        private void SaveLevel()
        {
            SaveFileDialog dialog = new SaveFileDialog();

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            BinaryWriter writer = new BinaryWriter(File.OpenWrite(dialog.FileName));

            writer.Write(levelWidth);
            writer.Write(levelHeight);
            writer.Write(tileType);
            for (int i = 0; i < levelWidth / 20; i++ )
            {
                for (int j = 0; j < levelHeight / 20; j++)
                {
                    writer.Write(tiles[i, j]);
                }
            }
            writer.Write(zombieSpawns.Count);
            writer.Write(zombieSpawnType);
            foreach (Sprite z in zombieSpawns)
            {
                writer.Write(z.Position.X);
                writer.Write(z.Position.Y);
            }

            writer.Write(zombieEnemies.Count);
            foreach (Sprite z in zombieEnemies)
            {
                writer.Write(z.Position.X);
                writer.Write(z.Position.Y);
            }
            writer.Write(keys.Count);
            foreach (Sprite k in keys)
            {
                writer.Write(k.Position.X);
                writer.Write(k.Position.Y);
            }
            writer.Write(player.Position.X);
            writer.Write(player.Position.Y);

            writer.Write(doorType);
            writer.Write(keyRequired); 
            writer.Write(enemyRequired);
            writer.Write(doorPosition.Position.X);
            writer.Write(doorPosition.Position.Y);

            writer.Write(weapons.Count);
            foreach (Sprite w in weapons)
            {
                writer.Write(w.Position.X);
                writer.Write(w.Position.Y);
            }

            writer.Write(backGroundImage);
            writer.Close();
        }

        public override void Enter()
        {
            Engine.Display.Mode = new DisplayMode(960, 720, 0, false, true);

            Engine.Display.CreateScene();
            Engine.Display.Scene.Width = 320;
            Engine.Display.Scene.Height = 240;

            tiles = new int[(levelWidth / 20), (levelHeight / 20)];
            tileSprites = new Sprite[(levelWidth / 20), (levelHeight / 20)];
            for (int i = 0; i < levelWidth / 20; i++)
            {
                for (int j = 0; j < levelHeight / 20; j++)
                {
                    tiles[i, j] = -1;
                    tileSprites[i, j] = Engine.Display.Scene.CreateSprite("Tiles.xml");
                    tileSprites[i, j].Position = new Vector(i * 20, j * 20);
                }
            }

            keys = new List<Sprite>();
            zombieSpawns = new List<Sprite>();
            zombieEnemies = new List<Sprite>();
            weapons = new List<Sprite>();
        }

        public override void Update()
        {
            for (int i = 0; i < levelWidth / 20; i++)
            {
                for(int j = 0; j < levelHeight / 20; j++)
                {
                    if (tiles[i,j] >= 0)
                    {
                        tileSprites[i,j].IsHidden = false;
                        tileSprites[i,j].FrameIndex = tiles[i,j];
                    }
                    else
                    {
                        tileSprites[i,j].IsHidden = true;
                    }
                }
            }
            if (Engine.Mouse.LeftDown)
            {
                foreach (Sprite key in keys)
                {
                    if (key.Bounds.Intersects(new Rectangle(Engine.Mouse.WorldPosition, 1, 1)))
                    {
                        key.Position += Engine.Mouse.WorldVelocity;
                    }
                }
                foreach (Sprite zombieSpawnSprite in zombieSpawns)
                {
                 if (zombieSpawnSprite.Bounds.Intersects(new Rectangle(Engine.Mouse.WorldPosition, 1, 1)))
                 {
                     zombieSpawnSprite.Position += Engine.Mouse.WorldVelocity;
                 }
                }
                foreach (Sprite zombieSprite in zombieEnemies)
                {
                 if (zombieSprite.Bounds.Intersects(new Rectangle(Engine.Mouse.WorldPosition, 1, 1)))
                 {
                     zombieSprite.Position += Engine.Mouse.WorldVelocity;
                 }
                }
            
                foreach (Sprite weaponSprite in weapons)
                {
                    if (weaponSprite.Bounds.Intersects(new Rectangle(Engine.Mouse.WorldPosition, 1, 1)))
                    {
                        weaponSprite.Position += Engine.Mouse.WorldVelocity;
                    }
                }
                if (player.Bounds.Intersects(new Rectangle(Engine.Mouse.WorldPosition, 1, 1)))
                {
                    player.Position += Engine.Mouse.WorldVelocity;
                }
                if (doorPosition.Bounds.Intersects(new Rectangle(Engine.Mouse.WorldPosition, 1, 1)))
                {
                    doorPosition.Position += Engine.Mouse.WorldVelocity;
                }

            }

            if (Engine.Mouse.RightDown)
            {
                Engine.Display.Scene.Camera -= Engine.Mouse.WorldVelocity;
                Vector Camera = Engine.Display.Scene.Camera - Engine.Mouse.WorldVelocity;
                if (Camera.X < 0)
                {
                    Camera.X = 0;
                }
                if (Camera.X > levelWidth - Engine.Display.Scene.Width)
                {
                    Camera.X = levelWidth - Engine.Display.Scene.Width;
                }
                if (Camera.Y < 0)
                {
                    Camera.Y = 0;
                }
                if (Camera.Y > levelHeight - Engine.Display.Scene.Height)
                {
                    Camera.Y = levelHeight - Engine.Display.Scene.Height;
                }
                Engine.Display.Scene.Camera = Camera;

            }

            if(Engine.Keyboard.WasKeyPressed(Totem.Input.Keys.Space))
            {
                switch(mode)
                {
                    case 1:
                        int x = (int)Engine.Mouse.WorldPosition.X / 20;
                        int y = (int)Engine.Mouse.WorldPosition.Y / 20;
                        tiles[x,y] = tileType;
                        break;
                    case 2:
                        var keySprite = Engine.Display.Scene.CreateSprite("Images/KeyPickup.png");
                        keySprite.Position = Engine.Mouse.WorldPosition;
                        keys.Add(keySprite);
                        break;
                    case 3:
                        if (player != null)
                        {
                            player.Destroy();
                        }
                        player = Engine.Display.Scene.CreateSprite("Images/PlayerSprite.png", 20, 20);
                        player.Position = Engine.Mouse.WorldPosition;
                        
                        break;
                    case 4:
                        Sprite zombieSpawnSprite = null; 
                        if (zombieSpawnType == 1)
                        {
                            zombieSpawnSprite = Engine.Display.Scene.CreateSprite("ZombieSpawnDoor.xml");
                        }
                        if (zombieSpawnType == 2)
                        {
                            zombieSpawnSprite = Engine.Display.Scene.CreateSprite("Images/ZombieSpawn.png");
                        }
                        zombieSpawnSprite.Position = Engine.Mouse.WorldPosition;
                        zombieSpawns.Add(zombieSpawnSprite);
                        break;
                    case 5:
                        doorPosition.Position = Engine.Mouse.WorldPosition;
                        break;
                    case 6:
                        OpenLevel();
                        break;
                    case 7:
                        var zombieSprite = Engine.Display.Scene.CreateSprite("Images/Zombie2.png");
                        zombieSprite.Position = Engine.Mouse.WorldPosition;
                        zombieEnemies.Add(zombieSprite);
                        break;
                    case 8:
                        var weaponSprite = Engine.Display.Scene.CreateSprite("Images/AxePickup.png");
                        weaponSprite.Position = Engine.Mouse.WorldPosition;
                        weapons.Add(weaponSprite);
                        break;
                    case 9:
                        OpenFileDialog dialog = new OpenFileDialog();
                        if (dialog.ShowDialog() != DialogResult.OK)
                            return;
                        backGroundImage = dialog.FileName;
                        background = Engine.Display.Scene.CreateSprite(dialog.FileName);
                        background.Position = new Vector(0, 0);
                        background.Order = -1;
                        break;
                }
            }
            if (Engine.Keyboard.WasKeyPressed(Totem.Input.Keys.W))
            {
                mode = 8;
            }
            if (Engine.Keyboard.WasKeyPressed(Totem.Input.Keys.B))
            {
                mode = 9;
            }
            if (Engine.Keyboard.WasKeyPressed(Totem.Input.Keys.X))
            {
                mode = 4;
                zombieSpawnType = int.Parse(Microsoft.VisualBasic.Interaction.InputBox("Choose Spawn Type", "Enter Number", "1", 0, 0));
            }
            if (Engine.Keyboard.WasKeyPressed(Totem.Input.Keys.E))
            {
                if (doorPosition != null)
                {
                    doorPosition.Destroy();
                }
                mode = 5;
                doorType = int.Parse(Microsoft.VisualBasic.Interaction.InputBox("Choose Door Type", "Enter Number", "1", 0, 0));
                switch (doorType)
                {
                    case 1:
                        doorPosition = Engine.Display.Scene.CreateSprite("Images/DoorClosed.png");
                        break;
                    case 2:
                        doorPosition = Engine.Display.Scene.CreateSprite("Images/KeyDoorClosed.png");
                        int keyRequired = int.Parse(Microsoft.VisualBasic.Interaction.InputBox("Choose # of Keys required to open:", "Enter Number", "1", 0, 0));
                        break;
                    case 3:
                        doorPosition = Engine.Display.Scene.CreateSprite("Images/EnemyDoorClosed.png");
                        int enemyRequired = int.Parse(Microsoft.VisualBasic.Interaction.InputBox("Choose # of Enemies required to Open:", "Enter Number", "1", 0, 0));
                        break;
                }
            }
            if (Engine.Keyboard.WasKeyPressed(Totem.Input.Keys.Z))
            {
                mode = 7;
            }
            if (Engine.Keyboard.WasKeyPressed(Totem.Input.Keys.Tab))
            {
                OpenLevel();
            }
            if (Engine.Keyboard.WasKeyPressed(Totem.Input.Keys.Escape))
            {
                SaveLevel();
            }
            if(Engine.Keyboard.WasKeyPressed(Totem.Input.Keys.T))
            {
                mode = 1;
                tileType = int.Parse(Microsoft.VisualBasic.Interaction.InputBox("Choose Tile Type", "Enter Number", "1", 0, 0));
            }
           if(Engine.Keyboard.WasKeyPressed(Totem.Input.Keys.K))
            {
               mode = 2;
            }
            if(Engine.Keyboard.WasKeyPressed(Totem.Input.Keys.P))
            {
                mode = 3;
               
            }
            if (Engine.Keyboard.WasKeyPressed(Totem.Input.Keys.Delete))
            {
                for (int i = 0; i < levelWidth / 20; i++)
                {
                    for (int j = 0; j < levelHeight / 20; j++)
                    {
                        if (new Rectangle(i *20, j *20, 20, 20).Intersects(new Rectangle(Engine.Mouse.WorldPosition, 1, 1)))
                        {
                            tiles[i, j] = -1;
                        }
                    }
                }
                for (int i = 0; i < keys.Count; i++)
                {
                    if (keys[i].Bounds.Intersects(new Rectangle(Engine.Mouse.WorldPosition, 1, 1)))
                    {
                        keys[i].Destroy();
                        keys.RemoveAt(i);
                    }
                }
                for (int i = 0; i < zombieSpawns.Count; i++)
                {
                    if (zombieSpawns[i].Bounds.Intersects(new Rectangle(Engine.Mouse.WorldPosition, 1, 1)))
                    {
                        zombieSpawns[i].Destroy();
                        zombieSpawns.RemoveAt(i);
                    }
                }
                for (int i = 0; i < zombieEnemies.Count; i++)
                {
                    if (zombieEnemies[i].Bounds.Intersects(new Rectangle(Engine.Mouse.WorldPosition, 1, 1)))
                    {
                        zombieEnemies[i].Destroy();
                        zombieEnemies.RemoveAt(i);
                    }
                }
                for (int i = 0; i < weapons.Count; i++)
                {
                    if (weapons[i].Bounds.Intersects(new Rectangle(Engine.Mouse.WorldPosition, 1, 1)))
                    {
                        weapons[i].Destroy();
                        weapons.RemoveAt(i);
                    }
                }
                if (player.Bounds.Intersects(new Rectangle(Engine.Mouse.WorldPosition, 1, 1)))
                {
                    player.Destroy();
                }
            }
        }
    }
}
