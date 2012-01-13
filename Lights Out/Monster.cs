using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libtcod;

namespace Lights_Out
{
    public class Monster : Creature
    {
        int health;
        public int Health
        { get { return health; } }

        MonsterAI ai;

        public Monster(char c, TCODColor color, Map map, int health)
            : base(c, color, map)
        {
            this.health = health;
            map.AddCreature(this);
            ai = new MonsterAI(this, map);
        }

        public void TakeDamage(int dmg)
        {
            if (Game.MonsterDamage)
            {
                health -= dmg;
                Console.WriteLine(String.Format("Monster lose {0} HP: {1} remain", dmg, health));
                if (health <= 0)
                    currentMap.RemoveCreature(this);
            }
        }

        public void Act()
        {
            ai.Act();
        }
    }
}
