using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lights_Out
{
    static class ItemsData
    {
        public class TorchLight : Item
        {
            public TorchLight()
                : base("Torch", '|', false)
            {
                SetLight(new Light(20, 10, 100));
            }
        }

        public class WeakTorch : Item
        {
            public WeakTorch()
                : base("Weak Torch", 'i', false)
            {
                SetLight(new Light(10, 5, 100));
            }
        }

        public class FlashScroll : Item
        {
            public FlashScroll()
                : base("Flash Scroll", '%', false)
            {

            }

            public override void Use()
            {
                Light l = new Light(100, 100, 1);
                l.PlaceAt(map.Player.posX, map.Player.posY, map);
            }
        }
    }
}
