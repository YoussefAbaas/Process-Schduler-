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
    public partial class SJF : Form
    {
        public SJF()
        {
            InitializeComponent();
        }

        private void SJF_Load(object sender, EventArgs e)
        {
            create_p();
        }
        private void create_p()
        {
            int MaxProsNum = Form1.process_no;
            Label[] labels = new Label[MaxProsNum];
            TextBox[] textBoxsArr = new TextBox[MaxProsNum];
            TextBox[] textBoxsBrst = new TextBox[MaxProsNum];
            TextBox[] textBoxsP = new TextBox[MaxProsNum];
            int i;
            for (i = 0; i < MaxProsNum; i++)
            {
                //Create label
                labels[i] = new Label();
                labels[i].Text = String.Format("P{0}", i);
                labels[i].Name = String.Format("L{0}", i);
                labels[i].Tag = i;
                //Position label on screen
                labels[i].Width = 59;
                labels[i].Height = 20;
                labels[i].Left = 4;
                labels[i].Top = 41 + (i * 20);
                //Create textbox
                textBoxsArr[i] = new TextBox();
                textBoxsArr[i].Name = String.Format("P{0}", i);
                textBoxsArr[i].Tag = i;
                textBoxsArr[i].Width = 59;
                textBoxsArr[i].Height = 20;
                textBoxsArr[i].Left = 4;
                textBoxsArr[i].Top = 41 + (i * 20);
                //Create textbox
                textBoxsBrst[i] = new TextBox();
                textBoxsBrst[i].Name = String.Format("B{0}", i);
                textBoxsBrst[i].Tag = i;
                textBoxsBrst[i].Width = 59;
                textBoxsBrst[i].Height = 20;
                textBoxsBrst[i].Left = 4;
                textBoxsBrst[i].Top = 41 + (i * 20);
                //Create textbox
                textBoxsP[i] = new TextBox();
                textBoxsP[i].Name = String.Format("H{0}", i);
                textBoxsP[i].Tag = i;
                textBoxsP[i].Width = 59;
                textBoxsP[i].Height = 20;
                textBoxsP[i].Left = 4;
                textBoxsP[i].Top = 41 + (i * 20);
                groupBox1.Controls.Add(labels[i]);
                groupBox2.Controls.Add(textBoxsArr[i]);
                groupBox3.Controls.Add(textBoxsBrst[i]);
               // groupBox4.Controls.Add(textBoxsP[i]);

            }
        }
        private void Draw_Gantt(int MaxProsNum, process[] processes)
        {



            for (int i = 0; i < MaxProsNum; i++)
            {
                /** Create new labels **/
                Label newLabel = new Label();  // process name
                Label newLabel2 = new Label();  // starting time 

                newLabel.Text = processes[i].pid;
                newLabel2.Text = Convert.ToString(processes[i].start_time);
                newLabel.BackColor = Color.Gray;
                newLabel.ForeColor = Color.White;
                newLabel2.BackColor = Color.Black;
                newLabel2.ForeColor = Color.White;
                newLabel2.Height = newLabel.Height;
                newLabel2.Width = newLabel.Width / 4;
                newLabel.Margin = new Padding(0);
                newLabel2.Margin = new Padding(0);
                newLabel.TextAlign = ContentAlignment.MiddleCenter;
                newLabel2.TextAlign = ContentAlignment.MiddleCenter;

                flowLayoutPanel1.Controls.Add(newLabel2);
                flowLayoutPanel1.Controls.Add(newLabel);
                if (i == MaxProsNum - 1)
                {// the last label printed

                    Label newLabel3 = new Label(); // End time of last process

                    newLabel3.Text = Convert.ToString(processes[i].start_time + processes[i].burst_time);
                    newLabel3.BackColor = Color.Black;
                    newLabel3.ForeColor = Color.White;
                    newLabel3.Height = newLabel.Height;
                    newLabel3.Width = newLabel.Width / 4;
                    newLabel3.Margin = new Padding(0);
                    newLabel3.TextAlign = ContentAlignment.MiddleCenter;

                    flowLayoutPanel1.Controls.Add(newLabel3);
                }

            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        private void button1_Click_1(object sender, EventArgs e)
        {

            flowLayoutPanel1.Controls.Clear();
            int MaxProsNum = Form1.process_no;
            process[] processes = new process[MaxProsNum];
            int[] starting_time = new int[MaxProsNum];
            int[] end_time = new int[MaxProsNum];
            int[] waiting_time = new int[MaxProsNum];
            int[] burst_time = new int[MaxProsNum];
            int counter = 0;
            int counter1 = 0;
            int counter2 = 0;
            int counter3 = 0;
            double total_wait = 0;
            double avg_wait;

            foreach (Control c in groupBox1.Controls)
            {
                if (c is Label)
                {
                    Label txt = new Label();
                    txt = (Label)c;
                    if (txt.Text != "")
                    {
                        string s = c.Text;
                        processes[counter2].pid = c.Text;
                        counter2++;

                    }
                }
            }
            foreach (Control c in groupBox2.Controls)
            {
                uint parsedValue;
                if (!uint.TryParse(c.Text, out parsedValue))
                {
                    MessageBox.Show("error with inputs");
                    return;
                }
                if (c is TextBox)
                {
                    TextBox txt = new TextBox();
                    txt = (TextBox)c;
                    if (txt.Text != "")
                    {
                        processes[counter].arrival_time = int.Parse(c.Text);
                        counter++;

                    }
                }
            }
            foreach (Control c in groupBox3.Controls)
            {
                if (c is TextBox)
                {
                    uint parsedValue;
                    if (!uint.TryParse(c.Text, out parsedValue))
                    {
                        MessageBox.Show("error with inputs");
                        return;
                    }
                    TextBox txt = new TextBox();
                    txt = (TextBox)c;
                    if (txt.Text != "")
                    {
                        processes[counter1].burst_time = int.Parse(c.Text);

                        counter1++;
                      
                    }
                }
            }
           
            int current_time = 0;
            int completed = 0;
            int prev = 0;
            int[] is_completed = Enumerable.Repeat((int)0, MaxProsNum).ToArray();

            while (completed != MaxProsNum)
            {
                int idx = -1;
                int mx = 1000000;
                for (int i = 0; i < MaxProsNum; i++)
                {
                    if (processes[i].arrival_time <= current_time && is_completed[i] == 0)
                    {
                        if (processes[i].burst_time < mx)
                        {
                            mx = processes[i].burst_time;
                            idx = i;
                        }
                        if (processes[i].burst_time == mx)
                        {
                            if (processes[i].arrival_time < processes[idx].arrival_time)
                            {
                                mx = processes[i].burst_time;
                                idx = i;
                            }
                        }
                    }
                }
                if (idx != -1)
                {
                    processes[idx].start_time = current_time;
                    processes[idx].completion_time = processes[idx].start_time + processes[idx].burst_time;
                    processes[idx].waiting_time = processes[idx].start_time - processes[idx].arrival_time;


                    total_wait += processes[idx].waiting_time;


                    is_completed[idx] = 1;
                    completed++;
                    current_time = processes[idx].completion_time;
                    prev = current_time;
                }
                else
                {
                    current_time++;
                }

            }

            

            avg_wait = total_wait / MaxProsNum;
            textBox1.Text = avg_wait.ToString();
            Array.Sort(processes, delegate (process p1, process p2) {
                return p1.start_time.CompareTo(p2.start_time);
            });
            Draw_Gantt(MaxProsNum, processes);



        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
