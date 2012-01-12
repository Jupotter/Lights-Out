using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libtcod;

namespace Lights_Out
{
    public class Map
    {
        public const int MAP_WIDTH  = 80;
        public const int MAP_HEIGHT = 50;

        TCODMap tcodmap;
        public int StartPosX;
        public int StartPosY;

        List<Creature> creatures;
        List<Light> lights;
        List<Item> items;
        public Creature Player;

        public TCODMap TCODMap
        { get { return tcodmap; } }

        public Map()
        {
            tcodmap = new TCODMap(MAP_WIDTH, MAP_HEIGHT);
            lights = new List<Light>();
            creatures = new List<Creature>();
            items = new List<Item>();

            for (int i = 0; i < MAP_WIDTH; i++)
                for (int j = 0; j < MAP_HEIGHT; j++)
                {
                    this[i, j] = true;
                }
        }

        public void Draw(TCODConsole cons)
        {
            cons.setForegroundColor(TCODColor.black);
            for (int i = 0; i < MAP_WIDTH; i++)
                for (int j = 0; j < MAP_HEIGHT; j++)
                {
                    cons.putChar(i, j, this[i, j] ? '#' : '.');

                    foreach (Item item in items.FindAll(item => item.PosX == i && item.PosY == j))
                    {
                        item.Draw(cons);
                    }

                    int intens = 0;
                    foreach (Light light in lights)
                    {
                        int t = light.IntensityAt(i, j);
                        intens = System.Math.Max(intens, t);
                    }
                    int pl = Player.Light.IntensityAt(i, j);
                    intens = System.Math.Max(intens, pl);

                    TCODColor back = new TCODColor();

                    back = back.Plus(TCODColor.orange);

                    back.setValue((float)intens / 20);
                    cons.setCharForeground(i, j, back);
                }
        }

        public void Update()
        {
        }

        public bool SetStartPos(int X, int Y)
        {
            if (X >= 0 && X < Map.MAP_WIDTH
                && Y >= 0 && Y < Map.MAP_HEIGHT)
            {
                StartPosX = X;
                StartPosY = Y;
                return true;
            }
            return false;
        }

        public void AddLight(Light light)
        {
            lights.Add(light);
        }

        public void RemoveLight(Light light)
        {
            lights.Remove(light);
        }

        public void RemoveLighstAt(int X, int Y)
        {
            lights.RemoveAll(l => l.PosX == X && l.PosY == Y);
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }

        public void RemoveItemsAt(int X, int Y)
        {
            items.RemoveAll(i => i.PosX == X && i.PosY == Y);
        }

        public List<Item> GetItemsAt(int X, int Y)
        {
            return items.FindAll(i => i.PosX == X && i.PosY == Y);
        }

        public void AddCreature(Creature creature)
        {
            creatures.Add(creature);
        }

        public bool this[int x, int y]
        {
            get { return !tcodmap.isWalkable(x, y); ; }
            set { tcodmap.setProperties(x, y, !value, !value); }
        }
    }
}
