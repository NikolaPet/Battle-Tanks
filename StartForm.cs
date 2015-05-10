using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ArcadeGamez_Featuring_Marko_and_Nikola
{
    
    public partial class StartForm : Form
    {
        LinkedList<Player> players;
        Player player=null;
        public StartForm()
        {
            InitializeComponent();
           // File.Create("highscores.bin");
            lblGoodToGo.Text = "";
            btnChange.Enabled = false;
        }
        //Vkluchuvanje na Shoot The Box
        private void button1_Click(object sender, EventArgs e)
        {
            if (tbPlayerName.Text.Equals(""))
            {
                MessageBox.Show("Create a player, than play");
              
            }
            else if(player==null)
            {
                MessageBox.Show("Create a player, than play");
            }
            else
            {
                Instructions ins = new Instructions(this, player);
                ins.ShowDialog();
            }
            
            
        }
        //Vklucuvanje na Tank Battles
        private void button2_Click(object sender, EventArgs e)
        {
            if (tbPlayerName.Text.Equals(""))
            {
                MessageBox.Show("Create a player, than play");

            }
            else if (player == null)
            {
                MessageBox.Show("Create a player, than play");
            }
            else
            {
                SettingsForm setf = new SettingsForm(this, player);
                setf.ShowDialog();
            }
        }
        //Zachuvuvanje vo fajl na momentalniot igrach
        public void SaveCurrentPlayer()
        {
            bool dubs = false;
            players = SerializeList<Player>.BinaryDeserialize();
            if (players == null)
            {
                players = new LinkedList<Player>();
                
            }
            foreach (Player p in players)
            {
                if (player.GetHashCode()==p.GetHashCode())
                {
                    p.score1 = player.score1;
                    p.score2 = player.score2;
                    dubs = true;
                }
            }
            if (!dubs)
            {
                players.AddLast(player);
            }
            
            SerializeList<Player>.BinarySerialize(players);
        }

        private void bttnCreate_Click(object sender, EventArgs e)
        {
            
            if (tbPlayerName.Text.Equals(""))
            {
                MessageBox.Show("Enter your name first, than play");
            }
            else
            {
                player = new Player(tbPlayerName.Text);
                lblGoodToGo.Text = player.Name + " is ready to play!";
                tbPlayerName.Enabled = false;
                btnChange.Enabled = true;
            }
            
        }

        private void tbPlayerName_TextChanged(object sender, EventArgs e)
        {
            player = null;
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            player = null;
            tbPlayerName.Enabled = true;
            tbPlayerName.Clear();
            lblGoodToGo.Text = "";
            btnChange.Enabled = false;
        }

        private void Game1Highscores_Click(object sender, EventArgs e)
        {
            players = SerializeList<Player>.BinaryDeserialize();
            if (players != null)
            {
                List<Player> p = players.ToList<Player>();
                p = p.OrderByDescending(x => x.score1).ToList();  

                HighScoreGame1 hsg = new HighScoreGame1(p);
                hsg.ShowDialog();
            }
        }

        private void OverrallHighscore_Click(object sender, EventArgs e)
        {
            players = SerializeList<Player>.BinaryDeserialize();
            if (players != null)
            {
                List<Player> p = players.ToList<Player>();
                p = p.OrderByDescending(x => x.score1+x.score2).ToList();
                StringBuilder sb = new StringBuilder();

                HighScoreOverall hsg = new HighScoreOverall(p);
                hsg.ShowDialog();
            }
        }

        private void Game2Highscore_Click(object sender, EventArgs e)
        {
            players = SerializeList<Player>.BinaryDeserialize();
            if (players != null)
            {
                List<Player> p = players.ToList<Player>();
                p = p.OrderByDescending(x => x.score2).ToList();
                StringBuilder sb = new StringBuilder();

                HighscoreGame2 hsg = new HighscoreGame2(p);
                hsg.ShowDialog();
            }
        }
        

        
    }
    //Klasa sto gi cuva rezultatite od dvete igri
    [Serializable]
    public class Player : ISerializable
    {
        public String Name { get;set; }
        public int score1 {get;set;}
        public int score2 {get;set;}
        public Player(String Name){
            this.Name=Name;
            score1=0;
            score2=0;
        }
        //ova e za deserijalizacija
        private Player(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString("Name");
            score1 = info.GetInt32("score1");
            score2 = info.GetInt32("score2");
        }
        //ova e za serijalizacija
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("score1", score1);
            info.AddValue("score2", score2);
        }
        public string Game1ToString()
        {
            return String.Format("{0,-20}{1,-10}", Name, score1);
        }
        public string Game2ToString()
        {
            return String.Format("{0,-20}{1,-10}", Name, score2);
        }
       public string OverallToString()
        {
            return String.Format("{0,-20}{1,-10}", Name, score2 + score1);
        }
        public override int GetHashCode()
        {
            int hash = 7;
            char[] name = Name.ToCharArray(); 
            for (int i = 0; i <Name.Length; i++)
            {
                hash = hash * 31 + name[i];
            }
            return hash;
        }
    }

    class SerializeList<T>
    {
        public static void BinarySerialize(LinkedList<T> list)
        {
            using(FileStream str = File.Create("highscores.bin")){
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(str,list);
            }
        }
        public static LinkedList<T> BinaryDeserialize()
        {
            LinkedList<T> res = null;
           
                using (FileStream str = File.OpenRead("highscores.bin"))
                {
                    if (str.Length != 0)
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        res = (LinkedList<T>)bf.Deserialize(str);
                    }
                }    
            return res;
        }
    }
}
