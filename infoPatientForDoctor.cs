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
    public partial class InfoPatientForDoctor : UserControl
    {

        public event EventHandler editClicked;

        public InfoPatientForDoctor()
        {
            InitializeComponent();

            fullname.ReadOnly = true;
            email.ReadOnly = true;
            phone.ReadOnly = true;
            dob.ReadOnly = true;
            description.ReadOnly = false;

            description.TextChanged += Description_TextChanged;
        }

        public event EventHandler applyClicked;
        private string _fullname;
        private string _email;
        private string _phone;
        private string _dob;
        private string _description;
        private int _idUser;
        private int _idPatient;

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

        public string FullName
        {
            get { return _fullname; }
            set { _fullname = value; fullname.Text = value; }
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

        public string DateOfBirth
        {
            get { return _dob; }
            set { _dob = value; dob.Text = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; description.Text = value; }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            editClicked?.Invoke(this, EventArgs.Empty);
        }
        private void Description_TextChanged(object sender, EventArgs e)
        {
            _description = description.Text;
        }

    
    }
}
