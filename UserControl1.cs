using System;
using System.Windows.Forms;

namespace Search_The_GP
{
    public partial class PatientList : UserControl
    {
        public event EventHandler readClicked;

        public PatientList()
        {
            InitializeComponent();
        }

        private string _patients;
        private string _description;
        private string _email;
        private string _phone;
        private string _username;
        private string _password;
        private string _role;
        private string _dob;
        private int _idUser;
        private int _idPatient;

        public string Patients
        {
            get { return _patients; }
            set { _patients = value; fullname.Text = value; }
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
        public int IdUser
        {
            get { return _idUser; }
            set { _idUser = value; }
        }
        public int IdPatient
        {
            get { return _idPatient; }
            set { _idPatient = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            readClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
