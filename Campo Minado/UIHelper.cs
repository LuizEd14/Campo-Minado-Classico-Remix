using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Campo_Minado
{
    public static class UIHelper
    {
        public static Color CorDoNumero(int numero)
        {
            switch (numero)
            {
                case 1: return Color.Blue;
                case 2: return Color.Green;
                case 3: return Color.Red;
                case 4: return Color.DarkBlue;
                case 5: return Color.Brown;
                case 6: return Color.DarkCyan;
                case 7: return Color.Black;
                case 8: return Color.Gray;
                default: return Color.Black;
            }
        }

        public static void CentralizarPainel(Control container, Control painel)
        {
            container.Location = new Point(
                (painel.Width - container.Width) / 2,
                (painel.Height - container.Height) / 2
            );
        }
    }
}
