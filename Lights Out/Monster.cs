using System;
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
            : base(c, color)
        {
            this.health = health;
            map.AddCreature(this);
            ai = new MonsterAI(this);
            ai.SetMap(map);
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

        public override bool PlaceAt(int x, int y, Map map)
        {
            if (currentMap != map)
            {
                if (currentMap != null)
                currentMap.RemoveCreature(this);
                map.AddCreature(this);
                ai.SetMap(map);
            }
            bool ret = base.PlaceAt(x, y, map);
            return ret;
        }

        public void Act()
        {
            ai.Act();
        }
    }
}
