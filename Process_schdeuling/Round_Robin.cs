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
    public partial class round_robin : Form
    {
        public round_robin()
        {
            InitializeComponent();
        }

        private void round_robin_Load(object sender, EventArgs e)
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
            for (int i = 0; i < MaxProsNum; i++)
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
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            uint parsedValue;
            if (!uint.TryParse(quantum.Text, out parsedValue))
            {
                MessageBox.Show("error in quantum time value");
                return;
            }
            flowLayoutPanel1.Controls.Clear();
            int quantum_time = int.Parse(quantum.Text);
            int MaxProsNum = Form1.process_no;
            process[] processes = new process[MaxProsNum];
            int[] starting_time = new int[MaxProsNum];
            int[] end_time = new int[MaxProsNum];
            int[] waiting_time = new int[MaxProsNum];
            int[] burst_time = new int[MaxProsNum];
            int counter = 0;
            int counter1 = 0;
            int counter2 = 0;
            double total_wait = 0;
            double avg_wait;
            int[] remaining_time = new int[MaxProsNum]; // check remaining time (burst)
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
                if (c is TextBox)
                {

                   
                    if (!uint.TryParse(c.Text, out parsedValue))
                    {
                        MessageBox.Show("error with inputs");
                        return;
                    }
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
                         remaining_time[counter1]= int.Parse(c.Text); 
                        counter1++;
                       
                    }
                }
            }

            Array.Sort(processes, delegate (process p1, process p2) {
                return p1.arrival_time.CompareTo(p2.arrival_time);
            });
            Queue<int> q = new Queue<int>();
            
            int[] flag= Enumerable.Repeat((int)0, MaxProsNum).ToArray(); //flag to check processes in queue or not
            
            int current_time = 0;
            int finished = 0; // no of finishing processes
            int index;
            q.Enqueue(0);
            flag[0] = 1;
            while(finished!=MaxProsNum)
            {
                index = q.Peek();
                q.Dequeue();
                if (remaining_time[index]== processes[index].burst_time) // first visit to cpu 
                {
                    processes[index].start_time = Math.Max(current_time, processes[index].arrival_time);
                    current_time = processes[index].start_time;
                }
                if(remaining_time[index]-quantum_time>0) // still need time
                {
                    remaining_time[index] = remaining_time[index]-quantum_time;
                    Draw_Gantt(1,processes[index],current_time+int.Parse(quantum.Text), current_time);
                    current_time = current_time + quantum_time;
                   
                }
                else // finished
                {
                    Draw_Gantt(1, processes[index], current_time + remaining_time[index],current_time);
                    current_time = current_time + remaining_time[index];
                    remaining_time[index] = 0;
                    finished++;
                    processes[index].completion_time = current_time;
                    processes[index].waiting_time = processes[index].completion_time-processes[index].arrival_time- processes[index].burst_time;
                    total_wait =total_wait+processes[index].waiting_time;


                }

                for (int i = 1; i < MaxProsNum; i++) // if process arrived and did not enter before
                {
                    if (remaining_time[i] > 0 && processes[i].arrival_time <= current_time && flag[i] == 0)
                    {
                        q.Enqueue(i);
                        flag[i] = 1;
                    }
                }

                if(remaining_time[index]>0)
                {
                    q.Enqueue(index);

                }
                if (!q.Any()) // if queue is empty search in list for remaining processes
                {
                    for (int i = 1; i < MaxProsNum; i++)
                    {
                        if (remaining_time[i] > 0)
                        {
                            q.Enqueue(i);
                            flag[i] = 1;
                            break;
                        }
                    }
                }
                
                
            }
            avg_wait = total_wait / MaxProsNum;
            textBox1.Text = avg_wait.ToString();
        }
        private void Draw_Gantt(int MaxProsNum, process p, int end_time,int start_time)
        {
            for (int i = 0; i < MaxProsNum; i++)
            {
                /** Create new labels **/
                Label newLabel = new Label();  // process name
                Label newLabel2 = new Label();  // starting time 

                newLabel.Text = p.pid;
                newLabel2.Text = Convert.ToString(start_time);
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

                    newLabel3.Text = Convert.ToString(end_time);
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
