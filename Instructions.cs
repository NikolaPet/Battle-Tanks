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
    public partial class Instructions : Form
    {
        StartForm sf;
        Player p;
        public Instructions()
        {
            InitializeComponent();
        }
        public Instructions(StartForm f, Player pl)
        {
            InitializeComponent();
            sf = f;
            p = pl;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            
            ShootTheBox newGame = new ShootTheBox(sf,p);
            newGame.ShowDialog();
            this.Close();
        }
    }
}
