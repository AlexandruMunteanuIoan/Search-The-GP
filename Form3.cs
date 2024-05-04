using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Documents;
using System.Drawing.Text;

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
            contentRequest.Visible = false;

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
            contentRequest.Visible = false;
        }

        private void btnPatients_Click(object sender, EventArgs e)
        {
            // Schimbăm vizibilitatea panourilor pentru a afișa lista de pacienți
            contentPatients.Visible = true;
            contentProfil.Visible = false;
            contentDoctors.Visible = false;
            contentRequest.Visible = false;

            getPatients();
        }

        private void btnDoctors_Click(object sender, EventArgs e)
        {
            // Schimbăm vizibilitatea panourilor pentru a afișa lista de medici
            contentPatients.Visible = false;
            contentProfil.Visible = false;
            contentDoctors.Visible = true;
            contentRequest.Visible = false;

            getDoctors();
        }

        private void btnRequests_Click(object sender, EventArgs e)
        {
            // Schimbăm vizibilitatea panourilor pentru a afișa lista de medici
            contentPatients.Visible = false;
            contentProfil.Visible = false;
            contentDoctors.Visible = false;
            contentRequest.Visible = true;

            getRequests();
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
            flowLayoutPanel1.Controls.Clear();
            PatientList[] patientList = new PatientList[7];
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
            getInfoPatient();
        }

        private void getInfoPatient()
        {

            InfoPatient patientList = new InfoPatient();
            
                patientList = new InfoPatient();
                patientList.FullName = $"Munteanu Alexandru Ioan (i)";
                patientList.Phone = "+40 738474815";
                patientList.Email = "alexandru.munteanu6@student.usv.ro";

                infoPatient.Controls.Clear();
                infoPatient.Controls.Add(patientList);
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
            InfoDoctor doctorInfo = new InfoDoctor();

            doctorInfo = new InfoDoctor();
            doctorInfo.FullName = " Alexandru Munteanu Ioan (MAI)";
            doctorInfo.Phone = "+40 738474815";

            infoDoctor.Controls.Clear();
            infoDoctor.Controls.Add(doctorInfo);
        }


        //Requests
        private void getRequests()
        {
            flowLayoutPanel3.RightToLeft = RightToLeft.Yes;
            flowLayoutPanel3.Controls.Clear();
            flowLayoutPanel4.Controls.Clear();

            Request[] requests = new Request[15];
            for (int i = 0; i < requests.Length/2; i++)
            {
                requests[i] = new Request();
                requests[i].Doctor = "Munteanu Alexandru "+ i;
                requests[i].Patient = "Ion Popescu " + i;
                requests[i].Status = "Rejected";
                requests[i].StatusColor();

                flowLayoutPanel3.Controls.Add(requests[i]);
            }

            for(int i = requests.Length/2; i <  requests.Length -1;i++)
            {
                requests[i] = new Request();
                requests[i].Doctor = "Munteanu Alexandru " + (i);
                requests[i].Patient = "Ion Popescu " + (i);
                requests[i].Status = "Accepted";
                requests[i].StatusColor();

                flowLayoutPanel4.Controls.Add(requests[i]);
            }

            requests[requests.Length-1] = new Request();
            requests[requests.Length-1].Doctor = "Munteanu Alexandru " + (requests.Length - 1);
            requests[requests.Length - 1].Patient = "Ion Popescu " + (requests.Length - 1);
            requests[requests.Length - 1].Status = "Pending";
            requests[requests.Length - 1].StatusColor();

            flowLayoutPanel4.Controls.Add(requests[requests.Length - 1]);

            foreach (Request request in requests)
            {
                request.editClicked += Request_ButtonEditClicked;
                request.deleteClicked += Request_ButtonDeleteClicked;

            }
        }
        private void Request_ButtonEditClicked(object sender, EventArgs e)
        {
            MessageBox.Show("Edit", "Titlu Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Request_ButtonDeleteClicked(object sender, EventArgs e)
        {
            MessageBox.Show("Delete", "Titlu Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
