using System;
using System.Windows.Forms;

namespace Search_The_GP
{
    public partial class Request : UserControl
    {
        public event EventHandler editClicked;
        public event EventHandler deleteClicked;

        public Request()
        {
            InitializeComponent();
        }

        private string _doctor;
        private string _patient;
        private string _dateOfRequest;
        private string _status;
        private int _idCerere;

        public int IdCerere
        {
            get { return _idCerere; }
            set { _idCerere = value; }
        }

        public string Patient
        {
            get { return _patient; }
            set { _patient = value; fnPatient.Text = value; }
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

        public string GetStatusText()
        {
            return status.Text.ToLower();
        }

        public void StatusColor()
        {
            switch (status.Text.ToLower())
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
                    status.Text = "Se Proceseaza";
                    status.ForeColor = System.Drawing.Color.Blue;
                    break;
                default:
                    break;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            editClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
