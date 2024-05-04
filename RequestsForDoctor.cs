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
    public partial class RequestsForDoctor : UserControl
    {
        public RequestsForDoctor()
        {
            InitializeComponent();
        }

        public event EventHandler rejectClicked;
        public event EventHandler infoClicked;
        public event EventHandler acceptClicked;

        private string _patient;
        private string _dateOfRequest;
        private string _status;

        public string Doctor
        {
            get { return _patient; }
            set { _patient = value; fnPatient.Text = value; }
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
        private void btnInfo_Click(object sender, EventArgs e)
        {
            infoClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            acceptClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            rejectClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
