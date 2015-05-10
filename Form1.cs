using ArcadeGamez_Featuring_Marko_and_Nikola.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcadeGamez_Featuring_Marko_and_Nikola
{
    public enum EndGame
    {
        WIN,
        LOSS
    }

    public enum Difficulty
    {
        EASY,
        NORMAL,
        HARD,
        IMPOSSIBLE,
        ARCADE
    }

    public partial class Form1 : Form
    {

        // tajmeri, brojaci i stoperici

        Timer timer;
        Timer timer2;
        Timer timer3;
        Timer timer4;

        Stopwatch stopwatch = new Stopwatch();

        long TIMER3_ELAPSED = 0;
        long TIMER4_ELAPSED = 0;

        // za crtanje

        Graphics graphics;
        Rectangle bounds;
        Pen pen;
        Pen matrixPen;
        Pen mainGridPen;
        Bitmap doubleBuffer;
        Image ImgTank;
        Image ImgBlock;

        // objekti 

        GoodGuy GoodGuy;
        List<Projectile> goodProjectiles;
        List<Projectile> badProjectiles;
        List<BadGuy> badGuys = new List<BadGuy>();
        StartForm f;
        Player p;
        // dopolnitelni clenovi

        Direction lastPressed;
        Direction current = Direction.NONE;
        Boolean isMoving = false;
        Boolean firstTimeShooting = false;
        long shootingDelay = -1000; 
        Random random;
        List<Block> blocks;
       
        // unikatni clenovi vo zavisnost od igrata
        
        Difficulty difficulty;
        int level;
        int timer3count = 0;
        Boolean arcade;
        Boolean ended;
        long tanksDestroyed;

        Boolean arcade1 = true;
        Boolean arcade2 = true;
        Boolean arcade3 = true;
        Boolean arcade4 = true;
        Boolean arcade5 = true;

        Boolean isPaused = false;

        public Form1(Difficulty dif, Boolean arcadeGame,StartForm f,Player p)
        {
            InitializeComponent();
            pen = new Pen(Color.BlanchedAlmond, 10); 
            matrixPen = new Pen(Color.Bisque, 3); // za crtanje na pravoagolnichinjata
            mainGridPen = new Pen(Color.RoyalBlue, 10); // za crtanje na glavniot pravoagolnik
            level = 1;
            arcade = arcadeGame;
            difficulty = dif;
            ended = false;
            random = new Random();
            blocks = new List<Block>();
            lastPressed = Direction.NONE;
            goodProjectiles = new List<Projectile>();
            badProjectiles = new List<Projectile>();
            ImgTank = Resources.tankBottom;
            ImgBlock = Resources.grassUnscaled;
            DoubleBuffered = true;
            bounds = new Rectangle(6, 6, this.Bounds.Width-28, this.Bounds.Height-50);
            doubleBuffer = new Bitmap(Width, Height);
            graphics = CreateGraphics();
            setAllTimers();
            this.f = f;
            this.p = p;
            if (arcade == false)
                newGame(difficulty, level);
            else
                newArcadeGame();
            
        }

        void spawnGoodGuy()
        {
            GoodGuy = new GoodGuy(this.Bounds.Width / 2 - 29, this.Bounds.Height / 2 - 40, Direction.BOTTOM, this.Width, this.Height, blocks);
        }

        void spawnBadGuys(Difficulty dif)
        {
            for (int i = 0; i < 4; i++)
                badGuys.Add(new BadGuy());
            badGuys[0] = new BadGuy(this.Bounds.Width / 2 - 380, this.Bounds.Height / 2 - 290, Direction.BOTTOM, this.Width, this.Height, dif, blocks, 2);
            badGuys[1] = new BadGuy(this.Bounds.Width / 2 - 380, this.Bounds.Height / 2 + 215, Direction.TOP, this.Width, this.Height, dif, blocks, 2);
            badGuys[2] = new BadGuy(this.Bounds.Width / 2 + 320, this.Bounds.Height / 2 + 215, Direction.TOP, this.Width, this.Height, dif, blocks, 2);
            badGuys[3] = new BadGuy(this.Bounds.Width / 2 + 320, this.Bounds.Height / 2 - 290, Direction.BOTTOM, this.Width, this.Height, dif, blocks, 2);
        }


        void setAllTimers()
        {
            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += timer_Tick;
            timer2 = new Timer();
            timer2.Interval = 10;
            timer2.Tick += timer2_Tick;
            timer3 = new Timer();
            timer3.Interval = 10;
            timer3.Tick += timer3_Tick;
            timer4 = new Timer();
            timer4.Interval = 10;
            timer4.Tick += timer4_Tick;
        }

        void stopAllTimers()
        {
            timer.Stop();
            timer2.Stop();
            timer3.Stop();
            timer4.Stop();
        }

        void StartAllTimers()
        {
            timer.Start();
            timer2.Start();
            timer3.Start();
            timer4.Start();
        }

        void increaseBadProjectilesSpeedArcade(int n)
        {
            foreach (Projectile bp in badProjectiles)
                bp.Velocity += n;
        }

        void removeProjectiles()
        {
            if (badProjectiles.Count != 0)
            {
                badProjectiles.RemoveRange(0, badProjectiles.Count);
            }

            if (goodProjectiles.Count != 0)
            {
                goodProjectiles.RemoveRange(0, goodProjectiles.Count);
            }
        }

        void removeBlocks()
        {
            blocks.RemoveRange(0, blocks.Count);
        }


        void newGame(Difficulty dif, int nivo)
        {

            spawnGoodGuy();

            firstTimeShooting = false;

            removeProjectiles();

            removeBlocks();

            spawnBadGuys(dif);

            stopAllTimers();

            if (nivo == 5)
                this.Close();
            else
            {
                createBlocks();
                timer3.Start();
                timer4.Start();
            }
        }

        void setArcadeChecks(Boolean b)
        {
            arcade1 = b;
            arcade2 = b;
            arcade3 = b;
            arcade4 = b;
            arcade5 = b;
        }

        void newArcadeGame()
        {
            tanksDestroyed = 0;

            setArcadeChecks(true);

            spawnGoodGuy();
            spawnBadGuys(Difficulty.ARCADE);
            timer3.Start();
            timer4.Start();
            
        }

        void createNewWorld(Graphics g)
        {
            DoubleBuffered = true;
            if (!arcade)
            {
                for (int k = 0; k < 9; k++)
                {
                    for (int i = 0, j = 10; i < 15; i++, j += 10 + (ImgTank.Width / 2))
                    {
                        g.DrawRectangle(matrixPen, j + i * (ImgTank.Width / 2), 3 * k * (ImgTank.Height / 2) + 10, ImgTank.Width + 10, ImgTank.Height + 20);
                        if (level == 1)
                        {
                            if (k == 3 || k == 5)
                            {
                                if (i == 6 || i == 8)
                                    g.DrawImage(Resources.grass, j + i * (ImgTank.Width / 2), 3 * k * (ImgTank.Height / 2) + 10, ImgTank.Width + 10, ImgTank.Height + 20);
                            }
                            if (k == 1 || k == 2 || k == 6 || k == 7)
                            {
                                if (i == 1 || i == 2 || i == 12 || i == 13)
                                    g.DrawImage(Resources.grass, j + i * (ImgTank.Width / 2), 3 * k * (ImgTank.Height / 2) + 10, ImgTank.Width + 10, ImgTank.Height + 20);
                            }
                            if (k == 7)
                            {
                                if (i >= 4 && i <= 10)
                                    g.DrawImage(Resources.ice, j + i * (ImgTank.Width / 2), 3 * k * (ImgTank.Height / 2) + 10, ImgTank.Width + 10, ImgTank.Height + 20);
                            }
                        }

                        else if (level == 2)
                        {
                            if (k == 1 || k == 7)
                            {
                                if (i >= 1 && i <= 13)
                                    g.DrawImage(Resources.grass, j + i * (ImgTank.Width / 2), 3 * k * (ImgTank.Height / 2) + 10, ImgTank.Width + 10, ImgTank.Height + 20);
                            }
                            if (i == 1 || i == 13)
                            {
                                if (k >= 2 && k <= 6)
                                    g.DrawImage(Resources.ice, j + i * (ImgTank.Width / 2), 3 * k * (ImgTank.Height / 2) + 10, ImgTank.Width + 10, ImgTank.Height + 20);
                            }
                            if (k == 4)
                            {
                                if (i == 0 || i == 2 || i == 3 || i == 4 || i == 10 || i == 11 || i == 12 || i == 14)
                                    g.DrawImage(Resources.metal, j + i * (ImgTank.Width / 2), 3 * k * (ImgTank.Height / 2) + 10, ImgTank.Width + 10, ImgTank.Height + 20);
                            }
                            if (i == 7)
                            {
                                if (k == 0 || k == 2 || k == 3 || k == 5 || k == 6 || k == 8)
                                    g.DrawImage(Resources.metal, j + i * (ImgTank.Width / 2), 3 * k * (ImgTank.Height / 2) + 10, ImgTank.Width + 10, ImgTank.Height + 20);
                            }
                        }

                        else if (level == 3)
                        {
                            if ((i == 6 && k != 7) || (i == 8 && k != 1))
                                g.DrawImage(Resources.metal, j + i * (ImgTank.Width / 2), 3 * k * (ImgTank.Height / 2) + 10, ImgTank.Width + 10, ImgTank.Height + 20);
                            if ((k == 1 && i == 1) || (k == 5 && i == 3) || (k == 3 && i == 11) || (k == 7 && i == 13))
                                g.DrawImage(Resources.grass, j + i * (ImgTank.Width / 2), 3 * k * (ImgTank.Height / 2) + 10, ImgTank.Width + 10, ImgTank.Height + 20);
                            if (((k == 4 || k == 6) && (i == 2 || i == 3 || i == 4)) || (k == 5 && (i == 2 || i == 4)))
                                g.DrawImage(Resources.ice, j + i * (ImgTank.Width / 2), 3 * k * (ImgTank.Height / 2) + 10, ImgTank.Width + 10, ImgTank.Height + 20);
                            if (((k == 2 || k == 4) && (i == 10 || i == 11 || i == 12)) || (k == 3 && (i == 10 || i == 12)))
                                g.DrawImage(Resources.ice, j + i * (ImgTank.Width / 2), 3 * k * (ImgTank.Height / 2) + 10, ImgTank.Width + 10, ImgTank.Height + 20);
                        }

                        else if (level == 4)
                        {
                            if (i == 2 || i == 5 || i == 9 || i == 12)
                                g.DrawImage(Resources.ice, j + i * (ImgTank.Width / 2), 3 * k * (ImgTank.Height / 2) + 10, ImgTank.Width + 10, ImgTank.Height + 20);
                            if (k == 1 || k == 2 || k == 6 || k == 7)
                            {
                                if (i == 0 || i == 1 || i == 13 || i == 14)
                                    g.DrawImage(Resources.grass, j + i * (ImgTank.Width / 2), 3 * k * (ImgTank.Height / 2) + 10, ImgTank.Width + 10, ImgTank.Height + 20);
                            }
                            if (k == 3 || k == 4 || k == 5)
                            {
                                if (i == 3 || i == 11)
                                    g.DrawImage(Resources.grass, j + i * (ImgTank.Width / 2), 3 * k * (ImgTank.Height / 2) + 10, ImgTank.Width + 10, ImgTank.Height + 20);
                            }
                            if (k == 1 || k == 7)
                            {
                                if (i == 6 || i == 7 || i == 8)
                                    g.DrawImage(Resources.metal, j + i * (ImgTank.Width / 2), 3 * k * (ImgTank.Height / 2) + 10, ImgTank.Width + 10, ImgTank.Height + 20);
                            }
                        }
                    }
                }
            }

            else
            {
                for (int k = 0; k < 9; k++)
                {
                    for (int i = 0, j = 10; i < 15; i++, j += 10 + (ImgTank.Width / 2))
                    {
                        g.DrawRectangle(new Pen(Color.Chocolate), j + i * (ImgTank.Width / 2), 3 * k * (ImgTank.Height / 2) + 10, ImgTank.Width + 10, ImgTank.Height + 20);
                    }
                }
            }
        }

        void createBlocks()
        {
            if (true) // ne e potrebno
            {
                for (int k = 0; k < 9; k++)
                {
                    for (int i = 0, j = 10; i < 15; i++, j += 10 + (ImgTank.Width / 2))
                    {
                        if (level == 1)
                        {
                            if (k == 3 || k == 5)
                            {
                                if (i == 6 || i == 8)
                                    blocks.Add(new GrassBlock(j + i * (ImgTank.Width / 2) + 15, 3 * k * (ImgTank.Height / 2) + 13, ImgTank.Width + 45, ImgTank.Height + 50));
                            }
                            if (k == 1 || k == 2 || k == 6 || k == 7)
                            {
                                if (i == 1 || i == 2 || i == 12 || i == 13)
                                    blocks.Add(new GrassBlock(j + i * (ImgTank.Width / 2) + 15, 3 * k * (ImgTank.Height / 2) + 13, ImgTank.Width + 45, ImgTank.Height + 50));
                            }
                            if (k == 7)
                            {
                                if (i >= 4 && i <= 10)
                                    blocks.Add(new IceBlock(j + i * (ImgTank.Width / 2) + 15, 3 * k * (ImgTank.Height / 2) + 13, ImgTank.Width + 45, ImgTank.Height + 50));
                            }
                        }

                        else if (level == 2)
                        {
                            if (k == 1 || k == 7)
                            {
                                if (i >= 1 && i <= 13)
                                    blocks.Add(new GrassBlock(j + i * (ImgTank.Width / 2) + 15, 3 * k * (ImgTank.Height / 2) + 13, ImgTank.Width + 45, ImgTank.Height + 50)); 
                            }
                            if (i == 1 || i == 13)
                            {
                                if (k >= 2 && k <= 6)
                                    blocks.Add(new IceBlock(j + i * (ImgTank.Width / 2) + 15, 3 * k * (ImgTank.Height / 2) + 13, ImgTank.Width + 45, ImgTank.Height + 50));
                            }
                            if (k == 4)
                            {
                                if (i == 0 || i == 2 || i == 3 || i == 4 || i == 10 || i == 11 || i == 12 || i == 14)
                                    blocks.Add(new MetalBlock(j + i * (ImgTank.Width / 2) + 15, 3 * k * (ImgTank.Height / 2) + 13, ImgTank.Width + 45, ImgTank.Height + 50)); 
                            }
                            if (i == 7)
                            {
                                if (k == 0 || k == 2 || k == 3 || k == 5 || k == 6 || k == 8)
                                    blocks.Add(new MetalBlock(j + i * (ImgTank.Width / 2) + 15, 3 * k * (ImgTank.Height / 2) + 13, ImgTank.Width + 45, ImgTank.Height + 50));
                            }
                        }

                        else if (level == 3)
                        {
                            if ((i == 6 && k != 7) || (i == 8 && k != 1))
                                blocks.Add(new MetalBlock(j + i * (ImgTank.Width / 2) + 15, 3 * k * (ImgTank.Height / 2) + 13, ImgTank.Width + 45, ImgTank.Height + 50));
                            if ((k == 1 && i == 1) || (k == 5 && i == 3) || (k == 3 && i == 11) || (k == 7 && i == 13))
                                blocks.Add(new GrassBlock(j + i * (ImgTank.Width / 2) + 15, 3 * k * (ImgTank.Height / 2) + 13, ImgTank.Width + 45, ImgTank.Height + 50));
                            if (((k == 4 || k == 6) && (i == 2 || i == 3 || i == 4)) || (k == 5 && (i == 2 || i == 4)))
                                blocks.Add(new IceBlock(j + i * (ImgTank.Width / 2) + 15, 3 * k * (ImgTank.Height / 2) + 13, ImgTank.Width + 45, ImgTank.Height + 50));
                            if (((k == 2 || k == 4) && (i == 10 || i == 11 || i == 12)) || (k == 3 && (i == 10 || i == 12)))
                                blocks.Add(new IceBlock(j + i * (ImgTank.Width / 2) + 15, 3 * k * (ImgTank.Height / 2) + 13, ImgTank.Width + 45, ImgTank.Height + 50));
                        }

                        else if (level == 4)
                        {
                            if (i == 2 || i == 5 || i == 9 || i == 12)
                                blocks.Add(new IceBlock(j + i * (ImgTank.Width / 2) + 15, 3 * k * (ImgTank.Height / 2) + 13, ImgTank.Width + 45, ImgTank.Height + 50));
                            if (k == 1 || k == 2 || k == 6 || k == 7)
                            {
                                if (i == 0 || i == 1 || i == 13 || i == 14)
                                    blocks.Add(new GrassBlock(j + i * (ImgTank.Width / 2) + 15, 3 * k * (ImgTank.Height / 2) + 13, ImgTank.Width + 45, ImgTank.Height + 50));
                            }
                            if (k == 3 || k == 4 || k == 5)
                            {
                                if (i == 3 || i == 11)
                                    blocks.Add(new GrassBlock(j + i * (ImgTank.Width / 2) + 15, 3 * k * (ImgTank.Height / 2) + 13, ImgTank.Width + 45, ImgTank.Height + 50));
                            }
                            if (k == 1 || k == 7)
                            {
                                if (i == 6 || i == 7 || i == 8)
                                    blocks.Add(new MetalBlock(j + i * (ImgTank.Width / 2) + 15, 3 * k * (ImgTank.Height / 2) + 13, ImgTank.Width + 45, ImgTank.Height + 50));
                            }
                        }
                    }
                }
            }
            
        }

        void timer4_Tick(object sender, EventArgs e)          // DVIZENJE NA LOSITE PROEKTILI
        {
            List<Projectile> toBeRemoved = new List<Projectile>();
            DoubleBuffered = true;
            TIMER4_ELAPSED += 10;
            if (difficulty == Difficulty.EASY)
            {
                if (TIMER4_ELAPSED % 920 == 0)
                {
                    foreach (BadGuy badGuy in badGuys)
                    {
                        badProjectiles.Add(new Projectile(badGuy.X + 10, badGuy.Y - 10, badGuy.direction, this.Width, this.Height, difficulty, 0));
                    }
                }
            }

            else if (difficulty == Difficulty.NORMAL)
            {
                if (TIMER4_ELAPSED % 600 == 0)
                {
                    foreach (BadGuy badGuy in badGuys)
                    {
                        badProjectiles.Add(new Projectile(badGuy.X + 10, badGuy.Y - 10, badGuy.direction, this.Width, this.Height, difficulty, 0));
                    }
                }
            }

            else if (difficulty == Difficulty.HARD)
            {
                if (TIMER4_ELAPSED % 400 == 0 || TIMER4_ELAPSED % 560 == 0)
                {
                    foreach (BadGuy badGuy in badGuys)
                    {
                        badProjectiles.Add(new Projectile(badGuy.X + 10, badGuy.Y - 10, badGuy.direction, this.Width, this.Height, difficulty, 0));
                    }
                }
            }

            else if (difficulty == Difficulty.IMPOSSIBLE)
            {
                if (TIMER4_ELAPSED % 200 == 0 || TIMER4_ELAPSED % 350 == 0)
                {
                    foreach (BadGuy badGuy in badGuys)
                    {
                        badProjectiles.Add(new Projectile(badGuy.X + 10, badGuy.Y - 10, badGuy.direction, this.Width, this.Height, difficulty, 0));
                    }
                }
            }
            else
            {
                if (tanksDestroyed < 10)
                {
                    if (TIMER4_ELAPSED % 1000 == 0)
                    {
                        foreach (BadGuy badGuy in badGuys)
                        {
                            badProjectiles.Add(new Projectile(badGuy.X + 10, badGuy.Y - 10, badGuy.direction, this.Width, this.Height, difficulty, 4));
                        }
                    }
                }

                else if (tanksDestroyed >= 10 && tanksDestroyed < 20)
                {
                    if (arcade1)
                    {
                        foreach (BadGuy badGuy in badGuys)
                        {
                            badGuy.Velocity += 1;
                        }
                        arcade1 = false;
                    }

                    if (TIMER4_ELAPSED % 850 == 0)
                    {
                        foreach (BadGuy badGuy in badGuys)
                        {
                            badProjectiles.Add(new Projectile(badGuy.X + 10, badGuy.Y - 10, badGuy.direction, this.Width, this.Height, difficulty, 5));
                        }
                    }
                }

                else if (tanksDestroyed >= 20 && tanksDestroyed < 30)
                {
                    if (arcade2)
                    {
                        foreach (BadGuy badGuy in badGuys)
                        {
                            badGuy.Velocity += 1;
                        }
                        arcade2 = false;
                    }

                    if (TIMER4_ELAPSED % 700 == 0)
                    {
                        foreach (BadGuy badGuy in badGuys)
                        {
                            badProjectiles.Add(new Projectile(badGuy.X + 10, badGuy.Y - 10, badGuy.direction, this.Width, this.Height, difficulty, 6));
                        }
                    }
                }

                else if (tanksDestroyed >= 30 && tanksDestroyed < 40)
                {
                    if (arcade3)
                    {
                        foreach (BadGuy badGuy in badGuys)
                        {
                            badGuy.Velocity += 1;
                        }
                        arcade3 = false;
                    }

                    if (TIMER4_ELAPSED % 550 == 0)
                    {
                        foreach (BadGuy badGuy in badGuys)
                        {
                            badProjectiles.Add(new Projectile(badGuy.X + 10, badGuy.Y - 10, badGuy.direction, this.Width, this.Height, difficulty, 7));
                        }
                    }
                }

                else if (tanksDestroyed >= 40 && tanksDestroyed < 50)
                {
                    if (arcade4)
                    {
                        foreach (BadGuy badGuy in badGuys)
                        {
                            badGuy.Velocity += 1;
                        }
                        arcade4 = false;
                    }

                    if (TIMER4_ELAPSED % 400 == 0)
                    {
                        foreach (BadGuy badGuy in badGuys)
                        {
                            badProjectiles.Add(new Projectile(badGuy.X + 10, badGuy.Y - 10, badGuy.direction, this.Width, this.Height, difficulty, 8));
                        }
                    }
                }

                else if (tanksDestroyed >= 50)
                {
                    if (arcade5)
                    {
                        foreach (BadGuy badGuy in badGuys)
                        {
                            badGuy.Velocity += 2;
                        }
                        arcade5 = false;
                    }

                    if (TIMER4_ELAPSED % 250 == 0)
                    {
                        foreach (BadGuy badGuy in badGuys)
                        {
                            badProjectiles.Add(new Projectile(badGuy.X + 10, badGuy.Y - 10, badGuy.direction, this.Width, this.Height, difficulty, 9));
                        }
                    }
                }

                // handle shooting
            }
            
            moveBadProjectiles();

            for (int i = 0; i < badProjectiles.Count; i++)
            {
                if (GoodGuy.isWithinArea(badProjectiles[i].X, badProjectiles[i].Y))
                {
                    End(EndGame.LOSS);
                }

                if (badProjectiles[i].deleteMe)
                {
                    toBeRemoved.Add(badProjectiles[i]);
                }
            }

            for (int i = 0; i < badProjectiles.Count; i++)
            {
                for (int j = 0; j < blocks.Count; j++)
                {
                    if (blocks[j].Type() != BlockType.ICE)
                    {
                        if (blocks[j].isWithinArea(badProjectiles[i].X, badProjectiles[i].Y))
                            toBeRemoved.Add(badProjectiles[i]);
                    }
                }
            }

            foreach (Projectile p in toBeRemoved)
                badProjectiles.Remove(p);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            Graphics g = Graphics.FromImage(doubleBuffer);
            g.Clear(Color.White);
            Boolean canMove = true;
            foreach (Block block in blocks)
            {
                if (block.Type() == BlockType.ICE)
                {
                    if(block.isWithinArea(GoodGuy.X, GoodGuy.Y))
                    {
                        GoodGuy.increaseVelocity();
                        break;
                    }
                }

                else if (block.Type() == BlockType.METAL)
                {
                    if (block.isWithinArea(GoodGuy.X + 10, GoodGuy.Y-10))
                    {
                        canMove = false;
                        break;
                    }
                }
            }

            if(canMove)
                GoodGuy.Move(lastPressed);

            GoodGuy.revertVelocityToNormal();
        }


        void timer2_Tick(object sender, EventArgs e)         // DVIZENJE NA DOBRITE PROEKTILI
        {

            DoubleBuffered = true;
            List<Projectile> toBeRemoved = new List<Projectile>();

            for (int i = 0; i < goodProjectiles.Count; i++)
            {
                for (int j = 0; j < badGuys.Count; j++)
                {
                    if (badGuys[j].isWithinArea(goodProjectiles[i].X, goodProjectiles[i].Y)) 
                    {
                        BadGuy b = badGuys[j];
                        badGuys.Remove(b);
                        tanksDestroyed++;
                    }
                }

                for (int k = 0; k < blocks.Count; k++)
                {
                    if (blocks[k].Type() != BlockType.ICE)
                    {
                        if (blocks[k].isWithinArea(goodProjectiles[i].X, goodProjectiles[i].Y))
                        {
                            Projectile p = goodProjectiles[i];
                            toBeRemoved.Add(p);
                        }
                    }
                }
            }

            foreach (Projectile p in toBeRemoved)
            {
                goodProjectiles.Remove(p);
            }

            moveGoodProjectiles();

            if (arcade)
            {
                if (!ended)
                {
                    if (badGuys.Count < 4)
                    {
                        int a = random.Next(4);
                        if (tanksDestroyed >= 1 && tanksDestroyed < 10)
                        {
                            if (a == 0)
                                badGuys.Add(new BadGuy(this.Bounds.Width / 2 - 380, this.Bounds.Height / 2 - 290, Direction.BOTTOM, this.Width, this.Height, Difficulty.ARCADE, blocks, 2));
                            else if (a == 1)
                                badGuys.Add(new BadGuy(this.Bounds.Width / 2 - 380, this.Bounds.Height / 2 + 215, Direction.TOP, this.Width, this.Height, Difficulty.ARCADE, blocks, 2));
                            else if (a == 2)
                                badGuys.Add(new BadGuy(this.Bounds.Width / 2 + 320, this.Bounds.Height / 2 + 215, Direction.TOP, this.Width, this.Height, Difficulty.ARCADE, blocks, 2));
                            else
                                badGuys.Add(new BadGuy(this.Bounds.Width / 2 + 320, this.Bounds.Height / 2 - 290, Direction.BOTTOM, this.Width, this.Height, Difficulty.ARCADE, blocks, 2));
                        }

                        else if (tanksDestroyed >= 10 && tanksDestroyed < 20)
                        {
                            if (a == 0)
                                badGuys.Add(new BadGuy(this.Bounds.Width / 2 - 380, this.Bounds.Height / 2 - 290, Direction.BOTTOM, this.Width, this.Height, Difficulty.ARCADE, blocks, 3));
                            else if (a == 1)
                                badGuys.Add(new BadGuy(this.Bounds.Width / 2 - 380, this.Bounds.Height / 2 + 215, Direction.TOP, this.Width, this.Height, Difficulty.ARCADE, blocks, 3));
                            else if (a == 2)
                                badGuys.Add(new BadGuy(this.Bounds.Width / 2 + 320, this.Bounds.Height / 2 + 215, Direction.TOP, this.Width, this.Height, Difficulty.ARCADE, blocks, 3));
                            else
                                badGuys.Add(new BadGuy(this.Bounds.Width / 2 + 320, this.Bounds.Height / 2 - 290, Direction.BOTTOM, this.Width, this.Height, Difficulty.ARCADE, blocks, 3));
                        }

                        else if (tanksDestroyed >= 20 && tanksDestroyed < 30)
                        {
                            if (a == 0)
                                badGuys.Add(new BadGuy(this.Bounds.Width / 2 - 380, this.Bounds.Height / 2 - 290, Direction.BOTTOM, this.Width, this.Height, Difficulty.ARCADE, blocks, 4));
                            else if (a == 1)
                                badGuys.Add(new BadGuy(this.Bounds.Width / 2 - 380, this.Bounds.Height / 2 + 215, Direction.TOP, this.Width, this.Height, Difficulty.ARCADE, blocks, 4));
                            else if (a == 2)
                                badGuys.Add(new BadGuy(this.Bounds.Width / 2 + 320, this.Bounds.Height / 2 + 215, Direction.TOP, this.Width, this.Height, Difficulty.ARCADE, blocks, 4));
                            else
                                badGuys.Add(new BadGuy(this.Bounds.Width / 2 + 320, this.Bounds.Height / 2 - 290, Direction.BOTTOM, this.Width, this.Height, Difficulty.ARCADE, blocks, 4));
                        }

                        else if (tanksDestroyed >= 30 && tanksDestroyed < 40)
                        {
                            if (a == 0)
                                badGuys.Add(new BadGuy(this.Bounds.Width / 2 - 380, this.Bounds.Height / 2 - 290, Direction.BOTTOM, this.Width, this.Height, Difficulty.ARCADE, blocks, 5));
                            else if (a == 1)
                                badGuys.Add(new BadGuy(this.Bounds.Width / 2 - 380, this.Bounds.Height / 2 + 215, Direction.TOP, this.Width, this.Height, Difficulty.ARCADE, blocks, 5));
                            else if (a == 2)
                                badGuys.Add(new BadGuy(this.Bounds.Width / 2 + 320, this.Bounds.Height / 2 + 215, Direction.TOP, this.Width, this.Height, Difficulty.ARCADE, blocks, 5));
                            else
                                badGuys.Add(new BadGuy(this.Bounds.Width / 2 + 320, this.Bounds.Height / 2 - 290, Direction.BOTTOM, this.Width, this.Height, Difficulty.ARCADE, blocks, 5));
                        }

                        else if (tanksDestroyed >= 40 && tanksDestroyed < 50)
                        {
                            if (a == 0)
                                badGuys.Add(new BadGuy(this.Bounds.Width / 2 - 380, this.Bounds.Height / 2 - 290, Direction.BOTTOM, this.Width, this.Height, Difficulty.ARCADE, blocks, 6));
                            else if (a == 1)
                                badGuys.Add(new BadGuy(this.Bounds.Width / 2 - 380, this.Bounds.Height / 2 + 215, Direction.TOP, this.Width, this.Height, Difficulty.ARCADE, blocks, 6));
                            else if (a == 2)
                                badGuys.Add(new BadGuy(this.Bounds.Width / 2 + 320, this.Bounds.Height / 2 + 215, Direction.TOP, this.Width, this.Height, Difficulty.ARCADE, blocks, 6));
                            else
                                badGuys.Add(new BadGuy(this.Bounds.Width / 2 + 320, this.Bounds.Height / 2 - 290, Direction.BOTTOM, this.Width, this.Height, Difficulty.ARCADE, blocks, 6));
                        }

                        else
                        {
                            if (a == 0)
                                badGuys.Add(new BadGuy(this.Bounds.Width / 2 - 380, this.Bounds.Height / 2 - 290, Direction.BOTTOM, this.Width, this.Height, Difficulty.ARCADE, blocks, 7));
                            else if (a == 1)
                                badGuys.Add(new BadGuy(this.Bounds.Width / 2 - 380, this.Bounds.Height / 2 + 215, Direction.TOP, this.Width, this.Height, Difficulty.ARCADE, blocks, 7));
                            else if (a == 2)
                                badGuys.Add(new BadGuy(this.Bounds.Width / 2 + 320, this.Bounds.Height / 2 + 215, Direction.TOP, this.Width, this.Height, Difficulty.ARCADE, blocks, 7));
                            else
                                badGuys.Add(new BadGuy(this.Bounds.Width / 2 + 320, this.Bounds.Height / 2 - 290, Direction.BOTTOM, this.Width, this.Height, Difficulty.ARCADE, blocks, 7));
                        }
                    }
                }
            }

            if (badGuys.Count == 0)
            {
                End(EndGame.WIN);
            }
           
        }


        void moveRandomly(BadGuy b)
        {
            Direction randomDir = b.direction;
            if (difficulty == Difficulty.EASY)
            {
                if (TIMER3_ELAPSED % 550 == 0 || TIMER3_ELAPSED % 2990 == 0)
                {
                    int whereTo = random.Next(101);
                    if (whereTo <= 25)
                        randomDir = Direction.BOTTOM;
                    else if (whereTo > 25 && whereTo <= 50)
                        randomDir = Direction.LEFT;
                    else if (whereTo > 50 && whereTo <= 75)
                        randomDir = Direction.TOP;
                    else
                        randomDir = Direction.RIGHT;
                }
            }

            else if (difficulty == Difficulty.NORMAL)
            {
                if (TIMER3_ELAPSED % 1000 == 0 || TIMER3_ELAPSED % 2990 == 0)
                {
                    int whereTo = random.Next(101);
                    if (whereTo <= 25)
                        randomDir = Direction.BOTTOM;
                    else if (whereTo > 25 && whereTo <= 50)
                        randomDir = Direction.LEFT;
                    else if (whereTo > 50 && whereTo <= 75)
                        randomDir = Direction.TOP;
                    else
                        randomDir = Direction.RIGHT;
                }
            }

            else if (difficulty == Difficulty.HARD)
            {
                if (TIMER3_ELAPSED % 800 == 0 || TIMER3_ELAPSED % 2990 == 0)
                {
                    int whereTo = random.Next(101);
                    if (whereTo <= 25)
                        randomDir = Direction.BOTTOM;
                    else if (whereTo > 25 && whereTo <= 50)
                        randomDir = Direction.LEFT;
                    else if (whereTo > 50 && whereTo <= 75)
                        randomDir = Direction.TOP;
                    else
                        randomDir = Direction.RIGHT;
                }
            }

            else
            {
                if (TIMER3_ELAPSED % 800 == 0 || TIMER3_ELAPSED % 2990 == 0)
                {
                    int whereTo = random.Next(101);
                    if (whereTo <= 25)
                        randomDir = Direction.BOTTOM;
                    else if (whereTo > 25 && whereTo <= 50)
                        randomDir = Direction.LEFT;
                    else if (whereTo > 50 && whereTo <= 75)
                        randomDir = Direction.TOP;
                    else
                        randomDir = Direction.RIGHT;
                }
            }

            if(b.checkBounds(randomDir))
                b.Move(randomDir);
            else
                b.Move(Opposite(randomDir));
        }

        void timer3_Tick(object sender, EventArgs e)       // DVIZENJE NA BAD GUYS
        {

            TIMER3_ELAPSED += 10;
            
            Direction d = GoodGuy.direction;

            foreach (BadGuy b in badGuys)         
                moveRandomly(b);
               
            DoubleBuffered = true;
            Invalidate();
        }

        public Direction Opposite(Direction dir)
        {
            if (dir == Direction.TOP)
                return Direction.BOTTOM;
            else if (dir == Direction.BOTTOM)
                return Direction.TOP;
            else if (dir == Direction.LEFT)
                return Direction.RIGHT;
            else
                return Direction.LEFT;
        }

        void moveGoodProjectiles()
        {
            foreach (Projectile p in goodProjectiles)
            {
                p.Move(p.direction);
            }
        }

        void moveBadProjectiles()
        {
            foreach (Projectile p in badProjectiles)
            {
                p.Move(p.direction);
            }
        }

        void drawAllProjectiles(Graphics g)
        {
            foreach (Projectile p in goodProjectiles)
            {
                p.Draw(new Pen(Color.Red, 3), g);
            }

            foreach (Projectile p in badProjectiles)
            {
                p.Draw(new Pen(Color.Black, 3), g);
            }
        }

        void drawAllbadGuys(Graphics g)
        {
            foreach (BadGuy badGuy in badGuys)
                badGuy.Draw(g);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.DrawRectangle(mainGridPen, bounds);

            DoubleBuffered = true;

            createNewWorld(g);

            GoodGuy.Draw(g);

            drawAllbadGuys(g);

            drawAllProjectiles(g);
        }

        

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            DoubleBuffered = true;

            if (isPaused == false)
            {
                if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Space)
                {
                    timer.Start();
                }

                if (e.KeyCode == Keys.Up)
                {
                    lastPressed = Direction.TOP;
                    isMoving = true;
                    GoodGuy.Move(lastPressed);
                }

                else if (e.KeyCode == Keys.Down)
                {
                    lastPressed = Direction.BOTTOM;
                    isMoving = true;
                    GoodGuy.Move(lastPressed);
                }

                else if (e.KeyCode == Keys.Left)
                {

                    lastPressed = Direction.LEFT;
                    isMoving = true;
                    GoodGuy.Move(lastPressed);
                }

                else if (e.KeyCode == Keys.Right)
                {
                    lastPressed = Direction.RIGHT;
                    isMoving = true;
                    GoodGuy.Move(lastPressed);
                }

                else if (e.KeyCode == Keys.Space)
                {
                    if (isMoving)
                    {
                        if (!firstTimeShooting)
                        {
                            firstTimeShooting = true;
                            timer2.Start();
                            stopwatch.Start();
                        }

                        if (stopwatch.ElapsedMilliseconds - shootingDelay >= 500)
                        {
                            goodProjectiles.Add(new Projectile(GoodGuy.X + 10, GoodGuy.Y - 10, GoodGuy.direction, this.Width, this.Height, Difficulty.NORMAL, 0)); // smeni
                            shootingDelay = stopwatch.ElapsedMilliseconds;
                        }

                        timer.Start();
                    }

                    else
                    {
                        if (!firstTimeShooting)
                        {
                            firstTimeShooting = true;
                            timer2.Start();
                            stopwatch.Start();
                        }

                        if (stopwatch.ElapsedMilliseconds - shootingDelay >= 500)
                        {
                            goodProjectiles.Add(new Projectile(GoodGuy.X + 10, GoodGuy.Y - 10, GoodGuy.direction, this.Width, this.Height, Difficulty.NORMAL, 0)); // smeni
                            shootingDelay = stopwatch.ElapsedMilliseconds;
                        }

                        timer.Stop();
                    }
                }

                else if (e.KeyCode == Keys.Z)
                {
                    if (!firstTimeShooting)
                    {
                        firstTimeShooting = true;
                        timer2.Start();
                        stopwatch.Start();
                    }

                    isMoving = false;
                    timer.Stop();
                }
            }
        }

        void End(EndGame end)
        {
            stopAllTimers();
            this.graphics.Clear(Color.White);
            graphics.DrawRectangle(mainGridPen, bounds); // smeni ovde
            DoubleBuffered = true;

            for (int k = 0; k < 9; k++)
            {
                for (int i = 0, j = 10; i < 15; i++, j += 10 + (ImgTank.Width / 2))
                    graphics.DrawRectangle(matrixPen, j + i * (ImgTank.Width / 2), 3 * k * (ImgTank.Height / 2) + 10, ImgTank.Width + 10, ImgTank.Height + 20);

            }

            if (end == EndGame.WIN)
            {
                MessageBox.Show("WELL PLAYED!");
                level++;
                newGame(difficulty, level);
            }

            else
            {
                ended = true;
                if (!arcade)
                {
                    MessageBox.Show("You got shot, cap'n!", "Game over");
                }

                else
                {
                    p.score2 = (int)this.calculatePoints();
                    f.SaveCurrentPlayer();
                    MessageBox.Show("You fought valiantly, soldier! Sadly, your tank was destroyed. \r\n\r\n\t\t          Score: " + calculatePoints() , "Game over");
                }

                this.Close();
            }
        }

        long calculatePoints()
        {
            if (tanksDestroyed <= 10)
                return tanksDestroyed;
            else if (tanksDestroyed > 10 && tanksDestroyed <= 20)
                return 10 + (tanksDestroyed - 10) * 2;
            else if (tanksDestroyed > 20 && tanksDestroyed <= 30)
                return 10 + 20 + (tanksDestroyed - 20) * 3;
            else if (tanksDestroyed > 30 && tanksDestroyed <= 40)
                return 10 + 20 + 30 + (tanksDestroyed - 30) * 4;
            else if (tanksDestroyed > 40 && tanksDestroyed <= 50)
                return 10 + 20 + 30 + 40 + (tanksDestroyed - 40) * 5;
            else
                return 10 + 20 + 30 + 40 + 50 + (tanksDestroyed - 50) * 6;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char c = e.KeyChar;
            if (c == (Char)Keys.Up || c == (Char)Keys.Down || c == (Char)Keys.Left || c == (Char)Keys.Right)
                isMoving = true;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.P)
            {
                if (isPaused)
                    StartAllTimers();
                else
                    stopAllTimers();
                isPaused = !isPaused;
            }
        }

    }
}
