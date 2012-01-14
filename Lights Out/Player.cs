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
            : base('@', TCODColor.green)
        {
            currentMap = map;
            equiped = null;
            light = new Light(0,0,0);
            inventory = new Inventory(this);
            inventory.Add(new ItemsData.TorchLight());
            Equip(Inventory.GetAtLetter('a'));
            for (int i = 0; i < 5; i++)
            {
                inventory.Add(new ItemsData.WeakTorch());
            }
            inventory.Add(new ItemsData.FlashScroll());
            inventory.Add(new ItemsData.FlashScroll());
            inventory.Add(new ItemsData.FlashScroll());
        }

        public void Equip(Item i)
        {
            if (currentMap != null)
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

        public override bool PlaceAt(int x, int y, Map map)
        {
            if (map != currentMap)
            {
                currentMap.Player = null;
                map.Player = this;
            }
            bool ret = base.PlaceAt(x, y, map);
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
