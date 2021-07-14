using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Process_schdeuling
{
    public partial class Form1 : Form
    {
        public static int process_no;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            uint parsedValue;
            if (!uint.TryParse(process_num.Text, out parsedValue))
            {
                MessageBox.Show("error with inputs");
                return;
            }
            process_no = int.Parse(process_num.Text);
            if(algorithm.Text== "First come first served") // go to FCFS gui 
            {
                this.Hide();
                FCFS fcfs1 = new FCFS();
                fcfs1.Show();
            }
            if (algorithm.Text == "Round robin") // go to round robin gui 
            {
                this.Hide();
                round_robin r_r = new round_robin();
                r_r.Show();
            }
            if (algorithm.Text == "Priority non preemptive") // go to round robin gui 
            {
                this.Hide();
                Nonpre_priority nonproirity = new Nonpre_priority();
                nonproirity.Show();
            }
            if (algorithm.Text == "Priority preemptive") // go to round robin gui 
            {
                this.Hide();
                prem_priority proirity = new prem_priority();
                proirity.Show();
            }
            if (algorithm.Text == "Sjf non preemptive") // go to round robin gui 
            {
                this.Hide();
                SJF sjf_non = new SJF();
                sjf_non.Show();
            }
            if (algorithm.Text == "Sjf preemptive") // go to round robin gui 
            {
                this.Hide();
                SJF_prem sjf_prem = new SJF_prem();
                sjf_prem.Show();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void algorithm_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {

        }

        private void process_num_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
