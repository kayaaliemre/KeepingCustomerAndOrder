using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database
{
    public partial class EnterPageForm : Form
    {
        public EnterPageForm()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(MyForm_FormClosed);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            CustomerForm form = new CustomerForm();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            EmployeeForm form = new EmployeeForm();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            ProductForm form = new ProductForm();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            BillingForm form = new BillingForm();
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            SetupForm form = new SetupForm();
            form.Show();
        }
        private void MyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
           Application.Exit();
        }
    }
}
