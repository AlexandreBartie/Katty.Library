using System.Windows.Forms;

namespace Katty
{
    public static class myControl
    {
        public static void TurnOnOff(bool prmON, Control prmObjectA, Control prmObjectB) => TurnOnOff(prmON, prmObjectA, prmObjectB, prmAtive: true);
        public static void TurnOnOff(bool prmON, Control prmObjectA, Control prmObjectB, bool prmAtive)
        {
            prmObjectA.Visible = prmON && prmAtive; prmObjectB.Visible = !prmON && prmAtive;
        }

    }

    public static class myMenu
    {
        public static bool InvertCheck(ToolStripMenuItem prmMenu)
        {
            prmMenu.Checked = !prmMenu.Checked; return prmMenu.Checked;
        }

    }
}
