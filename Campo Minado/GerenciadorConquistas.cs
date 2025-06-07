using System;
using CampoMinado;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace Campo_Minado
{
    public static class GerenciadorConquistas
    {
        public static HashSet<Conquista> conquistasDesbloqueadas = new HashSet<Conquista>();

        public static void VerificarConquistas(
                bool perdeuVida,
                bool marcou,
                int vidasExtras,
                int vidasColetadas,
                int jogosGanhados,
                TimeSpan tempoTotal)
            {
                TimeSpan tempoLimite = TimeSpan.FromSeconds(180); // 3 minutos
                SoundPlayer somVitoria = new SoundPlayer(Properties.Resources.Vitória_1);

            // Primeira vitória
            if (!conquistasDesbloqueadas.Contains(Conquista.PrimeiraVitoria))
            {
                conquistasDesbloqueadas.Add(Conquista.PrimeiraVitoria);
                somVitoria.Play();
                MessageBox.Show("Conquista desbloqueada: Primeira Vitória! 🎉");
            }

            // Vencer 10 vezes
            if (jogosGanhados == 10 && !conquistasDesbloqueadas.Contains(Conquista.Sobrevivente))
            {
                conquistasDesbloqueadas.Add(Conquista.Sobrevivente);
                somVitoria.Play();
                MessageBox.Show("Conquista desbloqueada: Sobrevivente! 🛡");
            }

            // Mais de 100 vidas
            if (vidasExtras >= 100 && !conquistasDesbloqueadas.Contains(Conquista.Recolher100Vidas))
            {
                conquistasDesbloqueadas.Add(Conquista.Recolher100Vidas);
                somVitoria.Play();
                MessageBox.Show("Conquista desbloqueada: Muitos Corações! 🎉");
            }

            // Tempo Recorde de 3 minutos
            if (tempoTotal <= tempoLimite && !conquistasDesbloqueadas.Contains(Conquista.VencerEmTempoRecorde))
            {
                conquistasDesbloqueadas.Add(Conquista.VencerEmTempoRecorde);
                somVitoria.Play();
                MessageBox.Show("Conquista desbloqueada: Tempo Recorde! ⏱");
            }

            // Sem Bandeiras
            if (!marcou && !conquistasDesbloqueadas.Contains(Conquista.SemBandeiras))
            {
                conquistasDesbloqueadas.Add(Conquista.SemBandeiras);
                somVitoria.Play();
                MessageBox.Show("Conquista desbloqueada: Sem Bandeiras! 🏴");
            }

            // Vencer sem perder vidas
            if (!perdeuVida && !conquistasDesbloqueadas.Contains(Conquista.VencerSemPerderVidas))
            {
                conquistasDesbloqueadas.Add(Conquista.VencerSemPerderVidas);
                somVitoria.Play();
                MessageBox.Show("Conquista desbloqueada: Sem Perder Vidas! ❤️🛡");
            }

            // Recolher todas as vidas extras
            if (vidasColetadas >= vidasExtras-1 && !conquistasDesbloqueadas.Contains(Conquista.CacadorDeCoracoes))
            {
                conquistasDesbloqueadas.Add(Conquista.CacadorDeCoracoes);
                somVitoria.Play();
                MessageBox.Show("Conquista desbloqueada: Caçador de Vidas! ❤️");
            }

                SalvarConquistas();
        }

        public static void SalvarConquistas()
        {
            using (StreamWriter writer = new StreamWriter("conquistas.txt"))
            {
                foreach (var c in conquistasDesbloqueadas)
                    writer.WriteLine(c.ToString());
            }
        }

        public static void CarregarConquistas()
        {
            conquistasDesbloqueadas.Clear();
            if (!File.Exists("conquistas.txt")) return;

            foreach (var line in File.ReadAllLines("conquistas.txt"))
            {
                if (Enum.TryParse(line, out Conquista c))
                    conquistasDesbloqueadas.Add(c);
            }
        }
    }
}
