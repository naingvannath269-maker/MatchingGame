using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;


namespace Matchin_game_pro
{

   
    public partial class Form2 : Form
    {
       
        public Form2()
        {
            InitializeComponent();
           
        }

        WindowsMediaPlayer soundcl = new WindowsMediaPlayer();
        WindowsMediaPlayer soundbg = new WindowsMediaPlayer();
        WindowsMediaPlayer matching = new WindowsMediaPlayer();
        

        // Declare datatype
        PictureBox firstClick = null;
        PictureBox secondClick = null;
        PictureBox HideFirstClick = null;
        PictureBox HideSecondClick = null;

        // score
        int score;

        //time countdown
        int timeleft = 20; // 20s

        // Random Picture
        Random random_picture = new Random();

        //Insert name of each picture as a string 
        List<string> ImageName = new List<string>
        {
            "bananas", "bananas", "mango", "mango", "blueberry", "blueberry","cherries","cherries"
        };
        private void Form2_Load(object sender, EventArgs e)
        {
            lbl_score.Text = "";
            soundbg.URL = @"Sound\cute1.wav";
            soundbg.settings.setMode("loop", true);
            soundbg.controls.play();

            // using foreach loop
            foreach (PictureBox box in tableLayoutPanel1.Controls)
            {
                int index = random_picture.Next(ImageName.Count);
                string image = ImageName[index];

                box.Tag = image;
                box.Image = null;
                ImageName.RemoveAt(index);

                box.Click += pictureBox_Click;
            }

            // set the time
            lbl_Time.Text = timeleft + "s";
            timer2.Start();

        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            soundcl.URL = "switch14.wav";
            soundcl.controls.play();
            // Click while hiding picture

            if (timer1.Enabled)
                return;

            PictureBox clickedBox = sender as PictureBox;
            if (clickedBox == null)
                return;

            string image_name = clickedBox.Tag.ToString();
            clickedBox.Image = (Image)Properties.Resources.ResourceManager.GetObject(image_name);

            // set the first click
            if (firstClick == null)
            {
                firstClick = clickedBox;
                return;
            }
            if (clickedBox == firstClick)
                return;

            // set the second click
            secondClick = clickedBox;

            // set matching Condition
            if (firstClick.Tag.ToString() == secondClick.Tag.ToString())
            {
                matching.URL = "Matching.wav";
                matching.controls.play();
                // Matching lock 
                firstClick.Enabled = false;
                secondClick.Enabled = false;

                //
                firstClick = null;
                secondClick = null;

                // score 
                score++;
                lbl_score.Text = score.ToString();

                if (score == 4)
                {
                    soundbg.controls.stop();
                    timer2.Stop();
                    Win_Game();
                }

            }
            // if it is not matching 
            else
            {
                HideFirstClick = firstClick;
                HideSecondClick = secondClick;
                timer1.Start();
                firstClick = null;
                secondClick = null;
                

            }
        }

        private void Game_timer(object sender, EventArgs e)
        {
            timer1.Stop();
            if (HideFirstClick != null && HideSecondClick != null)
            {
                HideFirstClick.Image = null;
                HideSecondClick.Image = null;
            }

            HideFirstClick = null;
            HideSecondClick = null;
        }

        // Time Countdown
        private void Time_Countdown(object sender, EventArgs e)
        {
            //using else if to set the condition
            if (timeleft > 0)
            {
                timeleft--;
                lbl_Time.Text = timeleft + "s";
            }
            // if it is not equal
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
            if(MessageBox.Show("You haven't finished yet!!\nDo you want to exit?", "Alert",MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
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
