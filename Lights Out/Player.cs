using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libtcod;

namespace Lights_Out
{
    public class Player : Creature
    {
        Inventory inventory;
        public Inventory Inventory
        { get { return inventory;} }

        Light light;
        public Light Light
        { get { return light; } }


        public Player(Map map)
            : base('@', TCODColor.green, map)
        {
            light = new Light(20);
            inventory = new Inventory();
            inventory.Add(new Item("item1", '%', true));
            inventory.Add(new Item("item2", '%', false));
            inventory.Add(new Item("item3", '%', false));
            inventory.Add(new Item("item4", '%', false));
            inventory.Add(new Item("item5", '%', false));
            inventory.Add(new Item("item6", '%', false));
            inventory.Add(new Item("item1", '%', true));
            inventory.Add(new Item("item1", '%', true));
            inventory.Add(new Item("item1", '%', true));
            inventory.Add(new Item("item6", '%', false));
        }

        public override bool PlaceAt(int x, int y)
        {
            bool ret = base.PlaceAt(x, y);
            if (ret)
                light.PlaceAt(posX, posY, currentMap);
            return ret;
        }

        public override bool Move(int dX, int dY)
        {
            bool ret = base.Move(dX, dY);
            if (ret)
                light.PlaceAt(posX, posY, currentMap);
            return ret;
        }
    }
}
