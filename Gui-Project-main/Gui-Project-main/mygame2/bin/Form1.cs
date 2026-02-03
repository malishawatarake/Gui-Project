using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace mygame2
{
    public partial class Form1 : Form
    {
        WindowsMediaPlayer gameMedia;
        WindowsMediaPlayer shootgMedia;
        WindowsMediaPlayer explosion;


        int playerSpeed;
        PictureBox[] stars;
        int backgroundspeed;
        Random rnd;
        PictureBox[] munitions;
        int MunitionSpeed;

        PictureBox[] enemies;
        int enemiSpeed;
        PictureBox[] enemiesMunition;
        int enemiesMunitionSpeed;

        int score;
        int level;
        int deficulty;
        bool pause;
        bool gameIsOver;

        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pause = false;
            gameIsOver = false;
            score = 0;
            level = 1;
            deficulty = 9;



            playerSpeed = 4;
            backgroundspeed = 4;
            MunitionSpeed = 20;
            enemiSpeed = 4;
            enemiesMunitionSpeed = 4;
            munitions = new PictureBox[3];

            Image munition = Image.FromFile(@"D:asserts\munition.png");

            Image enemi1 = Image.FromFile("D:asserts\\E1.png");
            Image enemi2 = Image.FromFile("D:asserts\\E2.png");
            Image enemi3 = Image.FromFile("D:asserts\\E3.png");
            Image boss1 = Image.FromFile("D:asserts\\Boss1.png");
            Image boss2 = Image.FromFile("D:asserts\\Boss2.png");

            enemies = new PictureBox[10];
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i]= new PictureBox();
                enemies[i].Size = new Size(40, 40);
                enemies[i].SizeMode = PictureBoxSizeMode.Zoom;
                enemies[i].BorderStyle = BorderStyle.None;
                enemies[i].Visible= false;
                this.Controls.Add(enemies[i]);
                enemies[i].Location = new Point((i + 1) * 50, -50);

            }
                enemies[0].Image = boss1;
                enemies[1].Image= boss2;
                enemies[2].Image = enemi3;
                enemies[3].Image = enemi3;
                enemies[4].Image = enemi1;
                enemies[5].Image = enemi3;
                enemies[6].Image = enemi2;
                enemies[7].Image = enemi3;
                enemies[8].Image = boss2;
                                                                                                                                                                                       
            for(int i=0;i< munitions.Length; i++)
            {
                munitions[i]= new PictureBox();
                munitions[i].Size = new Size(8, 8);
                munitions[i].Image= munition;
                munitions[i].SizeMode = PictureBoxSizeMode.Zoom;
                munitions[i].BorderStyle= BorderStyle.None;
                this.Controls.Add(munitions[i]);
            }
               gameMedia = new WindowsMediaPlayer();
               shootgMedia= new WindowsMediaPlayer();
               explosion = new WindowsMediaPlayer();

            gameMedia.URL = "D:\\songs\\GameSong.mp3";
            shootgMedia.URL = "D:\\songs\\shoot.mp3";
            explosion.URL = "D:\\songs\\boom.mp3";

            gameMedia.settings.setMode("loop", true);
            gameMedia.settings.volume = 5;
            shootgMedia.settings.volume = 1;
            explosion.settings.volume = 6;





            stars = new PictureBox[10];
            rnd = new Random();

            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new PictureBox();
                stars[i].BorderStyle= BorderStyle.None;
                stars[i].Location = new Point(rnd.Next(20, 500), rnd.Next(-10, 400));
                if (i % 2 == 1)
                {
                    stars[i].Size = new Size(2, 2);
                    stars[i].BackColor = Color.Wheat;
                }
                else
                {
                    stars[i].Size = new Size(3, 3);
                    stars[i].BackColor = Color.DarkGray;
                }
                this.Controls.Add(stars[i]);
            }

            enemiesMunition = new PictureBox[10];

            for(int i=0;i< enemiesMunition.Length; i++)
            {
                enemiesMunition[i] = new PictureBox();
                enemiesMunition[i].Size= new Size(2, 25);
                enemiesMunition[i].Visible= false;
                enemiesMunition[i].BackColor= Color.Yellow;
                int x = rnd.Next(0, 10);
                enemiesMunition[i].Location = new Point(enemies[x].Location.X, enemies[x].Location.Y - 20);
                this.Controls.Add(enemiesMunition[i]);
            }

            gameMedia.controls.play();

        }


        private void movebtimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < stars.Length / 2; i++) {
                stars[i].Top += backgroundspeed;
                if (stars[i].Top >= this.Height)
                {
                    stars[i].Top = -stars[i].Height;

                }
            }
            for (int i = stars.Length / 2; i < stars.Length; i++)
            {
                stars[i].Top += backgroundspeed - 2;
                if(stars[i].Top >= this.Height)
                {
                    stars[i].Top = -stars[i].Height;
                }
            }
        }

        private void LeftMoveTimer_Tick(object sender, EventArgs e)
        {
            if (player.Left > 10)
            {
                player.Left -= playerSpeed;
            }
        }

        private void RightMoveTimer_Tick(object sender, EventArgs e)
        {
            if (player.Right < 580) {
                player.Left += playerSpeed;
            }
        }

        private void DownMoveTimer_Tick(object sender, EventArgs e)
        {
            if (player.Top < 400)
            {
                player.Top += playerSpeed;
            }
        }

        private void UpMoveTimer_Tick(object sender, EventArgs e)
        {
            if (player.Top > 10)
            {
                player.Top -= playerSpeed;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!pause)
            {
                if (e.KeyCode == Keys.Right)
                {
                    RightMoveTimer.Start();

                }
                if (e.KeyCode == Keys.Left)
                {
                    LeftMoveTimer.Start();
                }
                if (e.KeyCode == Keys.Down)
                {
                    DownMoveTimer.Start();
                }
                if (e.KeyCode == Keys.Up)
                {
                    UpMoveTimer.Start();
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            RightMoveTimer.Stop();
            LeftMoveTimer.Stop(); 
            DownMoveTimer.Stop();
            UpMoveTimer.Stop();

            if (!gameIsOver)
            {
                if (pause)
                {
                    StartTimers();
                    label1.Visible=false;
                    gameMedia.controls.play();
                    pause= false;
                }
                else
                {
                    label1.Location=new Point(this.Width/2-120,150);
                    label1.Text = "PAUSED";
                    label1.Visible = true;
                    gameMedia.controls.pause();
                    StopTimers();
                    pause= true;
                }
            }
        }

        private void MoveMunitionTimer_Tick(object sender, EventArgs e)
        {
            shootgMedia.controls.play();
            for (int i = 0; i < munitions.Length; i++) {
                if (munitions[i].Top > 0)
                {
                    munitions[i].Visible = true;
                    munitions[i].Top -= MunitionSpeed;
                    collision();
                }
                else
                {
                    munitions[i].Visible=false;
                    munitions[i].Location = new Point(player.Location.X + 20, player.Location.Y - i * 30);
                }
            
            }
        }

        private void moveEnemiesTimer_Tick(object sender, EventArgs e)
        {
            MoveEnemies(enemies, enemiSpeed);
        }

        private void MoveEnemies(PictureBox[] array,int speed)
        {
            for(int i=0; i<array.Length; i++)
            {
                array[i].Visible = true;
                array[i].Top += speed;

                if (array[i].Top > this.Height)
                {
                    array[i].Location = new Point((i + 1) * 50, -200);
                }
            }
        }

        private void collision()
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (munitions[0].Bounds.IntersectsWith(enemies[i].Bounds)||
                    munitions[1].Bounds.IntersectsWith(enemies[i].Bounds)|| munitions[2].Bounds.IntersectsWith(enemies[i].Bounds))
                {
                    explosion.controls.play();

                    score += 1;
                    scorelbl.Text =(score<10)?"0"+score.ToString():score.ToString();

                    if (score % 30 == 0)
                    {
                        level += 1;
                        levellbl.Text = (level < 10) ? "0" + level.ToString() : level.ToString();

                        if (enemiSpeed <= 10 && enemiesMunitionSpeed <= 10 && deficulty >= 0)
                        {
                            deficulty--;
                            enemiSpeed++;
                            enemiesMunitionSpeed++;
                        }
                        if (level == 10)
                        {
                            GameOver("NICE DONE");
                        }
                    }
                    enemies[i].Location = new Point((i + 1) * 50, -100);
                }
                if(player.Bounds.IntersectsWith(enemies[i].Bounds)){

                    explosion.settings.volume = 30;
                    explosion.controls.play();
                    player.Visible = false;
                    GameOver("GAME OVER");
                }
                   
                

            }

        }

        private void GameOver(string str)
        {
            label1.Text= str;
            label1.Location = new Point(70, 70);
            label1.Visible = true;
            ReplayBtn.  Visible = true;
            ExitBtn.Visible = true;
            gameMedia.controls.stop();
            StopTimers();
        }

        private void StopTimers()
        {
            movebtimer.Stop();
            moveEnemiesTimer.Stop();
            MoveMunitionTimer.Stop();
            EnemiesMuntionTimer.Stop();

        }

        private void StartTimers()
        {
            movebtimer.Start();
            moveEnemiesTimer.Start();
            MoveMunitionTimer.Start();
            EnemiesMuntionTimer.Start();

        }

        private void EnemiesMuntionTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < enemiesMunition.Length-deficulty; i++) {
                if (enemiesMunition[i].Top < this.Height)
                {
                    enemiesMunition[i].Visible = true;
                    enemiesMunition[i].Top += enemiesMunitionSpeed;
                }
                else
                {
                    enemiesMunition[i].Visible=false;
                    int x= rnd.Next(0,10);
                    enemiesMunition[i].Location = new Point(enemies[x].Location.X + 20, enemies[x].Location.Y + 30);
                }
            }
        }
        private void CollisionWithEnemisMuntion()
        {
            for (int i = 0; i < enemiesMunition.Length; i++)
            {
                if (enemiesMunition[i].Bounds.IntersectsWith(player.Bounds))
                {
                    enemiesMunition[i].Visible = false;
                    explosion.settings.volume = 30;
                    explosion.controls.play();
                    player.Visible = false;
                    GameOver("Game Over");
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Replay_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponent();
            Form1_Load(e, e);
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }
    }
    }


