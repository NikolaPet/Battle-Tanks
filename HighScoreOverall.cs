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
    public partial class HighScoreOverall : Form
    {
        public HighScoreOverall()
        {
            InitializeComponent();
        }
        public HighScoreOverall(List<Player> players)
        {
           
            InitializeComponent();
            foreach (Player p in players)
            {
                listBox1.Items.Add(p.OverallToString());
            }
        }
    }
}
