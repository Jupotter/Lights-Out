using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libtcod;

namespace Lights_Out
{
    public enum ItemState
    { Held, Dropped }

    public class Item
    {
        public ItemState State;
        bool isLight;
        Light light;
        Map map;

        char tile;
        int posX, posY;
        TCODColor color;

        public int PosX
        { get { return posX; } }
        public int PosY
        { get { return posY; } }
        public char Tile
        { get { return tile; } }
        public TCODColor Color
        { get { return color == null ? TCODColor.white : color; } }

        string name;
        public string Name
        { get { return name; } }

        public Item(string name, char tile)
        {
            this.name = name;
            this.tile = tile;
            isLight = false;
        }

        public void SetLight(Light light)
        {
            this.light = light;
            isLight = true;
        }

        public bool Drop(int x, int y, Map map)
        {
            if (x >= 0 && x < Map.MAP_WIDTH
                && y >= 0 && y < Map.MAP_HEIGHT)
            {
                posX = x;
                posY = y;
                this.map = map;
                this.State = ItemState.Dropped;
                map.AddItem(this);
                if (isLight)
                    light.PlaceAt(x, y, map);
                return true;
            }
            return false;
        }

        public void Get()
        {
            this.State = ItemState.Held;
            map.RemoveItem(this);
            if (isLight)
                map.RemoveLight(light);
        }

        public void Draw(TCODConsole cons)
        {
            cons.putChar(posX, posY, (int)tile);
        }
    }
}
