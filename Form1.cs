using System;
using System.Media;
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
    public partial class Form1 : Form
    {
        // Static flag to ensure music plays only once
        
        public Form1()
        {
            InitializeComponent();

        }

        SoundPlayer soundbg = new SoundPlayer(@"Sound\cute2.wav");

        //linking form to another form
        // Link form1 to form2
        private void btn_lvl1_Click_1(object sender, EventArgs e)
        {
            soundbg.Stop();
            this.Hide();
            Form2 form2 = new Form2();
            form2.ShowDialog();
            

            // stop the music
          //  axWindowsMediaPlayer1.Ctlcontrols.stop();

            // When Form2 closes, resume Form1 music
          //  axWindowsMediaPlayer1.Ctlcontrols.play();
        }
        //Link form2 to form3
        private void btn_lvl2_Click(object sender, EventArgs e)
        {
            soundbg.Stop();
            this.Hide();
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        private void btn_lvl3_Click(object sender, EventArgs e)
        {
            soundbg.Stop();
            this.Hide();
            Form4 form4 = new Form4();
            form4.ShowDialog();
        }

        private void btn_lvl4_Click(object sender, EventArgs e)
        {
            soundbg.Stop();
            this.Hide();
            Form5 form5 = new Form5();
            form5.ShowDialog();
        }

        private void btn_lvl5_Click(object sender, EventArgs e)
        {
            soundbg.Stop();
            this.Hide();
            Form6 form6 = new Form6();
            form6.ShowDialog();
        }

        private void btn_lvl6_Click(object sender, EventArgs e)
        {
            soundbg.Stop();
            this.Hide();
            Form7 form7 = new Form7();
            form7.ShowDialog();
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
            
        }
        // Stop the music
        

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            
            soundbg.PlayLooping();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to exit the game?", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        /*private void Form1_Activated(object sender, EventArgs e)
{
   // Resume Form1 music
   axWindowsMediaPlayer1.Ctlcontrols.play();
}*/

    }
}
