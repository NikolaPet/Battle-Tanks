namespace ArcadeGamez_Featuring_Marko_and_Nikola
{
    partial class ShootTheBox
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
            this.lbl11 = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.gameOver = new System.Windows.Forms.Label();
            this.lblscore = new System.Windows.Forms.Label();
            this.playerName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl11
            // 
            this.lbl11.AutoSize = true;
            this.lbl11.BackColor = System.Drawing.Color.Transparent;
            this.lbl11.Location = new System.Drawing.Point(1, 0);
            this.lbl11.Name = "lbl11";
            this.lbl11.Size = new System.Drawing.Size(39, 13);
            this.lbl11.TabIndex = 0;
            this.lbl11.Text = "Player:";
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.BackColor = System.Drawing.Color.Transparent;
            this.lbl1.Location = new System.Drawing.Point(93, 0);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(38, 13);
            this.lbl1.TabIndex = 1;
            this.lbl1.Text = "Score:";
            // 
            // gameOver
            // 
            this.gameOver.AutoSize = true;
            this.gameOver.BackColor = System.Drawing.Color.Transparent;
            this.gameOver.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameOver.ForeColor = System.Drawing.Color.Maroon;
            this.gameOver.Location = new System.Drawing.Point(235, 0);
            this.gameOver.Name = "gameOver";
            this.gameOver.Size = new System.Drawing.Size(31, 13);
            this.gameOver.TabIndex = 2;
            this.gameOver.Text = "hola";
            // 
            // lblscore
            // 
            this.lblscore.AutoSize = true;
            this.lblscore.BackColor = System.Drawing.Color.Transparent;
            this.lblscore.Location = new System.Drawing.Point(137, 0);
            this.lblscore.Name = "lblscore";
            this.lblscore.Size = new System.Drawing.Size(35, 13);
            this.lblscore.TabIndex = 3;
            this.lblscore.Text = "label1";
            // 
            // playerName
            // 
            this.playerName.AutoSize = true;
            this.playerName.BackColor = System.Drawing.Color.Transparent;
            this.playerName.Location = new System.Drawing.Point(38, 0);
            this.playerName.Name = "playerName";
            this.playerName.Size = new System.Drawing.Size(35, 13);
            this.playerName.TabIndex = 4;
            this.playerName.Text = "label1";
            // 
            // ShootTheBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 184);
            this.Controls.Add(this.playerName);
            this.Controls.Add(this.lblscore);
            this.Controls.Add(this.gameOver);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.lbl11);
            this.Name = "ShootTheBox";
            this.Text = "Shoot The Box";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form2_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form2_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form2_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form2_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl11;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label gameOver;
        private System.Windows.Forms.Label lblscore;
        private System.Windows.Forms.Label playerName;




    }
}