namespace Breakout
{
	partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.igraVrijeme = new System.Windows.Forms.Timer(this.components);
            this.rezultatTekst = new System.Windows.Forms.Label();
            this.ploca = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelVrijeme = new System.Windows.Forms.Label();
            this.textPocetniUdarac = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timerGlavni = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ploca)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // igraVrijeme
            // 
            this.igraVrijeme.Interval = 20;
            this.igraVrijeme.Tick += new System.EventHandler(this.igraTimerEvent);
            // 
            // rezultatTekst
            // 
            this.rezultatTekst.AutoSize = true;
            this.rezultatTekst.BackColor = System.Drawing.Color.LavenderBlush;
            this.rezultatTekst.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.rezultatTekst.Location = new System.Drawing.Point(3, 121);
            this.rezultatTekst.Name = "rezultatTekst";
            this.rezultatTekst.Size = new System.Drawing.Size(86, 31);
            this.rezultatTekst.TabIndex = 3;
            this.rezultatTekst.Text = "label1";
            // 
            // ploca
            // 
            this.ploca.BackColor = System.Drawing.Color.Thistle;
            this.ploca.Image = global::Breakout.Properties.Resources.ploca;
            this.ploca.Location = new System.Drawing.Point(301, 527);
            this.ploca.Name = "ploca";
            this.ploca.Size = new System.Drawing.Size(142, 20);
            this.ploca.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ploca.TabIndex = 1;
            this.ploca.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.AllowDrop = true;
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Enabled = false;
            this.splitContainer1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.splitContainer1.Location = new System.Drawing.Point(-1, -3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.LavenderBlush;
            this.splitContainer1.Panel1.Controls.Add(this.labelVrijeme);
            this.splitContainer1.Panel1.Controls.Add(this.textPocetniUdarac);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.rezultatTekst);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Thistle;
            this.splitContainer1.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.splitContainer1.Panel2.Controls.Add(this.ploca);
            this.splitContainer1.Panel2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.splitContainer1.Size = new System.Drawing.Size(903, 597);
            this.splitContainer1.SplitterDistance = 165;
            this.splitContainer1.TabIndex = 4;
            // 
            // labelVrijeme
            // 
            this.labelVrijeme.AutoSize = true;
            this.labelVrijeme.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelVrijeme.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelVrijeme.Location = new System.Drawing.Point(7, 46);
            this.labelVrijeme.Name = "labelVrijeme";
            this.labelVrijeme.Size = new System.Drawing.Size(115, 44);
            this.labelVrijeme.TabIndex = 6;
            this.labelVrijeme.Text = "00:00";
            this.labelVrijeme.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textPocetniUdarac
            // 
            this.textPocetniUdarac.BackColor = System.Drawing.Color.LavenderBlush;
            this.textPocetniUdarac.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textPocetniUdarac.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textPocetniUdarac.ForeColor = System.Drawing.SystemColors.InfoText;
            this.textPocetniUdarac.Location = new System.Drawing.Point(9, 378);
            this.textPocetniUdarac.Multiline = true;
            this.textPocetniUdarac.Name = "textPocetniUdarac";
            this.textPocetniUdarac.ReadOnly = true;
            this.textPocetniUdarac.Size = new System.Drawing.Size(149, 191);
            this.textPocetniUdarac.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 36);
            this.label1.TabIndex = 5;
            this.label1.Text = "Time:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerGlavni
            // 
            this.timerGlavni.Interval = 1000;
            this.timerGlavni.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(901, 580);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximumSize = new System.Drawing.Size(919, 627);
            this.MinimumSize = new System.Drawing.Size(919, 627);
            this.Name = "Form1";
            this.Text = "Breakout";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.klikPloca);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.klikPlocaPusten);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pocetniUdarac);
            ((System.ComponentModel.ISupportInitialize)(this.ploca)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.PictureBox ploca;
        private System.Windows.Forms.Timer igraVrijeme;
        private System.Windows.Forms.Label rezultatTekst;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.TextBox textPocetniUdarac;
        private System.Windows.Forms.Label labelVrijeme;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerGlavni;
    }
}

