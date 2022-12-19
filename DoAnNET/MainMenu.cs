using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnNET
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Attendances attendances = new Attendances();
            attendances.MdiParent = this;
            attendances.Show();
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Students students = new Students();
            students.MdiParent = this;
            students.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Teachers teachers = new Teachers();
            teachers.MdiParent = this;
            teachers.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Events events = new Events();
            events.MdiParent = this;
            events.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Fees fees = new Fees();
            fees.MdiParent = this;
            fees.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.MdiParent = this;
            form1.Show();
        }

        
    }
}
