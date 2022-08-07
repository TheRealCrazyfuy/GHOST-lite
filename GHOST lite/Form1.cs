using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FluxAPI;
using System.IO;
using System.Diagnostics;

namespace GHOST_lite
{
    public partial class Form1 : Form
    {
        FluxAPI.API flux = new FluxAPI.API();
        public Form1()
        {
            InitializeComponent();
        }
        Point lastPoint;

        private void button5_Click(object sender, EventArgs e)
        {
            flux.Inject(); //Inject the API
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flux.Execute(fastColoredTextBox1.Text); //Execute from the text in FCTB
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Clear(); //Clear FCTB
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.CreateNew))
                using (StreamWriter sw = new StreamWriter(s))
                {
                    sw.Write(fastColoredTextBox1.Text);  //Save file from FCTB
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                openFileDialog1.Title = "Open";
                fastColoredTextBox1.Text = File.ReadAllText(openFileDialog1.FileName); //Load file to FCTB
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized; //Minimize the form
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill(); //Kills the process
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FluxAPI.API.OnInject += delegate () //Execute when injects
            {
                flux.Execute("game.StarterGui:SetCore('SendNotification', {Title = 'Injection succesful', Text = 'GHOST lite injected!', Time = 15})"); //Notification that injection was succesful
                flux.Execute("loadstring(game:HttpGet('https://raw.githubusercontent.com/EdgeIY/infiniteyield/master/source'))()"); //Infinite yield

            };

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            flux.ToggleInternalUI(); //Toggles the internal UI
        }
    }
}
