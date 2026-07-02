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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        WindowsMediaPlayer soundcl = new WindowsMediaPlayer();
        WindowsMediaPlayer soundbg = new WindowsMediaPlayer();
        WindowsMediaPlayer matching = new WindowsMediaPlayer();

        // Daclare datatype
        PictureBox firstClick = null;
        PictureBox secondClick = null;
        PictureBox Hide_first_Click = null;
        PictureBox Hide_second_click = null;

        // score
        int score;

        // count down time
        int timeleft = 45;

        //Random Picture
        Random random_picture = new Random();


        // Using String
        List<string> ImageName = new List<string>
        {
            "drink","drink","gelato","gelato","cookie","cookie","donut","donut",
            "pancakes", "pancakes", "cocktail","cocktail","moon-pie","moon-pie",
            "roll-cake","roll-cake"
        };
        private void Form4_Load(object sender, EventArgs e)
        {
            lbl_score.Text = "";
            soundbg.URL = "cute_song.wav";
            soundbg.settings.setMode("loop", true);
            soundbg.controls.play();

            // foreach loop
            foreach(PictureBox box in tableLayoutPanel1.Controls)
            {
                int index = random_picture.Next(ImageName.Count);
                string image = ImageName[index];

                box.Tag = image;
                box.Image = null;

                ImageName.RemoveAt(index);
                box.Click += pictureBox_Click;
            }
            // set time
            lbl_time.Text = timeleft + "s";
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

            string image_name = clickedBox.Tag.ToString();
            clickedBox.Image = (Image)Properties.Resources.ResourceManager.GetObject(image_name);

            //Set the first Click
            if (firstClick == null)
            {
                firstClick = clickedBox;
                return;
            }
            if (clickedBox == firstClick)
                return;

            //Second Click
            secondClick = clickedBox;

            // Matching condition
            if(firstClick.Tag.ToString() == secondClick.Tag.ToString())
            {
                matching.URL = "Matching.wav";
                matching.controls.play();

                //Lock the matching picture 
                firstClick.Enabled = false;
                secondClick.Enabled = false;

                timer1.Start();

                // If it doesn't has value
                firstClick = null;
                secondClick = null;

                //score
                score++;
                lbl_score.Text = score.ToString();
                if(score == 8)
                {
                    soundbg.controls.stop();
                    timer2.Stop();
                    Win_Game();
                }

            }
            //if it dosen't match 
            else
            {
                Hide_first_Click = firstClick;
                Hide_second_click = secondClick;

                firstClick = null;
                secondClick = null;

                timer1.Start();
            }
                
        }

        private void Game_Time(object sender, EventArgs e)
        {
            timer1.Stop();

            if(Hide_first_Click!=null && Hide_second_click != null)
            {
                Hide_first_Click.Image = null;
                Hide_second_click.Image = null;
            }
            Hide_first_Click = null;
            Hide_second_click = null;
        }

        private void time_countdown(object sender, EventArgs e)
        {
            if (timeleft > 0)
            {
                timeleft--;
                lbl_time.Text = timeleft + "s";
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
