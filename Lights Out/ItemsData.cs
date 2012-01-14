using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                SetLight(new Light(20, 10, 100));
            }

            static public Item Create() { return new TorchLight(); }
        }

        public class WeakTorch : Item
        {
            static public int weight = 30;

            public WeakTorch()
                : base("Weak Torch", 'i', false)
            {
                SetLight(new Light(10, 5, 100));
            }

            static public Item Create() { return new WeakTorch(); }
        }

        public class FlashScroll : Item
        {
            static public int weight = 2;

            public FlashScroll()
                : base("Flash Scroll", '%', false)
            { }

            public override void Use()
            {
                Light l = new Light(100, 100, 1);
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
