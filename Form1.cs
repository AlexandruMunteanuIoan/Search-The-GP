using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace Search_The_GP
{
    public partial class Form1 : Form
    {
 
        public Form1()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void username_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(username.Text == "")
                {
                    username.Text = "Username";
                    username.ForeColor = Color.Gray;
                    return;
                }
                username.ForeColor = Color.White;
                usernameEror.Visible = false;
            }
            catch
            {

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (password.Text == "")
                {
                    password.Text = "Password";
                    password.ForeColor = Color.Gray;
                    return;
                }
                password.ForeColor = Color.White;
                password.PasswordChar = '*';
                passwordEror.Visible = false;
            }
            catch
            {

            }
        }

        private void username_Click(object sender, EventArgs e)
        {
            username.SelectAll();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            password.SelectAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (username.Text =="Username")
            {
                usernameEror.Visible = true;
                username.Focus();
                return;
            }

            if (password.Text == "Password")
            {
                passwordEror.Visible = true;
                password.Focus();
                return;
            }

            string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=LOCALHOST)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=MORCL)));User Id=SYSTEM;Password=Morbius#070802;";

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Connection opened successfully.");

                    string query = $"SELECT * FROM users WHERE username='{username.Text}' AND password='{password.Text}'";

                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string userType = reader["TIP_UTILIZATOR"].ToString();
                                int userId = Convert.ToInt32(reader["id_user"]);

                                if (userType == "admin")
                                {
                                    this.Hide();
                                    Form3 form3 = new Form3(userId);
                                    form3.Show();

                                }
                                else if (userType == "pacient")
                                {
                                    this.Hide();
                                    Form4 form4 = new Form4(userId);
                                    form4.Show();
                                }
                                else if (userType == "medic")
                                {
                                    this.Hide();
                                    Form5 form5 = new Form5(userId);
                                    form5.Show();
                                }

                                
                            }
                            else
                            {
                                passwordEror.Visible = true;
                                usernameEror.Visible = true;
                            }
                        }
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show("An error occurred: " + exp.Message);
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide(); 
        }

    }
}
