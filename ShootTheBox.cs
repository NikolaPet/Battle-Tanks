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
    public partial class ShootTheBox : Form
    {
        
        Timer timer;
        Shooter tank;
        int GameWidth = 13;
        int GameHeight = 16;
        int TimeInterval = 100;
        Boolean KeyWasUp = true;
        long counter;
        int level=17;
        Player player;
        StartForm f;
        bool pause = false;
        Pen pen;
        public ShootTheBox()
        {
            InitializeComponent();
        }
        public ShootTheBox(StartForm form, Player player)
        {
            InitializeComponent();
            DoubleBuffered = true;
            f = form;
            this.player = player;
            newGame();
        }
        public void newGame()
        {
            counter = 0;
            gameOver.Text = "";
            playerName.Text = player.Name;
            pen = new Pen(Color.IndianRed);
            player.score1 = 0;

            tank = new Shooter(GameWidth, GameHeight);
            this.Width = Shooter.Radius * 2 *(GameWidth+1)+10;
            this.Height = Shooter.Radius * 2 * (GameHeight+1);
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeInterval;
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            if (!tank.EndGame())
            {
                counter++;
                if (counter % level == 0)
                {
                    tank.GenerateBoxes(GameWidth, GameHeight);
                }
                tank.Move(GameWidth);
                player.score1+=tank.CheckImpact();
                lblscore.Text = player.score1.ToString();
            }
            else
            {
                gameOver.Text = "Game Over";
                timer.Stop();
                EndOfGame end = new EndOfGame(this,player);
                end.Show();
            }
            levelUp();
            Invalidate();
        }
        void levelUp()
        {
            if (player.score1 == 20)
            {
                level=11;
                pen = new Pen(Color.LawnGreen);
            }
            else if (player.score1 == 40)
            {
                timer.Interval = 90;
                level=13;
                pen = new Pen(Color.Chocolate);
            }
            else if (player.score1 == 60)
            {
                timer.Interval = 85;
                pen = new Pen(Color.LightSteelBlue);

            }
            else if (player.score1 == 80)
            {
                timer.Interval = 80;
                pen = new Pen(Color.DarkRed);
            }
            else if (player.score1 == 100)
            {
                timer.Interval = 70;
                pen = new Pen(Color.DarkTurquoise);
            }
            else if (player.score1 == 120)
            {
                timer.Interval = 50;
                level = 13;
                pen = new Pen(Color.Firebrick);
            }
        }
        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            gr.Clear(Color.White);
            Brush br=new SolidBrush(Color.DarkSeaGreen);

            for (int i = 0; i < GameWidth; i++)
            {
                for (int j = 0; j < GameHeight-1; j++)
                {
                    gr.DrawRectangle(pen,i * 40, j * 40, (i + 1) * 40, (j + 1) * 40);
                }
            }
                gr.FillRectangle(br, 0, 0, (GameWidth + 1) * 40, 13);
            tank.Draw(gr);

       }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (KeyWasUp)
            {
                if (e.KeyCode == Keys.Left)
                {
                    tank.ChangeDirection(DirectionGame1.Left);
                }
                if (e.KeyCode == Keys.Right)
                {
                    tank.ChangeDirection(DirectionGame1.Right);
                }
                if (e.KeyCode == Keys.Up)
                {
                    tank.ChangeDirection(DirectionGame1.Up);
                }
                KeyWasUp = false;
            }
            Invalidate();
        }


        private void Form2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                tank.ChangeDirection(DirectionGame1.None);
            }
            KeyWasUp = true;
            Invalidate();
        }

        private void Form2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'p' || e.KeyChar == 'P')
            {
                if (!pause)
                {
                    timer.Stop();
                    pause = true;
                    gameOver.Text = "Paused";
                }
                else
                {
                    timer.Start();
                    pause = false;
                    gameOver.Text = "";
                }
            }
        }
        public void SaveCurrentPlayer()
        {
            f.SaveCurrentPlayer();
        }
        

        
    }
    public enum DirectionGame1
    {
        Left,Right,Up,None
    }
    class Shooter
    {
        public int X { get; set; }
        public int Y { get; set; }
        public static readonly int Radius=20;
        public int Velocity { get; set; }
        public Image image;
        private DirectionGame1 direction;
        private LinkedList<Bullet> bullets;
        public LinkedList<Box> boxes;
        public Shooter(int Width, int Height)
        {
            X = Width/2;
            Y = Height-1;
            Velocity = 1;
            image = Properties.Resources.tankSTB;
            direction = DirectionGame1.None;
            bullets = new LinkedList<Bullet>();
            boxes = new LinkedList<Box>();
            
        }
        public void ChangeDirection(DirectionGame1 NewDirection)
        {
            this.direction = NewDirection;
        }
        public void Move(int Width)
        {
            if (direction == DirectionGame1.None)
            {
                X = X;
            }
            if(direction==DirectionGame1.Left){
                X-=Velocity;
            }
            if (direction == DirectionGame1.Right)
            {
                X+=Velocity;
            }
            if (direction == DirectionGame1.Up)
            {
                bullets.AddLast(new Bullet(X, Y,Radius));
                ChangeDirection(DirectionGame1.None);
            }
            if (X < 0)
            {
                X = 0;
            }
            if (X >= Width+1)
            {
                X = Width;
            }
            MoveBoxes();
            MoveBullets();
            
        }

        public void Draw(Graphics gr)
        {
            gr.DrawImageUnscaled(image, X*Radius*2 ,Y*Radius*2 );
            DrawBoxes(gr);
            DrawBullets(gr);
        }
        public void MoveBullets()
        {
            foreach (Bullet b in bullets)
            {
                b.Move();
            }
        }
        void DrawBullets(Graphics gr)
        {
            foreach (Bullet b in bullets)
            {
                b.Draw(gr);
            }
        }
        public void GenerateBoxes(int width,int height)
        {
            boxes.AddLast(new Box(width, height, Radius));
        }
        public void MoveBoxes()
        {
            foreach (Box b in boxes)
            {
                b.Move();
            }
        }
        public void DrawBoxes(Graphics gr)
        {
            foreach (Box b in boxes)
            {
                b.Draw(gr);
            }
        }
        public int CheckImpact()
        {
            int hits = 0;
            LinkedList<Box> deleteBoxes=new LinkedList<Box>();
            LinkedList<Bullet> deleteBullets=new LinkedList<Bullet>();
            foreach (Bullet b in bullets)
            {  
                foreach (Box box in boxes)
                {
                    if (b.X == (int)box.X && b.Y == (int)box.Y)
                    {
                        deleteBoxes.AddLast(box);
                        deleteBullets.AddLast(b);
                    }
                    else if (b.X == (int)box.X && b.Y == (int)(box.Y+0.7))
                    { 
                        deleteBoxes.AddLast(box);
                        deleteBullets.AddLast(b);
                        
                    }
                    
                }
            }
            hits = deleteBullets.Count;
            foreach(Bullet b in deleteBullets){
                bullets.Remove(b);
            }
            foreach(Box b in deleteBoxes){
                boxes.Remove(b);
            }
            return hits;
        }
        
        public bool EndGame()
        {
            foreach(Box b in boxes){
                if(b.Y == Y+2)
                {
                    return true;
                }
                else if ((int)b.X == X && (int)b.Y == Y - 1)
                {
                    return true;
                }
            }
            return false;
        }
    }
    class Box
    {
        public float X {get; set;}
        public float Y { get; set; }
        public int Radius;
        public float Velocity { get; set; }
        public float h;
        Image image;
        public Box(int x,int y,int rad)
        {
            Random r = new Random();
            X = r.Next(x+1);
            Y = 0;
            Radius = rad;
            Velocity = (float)0.15;
            h = (float)y;
            image = Properties.Resources.box;
        }
        public void Move()
        {
            if (Y<=h)
            {
                Y+=Velocity;
            }
            else
            {
                Y = h + 1;
            }
        }
        public void Draw(Graphics gr)
        {
            gr.DrawRectangle(new Pen(Color.Black), X * Radius * 2, Y * Radius * 2, 30, 30);
            gr.FillRectangle(new SolidBrush(Color.Brown), X * Radius * 2, Y * Radius * 2, 30, 30);
            
            gr.DrawRectangle(new Pen(Color.Black), X * Radius * 2 +5, Y * Radius * 2 + 5, 20, 20);
           
            gr.DrawLine(new Pen(Color.Black), X * Radius * 2+5, Y * Radius * 2+5, X * Radius * 2 + 5, Y * Radius * 2+25);
            gr.DrawLine(new Pen(Color.Black), X * Radius * 2 + 10, Y * Radius * 2 + 5, X * Radius * 2 + 10, Y * Radius * 2 + 25);
            gr.DrawLine(new Pen(Color.Black), X * Radius * 2 + 15, Y * Radius * 2 + 5, X * Radius * 2 + 15, Y * Radius * 2 + 25);
            gr.DrawLine(new Pen(Color.Black), X * Radius * 2 + 20, Y * Radius * 2 + 5, X * Radius * 2 + 20, Y * Radius * 2 + 25);
            gr.DrawLine(new Pen(Color.Black), X * Radius * 2 + 25, Y * Radius * 2 + 5, X * Radius * 2 + 25, Y * Radius * 2 + 25);
            gr.DrawLine(new Pen(Color.Black), X * Radius * 2, Y * Radius * 2, X * Radius * 2 + 5, Y * Radius * 2 + 5);
            gr.DrawLine(new Pen(Color.Black), X * Radius * 2+30, Y * Radius * 2, X * Radius * 2 + 25, Y * Radius * 2 + 5);
            gr.DrawLine(new Pen(Color.Black), X * Radius * 2, Y * Radius * 2+30, X * Radius * 2 + 5, Y * Radius * 2 + 25);
            gr.DrawLine(new Pen(Color.Black), X * Radius *2+30, Y * Radius * 2+30, X * Radius * 2 + 25, Y * Radius * 2 + 25);
        }
    }
    class Bullet
    {
        public int X { get; set; }
        public int Y { get; set; }
        private int Radius;
        public int Velocity { get; set; }
        public Bullet(int X, int Y,int rad)
        {
            this.X = X;
            this.Y = Y;
            Velocity = 1;
            Radius = rad;
        }
        public void Move()
        {
            if(Y+Velocity>=0){
                Y--;
            }
            else
            {
                Y = -1;
            }
        }
        public void Draw(Graphics gr)
        {
            gr.FillRectangle(new SolidBrush(Color.Red), X * Radius * 2+15, Y * Radius * 2, 4, 10);
        }
    }
}
