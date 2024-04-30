using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Search_The_GP
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        public bool IsValidEmail(string email)
        {
            // Expresia regulată pentru validarea emailurilor
            string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\+40 \d{9}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(phoneNumber);
        }

        private bool IsValidName(string name)
        {
            // Expresia regulată pentru a valida numele (nu trebuie să conțină cifre sau caractere speciale în afara de "-")
            string pattern = @"^[A-Za-z\-]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(name);
        }

        private bool IsValidPassword(string password)
        {
            // Expresia regulată pentru a valida parola (exact 8 caractere/cifre)
            string pattern = @"^[A-Za-z0-9]{8}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(password);
        }

        private bool IsValidUsername(string username)
        {
            return username.Length >= 4;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();

            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (username.Text == "Ex: IonPopescu" || !IsValidUsername(username.Text))
            {
                usernameEror.Visible = true;
                username.Focus();
                return;
            }
            if (password.Text == "Password" || !IsValidPassword(password.Text))
            {
                passwordEror.Visible = true;
                password.Focus();
                return;
            }
            if (string.IsNullOrEmpty(email.Text) || !IsValidEmail(email.Text) || email.Text =="example@gmail.com")
            {
                emailError.Visible = true;
                email.Focus();
                return;
            }
            if (phone.Text == "+40 712345678" || !IsValidPhoneNumber(phone.Text))
            {
                phoneError.Visible = true;
                phone.Focus();
                return;
            }
            if (fname.Text == "Ex: Ion" || !IsValidName(fname.Text))
            {
                fnameError.Visible = true;
                fname.Focus();
                return;
            }
            if (lname.Text == "Ex: Popescu" || !IsValidName(lname.Text))
            {
                lnameError.Visible = true;
                lname.Focus();
                return;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void username_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (username.Text == "")
                {
                    username.Text = "Ex: IonPopescu";
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

        private void username_Click(object sender, EventArgs e)
        {
            username.SelectAll();
        }

        private void password_Click(object sender, EventArgs e)
        {
            password.SelectAll();
        }

        private void email_Click(object sender, EventArgs e)
        {
            email.SelectAll();
        }

        private void phone_Click(object sender, EventArgs e)
        {
            phone.SelectAll();
        }

        private void fname_Click(object sender, EventArgs e)
        {
            fname.SelectAll();
        }

        private void lname_Click(object sender, EventArgs e)
        {
            lname.SelectAll();
        }

        private void password_TextChanged(object sender, EventArgs e)
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

        private void email_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (email.Text == "")
                {
                    email.Text = "example@gmail.com";
                    email.ForeColor = Color.Gray;
                    return;
                }
                email.ForeColor = Color.White;
                emailError.Visible = false;
            }
            catch
            {

            }
        }

        private void phone_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (phone.Text == "")
                {
                    phone.Text = "+40 712345678";
                    phone.ForeColor = Color.Gray;
                    return;
                }
                phone.ForeColor = Color.White;
                phoneError.Visible = false;
            }
            catch
            {

            }
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void fname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (fname.Text == "")
                {
                    fname.Text = "ex: Ion";
                    fname.ForeColor = Color.Gray;
                    return;
                }
                fname.ForeColor = Color.White;

                fnameError.Visible = false;
            }
            catch
            {

            }
        }

        private void lname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (lname.Text == "")
                {
                    lname.Text = "ex: Popescu";
                    lname.ForeColor = Color.Gray;
                    return;
                }
                lname.ForeColor = Color.White;
                lnameError.Visible = false;
            }
            catch
            {

            }
        }
    }
}
