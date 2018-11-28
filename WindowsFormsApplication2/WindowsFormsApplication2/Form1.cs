using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {
            int tong = 0;
            int a = int.Parse(ud1.Text);
            int b = int.Parse(ud2.Text);
            if (cb1.Checked) {
                tong = 100000 + a * 150000 + b * 200000;
                txt2.Text = tong.ToString();
                DialogResult dr = MessageBox.Show("Bạn có muốn tính tiền ko tk ml", "xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            }
            else if (cb2.Checked) {
                tong = 1500000 + a * 150000 + b * 200000;
                txt2.Text = tong.ToString();
            }
            else if (cb1.Checked && cb2.Checked) {
                tong = 100000 + 1500000 + a * 150000 + b * 200000;
                txt2.Text = tong.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            txt2.Text = "";

        }
    }
}