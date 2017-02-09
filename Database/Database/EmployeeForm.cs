using System.Windows.Forms;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Database
{
    public partial class EmployeeForm : Form
    {
        public EmployeeForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Employee_FormClosing);

            var cmbItems = new List<KeyValuePair<int, string>>();
            cmbItems.Add(new KeyValuePair<int, string>(1, "Female"));
            cmbItems.Add(new KeyValuePair<int, string>(2, "Male"));
            comboBox1.DataSource = cmbItems;

            var cmbItems3 = new List<KeyValuePair<int, string>>();
            cmbItems3.Add(new KeyValuePair<int, string>(1, "Ankara"));
            cmbItems3.Add(new KeyValuePair<int, string>(2, "Istanbul"));
            cmbItems3.Add(new KeyValuePair<int, string>(3, "Eskişehir"));
            cmbItems3.Add(new KeyValuePair<int, string>(4, "Samsun"));
            cmbItems3.Add(new KeyValuePair<int, string>(5, "Izmir"));
            comboBox3.DataSource = cmbItems3;

   
            var cmbItems2 = new List<KeyValuePair<int, string>>();
            cmbItems2.Add(new KeyValuePair<int, string>(0, "--Select--"));
            cmbItems2.Add(new KeyValuePair<int, string>(1, "Management"));
            cmbItems2.Add(new KeyValuePair<int, string>(2, "Software"));
            cmbItems2.Add(new KeyValuePair<int, string>(3, "Accounting"));
            cmbItems2.Add(new KeyValuePair<int, string>(4, "Customer Service"));
            cmbItems2.Add(new KeyValuePair<int, string>(5, "Human Resources"));
            
            comboBox2.DataSource = cmbItems2;
            comboBox2.SelectedValue = 0;

            var cmbItems4 = new List<KeyValuePair<int, string>>();
            cmbItems4.Add(new KeyValuePair<int, string>(0, "--Select--"));
            cmbItems4.Add(new KeyValuePair<int, string>(1, "Ankara"));
            cmbItems4.Add(new KeyValuePair<int, string>(2, "Istanbul"));
            cmbItems4.Add(new KeyValuePair<int, string>(3, "Eskişehir"));
            cmbItems4.Add(new KeyValuePair<int, string>(4, "Samsun"));
            cmbItems4.Add(new KeyValuePair<int, string>(5, "Izmir"));
            comboBox4.DataSource = cmbItems4;
            comboBox4.SelectedValue = 0;

            var cmbItems5 = new List<KeyValuePair<int, string>>();
            cmbItems5.Add(new KeyValuePair<int, string>(0, "--Select--"));
            cmbItems5.Add(new KeyValuePair<int, string>(1, "Management"));
            cmbItems5.Add(new KeyValuePair<int, string>(2, "Software"));
            cmbItems5.Add(new KeyValuePair<int, string>(3, "Accounting"));
            cmbItems5.Add(new KeyValuePair<int, string>(4, "Customer Service"));
            cmbItems5.Add(new KeyValuePair<int, string>(5, "Human Resources"));
            comboBox5.DataSource = cmbItems5;
            comboBox5.SelectedValue = 0;
        }

        SqlConnection connection = new SqlConnection(@"server = .\SQL2014 ; database = Software ; Trusted_Connection = yes;");

        private void Employee_Load_1(object sender, EventArgs e)
        {
            showDatas();
        }

        private void insertButton(object sender, System.EventArgs e)
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
            if (comboBox3.Text == "")
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
            else if (dateTimePicker1.Text == "")
            {
                MessageBox.Show("Please fill all fields!!", "Alert", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            else
            {

                connection.Open();
                DateTime myDateTime = Convert.ToDateTime(dateTimePicker1.Value);
                SqlCommand command = new SqlCommand("Insert into Employee(employee_name,employee_surname,employee_gender,employee_salary,employee_department,employee_start_date,employee_coid) values('" + textBox2.Text.ToString() + "','" + textBox1.Text.ToString() + "','" + comboBox1.SelectedValue + "','" + textBox3.Text.ToString() + "','" + comboBox2.SelectedValue + "','" + myDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "','" + comboBox3.SelectedValue + "') select Employee.employee_gender from Employee inner join Gender ON Employee.employee_gender = Gender.gender_id", connection);
                command.ExecuteNonQuery();
                connection.Close();
                showDatas();
            }
        }
        private void deleteButton(object sender, System.EventArgs e)
        {
            connection.Open();
            int id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            SqlCommand command = new SqlCommand("Delete from Employee where employee_id=(" + id + ")", connection);
            command.ExecuteNonQuery();
            connection.Close();
            showDatas();
        }

        private void updateButton(object sender, System.EventArgs e)
        {
            connection.Open();
            int id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            DateTime myDateTime = Convert.ToDateTime(dateTimePicker1.Value);
            SqlCommand command = new SqlCommand("Update Employee set employee_name='" + textBox2.Text.ToString() + "', employee_surname = '" + textBox1.Text.ToString() + "',employee_gender='" + comboBox1.SelectedValue + "',employee_salary='" + textBox3.Text.ToString() + "',employee_department='" + comboBox2.SelectedValue + "',employee_start_date='" + myDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "',employee_coid='" + comboBox3.SelectedValue + "' where employee_id=" + id + "", connection);
            command.ExecuteNonQuery();
            connection.Close();
            showDatas();
        }
        private void searchButton(object sender, System.EventArgs e)
        {
            listView1.Items.Clear();
            connection.Open();
            SqlCommand command;
            if (textBox5.Text != "" && textBox6.Text != "")
            {
                command = new SqlCommand("Select * From Employee INNER JOIN Gender ON Gender.gender_id = Employee.employee_gender INNER JOIN Company ON Company.company_id = Employee.employee_coid INNER JOIN Department On Department.department_id = Employee.employee_department where employee_id like '%" + textBox5.Text.ToString() + "%' and employee_name like '%" + textBox6.Text.ToString() + "%'", connection);
            }
            else if (textBox6.Text != "")
            {
                command = new SqlCommand("Select * From Employee INNER JOIN Gender ON Gender.gender_id = Employee.employee_gender INNER JOIN Company ON Company.company_id = Employee.employee_coid INNER JOIN Department On Department.department_id = Employee.employee_department where employee_name like '%" + textBox6.Text.ToString() + "%'", connection);
            }
            else if (textBox5.Text != "")
            {
                command = new SqlCommand("Select * From Employee INNER JOIN Gender ON Gender.gender_id = Employee.employee_gender INNER JOIN Company ON Company.company_id = Employee.employee_coid INNER JOIN Department On Department.department_id = Employee.employee_department where employee_id like '%" + textBox5.Text.ToString() + "%'", connection);
            }
            else if (textBox7.Text != "")
            {
                command = new SqlCommand("Select * From Employee INNER JOIN Gender ON Gender.gender_id = Employee.employee_gender INNER JOIN Company ON Company.company_id = Employee.employee_coid INNER JOIN Department On Department.department_id = Employee.employee_department where employee_salary> '" + textBox7.Text.ToString() + "'", connection);
            }
            else if (textBox4.Text != "")
            {
                command = new SqlCommand("Select * From Employee INNER JOIN Gender ON Gender.gender_id = Employee.employee_gender INNER JOIN Company ON Company.company_id = Employee.employee_coid INNER JOIN Department On Department.department_id = Employee.employee_department where employee_salary< '" + textBox4.Text.ToString() + "'", connection);
            }
            else if (textBox7.Text != "" && textBox4.Text != "")
            {
                command = new SqlCommand("Select * From Employee INNER JOIN Gender ON Gender.gender_id = Employee.employee_gender INNER JOIN Company ON Company.company_id = Employee.employee_coid INNER JOIN Department On Department.department_id = Employee.employee_department where employee_salary between '" + textBox7.Text.ToString() + "' and '" + textBox4.Text.ToString() + "'", connection);
            }
            else if ((int)comboBox4.SelectedValue != 0 && (int)comboBox5.SelectedValue == 0)
            {
                command = new SqlCommand("Select * From Employee INNER JOIN Gender ON Gender.gender_id = Employee.employee_gender INNER JOIN Company ON Company.company_id = Employee.employee_coid INNER JOIN Department On Department.department_id = Employee.employee_department where employee_coid like '%" + comboBox4.SelectedValue + "%'", connection);
            }
            else if ((int)comboBox5.SelectedValue != 0 && (int)comboBox4.SelectedValue == 0)
            {
                command = new SqlCommand("Select * From Employee INNER JOIN Gender ON Gender.gender_id = Employee.employee_gender INNER JOIN Company ON Company.company_id = Employee.employee_coid INNER JOIN Department On Department.department_id = Employee.employee_department where employee_department ='" + comboBox5.SelectedValue + "'", connection);
            }
            else if ((int)comboBox4.SelectedValue != 0 && (int)comboBox5.SelectedValue != 0)
            {
                command = new SqlCommand("Select * From Employee INNER JOIN Gender ON Gender.gender_id = Employee.employee_gender INNER JOIN Company ON Company.company_id = Employee.employee_coid INNER JOIN Department On Department.department_id = Employee.employee_department where employee_coid like '%" + comboBox4.SelectedValue + "%' and employee_department = '" + comboBox5.SelectedValue + "'", connection);
            }
            else if (dateTimePicker2.Text != "")
            {
                DateTime myDateTime = Convert.ToDateTime(dateTimePicker2.Value);
                command = new SqlCommand("Select * From Employee INNER JOIN Gender ON Gender.gender_id = Employee.employee_gender INNER JOIN Company ON Company.company_id = Employee.employee_coid INNER JOIN Department On Department.department_id = Employee.employee_department where employee_start_date > '" + myDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "'", connection);
            }
            else
            {
                command = new SqlCommand("Select * From Employee INNER JOIN Gender ON Gender.gender_id = Employee.employee_gender INNER JOIN Company ON Company.company_id = Employee.employee_coid INNER JOIN Department On Department.department_id = Employee.employee_department ", connection);
            }
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem items = new ListViewItem();
                items.Text = reader["employee_id"].ToString();
                items.SubItems.Add(reader["employee_name"].ToString());
                items.SubItems.Add(reader["employee_surname"].ToString());
                items.SubItems.Add(reader["gender_name"].ToString());
                items.SubItems.Add(reader["employee_salary"].ToString());
                items.SubItems.Add(reader["department_name"].ToString());
                items.SubItems.Add(((DateTime)reader["employee_start_date"]).ToString("yyyy-MM-dd HH:mm:ss"));
                items.SubItems.Add(reader["company_name"].ToString());
                DateTime now = DateTime.Now;
                DateTime last = (DateTime)reader["employee_start_date"];
                var x = GetWorkingDays(last, now);
                items.SubItems.Add(x.ToString());
                listView1.Items.Add(items);
            }
            connection.Close();
        }

        private void showAllRecords(object sender, System.EventArgs e)
        {
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
            comboBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";
            dateTimePicker1.Text = "";
            dateTimePicker2.Text = "";
            listView1.Items.Clear();
            connection.Open();
            SqlCommand command = new SqlCommand("Select * From Employee INNER JOIN Gender ON Gender.gender_id = Employee.employee_gender INNER JOIN Company ON Company.company_id = Employee.employee_coid INNER JOIN Department On Department.department_id = Employee.employee_department ", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem items = new ListViewItem();
                items.Text = reader["employee_id"].ToString();
                items.SubItems.Add(reader["employee_name"].ToString());
                items.SubItems.Add(reader["employee_surname"].ToString());
                items.SubItems.Add(reader["gender_name"].ToString());
                items.SubItems.Add(reader["employee_salary"].ToString());
                items.SubItems.Add(reader["department_name"].ToString());
                items.SubItems.Add(((DateTime)reader["employee_start_date"]).ToString("yyyy-MM-dd HH:mm:ss"));
                items.SubItems.Add(reader["company_name"].ToString());
                DateTime now = DateTime.Now;
                DateTime last = (DateTime)reader["employee_start_date"];
                var x = GetWorkingDays(last, now);
                items.SubItems.Add(x.ToString());
                listView1.Items.Add(items);
            }
            connection.Close();
        }

        private void Employee_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            EnterPageForm form = new EnterPageForm();
            form.Show();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                int id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
                textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
                textBox1.Text = listView1.SelectedItems[0].SubItems[2].Text;
                comboBox1.Text = listView1.SelectedItems[0].SubItems[3].Text;
                textBox3.Text = listView1.SelectedItems[0].SubItems[4].Text;
                comboBox2.Text = listView1.SelectedItems[0].SubItems[5].Text;
                dateTimePicker1.Text = listView1.SelectedItems[0].SubItems[6].Text;
                comboBox3.Text = listView1.SelectedItems[0].SubItems[7].Text;

            }
            catch
            {
                MessageBox.Show(e.ToString());
            }
        }
        public int GetWorkingDays(DateTime from, DateTime to)
        {
            var totalDays = 0;
            for (var date = from; date < to; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday
                    && date.DayOfWeek != DayOfWeek.Sunday)
                    totalDays++;
            }
            return totalDays;
        }

    }
}
