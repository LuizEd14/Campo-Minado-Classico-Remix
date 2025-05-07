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
        private int linhas = 10, colunas = 10, minas = 10, tamanho = 35;
        private Button[,] botoes;
        private bool[,] temMina, revelado;
        private bool primeiraJogada = true, jogoAtivo, perdeuVida, Marcou, Ganhou = false;
        private int JogosGanhados = 0, vidasExtras = 1, vidasColetadas = 0;

        public Jogo()
        {
            InitializeComponent();
            CarregarConquistas();
            IniciarJogo();
            AtualizarLousaDeConquistas();
        }
        private async void Aguarde(int segundos)
        {
            await Task.Delay(segundos);
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
                    if (botoes[i, j] == btn)
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

        private void ReiniciarJogo()
        {
            Ganhou = false;
            jogoAtivo = true;
            perdeuVida = false;
            Marcou = false;
            pnlContainer.Controls.Clear();
            btnRosto.Image = Properties.Resources.neutral;

            botoes = new Button[linhas, colunas];
            temMina = new bool[linhas, colunas];
            revelado = new bool[linhas, colunas];

            IniciarJogo();
        }

        private void IniciarJogo()
        {
            jogoAtivo = true;
            primeiraJogada = true;
            CriarCampo();
            AjustarTabuleiro();
        }

        private void CriarCampo()
        {
            // Se houver botões existentes, remova eventos e destrua corretamente
            if (botoes != null)
            {
                foreach (Button btn in botoes)
                {
                    if (btn != null)
                    {
                        btn.MouseDown -= Botao_MouseDown;
                        pnlContainer.Controls.Remove(btn);
                        btn.Dispose(); // Libera recursos nativos do botão
                    }
                }
            }

            pnlContainer.Controls.Clear();
            botoes = new Button[linhas, colunas];
            temMina = new bool[linhas, colunas];
            revelado = new bool[linhas, colunas];

            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                {
                    Button btn = new Button();
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                    btn.Size = new Size(tamanho, tamanho);
                    btn.Location = new Point(j * tamanho, i * tamanho);
                    btn.Font = new Font("MINE-SWEEPER", 12); // Alterado para fonte padrão
                    btn.BackColor = Color.WhiteSmoke;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.MouseDown += Botao_MouseDown;
                    pnlContainer.Controls.Add(btn);
                    botoes[i, j] = btn;
                }
            }
        }

        private void AjustarTabuleiro()
        {
            pnlContainer.Size = new Size(colunas * tamanho, linhas * tamanho);
            pnlContainer.Location = new Point((pnlTabuleiro.Width - pnlContainer.ClientSize.Width) / 2,
                                               (pnlTabuleiro.Height - pnlContainer.ClientSize.Height) / 2);
        }

        private void RevelarCelula(int i, int j)
        {
            if (botoes[i, j].Tag as string == "VidaExtra" && !revelado[i, j])
            {
                vidasExtras++;
                AtualizarLabelVidas(); // Atualiza o label de vidas
                botoes[i, j].Text = "❤️";
                botoes[i, j].ForeColor = Color.Red;
                botoes[i, j].BackColor = Color.Pink;
                botoes[i, j].Enabled = false;
                revelado[i, j] = true;
                botoes[i, j].Tag = null; // Marca como já coletada
                vidasColetadas++;
                return;
            }
            if (primeiraJogada)
            {
                ColocarMinas(i, j); // Cria minas, mas evitando o primeiro clique
                primeiraJogada = false;
                cronometro.Restart(); // inicia o cronômetro
            }
            if (revelado[i, j] || botoes[i, j].Text == "🏴")
                return;

            revelado[i, j] = true;
            botoes[i, j].BackColor = Color.LightGray;

            if (temMina[i, j])
            {
                if (vidasExtras > 1)
                {
                    vidasExtras--;
                    AtualizarLabelVidas();
                    MessageBox.Show("Você perdeu uma vida! Você ainda tem " + vidasExtras + " vidas restantes.");
                    temMina[i, j] = false;
                    botoes[i, j].BackColor = Color.LightGray;
                    botoes[i, j].ForeColor = Color.Yellow;
                    botoes[i, j].Text = "🔥";
                    revelado[i, j] = true;
                    perdeuVida = true;
                    return;
                }
                else
                {
                    jogoAtivo = false;
                    JogosGanhados = 0;
                    botoes[i, j].BackColor = Color.Red;
                    botoes[i, j].Text = "*";
                    RevelarTudo();
                    try
                    {
                        somKaboom.Play();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao tocar som: " + ex.Message);
                    }
                    MessageBox.Show("Você perdeu!");
                    return;
                }
            }

            int minasAoRedor = ContarMinasAoRedor(i, j);
            if (minasAoRedor > 0)
            {
                botoes[i, j].Text = minasAoRedor.ToString();
                botoes[i, j].ForeColor = CorDoNumero(minasAoRedor);
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
            if (jogoAtivo && VerificarVitoria())
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
                VerificarConquistas();
                AtualizarLousaDeConquistas();

                MessageBox.Show("Parabéns! Você venceu!");
            }
        }

        private void RevelarTudo()
        {
            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                {
                    if (temMina[i, j])
                    {
                        botoes[i, j].Text = "*";
                        botoes[i, j].BackColor = Color.LightPink;
                    }
                    botoes[i, j].Enabled = false;
                }
            }
            LimparRecursos();
        }

        private int ContarMinasAoRedor(int i, int j)
        {
            int count = 0;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int ni = i + x, nj = j + y;
                    if (ni >= 0 && ni < linhas && nj >= 0 && nj < colunas && temMina[ni, nj])
                        count++;
                }
            }
            return count;
        }

        private void MarcarCelula(int i, int j)
        {
            if (!revelado[i, j])
            {
                botoes[i, j].Text = botoes[i, j].Text == "" ? "🏴" : "";
            }
        }

        private void AtualizarLabelVidas()
        {
            lblVida.Text = $"{vidasExtras}";
        }

        private void ColocarMinas(int? excluirI = null, int? excluirJ = null)
        {
            Random rand = new Random();
            int colocadas = 0;

            while (colocadas < minas)
            {
                int linha = rand.Next(linhas);
                int coluna = rand.Next(colunas);

                if (temMina[linha, coluna] || (excluirI.HasValue && excluirJ.HasValue &&
                    Math.Abs(linha - excluirI.Value) <= 1 && Math.Abs(coluna - excluirJ.Value) <= 1))
                    continue;

                temMina[linha, coluna] = true;
                colocadas++;
            }
        }

        private bool VerificarVitoria()
        {
            for (int i = 0; i < linhas; i++)
                for (int j = 0; j < colunas; j++)
                    if (!temMina[i, j] && !revelado[i, j])
                        return false;
            return true;
        }

        private Color CorDoNumero(int numero)
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

        enum Dificuldade { Facil, Medio, Dificil }
        enum Conquista { PrimeiraVitoria, VencerSemPerderVidas, Recolher100Vidas, VencerEmTempoRecorde, CacadorDeCoracoes, Sobrevivente, SemBandeiras }


        private void VerificarConquistas()
        {
            cronometro.Stop(); // para o cronômetro

            TimeSpan tempoTotal = cronometro.Elapsed;
            TimeSpan tempoLimite = TimeSpan.FromSeconds(180); // exemplo: 3 minuto para "Tempo Recorde"

            // Primeira vitória
            if (!conquistasDesbloqueadas.Contains(Conquista.PrimeiraVitoria))
            {
                conquistasDesbloqueadas.Add(Conquista.PrimeiraVitoria);
                somVitoria.Play();
                MessageBox.Show("Conquista desbloqueada: Primeira Vitória! 🎉");
                SalvarConquistas();
            }

            // Vencer 10 vezes
            if (JogosGanhados == 10 && !conquistasDesbloqueadas.Contains(Conquista.Sobrevivente))
            {
                conquistasDesbloqueadas.Add(Conquista.Sobrevivente);
                somVitoria.Play();
                MessageBox.Show("Conquista desbloqueada: Sobrevivente! 🛡");
                SalvarConquistas();
            }

            // Mais de 100 vidas
            if ( vidasExtras >= 100 && !conquistasDesbloqueadas.Contains(Conquista.Recolher100Vidas))
            {
                conquistasDesbloqueadas.Add(Conquista.Recolher100Vidas);
                somVitoria.Play();
                MessageBox.Show("Conquista desbloqueada: Muitos Corações! 🎉");
                SalvarConquistas();
            }

            // Tempo Recorde de 3 minutos
            if (tempoTotal <= tempoLimite && !conquistasDesbloqueadas.Contains(Conquista.VencerEmTempoRecorde))
            {
                conquistasDesbloqueadas.Add(Conquista.VencerEmTempoRecorde);
                somVitoria.Play();
                MessageBox.Show("Conquista desbloqueada: Tempo Recorde! ⏱");
                SalvarConquistas();
            }

            // Sem Bandeiras
            if (!Marcou && !conquistasDesbloqueadas.Contains(Conquista.SemBandeiras))
            {
                conquistasDesbloqueadas.Add(Conquista.SemBandeiras);
                somVitoria.Play();
                MessageBox.Show("Conquista desbloqueada: Sem Bandeiras! 🏴");
                SalvarConquistas();
            }

            // Vencer sem perder vidas
            if (!perdeuVida && !conquistasDesbloqueadas.Contains(Conquista.VencerSemPerderVidas))
            {
                conquistasDesbloqueadas.Add(Conquista.VencerSemPerderVidas);
                somVitoria.Play();
                MessageBox.Show("Conquista desbloqueada: Sem Perder Vidas! ❤️🛡");
                SalvarConquistas();
            }

            // Recolher todas as vidas extras
            if (vidasColetadas >= vidasExtras && !conquistasDesbloqueadas.Contains(Conquista.CacadorDeCoracoes))
            {
                conquistasDesbloqueadas.Add(Conquista.CacadorDeCoracoes);
                somVitoria.Play();
                MessageBox.Show("Conquista desbloqueada: Caçador de Vidas! ❤️");
                SalvarConquistas();
            }
        }

        private void LimparRecursos()
        {
            // Dispose de objetos que implementam IDisposable
            somVitoria.Dispose();
            somKaboom.Dispose();

            // Limpa os controles do painel de conquistas
            pnlConquistas.Controls.Clear();

            // Força a coleta de lixo
            GC.Collect();
        }

        private void SalvarConquistas()
        {
            using (StreamWriter writer = new StreamWriter("conquistas.txt"))
            {
                foreach (var conquista in conquistasDesbloqueadas)
                {
                    writer.WriteLine(conquista.ToString());
                }
            }
        }

        private void CarregarConquistas()
        {
            if (File.Exists("conquistas.txt"))
            {
                using (StreamReader reader = new StreamReader("conquistas.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (Enum.TryParse(line, out Conquista conquista))
                        {
                            conquistasDesbloqueadas.Add(conquista);
                        }
                    }
                }
            }
        }

        private void AtualizarLousaDeConquistas()
        {
            pnlConquistas.Controls.Clear();
            pnlConquistas.AutoScroll = true;
            pnlConquistas.HorizontalScroll.Enabled = true;
            pnlConquistas.VerticalScroll.Visible = false;

            int tamanho = 65;
            int margem = 5;
            int x = 10;

            ToolTip tooltip = new ToolTip();

            foreach (Conquista conquista in Enum.GetValues(typeof(Conquista)))
            {
                if (conquistasDesbloqueadas.Contains(conquista))
                {
                    PictureBox pic = new PictureBox();
                    pic.Size = new Size(tamanho, tamanho);
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    pic.Location = new Point(x, 10);
                    pic.BackColor = Color.Transparent;

                    switch (conquista)
                    {
                        case Conquista.Recolher100Vidas:
                            pic.Image = Properties.Resources.Conquista_Coracao;
                            tooltip.SetToolTip(pic, "Pegue mais de 100 corações.");
                            break;
                        case Conquista.Sobrevivente:
                            pic.Image = Properties.Resources.Conquista_Capacete;
                            tooltip.SetToolTip(pic, "Vença 10 vezes sem perder.");
                            break;
                        case Conquista.SemBandeiras:
                            pic.Image = Properties.Resources.Conquista_Sem_Bandeiras;

                            tooltip.SetToolTip(pic, "Não utilize bandeiras em uma rodada");
                            break;
                        case Conquista.PrimeiraVitoria:
                            pic.Image = Properties.Resources.Conquista_Primeira_Vitoria;
                            tooltip.SetToolTip(pic, "Ganhe pela primeira vez");
                            break;
                        case Conquista.VencerSemPerderVidas:
                            pic.Image = Properties.Resources.Conquista_Sem_Vidas_Perdas;
                            tooltip.SetToolTip(pic, "Não erre seu passo!");
                            break;
                        case Conquista.CacadorDeCoracoes:
                            pic.Image = Properties.Resources.Conquista_Todas_Vidas;
                            tooltip.SetToolTip(pic, "Recolha todos os corações do mapa.");
                            break;
                        case Conquista.VencerEmTempoRecorde:
                            pic.Image = Properties.Resources.Conquista_Tempo_Recorde;
                            tooltip.SetToolTip(pic, "Vença em menos de 3 minutos.");
                            break;
                        default:
                            pic.Image = Properties.Resources.locked;
                            break;
                    }

                    pnlConquistas.Controls.Add(pic);
                    x += tamanho + margem;
                }
            }
        }
    }
}
