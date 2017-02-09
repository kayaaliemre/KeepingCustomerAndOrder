using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database
{
    public partial class SetupForm : Form
    {
        public SetupForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Setup_FormClosing);
 
            var cmbItems2 = new List<KeyValuePair<int, string>>();
            cmbItems2.Add(new KeyValuePair<int, string>(0, "--Select--"));
            cmbItems2.Add(new KeyValuePair<int, string>(1, "Hospital"));
            cmbItems2.Add(new KeyValuePair<int, string>(2, "Hotel"));
            cmbItems2.Add(new KeyValuePair<int, string>(3, "Bank"));
            cmbItems2.Add(new KeyValuePair<int, string>(4, "University"));
            cmbItems2.Add(new KeyValuePair<int, string>(5, "Marketing"));
            cmbItems2.Add(new KeyValuePair<int, string>(5, "Military"));
            comboBox2.SelectedValue = 0;
            comboBox2.DataSource = cmbItems2;
            dateTimePicker2.CustomFormat = "";


            connection.Open();
            SqlCommand sc = new SqlCommand("select product_id,product_name from product", connection);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("product_id", typeof(string));
            dt.Columns.Add("product_name", typeof(string));
            dt.Load(reader);

            comboBox1.ValueMember = "product_id";
            comboBox1.DisplayMember = "product_name";
            comboBox1.DataSource = dt;

            connection.Close();


        }
        private void Setup_Load_1(object sender, EventArgs e)
        {
            showDatas();
        }
        private void Setup_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            EnterPageForm form = new EnterPageForm();
            form.Show();
        }
        SqlConnection connection = new SqlConnection(@"server = .\SQL2014 ; database = Software ; Trusted_Connection = yes;");
        private void insertButton(object sender, EventArgs e)
        {
            if (dateTimePicker1.Text == "")
            {
                MessageBox.Show("Please fill all fields!!", "Alert", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Please fill all fields!!", "Alert", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            else if (comboBox1.Text == "")
            {
                MessageBox.Show("Please fill all fields!!", "Alert", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            else
            {
                connection.Open();
                DateTime myDateTime = Convert.ToDateTime(dateTimePicker1.Value);
                SqlCommand command = new SqlCommand("Insert into Setup(setup_date,setup_product,setup_costumer) values('" + myDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "','" + comboBox1.SelectedValue + "','" + textBox2.Text.ToString() + "') select Setup.setup_product from Setup inner join Product ON Setup.setup_product = Product.product_id", connection);
                command.ExecuteNonQuery();
                connection.Close();
                showDatas();
            }
        }

        private void deleteButton(object sender, EventArgs e)
        {
            connection.Open();
            int id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            SqlCommand command = new SqlCommand("Delete from Setup where setup_id=(" + id + ")", connection);
            command.ExecuteNonQuery();
            connection.Close();
            showDatas();
        }

        private void updateButton(object sender, EventArgs e)
        {
            connection.Open();
            int id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            DateTime myDateTime = Convert.ToDateTime(dateTimePicker1.Value);
            SqlCommand command = new SqlCommand("Update Setup set setup_date='" + myDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "',setup_product='" + comboBox1.SelectedValue + "',setup_costumer='" + textBox2.Text.ToString() + "' where setup_id=" + id + "", connection);
            command.ExecuteNonQuery();
            connection.Close();
            showDatas();
        }

        private void searchButton(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            connection.Open();
            SqlCommand command;
            if (textBox1.Text != "")
            {
                command = new SqlCommand("Select * From Setup INNER JOIN Product ON Product.product_id = Setup.setup_product INNER JOIN Customer ON Customer.customer_id = Setup.setup_costumer where customer_name like '%" + textBox1.Text.ToString() + "%'", connection);
            }
            else if (textBox5.Text != "")
            {
                command = new SqlCommand("Select * From Setup INNER JOIN Product ON Product.product_id = Setup.setup_product INNER JOIN Customer ON Customer.customer_id = Setup.setup_costumer where customer_surname like '%" + textBox5.Text.ToString() + "%'", connection);
            }
            else if (textBox5.Text != "" && textBox1.Text != "")
            {
                command = new SqlCommand("Select * From Setup INNER JOIN Product ON Product.product_id = Setup.setup_product INNER JOIN Customer ON Customer.customer_id = Setup.setup_costumer where customer_name like '%" + textBox1.Text.ToString() + "%' customer_surname like '%" + textBox5.Text.ToString() + "%'", connection);
            }
            else if (textBox4.Text != "")
            {
                command = new SqlCommand("Select * From Setup INNER JOIN Product ON Product.product_id = Setup.setup_product INNER JOIN Customer ON Customer.customer_id = Setup.setup_costumer where customer_id like '%" + textBox4.Text.ToString() + "%'", connection);
            }

            else if (dateTimePicker2.Text != "")
            {
                DateTime myDateTime = Convert.ToDateTime(dateTimePicker2.Value);
                command = new SqlCommand("Select * From Setup INNER JOIN Product ON Product.product_id = Setup.setup_product INNER JOIN Customer ON Customer.customer_id = Setup.setup_costumer where setup_date > '" + myDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "'", connection);
            }
            else if ((int)comboBox2.SelectedValue != 0)
            {
                command = new SqlCommand("Select * From Setup INNER JOIN Product ON Product.product_id = Setup.setup_product INNER JOIN Customer ON Customer.customer_id = Setup.setup_costumer where setup_product like '%" + comboBox2.SelectedValue + "%'", connection);
            }
            else if (dateTimePicker2.Text != "" && (int)comboBox2.SelectedValue != 0)
            {
                DateTime myDateTime = Convert.ToDateTime(dateTimePicker2.Value);
                command = new SqlCommand("Select * From Setup INNER JOIN Product ON Product.product_id = Setup.setup_product INNER JOIN Customer ON Customer.customer_id = Setup.setup_costumer where setup_product like '%" + comboBox2.SelectedValue + "%' setup_date > '" + myDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "'", connection);
            }
            else
            {
                command = new SqlCommand("Select * From Setup INNER JOIN Product ON Product.product_id = Setup.setup_product INNER JOIN Customer ON Customer.customer_id = Setup.setup_costumer ", connection);
            }
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem items = new ListViewItem();
                items.Text = reader["setup_id"].ToString();
                items.SubItems.Add(((DateTime)reader["setup_date"]).ToString("yyyy-MM-dd HH:mm:ss"));
                items.SubItems.Add(reader["product_name"].ToString());
                items.SubItems.Add(reader["customer_id"].ToString());
                items.SubItems.Add(reader["customer_name"].ToString());
                items.SubItems.Add(reader["customer_surname"].ToString());
                listView1.Items.Add(items);
            }
            connection.Close();
        }

        private void showAllRecords(object sender, EventArgs e)
        {
            showDatas();
        }
        private void showDatas()
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox4.Text = "";
            textBox3.Text = "";
            textBox2.Text = "";
            textBox1.Text = "";
            textBox5.Text = "";
            listView1.Items.Clear();
            connection.Open();
            SqlCommand command = new SqlCommand("Select * From Setup INNER JOIN Product ON Product.product_id = Setup.setup_product INNER JOIN Customer ON Customer.customer_id = Setup.setup_costumer ", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem items = new ListViewItem();
                items.Text = reader["setup_id"].ToString();
                items.SubItems.Add(((DateTime)reader["setup_date"]).ToString("yyyy-MM-dd HH:mm:ss"));
                items.SubItems.Add(reader["product_name"].ToString());
                items.SubItems.Add(reader["customer_id"].ToString());
                items.SubItems.Add(reader["customer_name"].ToString());
                items.SubItems.Add(reader["customer_surname"].ToString());
                listView1.Items.Add(items);
            }
            connection.Close();
        }

        private void listView1_MouseDoubleClick(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
                dateTimePicker1.Text = listView1.SelectedItems[0].SubItems[1].Text;
                comboBox1.Text = listView1.SelectedItems[0].SubItems[2].Text;
                textBox2.Text = listView1.SelectedItems[0].SubItems[3].Text;
            }
            catch
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}