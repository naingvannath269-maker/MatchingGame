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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        WindowsMediaPlayer soundcl = new WindowsMediaPlayer();
        WindowsMediaPlayer soundbg = new WindowsMediaPlayer();
        WindowsMediaPlayer matching = new WindowsMediaPlayer();

        // Declare dataType
        PictureBox firstclick = null;
        PictureBox secondclick = null;
        PictureBox Hide_firstclick = null;
        PictureBox Hide_secondclick = null;

        // score
        int score;

        // set time
        int timeleft = 60;

        // Random Picture
        Random random_picture = new Random();

        // Insert picture as a string 
        List<string> ImageName = new List<string>
        {
            "apple","apple", "strawberry","strawberry","pea", "pea",
            "panna-cotta", "panna-cotta","eggplant", "eggplant","lemon","lemon",
            "orange", "orange","onion","onion","tomato","tomato",
            "french-fries","french-fries","donut","donut","corn","corn"
        };
        private void Form5_Load(object sender, EventArgs e)
        {
            lbl_score.Text = "";
            soundbg.URL = "cutewhistle.wav";
            soundbg.controls.play();

            //Using foreach Loop
            foreach (PictureBox box in tableLayoutPanel1.Controls)
            {
                int index = random_picture.Next(ImageName.Count);
                string image = ImageName[index];
                box.Tag = image;
                box.Image = null;
                ImageName.RemoveAt(index);
                box.Click += pictureBox_Click;

            }
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

            //Get image from resource
            string image_pic = clickedBox.Tag.ToString();
            clickedBox.Image = (Image)Properties.Resources.ResourceManager.GetObject(image_pic);

            // set the first click
            if(firstclick == null)
            {
                firstclick = clickedBox;
                return;
            }
            if (clickedBox == firstclick)
                return;

            // set the second click
            secondclick = clickedBox;

            // matching condition
            if(firstclick.Tag.ToString() == secondclick.Tag.ToString())
            {
                matching.URL = "Matching.wav";
                matching.controls.play();

                // Lock the matching
                firstclick.Enabled = false;
                secondclick.Enabled = false;

                timer1.Start();

                firstclick = null;
                secondclick = null;

                // score
                score++;
                lbl_score.Text = score.ToString();

                if(score == 12)
                {
                    soundbg.controls.stop();
                    timer2.Stop();
                    Win_Game();
                }
            }
            else
            {
                Hide_firstclick = firstclick;
                Hide_secondclick = secondclick;

                firstclick = null;
                secondclick = null;

                timer1.Start();

            }
        }

        private void Game_Time(object sender, EventArgs e)
        {
            timer1.Stop();
            if(Hide_firstclick != null && Hide_secondclick != null)
            {
                Hide_firstclick.Image = null;
                Hide_secondclick.Image = null;
            }
            Hide_firstclick = null;
            Hide_secondclick = null;
        }

        private void time_countdown(object sender, EventArgs e)
        {
            if(timeleft > 0)
            {
                timeleft--;
                lbl_time.Text = timeleft + "s";
            }
            else
            {
                soundbg.controls.stop();
                GameOver();
                timer2.Stop();
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
