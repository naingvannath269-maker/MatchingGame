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
    public partial class Form9 : Form
    {
        Form current_Level;
        public Form9(Form Level_Form)
        {
            InitializeComponent();
            current_Level = Level_Form;
        }

        //SoundPlayer soundbg = new SoundPlayer(@"Sound\Juii_ah_nis_sahav_nas.wav");
        WindowsMediaPlayer soundbg = new WindowsMediaPlayer();

        private void btn_next_Click(object sender, EventArgs e)
        {
            Form next_Level = null;

            if (current_Level is Form2)
                next_Level = new Form3();
            else if (current_Level is Form3) 
                next_Level = new Form4();
            else if (current_Level is Form4)
                next_Level = new Form5();
            else if (current_Level is Form5)
                next_Level = new Form6();
            else if(current_Level is Form6)
                next_Level = new Form7();
            else
            {
                MessageBox.Show("You finished all levels👏👏👏");
                Application.Exit();
                return;
            }
            
            next_Level.Show();
            soundbg.controls.stop();
            current_Level.Close(); // close won level
            this.Close(); // Close win form 
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            soundbg.controls.stop();
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
            this.Close();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            soundbg.URL = "win.mp3";
            soundbg.controls.play();
        }
    }
}
