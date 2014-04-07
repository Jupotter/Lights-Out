using libtcod;

namespace Lights_Out
{
    public class Player : Creature
    {
        readonly Inventory _inventory;
        public Inventory Inventory
        { get { return _inventory; } }

        public Light Light { get; private set; }


        public Player(Map map)
            : base('@', TCODColor.green)
        {
            CurrentMap = map;
            Light = new Light(0, 0, 0);
            _inventory = new Inventory(this);
            _inventory.Add(new ItemsData.TorchLight());
            Equip(Inventory.GetAtLetter('a'));
            for (int i = 0; i < 5; i++)
            {
                _inventory.Add(new ItemsData.WeakTorch());
            }
            _inventory.Add(new ItemsData.FlashScroll());
            _inventory.Add(new ItemsData.FlashScroll());
            _inventory.Add(new ItemsData.FlashScroll());
        }

        public void Equip(Item i)
        {
            if (CurrentMap != null)
                CurrentMap.RemoveLight(Light);
            if (i.IsLight)
                SetLight(i.Light);
        }

        void SetLight(Light light)
        {
            Light = light;
            light.PlaceAt(PosX, PosY, CurrentMap);
        }

        public override bool PlaceAt(int x, int y, Map map)
        {
            if (map != CurrentMap)
            {
                CurrentMap.Player = null;
                map.Player = this;
            }
            bool ret = base.PlaceAt(x, y, map);
            if (ret)
                Light.PlaceAt(PosX, PosY, CurrentMap);
            return ret;
        }

        public override bool Move(int dX, int dY)
        {
            bool ret = base.Move(dX, dY);
            if (ret)
                Light.PlaceAt(PosX, PosY, CurrentMap);
            return ret;
        }
    }
}
