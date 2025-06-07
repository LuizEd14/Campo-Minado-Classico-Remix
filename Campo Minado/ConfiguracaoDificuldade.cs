using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campo_Minado
{
    class ConfiguracaoDificuldade
    {
        public enum Dificuldade { Facil, Medio, Dificil }

        public static class ObterDificuldade
        {
            public static (int, int, int) ObterConfiguracao(Dificuldade dificuldade)
            {
                if (dificuldade == Dificuldade.Facil)
                    return (9, 9, 10);
                else if (dificuldade == Dificuldade.Medio)
                    return (16, 16, 40);
                else if (dificuldade == Dificuldade.Dificil)
                    return (16, 30, 99);
                else
                    return (9, 9, 10);
            }
        }

        public static int CalcularVidasExtras(int minas)
        {
            return Math.Max(1, minas / 5);
        }
    }
}
