using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libtcod;

namespace Lights_Out
{
    class MonsterAI
    {
        Dijkstra map;
        Monster client;

        public MonsterAI(Monster client, Map map)
        {
            this.client = client;
            this.map = map.Dijkstra;
        }

        public void Act()
        {
            if (Game.MonsterAI)
            {
                intCouple next = map.GetNext(client.posX, client.posY);
                client.PlaceAt(next.X, next.Y);
            }
        }
    }
}
