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

namespace Matchin_game_pro
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        WindowsMediaPlayer soundcl = new WindowsMediaPlayer();
        WindowsMediaPlayer soundbg = new WindowsMediaPlayer();
        WindowsMediaPlayer matching = new WindowsMediaPlayer();

        // Declare the data type of click
        PictureBox firstClick = null;
        PictureBox secondClick = null;
        PictureBox Hide_first_click = null;
        PictureBox Hide_second_click = null;

        // score 
        int score;

        // Random the picture
        Random random_picture = new Random();

        // Time Countdown
        int time_left = 30;

        // Uisng string 
        List<string> ImageName = new List<string>
        {
            "carrot","carrot","pea", "pea","bell-pepper","bell-pepper","cabbage","cabbage","potato","potato",
            "salad","salad"
        };
        private void Form3_Load(object sender, EventArgs e)
        {
            lbl_score.Text = "";
            soundbg.URL = "cutesong.wav";
            soundbg.controls.play();

            //Using foreach loop
            foreach (PictureBox box in tableLayoutPanel1.Controls)
            {
                int index = random_picture.Next(ImageName.Count);
                string image = ImageName[index];
                box.Tag = image;
                box.Image = null;

                ImageName.RemoveAt(index);
                box.Click += pictureBox_Click;
            }

            // set time countdown
            lbl_timeleft.Text = lbl_timeleft + "s";
            timer2.Start();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            soundcl.URL = "switch14.wav";
            soundcl.controls.play();

            if (timer1.Enabled)
                return;

            PictureBox clickedBox = sender as PictureBox;
            if (clickedBox == null)
                return;

            // Image
            string ImageName = clickedBox.Tag.ToString();
            clickedBox.Image = (Image)Properties.Resources.ResourceManager.GetObject(ImageName);

            // first click
            if(firstClick == null)
            {
                firstClick = clickedBox;
                return;
            }

            
            if(clickedBox == firstClick)
                return;

            secondClick = clickedBox;
            // Matching Condition
            if (firstClick.Tag.ToString() == secondClick.Tag.ToString())
            {
                matching.URL = "Matching.wav";
                matching.controls.play();

                // Lock the matching 
                firstClick.Enabled = false;
                secondClick.Enabled = false;

                timer1.Start();

                // if it doesn't has value
                firstClick = null;
                secondClick = null;

                // Score
                score++;
                lbl_score.Text = score.ToString();

                if (score == 6)
                {
                    soundbg.controls.stop();
                    timer2.Stop();
                    Win_Game();


                }
            }
            // if it is not matching
            else
            {
                Hide_first_click = firstClick;
                Hide_second_click = secondClick;

                firstClick = null;
                secondClick = null;

                timer1.Start();
            }
        }


        private void Game_Time(object sender, EventArgs e)
        {
            timer1.Stop();
            if (Hide_first_click != null && Hide_second_click != null)
            {
                Hide_first_click.Image = null;
                Hide_second_click.Image = null;
            }
            Hide_first_click = null;
            Hide_second_click = null;
        }

        private void Time_Countdown_game(object sender, EventArgs e)
        {
            if(time_left > 0)
            {
                time_left--;
                lbl_timeleft.Text = time_left + "s";
            }
            else
            {
                soundbg.controls.stop();
                timer2.Stop();
                GameOver();
            }
        }
        // Go to Gameover form when the player is lost the game 
        private void GameOver()
        {
            Form8 gameover = new Form8(this);
            gameover.Show();
            this.Close();
        }

        // wining game and go to the next level form 
        private void Win_Game()
        {
            Form9 win = new Form9(this);
            win.Show();
            this.Hide();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            timer2.Stop();
            if (MessageBox.Show("You haven't finished yet!!\nDo you want to exit?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                soundbg.controls.stop();
                this.Hide();
                Form1 form1 = new Form1();
                form1.Show();
            }
            else
            {
                timer2.Start();
            }
        }
    }
}

