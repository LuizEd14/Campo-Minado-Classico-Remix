using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Campo_Minado
{
    public partial class Jogo : Form
    {
        private HashSet<Conquista> conquistasDesbloqueadas = new HashSet<Conquista>();
        private SoundPlayer somVitoria = new SoundPlayer(Properties.Resources.Vitória_1);
        private SoundPlayer somKaboom = new SoundPlayer(Properties.Resources.Kaboom_1);
        private Stopwatch cronometro = new Stopwatch();
        public int linhas, colunas, minas;
        public bool primeiraJogada = true, jogoAtivo, perdeuVida, Marcou, Ganhou = false;
        public int JogosGanhados = 0, vidasExtras = 1, vidasColetadas = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            GerenciadorConquistas.CarregarConquistas();
        }

        public Jogo()
        {
            InitializeComponent();
            ReiniciarJogo();
            /* AtualizarLousaDeConquistas(); */
        }

        private async void Aguarde(int segundos)
        {
            await Task.Delay(segundos);
        }

        private void Jogo_ResizeEnd(object sender, EventArgs e)
        {
            UIHelper.CentralizarPainel(pnlContainer, pnlTabuleiro);
        }

        private void facilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DefinirDificuldade(Dificuldade.Facil);
        }

        private void medioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DefinirDificuldade(Dificuldade.Medio);
        }

        private void dificilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DefinirDificuldade(Dificuldade.Dificil);
        }

        private void btnRosto_Click(object sender, EventArgs e)
        {
            if (jogoAtivo)
            {
                LimparRecursos();
            }
            ReiniciarJogo();
        }

        private void btnRosto_MouseEnter(object sender, EventArgs e)
        {
            if (jogoAtivo == true)
            {
                btnRosto.Image = Properties.Resources.happy;
            }
        }

        private async void MicroReacao()
        {
            switch (jogoAtivo)
            {
                case true:
                    btnRosto.Image = Properties.Resources.wow;
                    await Task.Delay(50);
                    btnRosto.Image = Properties.Resources.neutral;
                    return;

                case false:
                    switch (Ganhou)
                    {
                        case true:
                            btnRosto.Image = Properties.Resources.cool;
                            return;

                        case false:
                            btnRosto.Image = Properties.Resources.dead;
                            return;
                    }
                    return;
            }
        }

        private void btnRosto_MouseLeave(object sender, EventArgs e)
        {
            if (jogoAtivo == true)
            {
                btnRosto.Image = Properties.Resources.neutral;
            }
        }

        private void Botao_MouseDown(object sender, MouseEventArgs e)
        {
            if (!jogoAtivo) return;

            Button btn = sender as Button;

            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                {
                    if (CampoMinado.Botoes[i, j] == btn)
                    {
                        if (e.Button == MouseButtons.Left)
                            RevelarCelula(i, j);
                        else if (e.Button == MouseButtons.Right)
                            MarcarCelula(i, j);
                    }
                }
            }
            MicroReacao();
        }

        private void DefinirDificuldade(Dificuldade dificuldade)
        {
            switch (dificuldade)
            {
                case Dificuldade.Facil: linhas = 9; colunas = 9; minas = 10; break;
                case Dificuldade.Medio: linhas = 16; colunas = 16; minas = 40; break;
                case Dificuldade.Dificil: linhas = 16; colunas = 30; minas = 99; break;
            }
            ReiniciarJogo();
        }

        public void ReiniciarJogo()
        {
            Ganhou = false;
            jogoAtivo = true;
            perdeuVida = false;
            Marcou = false;

            // 1) inicializa dimenões e matrizes estáticas
            CampoMinado.Inicializar(linhas, colunas, minas, vidasExtras);

            // 2) limpa o container e pinta o rosto
            pnlContainer.Controls.Clear();
            btnRosto.Image = Properties.Resources.neutral;

            // 3) centraliza o painel dentro do tabuleiro (passe container, depois painel)
            UIHelper.CentralizarPainel(pnlContainer, pnlTabuleiro);

            // 4) dispara a criação dos botões
            IniciarJogo();
        }

        private void IniciarJogo()
        {
            primeiraJogada = true;
            CampoMinado.CriarCampo(pnlContainer, 35, Botao_MouseDown);
            CampoMinado.ColocarVidasExtras(minas / 5);
            UIHelper.CentralizarPainel(pnlContainer, pnlTabuleiro);   
        }

        private void RevelarCelula(int i, int j)
        {
            if (CampoMinado.Botoes[i, j].Tag as string == "VidaExtra" && !CampoMinado.Revelado[i, j])
            {
                vidasExtras++;
                AtualizarLabelVidas(); // Atualiza o label de vidas
                CampoMinado.Botoes[i, j].Text = "❤️";
                CampoMinado.Botoes[i, j].ForeColor = Color.Red;
                CampoMinado.Botoes[i, j].BackColor = Color.Pink;
                CampoMinado.Botoes[i, j].Enabled = false;
                CampoMinado.Revelado[i, j] = true;
                CampoMinado.Botoes[i, j].Tag = null; // Marca como já coletada
                vidasColetadas++;
                return;
            }
            if (primeiraJogada)
            {
                CampoMinado.ColocarMinas(i, j); // Cria minas, mas evitando o primeiro clique
                primeiraJogada = false;
                cronometro.Restart(); // inicia o cronômetro
            }
            if (CampoMinado.Revelado[i, j] || CampoMinado.Botoes[i, j].Text == "🏴")
                return;

            CampoMinado.Revelado[i, j] = true;
            CampoMinado.Botoes[i, j].BackColor = Color.LightGray;

            if (CampoMinado.TemMina[i, j])
            {
                if (vidasExtras > 1)
                {
                    vidasExtras--;
                    AtualizarLabelVidas();
                    MessageBox.Show("Você perdeu uma vida! Você ainda tem " + vidasExtras + " vidas restantes.");
                    CampoMinado.TemMina[i, j] = false;
                    CampoMinado.Botoes[i, j].BackColor = Color.LightGray;
                    CampoMinado.Botoes[i, j].ForeColor = Color.Yellow;
                    CampoMinado.Botoes[i, j].Text = "🔥";
                    CampoMinado.Revelado[i, j] = true;
                    perdeuVida = true;
                    return;
                }
                else
                {
                    jogoAtivo = false;
                    JogosGanhados = 0;
                    CampoMinado.Botoes[i, j].BackColor = Color.Red;
                    CampoMinado.Botoes[i, j].Text = "💣";
                    CampoMinado.Botoes[i, j].ForeColor = Color.Black;
                    try
                    {
                        somKaboom.Play();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao tocar som: " + ex.Message);
                    }
                    RevelarTudo();
                    MessageBox.Show("Você perdeu!");
                    return;
                }
            }

            int minasAoRedor = CampoMinado.ContarMinasAoRedor(i, j);
            if (minasAoRedor > 0)
            {
                CampoMinado.Botoes[i, j].Text = minasAoRedor.ToString();
                CampoMinado.Botoes[i, j].ForeColor = UIHelper.CorDoNumero(minasAoRedor);
            }
            else
            {
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        int ni = i + x;
                        int nj = j + y;
                        if (ni >= 0 && ni < linhas && nj >= 0 && nj < colunas)
                            RevelarCelula(ni, nj);
                    }
                }
            }
            if (jogoAtivo && CampoMinado.VerificarVitoria())
            {
                try
                {
                    somVitoria.Play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao tocar som de vitória: " + ex.Message);
                }
                Aguarde(1000);
                Ganhou = true;
                jogoAtivo = false;
                JogosGanhados++;
                btnRosto.Image = Properties.Resources.cool;
                RevelarTudo();

                // Verifica as conquistas
                LimparRecursos();

                MessageBox.Show("Parabéns! Você venceu!");
                GerenciadorConquistas.VerificarConquistas(perdeuVida, Marcou, vidasExtras, vidasColetadas, JogosGanhados, cronometro.Elapsed);
            }
        }

        private void RevelarTudo()
        {
            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                {
                    if (CampoMinado.TemMina[i, j])
                    {
                        CampoMinado.Botoes[i, j].Text = "💣";
                        CampoMinado.Botoes[i, j].BackColor = Color.LightPink;
                        CampoMinado.Botoes[i, j].ForeColor = Color.Black;
                    }
                    CampoMinado.Botoes[i, j].Enabled = false;
                }
            }
            LimparRecursos();
        }


        private void MarcarCelula(int i, int j)
        {
            if (!CampoMinado.Revelado[i, j])
            {
                CampoMinado.Botoes[i, j].Text = CampoMinado.Botoes[i, j].Text == "" ? "🏴" : "";
            }
        }

        private void btnConquistas_Click(object sender, EventArgs e)
        {
            frmConquistas Conquistas = new frmConquistas();
            Conquistas.Show();
        }

        private void AtualizarLabelVidas()
        {
            lblVida.Text = $"{vidasExtras}";
        }

        enum Dificuldade { Facil, Medio, Dificil }
        enum Conquista { PrimeiraVitoria, VencerSemPerderVidas, Recolher100Vidas, VencerEmTempoRecorde, CacadorDeCoracoes, Sobrevivente, SemBandeiras }

        private void LimparRecursos()
        {
            // Dispose de objetos que implementam IDisposable
            somVitoria.Dispose();
            somKaboom.Dispose();

            /*Limpa os controles do painel de conquistas
            pnlConquistas.Controls.Clear();*/

            // Força a coleta de lixo
            GC.Collect();
        }
    }
}
