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
    public partial class DoctorList : UserControl
    {
        public event EventHandler readClicked;

        public DoctorList()
        {
            InitializeComponent();
        }

        private string _patients;
        private string _description;
        private string _email;
        private string _phone;
        private string _adres;

        public string Doctor
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
        public string Adres
        {
            get { return _adres; }
            set { _adres = value; adres.Text = value; }
        }

        private void read_Click(object sender, EventArgs e)
        {
            readClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}

