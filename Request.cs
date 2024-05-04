 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
        public void StatusColor()
        {
            switch (status.Text)
            {
                case "Rejected":
                    status.ForeColor = System.Drawing.Color.Red;
                    break;
                case "Accepted":
                    status.ForeColor = System.Drawing.Color.DarkGreen;
                    break;
                case "Pending":
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
