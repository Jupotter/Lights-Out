using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libtcod;

namespace Lights_Out
{
    public class Inventory
    {
        const int CAPACITY = 26;

        struct InvStruct { public Item item; public int num; };

        InvStruct[] items;
        SortedList<int, string> taken;
        int firstFree;

        public Inventory()
        {
            items = new InvStruct[CAPACITY];
            taken = new SortedList<int,string>();
            firstFree = 0;
        }

        public int CountAtLetter(char c)
        {
            int pos = (int)c - 97;
            if (taken.ContainsKey(pos))
                return items[(int)c - 97].num;
            return 0;
        }

        public Item GetAtLetter(char c)
        {

            int pos = (int)c - 97;
            if (taken.ContainsKey(pos))
                return items[(int)c - 97].item;
            return null;
        }

        public bool Add(Item item)
        {
            if (item.Stack)
            {
                int i = taken.IndexOfValue(item.Name);
                if (i != -1)
                {
                    items[i].num++;
                    return true;
                }
            }

            if (firstFree == -1)
                return false;

            items[firstFree] = new InvStruct { item = item, num = 1 };
            taken.Add(firstFree, item.Name);
            do
                firstFree++;
            while (taken.ContainsKey(firstFree));
            if (firstFree == 26)
                firstFree = -1;
            return true;
        }

        public void RemoveAtLetter(char c, int num)
        {
            int pos = (int)c - 97;
            if (pos >= 0 && pos < 26)
            {
                items[pos].num -= num;
                if (items[pos].num <= 0)
                {
                    taken.Remove(pos);
                    if (pos < firstFree && pos >= 0)
                        firstFree = pos;
                }
            }
        }

        public void RemoveAllAtLetter(char c)
        {
            int pos = (int)c - 97;
            taken.Remove(pos);
            if (pos < firstFree && pos >= 0)
                firstFree = pos;
        }

        public void Draw(TCODConsole cons)
        {
            TCODConsole temp = new TCODConsole(50, 30);
            int x = 2;
            foreach (int i in taken.Keys)
            {
                temp.print(2, x, String.Format("{0} - {1} {2}", (char)(i + 97), items[i].num, items[i].item.Name));
                x++;
            }
            TCODConsole.blit(temp, 0, 0, 50, x + 2, cons, 0, 0);
        }
    }
}
