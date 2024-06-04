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
    public partial class RequestsForPatient : UserControl
    {
        public event EventHandler deleteClicked;

        public RequestsForPatient()
        {
            InitializeComponent();

        }

        private string _doctor;
        private string _dateOfRequest;
        private string _status;
        private int _idCerere;

        public int IdCerere
        {
            get { return _idCerere; }
            set { _idCerere = value; }
        }

        public string Doctor
        {
            get { return _doctor; }
            set { _doctor = value; fnDoctor.Text = value; }
        }

        public string DateOfRequest
        {
            get { return _dateOfRequest; }
            set { _dateOfRequest = value; dateOfRequest.Text = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; status.Text = value; }

        }
        public void StatusColor()
        {
            switch (status.Text)
            {
                case "respinsa":
                    status.Text = "Respins";
                    status.ForeColor = System.Drawing.Color.Red;
                    break;
                case "acceptata":
                    status.Text = "Acceptat";
                    status.ForeColor = System.Drawing.Color.DarkGreen;
                    break;
                case "in_asteptare":
                    status.Text = "Se proceseaza";
                    status.ForeColor = System.Drawing.Color.Blue;
                    break;
                default:
                    break;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteClicked?.Invoke(this, EventArgs.Empty);
        }

    }
}
