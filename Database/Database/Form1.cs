using System;
using System.Windows.Forms;

namespace Database
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);

        //    if (System.Diagnostics.Debugger.IsAttached)
        //    {
        //        EnterPage form = new EnterPage();
        //        form.Show();
        //    }
        //}

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "admin" && textBox2.Text == "1234")
            {
                MessageBox.Show("Login successful. Welcome!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                EnterPageForm form = new EnterPageForm();
                this.Hide();
                form.ShowDialog();

            }
            else if (textBox1.Text == "" && textBox2.Text == "")
            {
                MessageBox.Show("Username and Password are not entered", "Alert", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("Please Enter your Username!", "Alert", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Please Enter your Password!", "Alert", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            else if (textBox1.Text != "admin")
            {
                MessageBox.Show("Username is wrong!", "Alert", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            else if (textBox2.Text != "1234")
            {
                MessageBox.Show("Password is wrong!", "Alert", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }
    }

}
