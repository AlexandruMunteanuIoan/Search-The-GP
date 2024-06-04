using System;
using System.Windows.Forms;

namespace Search_The_GP
{
    public partial class InfoDoctor : UserControl
    {
        public event EventHandler editClicked;
        public event EventHandler deleteClicked;

        public InfoDoctor()
        {
            InitializeComponent();

            // Setăm toate câmpurile de editare ca fiind read-only sau editabile
            fullname.ReadOnly = false;
            username.ReadOnly = false;
            email.ReadOnly = false;
            phone.ReadOnly = false;
            password.ReadOnly = false;
            role.ReadOnly = true;
            dob.ReadOnly = true;
            adres.ReadOnly = false;
            description.ReadOnly = false;
            nrOfAvailableSeats.ReadOnly = false;

            // Adăugăm evenimente TextChanged pentru a prelua datele modificate
            fullname.TextChanged += Fullname_TextChanged;
            username.TextChanged += Username_TextChanged;
            email.TextChanged += Email_TextChanged;
            phone.TextChanged += Phone_TextChanged;
            password.TextChanged += Password_TextChanged;
            role.TextChanged += Role_TextChanged;
            dob.TextChanged += Dob_TextChanged;
            adres.TextChanged += Adres_TextChanged;
            description.TextChanged += Description_TextChanged;
            nrOfAvailableSeats.TextChanged += NrOfAvailableSeats_TextChanged;
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
        private int _idUser;
        private int _idMedic;

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

        public string NrOfAvailableSeats
        {
            get { return _nrOfAvailableSeats; }
            set { _nrOfAvailableSeats = value; nrOfAvailableSeats.Text = value; }
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            editClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteClicked?.Invoke(this, EventArgs.Empty);
        }

        private void Fullname_TextChanged(object sender, EventArgs e)
        {
            _fullname = fullname.Text;
        }

        private void Username_TextChanged(object sender, EventArgs e)
        {
            _username = username.Text;
        }

        private void Email_TextChanged(object sender, EventArgs e)
        {
            _email = email.Text;
        }

        private void Phone_TextChanged(object sender, EventArgs e)
        {
            _phone = phone.Text;
        }

        private void Password_TextChanged(object sender, EventArgs e)
        {
            _password = password.Text;
        }

        private void Role_TextChanged(object sender, EventArgs e)
        {
            _role = role.Text;
        }

        private void Dob_TextChanged(object sender, EventArgs e)
        {
            _dob = dob.Text;
        }

        private void Adres_TextChanged(object sender, EventArgs e)
        {
            _adres = adres.Text;
        }

        private void Description_TextChanged(object sender, EventArgs e)
        {
            _description = description.Text;
        }

        private void NrOfAvailableSeats_TextChanged(object sender, EventArgs e)
        {
            _nrOfAvailableSeats = nrOfAvailableSeats.Text;
        }
    }
}
