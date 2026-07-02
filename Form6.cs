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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        WindowsMediaPlayer soundcl = new WindowsMediaPlayer();
        WindowsMediaPlayer soundbg = new WindowsMediaPlayer();
        WindowsMediaPlayer matching = new WindowsMediaPlayer();

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        //Declare datatyle
        PictureBox firstclick = null;
        PictureBox secondclick = null;
        PictureBox Hide_firstclick = null;
        PictureBox Hide_secondclick = null;

        // score 
        int score;

        // set time
        int timeleft = 80;

        // Random Picture
        Random random_pic = new Random();

        // Add picture bty using string 
        List<string> ImageName = new List<string>
        {
            "bananas","bananas","bell-pepper","bell-pepper","cabbage","cabbage","blueberry","blueberry",
            "salad","salad","potato","potato","dragon-fruit","dragon-fruit","drink","drink","mango","mango",
            "pancakes","pancakes","roll-cake","roll-cake","moon-pie","moon-pie","gelato","gelato","eggplant","eggplant"
        };
        private void Form6_Load(object sender, EventArgs e)
        {
            lbl_score.Text = "";
            soundbg.URL = "cutewhistle.wav";
            soundbg.controls.play();
            foreach (PictureBox box in tableLayoutPanel1.Controls)
            {
                int index = random_pic.Next(ImageName.Count);
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

            string image_name = clickedBox.Tag.ToString();
            clickedBox.Image = (Image)Properties.Resources.ResourceManager.GetObject(image_name);

            //Set the first Click
            if (firstclick == null)
            {
                firstclick = clickedBox;
                return;
            }
            if (clickedBox == firstclick)
                return;

            //Second Click
            secondclick = clickedBox;

            // Matching condition
            if (firstclick.Tag.ToString() == secondclick.Tag.ToString())
            {
                matching.URL = "Matching.wav";
                matching.controls.play();

                //Lock the matching picture 
                firstclick.Enabled = false;
                secondclick.Enabled = false;

                timer1.Start();

                // If it doesn't has value
                firstclick = null;
                secondclick = null;

                //score
                score++;
                lbl_score.Text = score.ToString();
                if (score == 14)
                {
                    timer2.Stop();
                    Win_Game();
                }

            }
            //if it dosen't match 
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
            if (Hide_firstclick != null && Hide_secondclick != null) 
            {
                Hide_firstclick.Image = null;
                Hide_secondclick.Image = null;
            }
            Hide_firstclick = null;
            Hide_secondclick = null;

        }

        private void Time_Countdown(object sender, EventArgs e)
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
