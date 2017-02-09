using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Database
{
    public partial class CustomerForm : Form
    {
        SqlConnection connection = new SqlConnection(@"server = .\SQL2014 ; database = Software ; Trusted_Connection = yes;");

        public CustomerForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Customer_FormClosing);
            var cmbItems = new List<KeyValuePair<int, string>>();
            cmbItems.Add(new KeyValuePair<int, string>(1, "Female"));
            cmbItems.Add(new KeyValuePair<int, string>(2, "Male"));
            comboBox1.DataSource = cmbItems;

            connection.Open();
            SqlCommand sc = new SqlCommand("select product_id,product_name from product", connection);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("product_id", typeof(string));
            dt.Columns.Add("product_name", typeof(string));
            dt.Load(reader);

            comboBox2.ValueMember = "product_id";
            comboBox2.DisplayMember = "product_name";
            comboBox2.DataSource = dt;

            connection.Close();


            //connection.Open();
            //string strCmd = "select product_name from Product";
            //SqlCommand cmd = new SqlCommand(strCmd, connection);
            //SqlDataAdapter da = new SqlDataAdapter(strCmd, connection);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //cmd.ExecuteNonQuery();
            //connection.Close();

            //comboBox2.DisplayMember = "product_name";
            //comboBox2.ValueMember = "product_name";
            //comboBox2.DataSource = ds;

            //comboBox2.Enabled = true;
        }

        private void Customer_Load_1(object sender, EventArgs e)
        {
            showDatas();
        }
        private void insertButton(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please fill all fields!!", "Alert", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Please fill all fields!!", "Alert", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("Please fill all fields!!", "Alert", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            if (textBox4.Text == "")
            {
                MessageBox.Show("Please fill all fields!!", "Alert", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            else if (comboBox1.Text == "")
            {
                MessageBox.Show("Please fill all fields!!", "Alert", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            else if (comboBox2.Text == "")
            {
                MessageBox.Show("Please fill all fields!!", "Alert", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            else
            {

                using (var db = new SoftwareEntities())
                {
                    db.Customer.Add(new Customer { customer_name = textBox1.Text, customer_surname = textBox2.Text, customer_gender = (int)Convert.ToInt32(comboBox1.SelectedValue), customer_adress = textBox3.Text, customer_job = textBox4.Text, customer_soft = (int)Convert.ToInt32(comboBox2.SelectedValue) });
                    db.SaveChanges();
                }
                showDatas();

                //alternatif yol entitiy framework kullanma yukarda

                //connection.Open();
                //SqlCommand command = new SqlCommand("Insert into Customer(customer_name,customer_surname,customer_gender,customer_adress,customer_job,customer_soft) values('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + comboBox1.SelectedValue + "','" + textBox3.Text.ToString() + "','" + textBox4.Text.ToString() + "','" + comboBox2.SelectedValue + "') select Customer.customer_gender from Customer inner join Gender ON Customer.customer_gender = Gender.gender_id", connection);
                //command.ExecuteNonQuery();
                //connection.Close();
                //showDatas();
            }
        }

        private void deleteButton(object sender, EventArgs e)
        {

            using (var db = new SoftwareEntities())
            {
                int id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
                Customer customer = new Customer { customer_id = id };
                db.Entry(customer).State = EntityState.Deleted;
                db.SaveChanges();

            }
            showDatas();

            //connection.Open();
            //int id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            //SqlCommand command = new SqlCommand("Delete from Customer where customer_id=(" + id + ")", connection);
            //command.ExecuteNonQuery();
            //connection.Close();

        }
        private void updateButton(object sender, EventArgs e)
        {
            using (var db = new SoftwareEntities())
            {
                int id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
                Customer customer = new Customer { customer_id = id };
                db.Customer.Attach(db.Customer.Single(c => c.customer_id == customer.customer_id));
                db.Customer.ApplyCurrentValues(customer);
                db.Savechanges();
            }


            //connection.Open();
            //int id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            //SqlCommand command = new SqlCommand("Update Customer set customer_name ='" + textBox1.Text.ToString() + "',customer_surname ='" + textBox2.Text.ToString() + "',customer_gender='" + comboBox1.SelectedValue + "',customer_adress='" + textBox3.Text.ToString() + "',customer_job='" + textBox4.Text.ToString() + "',customer_soft='" + comboBox2.SelectedValue + "' where customer_id=" + id + "", connection);
            //command.ExecuteNonQuery();
            //connection.Close();
            showDatas();
        }

        private void searchButton(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            connection.Open();
            SqlCommand command = new SqlCommand("Select * From Customer INNER JOIN Gender ON Gender.gender_id = Customer.customer_gender INNER JOIN softwares on softwares.software_id=customer.customer_soft where customer_id like '%" + textBox5.Text.ToString() + "%' and customer_name like '%" + textBox6.Text.ToString() + "%' and customer_surname like '%" + textBox7.Text.ToString() + "%'", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem items = new ListViewItem();
                items.Text = reader["customer_id"].ToString();
                items.SubItems.Add(reader["customer_name"].ToString());
                items.SubItems.Add(reader["customer_surname"].ToString());
                items.SubItems.Add(reader["gender_name"].ToString());
                items.SubItems.Add(reader["customer_adress"].ToString());
                items.SubItems.Add(reader["customer_job"].ToString());
                items.SubItems.Add(reader["software_name"].ToString());
                listView1.Items.Add(items);
            }
            connection.Close();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                int id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
                textBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;
                textBox2.Text = listView1.SelectedItems[0].SubItems[2].Text;
                comboBox1.Text = listView1.SelectedItems[0].SubItems[3].Text;
                textBox3.Text = listView1.SelectedItems[0].SubItems[4].Text;
                textBox4.Text = listView1.SelectedItems[0].SubItems[5].Text;
                comboBox2.Text = listView1.SelectedItems[0].SubItems[6].Text;
            }
            catch
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void Customer_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            EnterPageForm form = new EnterPageForm();
            form.Show();
        }
        private void showAllRecords(object sender, EventArgs e)
        {
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            showDatas();
        }
        private void showDatas()
        {

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            listView1.Items.Clear();
            connection.Open();
            SqlCommand command = new SqlCommand("Select * From Customer INNER JOIN Gender ON Gender.gender_id = Customer.customer_gender INNER JOIN softwares on softwares.software_id=customer.customer_soft", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem items = new ListViewItem();
                items.Text = reader["customer_id"].ToString();
                items.SubItems.Add(reader["customer_name"].ToString());
                items.SubItems.Add(reader["customer_surname"].ToString());
                items.SubItems.Add(reader["gender_name"].ToString());
                items.SubItems.Add(reader["customer_adress"].ToString());
                items.SubItems.Add(reader["customer_job"].ToString());
                items.SubItems.Add(reader["software_name"].ToString());
                listView1.Items.Add(items);
            }
            connection.Close();
        }

    }

}
