using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Campo_Minado
{
    public class CampoMinado
    {
        public static int Linhas { get; private set; }
        public static int Colunas { get; private set; }
        public static int Minas { get; private set; }
        public static bool[,] TemMina { get; private set; }
        public static bool[,] Revelado { get; private set; }
        public static Button[,] Botoes { get; private set; }
        public static int VidasExtras { get; private set; }
        public static bool[,] TemVidaExtra { get; private set; }

        private static Random rand = new Random();

        public static void Inicializar(int linhas, int colunas, int minas, int vidasExtras)
        {
            Linhas = linhas;
            Colunas = colunas;
            Minas = minas;
            VidasExtras = vidasExtras;

            TemMina = new bool[Linhas, Colunas];
            Revelado = new bool[Linhas, Colunas];
            Botoes = new Button[Linhas, Colunas];
        }

        public static void CriarCampo(Panel container, int tamanho, MouseEventHandler aoClicar)
        {
            container.Controls.Clear();

            // Redimensiona o painel para caber todos os botões
            container.Width = Colunas * tamanho;
            container.Height = Linhas * tamanho;

            for (int i = 0; i < Linhas; i++)
            {
                for (int j = 0; j < Colunas; j++)
                {
                    Button btn = new Button
                    {
                        Size = new Size(tamanho, tamanho),
                        Location = new Point(j * tamanho, i * tamanho),
                        Font = new Font("Montserrat", 12, FontStyle.Bold),
                        BackColor = Color.WhiteSmoke,
                        FlatStyle = FlatStyle.Flat
                    };
                    btn.MouseDown += aoClicar;
                    container.Controls.Add(btn);
                    Botoes[i, j] = btn;
                }
            }
        }

        public static void ColocarMinas(int? excluirI = null, int? excluirJ = null)
        {
            int colocadas = 0;
            while (colocadas < Minas)
            {
                int i = rand.Next(Linhas);
                int j = rand.Next(Colunas);

                if (TemMina[i, j] || (excluirI.HasValue && excluirJ.HasValue &&
                    Math.Abs(i - excluirI.Value) <= 1 && Math.Abs(j - excluirJ.Value) <= 1))
                    continue;

                TemMina[i, j] = true;
                colocadas++;
            }
        }

        public static int ContarMinasAoRedor(int i, int j)
        {
            int count = 0;
            for (int x = -1; x <= 1; x++)
                for (int y = -1; y <= 1; y++)
                {
                    int ni = i + x, nj = j + y;
                    if (ni >= 0 && ni < Linhas && nj >= 0 && nj < Colunas && TemMina[ni, nj])
                        count++;
                }
            return count;
        }

        public static bool VerificarVitoria()
        {
            for (int i = 0; i < Linhas; i++)
                for (int j = 0; j < Colunas; j++)
                    if (!TemMina[i, j] && !Revelado[i, j])
                        return false;
            return true;
        }

        public static bool TemMinaAoRedor(int i, int j)
        {
            for (int x = -1; x <= 1; x++)
                for (int y = -1; y <= 1; y++)
                {
                    int ni = i + x, nj = j + y;
                    if (ni >= 0 && ni < Linhas && nj >= 0 && nj < Colunas && TemMina[ni, nj])
                        return true;
                }
            return false;
        }

        public static void ColocarVidasExtras(int quantidade)
        {
            int colocadas = 0;
            while (colocadas < quantidade)
            {
                int i = rand.Next(Linhas);
                int j = rand.Next(Colunas);

                // Evita colocar onde já tem mina ou vida extra
                if (TemMina[i, j] || Botoes[i, j].Tag as string == "VidaExtra")
                    continue;

                // Verifica se há minas ao redor
                bool minaPorPerto = false;
                for (int x = -1; x <= 1 && !minaPorPerto; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        int ni = i + x, nj = j + y;
                        if (ni >= 0 && ni < Linhas && nj >= 0 && nj < Colunas && TemMina[ni, nj])
                        {
                            minaPorPerto = true;
                            break;
                        }
                    }
                }

                if (minaPorPerto)
                    continue;

                // Marca como vida extra
                Botoes[i, j].Tag = "VidaExtra";
                colocadas++;
            }
        }
    }
}
