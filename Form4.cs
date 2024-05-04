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
    public partial class Form4 : Form
    {

        private bool isEditMode = false;
        Random random = new Random();
        string[] statuses = { "Accepted", "Rejected", "Pending" };


        public Form4()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // Inițializăm vizibilitatea panourilor
            contentProfil.Visible = true;
            contentDoctors.Visible = false;
            contentRequest.Visible = false;

            ToggleEditMode(false);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnProfil_Click(object sender, EventArgs e)
        {
            // Schimbăm vizibilitatea panourilor pentru a afișa profilul
            contentProfil.Visible = true;
            contentDoctors.Visible = false;
            contentRequest.Visible = false;
        }

        private void btnDoctors_Click(object sender, EventArgs e)
        {
            // Schimbăm vizibilitatea panourilor pentru a afișa lista de medici
            contentProfil.Visible = false;
            contentDoctors.Visible = true;
            contentRequest.Visible = false;

            getDoctors();
        }

        private void btnRequests_Click(object sender, EventArgs e)
        {
            // Schimbăm vizibilitatea panourilor pentru a afișa lista de medici
            contentProfil.Visible = false;
            contentDoctors.Visible = false;
            contentRequest.Visible = true;

            getRequests();
        }

        private void ToggleEditMode(bool editMode)
        {
            if (editMode)
            {
                // Activați editarea câmpurilor text
                fullname.ReadOnly = false;
                username.ReadOnly = false;
                email.ReadOnly = false;
                phone.ReadOnly = false;
                password.ReadOnly = false;
                dob.ReadOnly = false;
                description.ReadOnly = false;


                // Modificăm textul și culoarea butonului
                btnEditProfil.Text = "Save";
                btnEditProfil.BackColor = Color.FromArgb(138, 132, 226);

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
                description.ReadOnly = true;

                // Modificăm textul și culoarea butonului
                btnEditProfil.Text = "Edit";
                btnEditProfil.BackColor = btnEditProfil.BackColor = Color.FromArgb(114, 155, 121);

                // Facem câmpul de parolă invizibil
                password.UseSystemPasswordChar = true;
            }
        }
        private void btnEditProfil_Click(object sender, EventArgs e)
        {
            isEditMode = !isEditMode;
            ToggleEditMode(isEditMode);
        }

        //Doctors
        private void getDoctors()
        {
            flowLayoutPanel2.Controls.Clear();
            DoctorList[] doctorsList = new DoctorList[7];
            for (int i = 0; i < doctorsList.Length; i++)
            {
                doctorsList[i] = new DoctorList();
                doctorsList[i].Doctor = $"Munteanu Alexandru (i)";
                doctorsList[i].Phone = "+40 738474815";

                flowLayoutPanel2.Controls.Add(doctorsList[i]);
            }

            foreach (DoctorList doctor in doctorsList)
            {
                doctor.readClicked += DoctorList_ButtonClicked;
            }
        }

        private void DoctorList_ButtonClicked(object sender, EventArgs e)
        {
            getInfoDoctor();
        }

        private void getInfoDoctor()
        {
            InfoDoctorForPatient doctorInfo = new InfoDoctorForPatient();

            doctorInfo = new InfoDoctorForPatient();
            doctorInfo.FullName = " Alexandru Munteanu Ioan (MAI)";
            doctorInfo.Phone = "+40 738474815";

            infoDoctor.Controls.Clear();
            infoDoctor.Controls.Add(doctorInfo);
            doctorInfo.applyClicked += Apply_ButtonClicked;
        }
        private void Apply_ButtonClicked(object sender, EventArgs e)
        {
            MessageBox.Show("Applied", "Titlu Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        //Requests
        private void getRequests()
        {
            flowLayoutPanel3.RightToLeft = RightToLeft.Yes;
            flowLayoutPanel3.Controls.Clear();
            flowLayoutPanel4.Controls.Clear();

            RequestsForPatient[] requests = new RequestsForPatient[5];
            for (int i = 0; i < requests.Length; i = i + 2)
            {
                requests[i] = new RequestsForPatient();
                requests[i].Doctor = "Munteanu Alexandru " + i;

                int randomIndex = random.Next(0, 3);
                requests[i].Status = statuses[randomIndex];

                requests[i].StatusColor();

                flowLayoutPanel3.Controls.Add(requests[i]);
            }

            for (int i = 1; i < requests.Length; i = i + 2)
            {
                requests[i] = new RequestsForPatient();
                requests[i].Doctor = "Munteanu Alexandru " + (i);

                int randomIndex = random.Next(0, 3);
                requests[i].Status = statuses[randomIndex];

                requests[i].StatusColor();

                flowLayoutPanel4.Controls.Add(requests[i]);
            }

            foreach (RequestsForPatient request in requests)
            {

                request.deleteClicked += Request_ButtonDeleteClicked;

            }
        }
        private void Request_ButtonDeleteClicked(object sender, EventArgs e)
        {
            MessageBox.Show("Delete", "Titlu Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

       
    }
}
