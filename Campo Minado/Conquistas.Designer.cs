
namespace Campo_Minado
{
    partial class frmConquistas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tblConquistas = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // tblConquistas
            // 
            this.tblConquistas.BackColor = System.Drawing.Color.OldLace;
            this.tblConquistas.ColumnCount = 4;
            this.tblConquistas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblConquistas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblConquistas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblConquistas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblConquistas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblConquistas.Location = new System.Drawing.Point(0, 0);
            this.tblConquistas.Name = "tblConquistas";
            this.tblConquistas.RowCount = 1;
            this.tblConquistas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblConquistas.Size = new System.Drawing.Size(380, 223);
            this.tblConquistas.TabIndex = 0;
            // 
            // frmConquistas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 223);
            this.Controls.Add(this.tblConquistas);
            this.Name = "frmConquistas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Conquistas";
            this.Load += new System.EventHandler(this.frmConquistas_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblConquistas;
    }
}