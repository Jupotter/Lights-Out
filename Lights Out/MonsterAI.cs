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
        Map source;
        Monster client;

        public MonsterAI(Monster client, Map map)
        {
            this.client = client;
            this.source = map;
            this.map = map.Dijkstra;
        }

        public void Act()
        {
            if (Game.MonsterAI)
            {
                intCouple next = map.GetNext(client.posX, client.posY);

                if (source.Player.posX == next.X && source.Player.posY == next.Y)
                {
                    source.Player.Light.Use(10);
                }
                else
                {
                    Monster mons = source.ContainMonster(next.X, next.Y);
                    if (mons == null)
                        client.PlaceAt(next.X, next.Y);
                }
            }
        }
    }
}
