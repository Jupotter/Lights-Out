
namespace Lights_Out
{
    public class Stairs
    {
        int posX;

        public int PosX
        { get { return posX; } }
        int posY;

        public int PosY
        { get { return posY; } }

        public Stairs(int x, int y)
        {
            posX = x;
            posY = y;
        }

        public void Draw(libtcod.TCODConsole cons)
        {
            cons.putChar(posX, posY, '>');
        }
    }
}
