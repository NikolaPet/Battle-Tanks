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
    public partial class EndOfGame : Form
    {
        ShootTheBox f;
        Player p;
        public bool repeat;
        public EndOfGame()
        {
            
            InitializeComponent();
        }
        public EndOfGame(ShootTheBox ff,Player p)
        {

            InitializeComponent(); 
            f = ff;
            this.p=p;
            score.Text = p.score1.ToString();
            playerName.Text = p.Name;
        }

        private void btnUnoMas_Click(object sender, EventArgs e)
        {
            f.newGame();
            this.Close();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            f.SaveCurrentPlayer();
            this.Close();
            f.Close();
        }
    }
}
