
using System.Windows.Forms;

namespace Campo_Minado
{
    partial class Jogo
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlTelinha = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblVida = new System.Windows.Forms.Label();
            this.pbCoracao = new System.Windows.Forms.PictureBox();
            this.btnRosto = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.facilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dificilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlConquistas = new System.Windows.Forms.Panel();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.pnlTabuleiro = new System.Windows.Forms.Panel();
            this.pnlTelinha.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCoracao)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.pnlTabuleiro.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTelinha
            // 
            this.pnlTelinha.BackColor = System.Drawing.Color.PaleTurquoise;
            this.pnlTelinha.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlTelinha.Controls.Add(this.tableLayoutPanel2);
            this.pnlTelinha.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTelinha.Location = new System.Drawing.Point(0, 0);
            this.pnlTelinha.Name = "pnlTelinha";
            this.pnlTelinha.Size = new System.Drawing.Size(716, 104);
            this.pnlTelinha.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnRosto, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pnlConquistas, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(712, 100);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblVida);
            this.panel3.Controls.Add(this.pbCoracao);
            this.panel3.Location = new System.Drawing.Point(103, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(100, 94);
            this.panel3.TabIndex = 2;
            // 
            // lblVida
            // 
            this.lblVida.AutoSize = true;
            this.lblVida.Font = new System.Drawing.Font("Comic Sans MS", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVida.Location = new System.Drawing.Point(45, 27);
            this.lblVida.Name = "lblVida";
            this.lblVida.Size = new System.Drawing.Size(35, 40);
            this.lblVida.TabIndex = 0;
            this.lblVida.Text = "1";
            // 
            // pbCoracao
            // 
            this.pbCoracao.Image = global::Campo_Minado.Properties.Resources.Coração;
            this.pbCoracao.Location = new System.Drawing.Point(12, 30);
            this.pbCoracao.Name = "pbCoracao";
            this.pbCoracao.Size = new System.Drawing.Size(37, 37);
            this.pbCoracao.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCoracao.TabIndex = 0;
            this.pbCoracao.TabStop = false;
            // 
            // btnRosto
            // 
            this.btnRosto.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnRosto.ContextMenuStrip = this.contextMenuStrip1;
            this.btnRosto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRosto.FlatAppearance.BorderSize = 0;
            this.btnRosto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRosto.Image = global::Campo_Minado.Properties.Resources.neutral;
            this.btnRosto.Location = new System.Drawing.Point(3, 3);
            this.btnRosto.Name = "btnRosto";
            this.btnRosto.Size = new System.Drawing.Size(94, 94);
            this.btnRosto.TabIndex = 0;
            this.btnRosto.UseVisualStyleBackColor = false;
            this.btnRosto.Click += new System.EventHandler(this.btnRosto_Click);
            this.btnRosto.MouseEnter += new System.EventHandler(this.btnRosto_MouseEnter);
            this.btnRosto.MouseLeave += new System.EventHandler(this.btnRosto_MouseLeave);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.facilToolStripMenuItem,
            this.medioToolStripMenuItem,
            this.dificilToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 70);
            // 
            // facilToolStripMenuItem
            // 
            this.facilToolStripMenuItem.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.facilToolStripMenuItem.Name = "facilToolStripMenuItem";
            this.facilToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.facilToolStripMenuItem.Text = "Fácil";
            this.facilToolStripMenuItem.Click += new System.EventHandler(this.facilToolStripMenuItem_Click);
            // 
            // medioToolStripMenuItem
            // 
            this.medioToolStripMenuItem.BackColor = System.Drawing.Color.Khaki;
            this.medioToolStripMenuItem.Name = "medioToolStripMenuItem";
            this.medioToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.medioToolStripMenuItem.Text = "Médio";
            this.medioToolStripMenuItem.Click += new System.EventHandler(this.medioToolStripMenuItem_Click);
            // 
            // dificilToolStripMenuItem
            // 
            this.dificilToolStripMenuItem.BackColor = System.Drawing.Color.PaleVioletRed;
            this.dificilToolStripMenuItem.Name = "dificilToolStripMenuItem";
            this.dificilToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.dificilToolStripMenuItem.Text = "Difícil";
            this.dificilToolStripMenuItem.Click += new System.EventHandler(this.dificilToolStripMenuItem_Click);
            // 
            // pnlConquistas
            // 
            this.pnlConquistas.AutoScroll = true;
            this.pnlConquistas.BackColor = System.Drawing.Color.OldLace;
            this.pnlConquistas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlConquistas.Location = new System.Drawing.Point(209, 3);
            this.pnlConquistas.Name = "pnlConquistas";
            this.pnlConquistas.Size = new System.Drawing.Size(500, 94);
            this.pnlConquistas.TabIndex = 3;
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.Gray;
            this.pnlContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlContainer.Location = new System.Drawing.Point(207, 95);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(344, 250);
            this.pnlContainer.TabIndex = 2;
            // 
            // pnlTabuleiro
            // 
            this.pnlTabuleiro.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlTabuleiro.AutoScroll = true;
            this.pnlTabuleiro.BackColor = System.Drawing.Color.Gray;
            this.pnlTabuleiro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlTabuleiro.Controls.Add(this.pnlContainer);
            this.pnlTabuleiro.Location = new System.Drawing.Point(12, 126);
            this.pnlTabuleiro.Name = "pnlTabuleiro";
            this.pnlTabuleiro.Size = new System.Drawing.Size(692, 444);
            this.pnlTabuleiro.TabIndex = 3;
            // 
            // Jogo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 582);
            this.Controls.Add(this.pnlTabuleiro);
            this.Controls.Add(this.pnlTelinha);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Jogo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Campo Minado Clássico Remix";
            this.pnlTelinha.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCoracao)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.pnlTabuleiro.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTelinha;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Button btnRosto;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem facilToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dificilToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel3;
        private Label lblVida;
        private PictureBox pbCoracao;
        private Panel pnlConquistas;
        private Panel pnlTabuleiro;
    }
}

