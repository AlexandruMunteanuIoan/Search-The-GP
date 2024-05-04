using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Search_The_GP
{
    public partial class InfoDoctorForPatient : UserControl
    {
        public InfoDoctorForPatient()
        {
            InitializeComponent();

            fullname.ReadOnly = true;
            email.ReadOnly = true;
            phone.ReadOnly = true;
            dob.ReadOnly = true;
            adres.ReadOnly = true;
            description.ReadOnly = true;
        }

        public event EventHandler applyClicked;
        private string _fullname;
        private string _email;
        private string _phone;
        private string _dob;
        private string _adres;
        private string _description;

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

        private void btnApply_Click(object sender, EventArgs e)
        {
            applyClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
