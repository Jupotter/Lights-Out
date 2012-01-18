using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libtcod;

namespace Lights_Out
{
    class Messages
    {
        static List<string> messages;
		int lines;
        TCODConsole console;
        public TCODConsole Console
        { get { return console; } }

        static Messages()
        {
            messages = new List<string>();
        }

		public Messages(TCODConsole cons)
		{
            console = cons;
            lines = cons.getHeight();
		}
		
		public void Draw()
		{
			console.rect(0, 0, 80, lines, true);
			int size = messages.Count;
			for (int i = 0; i < System.Math.Min(lines, size); i++)
			{
				this.console.print(0,lines-i-1,messages[size-i-1]);
			}
		}
		
		public static void AddMessage(string msg)
		{
			messages.Add(msg);
		}
    }
}
