using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Search_The_GP
{
    public partial class InfoDoctor : UserControl
    {
        public InfoDoctor()
        {
            InitializeComponent();

            fullname.ReadOnly = true;
            username.ReadOnly = true;
            email.ReadOnly = true;
            phone.ReadOnly = true;
            password.ReadOnly = true;
            role.ReadOnly = true;
            dob.ReadOnly = true;
            adres.ReadOnly = true;
            description.ReadOnly = true;
            nrOfAvailableSeats.ReadOnly = true;
        }

        private string _fullname;
        private string _username;
        private string _email;
        private string _password;
        private string _phone;
        private string _role;
        private string _dob;
        private string _adres;
        private string _description;
        private string _nrOfAvailableSeats;

        public string FullName
        {
            get { return _fullname; }
            set { _fullname = value; fullname.Text = value; }
        }

        public string UserName
        {
            get { return _username; }
            set { _username = value; username.Text = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; email.Text = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; password.Text = value; }
        }

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; phone.Text = value; }
        }

        public string Role
        {
            get { return _role; }
            set { _role = value; role.Text = value; }
        }

        public string DateOfBirth
        {
            get { return _dob; }
            set { _dob = value; dob.Text = value; }
        }

        public string Adres
        {
            get { return _adres; }
            set { _adres = value; adres.Text = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; description.Text = value; }
        }
        private string NrOfAvailableSeats
        {
            get { return _nrOfAvailableSeats; }
            set { _nrOfAvailableSeats = value; nrOfAvailableSeats.Text = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fullname.ReadOnly = false;
            username.ReadOnly = false;
            email.ReadOnly = false;
            phone.ReadOnly = false;
            password.ReadOnly = false;
            role.ReadOnly = false;
            dob.ReadOnly = false;
            adres.ReadOnly = false;
            description.ReadOnly = false;
            nrOfAvailableSeats.ReadOnly = false;
        }

    }
}
