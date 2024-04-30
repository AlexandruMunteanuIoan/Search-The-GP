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

namespace Search_The_GP
{
    public partial class Form1 : Form
    {
 
        public Form1()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void LoginPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoginForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

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
        }

        private void usernameEror_Click(object sender, EventArgs e)
        {

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
