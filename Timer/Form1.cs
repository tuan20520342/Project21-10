using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Timer
{
    public partial class Form1 : Form
    {
        static int timeInput=0;
        public Form1()
        {
            InitializeComponent();
            cbAction.SelectedIndex = 0;
            btnCancel.Enabled = false;
        }

        private void btnDo_Click(object sender, EventArgs e)
        {
            try
            {
                timeInput = int.Parse(txtDuration.Text) * 60;
                txtDuration.Enabled = false;
                cbAction.Enabled = false;
                if (timeInput < 0)
                    {
                        throw new Exception("Vui lòng nhập vào một số nguyên dương");
                    }
                else
                {
                    if (!timer1.Enabled)
                    {
                        timer1.Start();
                        btnDo.Enabled = false;
                        btnCancel.Enabled = true;
                    }         
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Vui lòng nhập vào một số nguyên dương");
                txtDuration.Text = string.Empty;
                label5.Text = string.Empty;
            }
            catch (OverflowException)
            {
                    MessageBox.Show("Số vừa nhập quá lớn.");
                    txtDuration.Text = string.Empty;
                    label5.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txtDuration.Text = string.Empty;
                label5.Text = string.Empty;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chương trình đã được cancel thành công.");
            if (timer1.Enabled)
            {
                timer1.Stop();
                label5.Text = "0";
                btnDo.Enabled = true;
                btnCancel.Enabled = false;
                txtDuration.Enabled = true;
                cbAction.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeInput--;
            label5.Text = timeInput.ToString();
            if(timeInput==0)
            {
                timer1.Stop();
                string filename = string.Empty;
                string arguments = string.Empty;
                if(cbAction.SelectedIndex==0)
                {
                    filename = "shutdown.exe";
                    arguments = "-s";
                }
                else if(cbAction.SelectedIndex == 1)
                {
                    filename = "shutdown.exe";
                    arguments = "-l";
                }
                else if(cbAction.SelectedIndex == 2)
                {

                    filename = "shutdown.exe";
                    arguments = "-r";
                }
                else if(cbAction.SelectedIndex == 3)
                {
                    filename = "Rundll32.exe";
                    arguments = "powrprof.dll, SetSuspendState 0,1,0";
                }
                ProcessStartInfo startinfo = new ProcessStartInfo(filename, arguments);
                Process.Start(startinfo);
            }
        }
    }
}
