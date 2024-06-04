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
        private int _idCerere;
        private int _idPatient;

        public int IdCerere
        {
            get { return _idCerere; }
            set { _idCerere = value; }
        }
        public int IdPatient
        {
            get { return _idPatient; }
            set { _idPatient = value; }
        }

        public string Patient
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
            status.Text = "Se proceseaza";
            status.ForeColor = System.Drawing.Color.Blue;
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
