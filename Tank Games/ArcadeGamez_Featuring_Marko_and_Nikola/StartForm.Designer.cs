namespace ArcadeGamez_Featuring_Marko_and_Nikola
{
    partial class StartForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartForm));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Game1Highscore = new System.Windows.Forms.Button();
            this.Game2Highscore = new System.Windows.Forms.Button();
            this.OverrallHighscore = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnChange = new System.Windows.Forms.Button();
            this.lblGoodToGo = new System.Windows.Forms.Label();
            this.bttnCreate = new System.Windows.Forms.Button();
            this.tbPlayerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(20, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(219, 200);
            this.button1.TabIndex = 0;
            this.button1.Text = "Shoot The Box";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(447, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(219, 200);
            this.button2.TabIndex = 1;
            this.button2.TabStop = false;
            this.button2.Text = "Tank Battles";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Game1Highscore
            // 
            this.Game1Highscore.Location = new System.Drawing.Point(12, 254);
            this.Game1Highscore.Name = "Game1Highscore";
            this.Game1Highscore.Size = new System.Drawing.Size(219, 35);
            this.Game1Highscore.TabIndex = 2;
            this.Game1Highscore.Text = "Shoot The Box Highscores";
            this.Game1Highscore.UseVisualStyleBackColor = true;
            this.Game1Highscore.Click += new System.EventHandler(this.Game1Highscores_Click);
            // 
            // Game2Highscore
            // 
            this.Game2Highscore.Location = new System.Drawing.Point(447, 254);
            this.Game2Highscore.Name = "Game2Highscore";
            this.Game2Highscore.Size = new System.Drawing.Size(219, 35);
            this.Game2Highscore.TabIndex = 3;
            this.Game2Highscore.Text = "Tank Battles Arcade Mode Highscores";
            this.Game2Highscore.UseVisualStyleBackColor = true;
            this.Game2Highscore.Click += new System.EventHandler(this.Game2Highscore_Click);
            // 
            // OverrallHighscore
            // 
            this.OverrallHighscore.Location = new System.Drawing.Point(238, 254);
            this.OverrallHighscore.Name = "OverrallHighscore";
            this.OverrallHighscore.Size = new System.Drawing.Size(203, 35);
            this.OverrallHighscore.TabIndex = 8;
            this.OverrallHighscore.Text = "Overall Highscores";
            this.OverrallHighscore.UseVisualStyleBackColor = true;
            this.OverrallHighscore.Click += new System.EventHandler(this.OverrallHighscore_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btnChange);
            this.groupBox1.Controls.Add(this.lblGoodToGo);
            this.groupBox1.Controls.Add(this.bttnCreate);
            this.groupBox1.Controls.Add(this.tbPlayerName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(238, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(203, 200);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Make A New Character";
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(7, 153);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(188, 32);
            this.btnChange.TabIndex = 5;
            this.btnChange.Text = "Change Player";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // lblGoodToGo
            // 
            this.lblGoodToGo.AutoSize = true;
            this.lblGoodToGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGoodToGo.Location = new System.Drawing.Point(6, 28);
            this.lblGoodToGo.Name = "lblGoodToGo";
            this.lblGoodToGo.Size = new System.Drawing.Size(41, 13);
            this.lblGoodToGo.TabIndex = 4;
            this.lblGoodToGo.Text = "label2";
            // 
            // bttnCreate
            // 
            this.bttnCreate.Location = new System.Drawing.Point(7, 111);
            this.bttnCreate.Name = "bttnCreate";
            this.bttnCreate.Size = new System.Drawing.Size(188, 32);
            this.bttnCreate.TabIndex = 2;
            this.bttnCreate.Text = "Create New Player";
            this.bttnCreate.UseVisualStyleBackColor = true;
            this.bttnCreate.Click += new System.EventHandler(this.bttnCreate_Click);
            // 
            // tbPlayerName
            // 
            this.tbPlayerName.Location = new System.Drawing.Point(6, 76);
            this.tbPlayerName.Name = "tbPlayerName";
            this.tbPlayerName.Size = new System.Drawing.Size(190, 20);
            this.tbPlayerName.TabIndex = 1;
            this.tbPlayerName.TextChanged += new System.EventHandler(this.tbPlayerName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter your Name";
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(678, 302);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.OverrallHighscore);
            this.Controls.Add(this.Game2Highscore);
            this.Controls.Add(this.Game1Highscore);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "StartForm";
            this.Text = "Tank Games";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button Game1Highscore;
        private System.Windows.Forms.Button Game2Highscore;
        private System.Windows.Forms.Button OverrallHighscore;
        private System.Windows.Forms.GroupBox groupBox1;
        protected System.Windows.Forms.Button bttnCreate;
        private System.Windows.Forms.TextBox tbPlayerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblGoodToGo;
        private System.Windows.Forms.Button btnChange;
    }
}

