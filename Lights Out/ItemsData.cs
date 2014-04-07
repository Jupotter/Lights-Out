using System.Collections.Generic;
using libtcod;

namespace Lights_Out
{
    static class ItemsData
    {
        private delegate Item ItemCreate();

        private struct WeightedCreator { public ItemCreate Create; public int Weight; };

        public class TorchLight : Item
        {
            static public int Weight = 10;

            public TorchLight()
                : base("Torch", '|', false)
            {
                SetLight(new Light(20, 10, 500, TCODColor.sepia));
            }

            static public Item Create() { return new TorchLight(); }
        }

        public class WeakTorch : Item
        {
            static public int Weight = 30;

            public WeakTorch()
                : base("Weak Torch", 'i', false)
            {
                SetLight(new Light(10, 5, 1000, TCODColor.sepia));
            }

            static public Item Create() { return new WeakTorch(); }
        }

        public class GlowingPebble : Item
        {
            static public int Weight = 10;

            public GlowingPebble()
                : base("Glowing Pebble", 'o', false)
            {
                SetLight(new Light(5, 5, -1, TCODColor.purple));
            }

            public override void SwitchLight(bool on)
            {
                base.SwitchLight(true);
            }

            public override bool Drop(int x, int y, Map map)
            {
                IsLight = true;
                SwitchLight(true);
                return base.Drop(x, y, map);
            }

            public override void Get()
            {
                IsLight = false;
                base.Get();
            }

            static public Item Create() { return new GlowingPebble(); }
        }

        public class FlashScroll : Item
        {
            static public int Weight = 2;

            public FlashScroll()
                : base("Flash Scroll", '%', false)
            { }

            public override void Use()
            {
                Light l = new Light(200, 200, 2);
                l.PlaceAt(Map.Player.PosX, Map.Player.PosY, Map);
            }

            static public Item Create() { return new FlashScroll(); }
        }

        static readonly List<WeightedCreator> ItemList;
        static public int TotalWeight;

        static ItemsData()
        {
            ItemList = new List<WeightedCreator>();
            ItemList.Add(new WeightedCreator { Create = TorchLight.Create, Weight = TorchLight.Weight }); TotalWeight += TorchLight.Weight;
            ItemList.Add(new WeightedCreator { Create = WeakTorch.Create, Weight = WeakTorch.Weight }); TotalWeight += WeakTorch.Weight;
            ItemList.Add(new WeightedCreator { Create = FlashScroll.Create, Weight = FlashScroll.Weight }); TotalWeight += FlashScroll.Weight;
            ItemList.Add(new WeightedCreator { Create = GlowingPebble.Create, Weight = GlowingPebble.Weight }); TotalWeight += GlowingPebble.Weight;
        }

        public static Item PickWeightedItem(int num)
        {
            int sum = 0;
            List<WeightedCreator>.Enumerator e = ItemList.GetEnumerator();
            do
            {
                e.MoveNext();
                sum += e.Current.Weight;
            } while (sum < num);
            return e.Current.Create();
        }
    }
}
