using System;
using System.Collections.Generic;
using libtcod;

namespace Lights_Out
{
    public class Inventory
    {
        const int CAPACITY = 26;

        struct InvStruct { public Item Item; public int Num; };

        readonly InvStruct[] _items;
        readonly SortedList<int, string> _taken;
        int _firstFree;
        readonly Player _client;

        public Inventory(Player client)
        {
            _client = client;
            _items = new InvStruct[CAPACITY];
            _taken = new SortedList<int,string>();
            _firstFree = 0;
        }

        public int CountAtLetter(char c)
        {
            int pos = c - 97;
            if (_taken.ContainsKey(pos))
                return _items[c - 97].Num;
            return 0;
        }

        public Item GetAtLetter(char c)
        {

            int pos = c - 97;
            if (_taken.ContainsKey(pos))
                return _items[c - 97].Item;
            return null;
        }

        public bool Add(Item item)
        {
            item.SetMap(_client.CurrentMap);
            if (item.Stack)
            {
                int i = _taken.IndexOfValue(item.Name);
                if (i != -1)
                {
                    _items[i].Num++;
                    return true;
                }
            }

            if (_firstFree == -1)
                return false;

            _items[_firstFree] = new InvStruct { Item = item, Num = 1 };
            _taken.Add(_firstFree, item.Name);
            do
                _firstFree++;
            while (_taken.ContainsKey(_firstFree));
            if (_firstFree == 26)
                _firstFree = -1;
            return true;
        }

        public void RemoveAtLetter(char c, int num)
        {
            int pos = c - 97;
            if (pos >= 0 && pos < 26)
            {
                _items[pos].Num -= num;
                if (_items[pos].Num <= 0)
                {
                    _taken.Remove(pos);
                    if (pos < _firstFree && pos >= 0)
                        _firstFree = pos;
                }
            }
        }

        public void RemoveAllAtLetter(char c)
        {
            int pos = c - 97;
            _taken.Remove(pos);
            if (pos < _firstFree && pos >= 0)
                _firstFree = pos;
        }

        public void Draw(TCODConsole cons)
        {
            TCODConsole temp = new TCODConsole(50, 30);
            int x = 2;
            foreach (int i in _taken.Keys)
            {
                temp.print(2, x, String.Format("{0} - {1}", (char)(i + 97), _items[i].Item));
                x++;
            }
            TCODConsole.blit(temp, 0, 0, 50, x + 2, cons, 0, 0);
        }
    }
}
