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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        WindowsMediaPlayer soundcl = new WindowsMediaPlayer();
        WindowsMediaPlayer soundbg = new WindowsMediaPlayer();
        WindowsMediaPlayer matching = new WindowsMediaPlayer();

        //Declair
        PictureBox firstclick = null;
        PictureBox secondclick = null;
        PictureBox Hide_firstclick = null;
        PictureBox Hide_secondclick = null;

        // score
        int score;

        // set time
        int timeleft = 100;

        // Random pic
        Random random_picture = new Random();

        // Add Picture by using list string 
        List<string> ImageName = new List<string>
        {
            "pea","pea","lemon","lemon", "donut","donut","onion","onion","tomato","tomato",
            "roll-cake","roll-cake","mango","mango","corn","corn","apple","apple","panna-cotta","panna-cotta",
            "blueberry","blueberry","cabbage","cabbage","salad","salad","bananas","bananas","bell-pepper","bell-pepper",
            "orange","orange"
        };
        private void Form7_Load(object sender, EventArgs e)
        {
            lbl_score.Text = "";
            soundbg.URL = "audiocoffee-kid-background-110768.mp3";
            soundbg.settings.setMode("loop", true);
            soundbg.controls.play();

            //using foreach loop
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

            //set time enabled
            if (timer1.Enabled)
                return;

            PictureBox clickedbox = sender as PictureBox;
            if (clickedbox == null)
                return;

            string image_name = clickedbox.Tag.ToString();
            clickedbox.Image = (Image)Properties.Resources.ResourceManager.GetObject(image_name);

            // Set the first click
            if(firstclick == null)
            {
                firstclick = clickedbox;
                return;
            }

            if (clickedbox == firstclick)
                return;

            // set the second click
            secondclick = clickedbox;

            // Set the matching condition
            if(firstclick.Tag.ToString() == secondclick.Tag.ToString())
            {
                matching.URL = "Matching.wav";
                matching.controls.play();

                //Lock the matching
                firstclick.Enabled = false;
                secondclick.Enabled = false;

                timer1.Start();

                // set it to null;
                firstclick = null;
                secondclick = null;

                // score 
                score++;
                lbl_score.Text = score.ToString();

                if (score == 16)
                {
                    timer2.Stop();
                    soundbg.controls.stop();
                    Win_Game();
                }
                
            }
            // If it not matching
            else
            {
                Hide_firstclick = firstclick;
                Hide_secondclick = secondclick;

                firstclick = null;
                secondclick = null;

                timer1.Start();

            }
        }

        private void Game_time(object sender, EventArgs e)
        {
            timer1.Stop();
            if (Hide_firstclick != null && Hide_secondclick != null)
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
