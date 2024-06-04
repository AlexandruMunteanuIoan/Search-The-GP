using System;
using System.Windows.Forms;

namespace Search_The_GP
{
    public partial class DoctorList : UserControl
    {
        public event EventHandler readClicked;

        public DoctorList()
        {
            InitializeComponent();
        }

        private string _doctor;
        private string _description;
        private string _email;
        private string _phone;
        private string _adres;
        private string _username;
        private string _password;
        private string _role;
        private string _dob;
        private string _nrOfAvailableSeats;
        private int _idUser;
        private int _idMedic;

        public string Doctor
        {
            get { return _doctor; }
            set { _doctor = value; fullname.Text = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; description.Text = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; email.Text = value; }
        }

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; phone.Text = value; }
        }

        public string Adres
        {
            get { return _adres; }
            set { _adres = value; adres.Text = value; }
        }

        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Role
        {
            get { return _role; }
            set { _role = value; }
        }

        public string DateOfBirth
        {
            get { return _dob; }
            set { _dob = value; }
        }

        public string NrOfAvailableSeats
        {
            get { return _nrOfAvailableSeats; }
            set { _nrOfAvailableSeats = value; }
        }
        public int IdUser
        {
            get { return _idUser; }
            set { _idUser = value; }
        }

        public int IdMedic
        {
            get { return _idMedic; }
            set { _idMedic = value; }
        }

        private void read_Click(object sender, EventArgs e)
        {
            readClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
