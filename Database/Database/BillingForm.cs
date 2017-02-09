using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Database
{
    public partial class BillingForm : Form
    {
        public BillingForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Billing_FormClosing);
            var cmbItems = new List<KeyValuePair<int, string>>();
            cmbItems.Add(new KeyValuePair<int, string>(1, "Cash"));
            cmbItems.Add(new KeyValuePair<int, string>(2, "Credit-card"));
            cmbItems.Add(new KeyValuePair<int, string>(3, "Pay-pal"));
            cmbItems.Add(new KeyValuePair<int, string>(4, "Check"));
            cmbItems.Add(new KeyValuePair<int, string>(5, "Installments"));
            comboBox1.DataSource = cmbItems;

            var cmbItems2 = new List<KeyValuePair<int, string>>();
            cmbItems2.Add(new KeyValuePair<int, string>(1, "Cash"));
            cmbItems2.Add(new KeyValuePair<int, string>(2, "Credit-card"));
            cmbItems2.Add(new KeyValuePair<int, string>(3, "Pay-pal"));
            cmbItems2.Add(new KeyValuePair<int, string>(4, "Check"));
            cmbItems2.Add(new KeyValuePair<int, string>(5, "Installments"));
            comboBox2.DataSource = cmbItems2;


            connection.Open();
            SqlCommand sc = new SqlCommand("select product_id,product_name from product", connection);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("product_id", typeof(string));
            dt.Columns.Add("product_name", typeof(string));
            dt.Load(reader);

            comboBox3.ValueMember = "product_id";
            comboBox3.DisplayMember = "product_name";
            comboBox3.DataSource = dt;

            connection.Close();
        }
        private void Billing_Load_1(object sender, EventArgs e)
        {
            showDatas();
        }

        private void Billing_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            EnterPageForm form = new EnterPageForm();
            form.Show();
        }
        SqlConnection connection = new SqlConnection(@"server = .\SQL2014 ; database = Software ; Trusted_Connection = yes;");
        private void insertButton(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Please fill all fields!!", "Alert", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            else if (comboBox3.Text == "")
            {
                MessageBox.Show("Please fill all fields!!", "Alert", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("Please fill all fields!!", "Alert", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            else if (textBox4.Text == "")
            {
                MessageBox.Show("Please fill all fields!!", "Alert", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            else
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Insert into Billing(billing_type,billing_cost,billing_customer,billing_product) values('" + comboBox1.SelectedValue + "','" + textBox4.Text.ToString() + "','" + textBox3.Text.ToString() + "','" + comboBox3.SelectedValue + "') select Billing.billing_type from Billing inner join Payment ON Billing.billing_type = Payment.payment_id INNER JOIN Product On Product.product_id=Billing.billing_product", connection);
                command.ExecuteNonQuery();
                connection.Close();
                showDatas();
            }
        }

        private void deleteButton(object sender, EventArgs e)
        {
            connection.Open();
            int id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            SqlCommand command = new SqlCommand("Delete from Billing where billing_id=(" + id + ")", connection);
            command.ExecuteNonQuery();
            connection.Close();
            showDatas();
        }

        private void updateButton(object sender, EventArgs e)
        {
            connection.Open();
            int id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            SqlCommand command = new SqlCommand("Update Billing set billing_type='" + comboBox1.SelectedValue + "',billing_cost='" + textBox4.Text.ToString() + "',billing_customer='" + textBox3.Text.ToString() + "',billing_product='" + comboBox3.SelectedValue + "' where billing_id=" + id + "", connection);
            command.ExecuteNonQuery();
            connection.Close();
            showDatas();
        }

        private void searchButton(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            connection.Open();
            SqlCommand command;

            if (textBox5.Text != "")
            {
                command = new SqlCommand("Select * From Billing INNER JOIN Payment ON Payment.payment_id = Billing.billing_type INNER JOIN Customer ON Customer.customer_id = Billing.billing_customer INNER JOIN Product On Product.product_id=Billing.billing_product where billing_id like '%" + textBox5.Text + "%'", connection);

            }
            else if (textBox2.Text != "")
            {
                command = new SqlCommand("Select * From Billing INNER JOIN Payment ON Payment.payment_id = Billing.billing_type INNER JOIN Customer ON Customer.customer_id = Billing.billing_customer INNER JOIN Product On Product.product_id=Billing.billing_product where billing_cost> '" + textBox2.Text.ToString() + "'", connection);
            }
            else if (textBox6.Text != "")
            {
                command = new SqlCommand("Select * From Billing INNER JOIN Payment ON Payment.payment_id = Billing.billing_type INNER JOIN Customer ON Customer.customer_id = Billing.billing_customer INNER JOIN Product On Product.product_id=Billing.billing_product where billing_cost< '" + textBox6.Text.ToString() + "'", connection);
            }
            else if (textBox2.Text != "" && textBox6.Text != "")
            {
                command = new SqlCommand("Select * From Billing INNER JOIN Payment ON Payment.payment_id = Billing.billing_type INNER JOIN Customer ON Customer.customer_id = Billing.billing_customer INNER JOIN Product On Product.product_id=Billing.billing_product where billing_cost between '" + textBox2.Text.ToString() + "' and '" + textBox6.Text.ToString() + "'", connection);
            }
            else
            {
                command = new SqlCommand("Select * From Billing INNER JOIN Payment ON Payment.payment_id = Billing.billing_type INNER JOIN Customer ON Customer.customer_id = Billing.billing_customer INNER JOIN Product On Product.product_id=Billing.billing_product where payment_id like '%" + comboBox2.SelectedValue.ToString() + "%'", connection);

            }
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem items = new ListViewItem();
                items.Text = reader["billing_id"].ToString();
                items.SubItems.Add(reader["payment_type"].ToString());
                items.SubItems.Add(reader["billing_cost"].ToString());
                items.SubItems.Add(reader["customer_id"].ToString());
                items.SubItems.Add(reader["customer_name"].ToString());
                items.SubItems.Add(reader["product_name"].ToString());
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
            comboBox3.Text = "";
            textBox5.Text = "";
            listView1.Items.Clear();
            connection.Open();
            SqlCommand command = new SqlCommand("Select * From Billing INNER JOIN Payment ON Payment.payment_id = Billing.billing_type INNER JOIN Customer ON Customer.customer_id = Billing.billing_customer INNER JOIN Product On Product.product_id=Billing.billing_product", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem items = new ListViewItem();
                items.Text = reader["billing_id"].ToString();
                items.SubItems.Add(reader["payment_type"].ToString());
                items.SubItems.Add(reader["billing_cost"].ToString());
                items.SubItems.Add(reader["customer_id"].ToString());
                items.SubItems.Add(reader["customer_name"].ToString());
                items.SubItems.Add(reader["customer_surname"].ToString());
                items.SubItems.Add(reader["product_name"].ToString());
                listView1.Items.Add(items);
            }
            connection.Close();
        }

        private void listView1_MouseDoubleClick(object sender, EventArgs e)
        {
            try
            {

                SqlCommand command = new SqlCommand("Select * From Billing INNER JOIN Payment ON Payment.payment_id = Billing.billing_type INNER JOIN Customer ON Customer.customer_id = Billing.billing_customer INNER JOIN Product On Product.product_id=Billing.billing_product", connection);

                int id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
                comboBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;
                textBox4.Text = listView1.SelectedItems[0].SubItems[2].Text;
                textBox3.Text = listView1.SelectedItems[0].SubItems[3].Text;
                comboBox3.Text = listView1.SelectedItems[0].SubItems[6].Text;
            }
            catch
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
