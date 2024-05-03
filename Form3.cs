using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Documents;

namespace Search_The_GP
{
    public partial class Form3 : Form
    {

        private bool isEditMode = false;
        private string originalPassword = "AdminMAI";

        public Form3()
        {
            InitializeComponent();
            // Setăm poziția formularului la centrul ecranului
            this.StartPosition = FormStartPosition.CenterScreen;

            // Inițializăm vizibilitatea panourilor
            contentPatients.Visible = false; 
            contentProfil.Visible = true;
            contentDoctors.Visible = false;

            nrOfAdmin.ReadOnly = true;
            nrOfDoctors.ReadOnly = true;
            nrOfPatients.ReadOnly = true;

            ToggleEditMode(false);
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

            getPatients();
        }

        private void btnDoctors_Click(object sender, EventArgs e)
        {
            // Schimbăm vizibilitatea panourilor pentru a afișa lista de medici
            contentPatients.Visible = false;
            contentProfil.Visible = false;
            contentDoctors.Visible = true;
        }
        private void fullname_TextChanged(object sender, EventArgs e)
        {

        }

        private void username_TextChanged(object sender, EventArgs e)
        {

        }

        private void email_TextChanged(object sender, EventArgs e)
        {

        }

        private void phone_TextChanged(object sender, EventArgs e)
        {

        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }

        private void role_TextChanged(object sender, EventArgs e)
        {

        }

        private void dob_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2btnEdit_Click(object sender, EventArgs e)
        {
            isEditMode = !isEditMode;
            ToggleEditMode(isEditMode);

        }
        private void ToggleEditMode(bool editMode)
        {
            if (editMode)
            {
                // Salvăm parola originală 
                originalPassword = password.Text;

                // Activați editarea câmpurilor text
                fullname.ReadOnly = false;
                username.ReadOnly = false;
                email.ReadOnly = false;
                phone.ReadOnly = false;
                password.ReadOnly = false;
                role.ReadOnly = false;
                dob.ReadOnly = false;

                // Modificăm textul și culoarea butonului
                button2btnEdit.Text = "Save";
                button2btnEdit.BackColor = Color.FromArgb(138, 132, 226);

                // Facem câmpul de parolă vizibil
                password.UseSystemPasswordChar = false;
            }
            else
            {
                // Dezactivăm editarea câmpurilor text
                fullname.ReadOnly = true;
                username.ReadOnly = true;
                email.ReadOnly = true;
                phone.ReadOnly = true;
                password.ReadOnly = true;
                role.ReadOnly = true;
                dob.ReadOnly = true;

                // Modificăm textul și culoarea butonului
                button2btnEdit.Text = "Edit";
                button2btnEdit.BackColor = button2btnEdit.BackColor = Color.FromArgb(114, 155, 121);

                // Facem câmpul de parolă invizibil
                password.UseSystemPasswordChar = true;
            }
        }

        //Patients
        private void getPatients()
        {
            PatientList[] patientList = new PatientList[20];
            for (int i = 0; i < patientList.Length; i++)
            {
                patientList[i] = new PatientList();
                patientList[i].Patients = "Munteanu Alexandru";
                patientList[i].Phone = "+40 738474815";
                patientList[i].Email = "alexandru.munteanu6@student.usv.ro";


                flowLayoutPanel1.Controls.Add(patientList[i]);

            }

            foreach (PatientList patient in patientList)
            {
                patient.readClicked += PatientList_ButtonClicked;
            }
        }

        private void PatientList_ButtonClicked(object sender, EventArgs e)
        {
            // Afișați mesajul atunci când butonul este apăsat
            MessageBox.Show("Button clicked in PatientList UserControl!");
        }
    }
}
