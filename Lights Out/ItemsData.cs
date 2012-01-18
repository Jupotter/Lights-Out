using System.Collections.Generic;
using libtcod;

namespace Lights_Out
{
    static class ItemsData
    {
        delegate Item itemCreate();
        struct weightedCreator { public itemCreate create; public int weight; };

        public class TorchLight : Item
        {
            static public int weight = 10;

            public TorchLight()
                : base("Torch", '|', false)
            {
                SetLight(new Light(20, 10, 500, TCODColor.sepia));
            }

            static public Item Create() { return new TorchLight(); }
        }

        public class WeakTorch : Item
        {
            static public int weight = 30;

            public WeakTorch()
                : base("Weak Torch", 'i', false)
            {
                SetLight(new Light(10, 5, 1000, TCODColor.sepia));
            }

            static public Item Create() { return new WeakTorch(); }
        }

        public class GlowingPebble : Item
        {
            static public int weight = 10;

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
                isLight = true;
                SwitchLight(true);
                return base.Drop(x, y, map);
            }

            public override void Get()
            {
                isLight = false;
                base.Get();
            }

            static public Item Create() { return new GlowingPebble(); }
        }

        public class FlashScroll : Item
        {
            static public int weight = 2;

            public FlashScroll()
                : base("Flash Scroll", '%', false)
            { }

            public override void Use()
            {
                Light l = new Light(200, 200, 2);
                l.PlaceAt(map.Player.posX, map.Player.posY, map);
            }

            static public Item Create() { return new FlashScroll(); }
        }

        static List<weightedCreator> ItemList;
        static public int TotalWeight;

        static ItemsData()
        {
            ItemList = new List<weightedCreator>();
            ItemList.Add(new weightedCreator { create = TorchLight.Create, weight = TorchLight.weight }); TotalWeight += TorchLight.weight;
            ItemList.Add(new weightedCreator { create = WeakTorch.Create, weight = WeakTorch.weight }); TotalWeight += WeakTorch.weight;
            ItemList.Add(new weightedCreator { create = FlashScroll.Create, weight = FlashScroll.weight }); TotalWeight += FlashScroll.weight;
            ItemList.Add(new weightedCreator { create = GlowingPebble.Create, weight = GlowingPebble.weight }); TotalWeight += GlowingPebble.weight;
        }

        public static Item PickWeightedItem(int num)
        {
            int sum = 0;
            List<weightedCreator>.Enumerator e = ItemList.GetEnumerator();
            do
            {
                e.MoveNext();
                sum += e.Current.weight;
            } while (sum < num);
            return e.Current.create();
        }
    }
}
