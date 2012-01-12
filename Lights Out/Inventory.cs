﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libtcod;

namespace Lights_Out
{
    public class Inventory
    {
        Item[] items;
        List<int> taken;
        int firstFree;

        public Inventory()
        {
            items = new Item[26];
            taken = new List<int>();
            firstFree = 0;
        }

        public Item GetAtLetter(char c)
        {
            int pos = (int)c - 97;
            if (taken.Contains(pos))
                return items[(int)c - 97];
            return null;
        }

        public bool Add(Item item)
        {
            if (firstFree == -1)
                return false;

            items[firstFree] = item;
            taken.Add(firstFree);
            taken.Sort();
            do
                firstFree++;
            while (taken.Contains(firstFree));
            if (firstFree == 26)
                firstFree = -1;
            return true;
        }

        public bool RemoveAtLetter(char c)
        {
            int pos = (int)c - 97;
            taken.Remove(pos);
            if (pos < firstFree && pos >= 0)
                firstFree = pos;
            return true;
        }

        public void Draw(TCODConsole cons)
        {
            TCODConsole temp = new TCODConsole(50, 30);
            int x = 2;
            foreach (int i in taken)
            {
                temp.print(2, x, String.Format("{0} - {1}", (char)(i + 97), items[i].Name));
                x++;
            }
            TCODConsole.blit(temp, 0, 0, 50, x + 2, cons, 0, 0);
        }
    }
}