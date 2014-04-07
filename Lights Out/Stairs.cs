
namespace Lights_Out
{
    public class Stairs
    {
        readonly int _posX;

        public int PosX
        { get { return _posX; } }

        readonly int _posY;

        public int PosY
        { get { return _posY; } }

        public Stairs(int x, int y)
        {
            _posX = x;
            _posY = y;
        }

        public void Draw(libtcod.TCODConsole cons)
        {
            cons.putChar(_posX, _posY, '>');
        }
    }
}
