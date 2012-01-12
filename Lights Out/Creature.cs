using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libtcod;

namespace Lights_Out
{
    public class Creature
    {
        public int posX;
        public int posY;
        char tile;
        TCODColor color;
        Map currentMap;

        public Inventory inventory;

        Light light;
        public Light Light
        { get { return light; } }

        public Creature(char tile, TCODColor color, Map map)
        {
            this.tile = tile;
            this.color = color;
            currentMap = map;
            inventory = new Inventory();
            inventory.Add(new Item("item1", '%'));
            inventory.Add(new Item("item2", '%'));
            inventory.Add(new Item("item3", '%'));
            inventory.Add(new Item("item4", '%'));
            inventory.Add(new Item("item5", '%'));
            inventory.Add(new Item("item6", '%'));
            inventory.RemoveAtLetter('c');

            light = new Light(20);
            PlaceAt(map.StartPosX, map.StartPosY);
        }

        public bool PlaceAt(int x, int y)
        {
            if (x >= 0 && x < Map.MAP_WIDTH
                && y >= 0 && y < Map.MAP_HEIGHT)
            {
                posX = x;
                posY = y;
                light.PlaceAt(posX, posY, currentMap);
                return true;
            }
            return false;
        }

        public bool Move(int dX, int dY)
        {
            int newX = posX + dX;
            int newY = posY + dY;
            if (!currentMap[newX, newY])
            {
                posX = newX;
                posY = newY;
                light.PlaceAt(posX, posY, currentMap);
                return true;
            }
            return false;
        }

        public void Draw(TCODConsole cons)
        {
            cons.putChar(posX, posY, (int)tile);
            cons.setCharForeground(posX, posY, color);
        }
    }
}
