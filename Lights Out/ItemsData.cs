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
                SetLight(new Light(20));
            }
        }

        public class WeakTorch : Item
        {
            public WeakTorch()
                : base("Weak Torch", 'i', true)
            {
                SetLight(new Light(10));
            }
        }
    }
}
