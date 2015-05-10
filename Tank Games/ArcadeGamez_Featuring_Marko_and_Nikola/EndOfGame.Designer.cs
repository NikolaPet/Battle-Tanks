namespace ArcadeGamez_Featuring_Marko_and_Nikola
{
    partial class EndOfGame
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
            this.btnUnoMas = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.fff = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.playerName = new System.Windows.Forms.Label();
            this.score = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnUnoMas
            // 
            this.btnUnoMas.Location = new System.Drawing.Point(30, 120);
            this.btnUnoMas.Name = "btnUnoMas";
            this.btnUnoMas.Size = new System.Drawing.Size(75, 23);
            this.btnUnoMas.TabIndex = 0;
            this.btnUnoMas.Text = "Try again!";
            this.btnUnoMas.UseVisualStyleBackColor = true;
            this.btnUnoMas.Click += new System.EventHandler(this.btnUnoMas_Click);
            // 
            // btnEnd
            // 
            this.btnEnd.Location = new System.Drawing.Point(142, 120);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(75, 23);
            this.btnEnd.TabIndex = 1;
            this.btnEnd.Text = "End Game";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // fff
            // 
            this.fff.AutoSize = true;
            this.fff.Location = new System.Drawing.Point(27, 35);
            this.fff.Name = "fff";
            this.fff.Size = new System.Drawing.Size(39, 13);
            this.fff.TabIndex = 2;
            this.fff.Text = "Player:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Score:";
            // 
            // playerName
            // 
            this.playerName.AutoSize = true;
            this.playerName.Location = new System.Drawing.Point(72, 35);
            this.playerName.Name = "playerName";
            this.playerName.Size = new System.Drawing.Size(35, 13);
            this.playerName.TabIndex = 4;
            this.playerName.Text = "label3";
            // 
            // score
            // 
            this.score.AutoSize = true;
            this.score.Location = new System.Drawing.Point(72, 72);
            this.score.Name = "score";
            this.score.Size = new System.Drawing.Size(35, 13);
            this.score.TabIndex = 5;
            this.score.Text = "label4";
            // 
            // EndOfGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 179);
            this.Controls.Add(this.score);
            this.Controls.Add(this.playerName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fff);
            this.Controls.Add(this.btnEnd);
            this.Controls.Add(this.btnUnoMas);
            this.Name = "EndOfGame";
            this.Text = "EndOfGame";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUnoMas;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Label fff;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label playerName;
        private System.Windows.Forms.Label score;
    }
}