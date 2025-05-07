using System;
using CampoMinado;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Campo_Minado
{
    public partial class frmConquistas : Form
    {
        public frmConquistas()
        {
            InitializeComponent();
        }

        private Image GetIconeConquista(Conquista c, bool desbloqueada)
        {
            if (!desbloqueada)
                return Properties.Resources.locked; // Ícone cinza de conquista bloqueada

            switch (c)
            {
                case Conquista.PrimeiraVitoria:
                    return Properties.Resources.Conquista_Primeira_Vitoria;
                case Conquista.Sobrevivente:
                    return Properties.Resources.Conquista_Capacete;
                case Conquista.Recolher100Vidas:
                    return Properties.Resources.Conquista_Coracao;
                case Conquista.VencerEmTempoRecorde:
                    return Properties.Resources.Conquista_Tempo_Recorde;
                case Conquista.SemBandeiras:
                    return Properties.Resources.Conquista_Sem_Bandeiras;
                case Conquista.VencerSemPerderVidas:
                    return Properties.Resources.Conquista_Sem_Vidas_Perdas;
                case Conquista.CacadorDeCoracoes:
                    return Properties.Resources.Conquista_Todas_Vidas;
                default:
                    return Properties.Resources.happy; // Ícone genérico de fallback
            }
        }

        private void frmConquistas_Load(object sender, EventArgs e)
        {
            GerenciadorConquistas.CarregarConquistas();

            int colunas = 4;
            int linhaAtual = 0;
            int colunaAtual = 0;

            tblConquistas.Controls.Clear();
            tblConquistas.RowStyles.Clear();
            tblConquistas.ColumnCount = colunas;

            // Descrições personalizadas
            Dictionary<Conquista, string> descricoes = new Dictionary<Conquista, string>
    {
        { Conquista.PrimeiraVitoria, "Ganhe seu primeiro jogo." },
        { Conquista.Sobrevivente, "Ganhe 10 jogos." },
        { Conquista.Recolher100Vidas, "Colete 100 vidas extras." },
        { Conquista.VencerEmTempoRecorde, "Vença em até 3 minutos." },
        { Conquista.SemBandeiras, "Vença sem marcar nenhuma bandeira." },
        { Conquista.VencerSemPerderVidas, "Vença sem perder vidas." },
        { Conquista.CacadorDeCoracoes, "Recolha todas as vidas do mapa." }
    };

            foreach (Conquista c in Enum.GetValues(typeof(Conquista)))
            {
                bool desbloqueada = GerenciadorConquistas.conquistasDesbloqueadas.Contains(c);

                PictureBox pic = new PictureBox
                {
                    Width = 64,
                    Height = 64,
                    Margin = new Padding(5),
                    BackColor = desbloqueada ? Color.Transparent : Color.LightGray,
                    BorderStyle = BorderStyle.None,
                    SizeMode = PictureBoxSizeMode.CenterImage,
                    BackgroundImage = GetIconeConquista(c, desbloqueada),
                    BackgroundImageLayout = ImageLayout.Stretch
                };

                // Tooltip com descrição customizada
                ToolTip toolTip = new ToolTip();
                string descricao = descricoes.ContainsKey(c) ? descricoes[c] : "Conquista não documentada.";
                toolTip.SetToolTip(pic, $"{descricao}");

                if (tblConquistas.RowCount <= linhaAtual)
                    tblConquistas.RowCount++;

                tblConquistas.Controls.Add(pic, colunaAtual, linhaAtual);

                colunaAtual++;
                if (colunaAtual >= colunas)
                {
                    colunaAtual = 0;
                    linhaAtual++;
                }
            }
        }
    }
}
