using System;
using libtcod;

namespace Lights_Out
{
    public class Monster : Creature
    {
        public int Health { get; private set; }

        readonly MonsterAI _ai;

        public Monster(char c, TCODColor color, Map map, int health)
            : base(c, color)
        {
            Health = health;
            map.AddCreature(this);
            _ai = new MonsterAI(this);
            _ai.SetMap(map);
        }

        public void TakeDamage(int dmg)
        {
            if (Game.MonsterDamage)
            {
                Health -= dmg;
                Messages.AddMessage(String.Format("Monster lose {0} HP: {1} remain", dmg, Health));
                if (Health <= 0)
                    CurrentMap.RemoveCreature(this);
            }
        }

        public override bool PlaceAt(int x, int y, Map map)
        {
            if (CurrentMap != map)
            {
                if (CurrentMap != null)
                    CurrentMap.RemoveCreature(this);
                map.AddCreature(this);
                _ai.SetMap(map);
            }
            bool ret = base.PlaceAt(x, y, map);
            return ret;
        }

        public void Act()
        {
            _ai.Act();
        }
    }
}
