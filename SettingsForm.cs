using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ArcadeGamez_Featuring_Marko_and_Nikola
{
    
    public partial class SettingsForm : Form
    {
        Form1 form;
        StartForm f;
        Player p;
        public SettingsForm()
        {
            InitializeComponent();
        }
        public SettingsForm(StartForm f,Player p)
        {
            InitializeComponent();
            label1.BackColor = Color.Transparent;
            radioButton1.BackColor = Color.Transparent;
            radioButton2.BackColor = Color.Transparent;
            radioButton3.BackColor = Color.Transparent;
            radioButton4.BackColor = Color.Transparent;
            radioButton5.BackColor = Color.Transparent;
            pictureBox1.BackColor = Color.Transparent;
            pictureBox2.BackColor = Color.Transparent;
            this.f = f;
            this.p = p;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
                form = new Form1(Difficulty.EASY, false,f,p);
            if(radioButton2.Checked == true)
                form = new Form1(Difficulty.NORMAL, false, f, p);
            if(radioButton3.Checked == true)
                form = new Form1(Difficulty.HARD, false, f, p);
            if(radioButton4.Checked == true)
                form = new Form1(Difficulty.IMPOSSIBLE, false, f, p);
            if (radioButton5.Checked == true)
                form = new Form1(Difficulty.ARCADE, true, f, p);
            Visible = false;
            form.ShowDialog();
            DialogResult dr = form.DialogResult;
            Visible = true;
            //if (dr == DialogResult.Cancel || dr == DialogResult.OK || dr == DialogResult.Abort)
              //  this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InstructionsForm iform = new InstructionsForm();
            iform.ShowDialog();
        }
    }
}
