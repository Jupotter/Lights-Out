using System.Collections.Generic;
using libtcod;

namespace Lights_Out
{
    class Messages
    {
        static private readonly List<string> _messages;
        readonly int _lines;
        readonly TCODConsole _console;
        public TCODConsole Console
        { get { return _console; } }

        static Messages()
        {
            _messages = new List<string>();
        }

        public Messages(TCODConsole cons)
        {
            _console = cons;
            _lines = cons.getHeight();
        }

        public void Draw()
        {
            _console.rect(0, 0, 80, _lines, true);
            int size = _messages.Count;
            for (int i = 0; i < System.Math.Min(_lines, size); i++)
            {
                _console.print(0, _lines - i - 1, _messages[size - i - 1]);
            }
        }

        public static void AddMessage(string msg)
        {
            _messages.Add(msg);
        }
    }
}
