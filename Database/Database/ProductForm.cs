using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Database
{
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Product_FormClosing);
        }
        private void Product_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            EnterPageForm form = new EnterPageForm();
            form.Show();
        }
        private void Product_Load_1(object sender, EventArgs e)
        {
            showDatas();
        }

        SqlConnection connection = new SqlConnection(@"server = .\SQL2014 ; database = Software ; Trusted_Connection = yes;");
        private void insertButton(object sender, EventArgs e)
        {





            using (var db = new SoftwareEntities())
            {
                db.Product.Add(new Product { product_name = textBox2.Text, product_cost = Convert.ToInt32(textBox1.Text)});
                db.SaveChanges();
            }







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
            else
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Insert into Softwares(software_name,employee_id) values('" + textBox2.Text.ToString() + "','" + textBox3.Text.ToString() + "') ", connection);
                SqlCommand command2 = new SqlCommand("Insert into Product(product_name,product_cost) values('" + textBox2.Text.ToString() + "','" + textBox1.Text.ToString() + "') ", connection);
                command.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                connection.Close();
                showDatas();
            }
        }

        private void deleteButton(object sender, EventArgs e)
        {
            connection.Open();
            int id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            SqlCommand command = new SqlCommand("Delete from Softwares where software_id = (" + id + ")", connection);
            command.ExecuteNonQuery();
            SqlCommand command2 = new SqlCommand("Delete from Product where product_id = (" + id + ")", connection);
            command2.ExecuteNonQuery();
            connection.Close();
            showDatas();
        }

        private void updateButton(object sender, EventArgs e)
        {
            connection.Open();
            int id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            SqlCommand command = new SqlCommand("Update Softwares set software_name='" + textBox2.Text.ToString() + "',employee_id='" + textBox3.Text.ToString() + "' where software_id =" + id + "", connection);
            command.ExecuteNonQuery();
            SqlCommand command2 = new SqlCommand("Update Product set product_name='" + textBox2.Text.ToString() + "',product_cost='" + textBox1.Text.ToString() + "' where product_id =" + id + "", connection);
            command2.ExecuteNonQuery();

            connection.Close();
            showDatas();
        }

        private void searchButton(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            connection.Open();
            SqlCommand command = new SqlCommand("Select * From Softwares INNER JOIN Product ON Product.product_id = Softwares.software_id AND Product.product_name = Softwares.software_name  INNER JOIN Employee ON Employee.employee_id = Softwares.employee_id where software_id like '%" + textBox6.Text + "%' and software_name like '%" + textBox5.Text.ToString() + "%' and Softwares.employee_id like '%" + textBox4.Text + "%'", connection);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem items = new ListViewItem();
                items.Text = reader["software_id"].ToString();
                items.SubItems.Add(reader["software_name"].ToString());
                items.SubItems.Add(reader["employee_id"].ToString());
                items.SubItems.Add(reader["employee_name"].ToString());
                items.SubItems.Add(reader["product_cost"].ToString());
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
            textBox4.Text = "";
            textBox3.Text = "";
            textBox2.Text = "";
            textBox1.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            listView1.Items.Clear();
            connection.Open();
            SqlCommand command = new SqlCommand("Select * From Softwares INNER JOIN Product ON Product.product_id = Softwares.software_id AND Product.product_name = Softwares.software_name  INNER JOIN Employee ON Employee.employee_id = Softwares.employee_id", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem items = new ListViewItem();
                items.Text = reader["software_id"].ToString();
                items.SubItems.Add(reader["software_name"].ToString());
                items.SubItems.Add(reader["employee_id"].ToString());
                items.SubItems.Add(reader["employee_name"].ToString());
                items.SubItems.Add(reader["product_cost"].ToString());
                listView1.Items.Add(items);
            }
            connection.Close();
        }

        private void listView1_MouseDoubleClick(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
                textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
                textBox3.Text = listView1.SelectedItems[0].SubItems[2].Text;
                textBox1.Text = listView1.SelectedItems[0].SubItems[4].Text;
            }
            catch
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
