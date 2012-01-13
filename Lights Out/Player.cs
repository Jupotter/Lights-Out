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


        Item equiped;

        public Player(Map map)
            : base('@', TCODColor.green, map)
        {
            equiped = null;
            light = new Light(0);
            inventory = new Inventory();
            for (int i = 0; i < 10; i++)
            {
                inventory.Add(new ItemsData.WeakTorch());
            }
            inventory.Add(new ItemsData.TorchLight());
        }

        public void Equip(Item i)
        {
            currentMap.RemoveLight(light);
            equiped = i;
            if (i.IsLight)
                setLight(i.Light);
        }

        void setLight(Light light)
        {
            this.light = light;
            light.PlaceAt(posX, posY, currentMap);
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
