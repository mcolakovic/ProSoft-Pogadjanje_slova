
namespace Server
{
    partial class FrmServer
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
            this.lblIgracB = new System.Windows.Forms.Label();
            this.lblIgracA = new System.Windows.Forms.Label();
            this.lblMaxBrojPokusaja = new System.Windows.Forms.Label();
            this.lblBrojPokusaja = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtRijec = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvTabela = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTabela)).BeginInit();
            this.SuspendLayout();
            // 
            // lblIgracB
            // 
            this.lblIgracB.AutoSize = true;
            this.lblIgracB.Location = new System.Drawing.Point(392, 324);
            this.lblIgracB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIgracB.Name = "lblIgracB";
            this.lblIgracB.Size = new System.Drawing.Size(0, 17);
            this.lblIgracB.TabIndex = 15;
            // 
            // lblIgracA
            // 
            this.lblIgracA.AutoSize = true;
            this.lblIgracA.Location = new System.Drawing.Point(392, 289);
            this.lblIgracA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIgracA.Name = "lblIgracA";
            this.lblIgracA.Size = new System.Drawing.Size(0, 17);
            this.lblIgracA.TabIndex = 14;
            // 
            // lblMaxBrojPokusaja
            // 
            this.lblMaxBrojPokusaja.AutoSize = true;
            this.lblMaxBrojPokusaja.Location = new System.Drawing.Point(388, 164);
            this.lblMaxBrojPokusaja.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaxBrojPokusaja.Name = "lblMaxBrojPokusaja";
            this.lblMaxBrojPokusaja.Size = new System.Drawing.Size(0, 17);
            this.lblMaxBrojPokusaja.TabIndex = 13;
            // 
            // lblBrojPokusaja
            // 
            this.lblBrojPokusaja.AutoSize = true;
            this.lblBrojPokusaja.Location = new System.Drawing.Point(388, 122);
            this.lblBrojPokusaja.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBrojPokusaja.Name = "lblBrojPokusaja";
            this.lblBrojPokusaja.Size = new System.Drawing.Size(0, 17);
            this.lblBrojPokusaja.TabIndex = 12;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(455, 38);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 28);
            this.btnStart.TabIndex = 11;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtRijec
            // 
            this.txtRijec.Location = new System.Drawing.Point(160, 41);
            this.txtRijec.Margin = new System.Windows.Forms.Padding(4);
            this.txtRijec.Name = "txtRijec";
            this.txtRijec.Size = new System.Drawing.Size(265, 22);
            this.txtRijec.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Zadata Rijec:";
            // 
            // dgvTabela
            // 
            this.dgvTabela.AllowUserToAddRows = false;
            this.dgvTabela.AllowUserToDeleteRows = false;
            this.dgvTabela.AllowUserToResizeColumns = false;
            this.dgvTabela.AllowUserToResizeRows = false;
            this.dgvTabela.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dgvTabela.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTabela.Location = new System.Drawing.Point(55, 122);
            this.dgvTabela.Margin = new System.Windows.Forms.Padding(4);
            this.dgvTabela.Name = "dgvTabela";
            this.dgvTabela.ReadOnly = true;
            this.dgvTabela.RowHeadersVisible = false;
            this.dgvTabela.RowHeadersWidth = 51;
            this.dgvTabela.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvTabela.Size = new System.Drawing.Size(271, 219);
            this.dgvTabela.TabIndex = 8;
            this.dgvTabela.SelectionChanged += new System.EventHandler(this.dgvTabela_SelectionChanged);
            // 
            // FrmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 396);
            this.Controls.Add(this.lblIgracB);
            this.Controls.Add(this.lblIgracA);
            this.Controls.Add(this.lblMaxBrojPokusaja);
            this.Controls.Add(this.lblBrojPokusaja);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtRijec);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvTabela);
            this.Name = "FrmServer";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTabela)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIgracB;
        private System.Windows.Forms.Label lblIgracA;
        private System.Windows.Forms.Label lblMaxBrojPokusaja;
        private System.Windows.Forms.Label lblBrojPokusaja;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtRijec;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvTabela;
    }
}

