
namespace Lights_Out
{
    class MonsterAI
    {
        Dijkstra _map;
        Map _source;
        readonly Monster _client;

        public MonsterAI(Monster client)
        {
            _client = client;
        }

        public void SetMap(Map map)
        {
            _source = map;
            _map = map.Dijkstra;
        }

        public void Act()
        {
            if (Game.MonsterAI)
            {
                IntCouple next = _map.GetNext(_client.PosX, _client.PosY);

                if (_source.Player.PosX == next.X && _source.Player.PosY == next.Y)
                {
                    _source.Player.Light.Use(10);
                }
                else
                {
                    Light light = _source.ContainLight(next.X, next.Y);
                    if (light != null)
                    {
                        light.Use(10);
                    }
                    else
                    {
                        Monster mons = _source.ContainMonster(next.X, next.Y);
                        if (mons == null)
                            _client.PlaceAt(next.X, next.Y, _client.CurrentMap);
                    }
                }
            }
        }
    }
}
