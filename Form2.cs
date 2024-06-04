using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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
            string pattern = @"^\d{10}$";
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
            string pattern = @"^[A-Za-z0-9]{8,20}$";
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

        private void button2_Click(object sender, EventArgs e)
        {
            // Validarea datelor
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
            if (string.IsNullOrEmpty(email.Text) || !IsValidEmail(email.Text) || email.Text == "example@gmail.com")
            {
                emailError.Visible = true;
                email.Focus();
                return;
            }
            if (phone.Text == "0712345678" || !IsValidPhoneNumber(phone.Text))
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
            if (typeSelect.SelectedItem == null || typeSelect.SelectedItem.ToString() == "Select one")
            {
                typeError.Visible = true;
                typeSelect.Focus();
                return;
            }

            DateTime dob = dateOfBirth.Value.Date;
            DateTime currentDate = DateTime.Now;
            DateTime maxDateOfBirth = currentDate.AddYears(-110);

            if (dob > currentDate || dob < maxDateOfBirth)
            {
                dobError.Visible = true;
                dateOfBirth.Focus();
                return;
            }

            string userType = typeSelect.SelectedItem.ToString();
            string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=LOCALHOST)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=MORCL)));User Id=SYSTEM;Password=Morbius#070802;";

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Connection opened successfully.");

                    string insertUserQuery = "INSERT INTO Users (id_user, username, email, password, tip_utilizator, nume, prenume, numar_telefon, data_nasterii) " +
                                             "VALUES (seq_users.NEXTVAL, :username, :email, :password, :typeSelect, :lname, :fname, :phone, TO_DATE(:dateOfBirth, 'MM/DD/YYYY')) " +
                                             "RETURNING id_user INTO :id_user";

                    using (OracleCommand cmd = new OracleCommand(insertUserQuery, con))
                    {
                        cmd.Parameters.Add(new OracleParameter("username", username.Text));
                        cmd.Parameters.Add(new OracleParameter("password", password.Text));
                        cmd.Parameters.Add(new OracleParameter("email", email.Text));
                        cmd.Parameters.Add(new OracleParameter("phone", phone.Text));
                        cmd.Parameters.Add(new OracleParameter("fname", fname.Text));
                        cmd.Parameters.Add(new OracleParameter("lname", lname.Text));
                        cmd.Parameters.Add(new OracleParameter("typeSelect", userType));
                        cmd.Parameters.Add(new OracleParameter("dateOfBirth", dob.ToString("MM/dd/yyyy")));
                        OracleParameter idUserParam = new OracleParameter("id_user", OracleDbType.Int32);
                        idUserParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(idUserParam);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            int userId = int.Parse(idUserParam.Value.ToString());
                            MessageBox.Show("User registered successfully.");

                            string secondaryInsertQuery = "";

                            if (userType == "pacient")
                            {
                                secondaryInsertQuery = "INSERT INTO Pacienti (id_pacient, id_user, descriere) VALUES (seq_pacienti.NEXTVAL, :id_user, :descriere)";
                            }
                            else if (userType == "medic")
                            {
                                secondaryInsertQuery = "INSERT INTO Medici (id_medic, id_user, descriere, adresa_cabinet, nr_Locuri_Disponibile) " +
                                                       "VALUES (seq_medici.NEXTVAL, :id_user, :descriere, :adresa_cabinet, :nr_Locuri_Disponibile)";
                            }

                            if (!string.IsNullOrEmpty(secondaryInsertQuery))
                            {
                                using (OracleCommand secondaryCmd = new OracleCommand(secondaryInsertQuery, con))
                                {
                                    secondaryCmd.Parameters.Add(new OracleParameter("id_user", userId));
                                    secondaryCmd.Parameters.Add(new OracleParameter("descriere", ""));

                                    if (userType == "medic")
                                    {
                                        secondaryCmd.Parameters.Add(new OracleParameter("adresa_cabinet", ""));
                                        secondaryCmd.Parameters.Add(new OracleParameter("nr_Locuri_Disponibile", 0));
                                    }

                                    int secondaryRowsAffected = secondaryCmd.ExecuteNonQuery();
                                    if (secondaryRowsAffected > 0)
                                    {
                                        MessageBox.Show($"{userType} registered successfully.");
                                        if (userType == "pacient")
                                        {
                                            Form4 form4 = new Form4(userId);
                                            form4.Show();
                                        }
                                        else if (userType == "medic")
                                        {
                                            Form5 form5 = new Form5(userId);
                                            form5.Show();
                                        }
                                        this.Hide();
                                    }
                                    else
                                    {
                                        MessageBox.Show($"Failed to register {userType}.");
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Failed to register user.");
                        }
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show("An error occurred: " + exp.Message);
                }
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
                    phone.Text = "0712345678";
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

        private void typeSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox comboBox = (ComboBox)sender;

                if (comboBox.SelectedItem == null || comboBox.SelectedItem.ToString() == "Select one")
                {
                    comboBox.Text = "Select one";
                    comboBox.ForeColor = Color.Gray;
                    return;
                }

                comboBox.ForeColor = Color.White;
                typeError.Visible = false;
            }
            catch
            {
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
