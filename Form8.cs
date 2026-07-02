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
    public partial class Form8 : Form
    {
        // declare the current form 
        Form current_Level;
        public Form8(Form level_Form)
        {
            InitializeComponent();
            current_Level = level_Form;
        }

        //SoundPlayer soundbg = new SoundPlayer(@"Sound\dek_tv_nah_kon.wav");
        WindowsMediaPlayer soundbg = new WindowsMediaPlayer();

        private void btn_reset_Click(object sender, EventArgs e)
        {
           /* Form2 form2 = new Form2();
            form2.Show();
            this.Close();*/
           // Create a new instance of the same level
           Form new_Level = (Form)Activator.CreateInstance(current_Level.GetType());
            new_Level.Show();
            soundbg.controls.stop();
            current_Level.Close();// close old level
            this.Close(); // close game over form
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            soundbg.controls.stop();
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void Form8_Load(object sender, EventArgs e)
        {

            soundbg.URL = "lose.mp3";
            soundbg.controls.play();
        }
    }
}
