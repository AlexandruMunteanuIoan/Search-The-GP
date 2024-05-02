using System;
using System.Windows.Forms;

namespace Search_The_GP
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            // Setăm poziția formularului la centrul ecranului
            this.StartPosition = FormStartPosition.CenterScreen;
            // Inițializăm vizibilitatea panourilor
            contentPatients.Visible = true; // Panoul de pacienți este afișat inițial
            contentProfil.Visible = false;
            contentDoctors.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnProfil_Click(object sender, EventArgs e)
        {
            // Schimbăm vizibilitatea panourilor pentru a afișa profilul
            contentPatients.Visible = false;
            contentProfil.Visible = true;
            contentDoctors.Visible = false;
        }

        private void btnPatients_Click(object sender, EventArgs e)
        {
            // Schimbăm vizibilitatea panourilor pentru a afișa lista de pacienți
            contentPatients.Visible = true;
            contentProfil.Visible = false;
            contentDoctors.Visible = false;
        }

        private void btnDoctors_Click(object sender, EventArgs e)
        {
            // Schimbăm vizibilitatea panourilor pentru a afișa lista de medici
            contentPatients.Visible = false;
            contentProfil.Visible = false;
            contentDoctors.Visible = true;
        }
    }
}
