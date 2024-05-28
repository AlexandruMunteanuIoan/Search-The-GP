using Oracle.ManagedDataAccess.Client;
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
    public partial class Form5 : Form
    {

        private bool isEditMode = false;
        Random random = new Random();
        string[] statuses = { "Accepted", "Rejected", "Pending" };
        private int userId;
        private string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=LOCALHOST)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=MORCL)));User Id=SYSTEM;Password=Morbius#070802;";


        public Form5(int userId)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.userId = userId;

            // Inițializăm vizibilitatea panourilor
            contentProfil.Visible = true;
            contentPatients.Visible = false;
            contentRequest.Visible = false;

            ToggleEditMode(false);
            LoadUserData();
        }

        private void LoadUserData()
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT u.*, m.descriere, m.adresa_cabinet, m.nr_Locuri_Disponibile FROM Users u " +
                                   "INNER JOIN Medici m ON u.id_user = m.id_user " +
                                   "WHERE u.id_user = :userId";
                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        cmd.Parameters.Add(":userId", OracleDbType.Int32).Value = userId;
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string nume = reader["nume"].ToString();
                                string prenume = reader["prenume"].ToString();
                                fullname.Text = $"{nume} {prenume}";
                                username.Text = reader["username"].ToString();
                                email.Text = reader["email"].ToString();
                                phone.Text = reader["numar_telefon"].ToString();
                                password.Text = reader["password"].ToString();
                                dob.Text = Convert.ToDateTime(reader["data_nasterii"]).ToShortDateString();
                                description.Text = reader["descriere"].ToString();
                                Adres.Text = reader["adresa_cabinet"].ToString();
                                nrLocuriDisponibile.Text = reader["nr_Locuri_Disponibile"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show("An error occurred: " + exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnProfil_Click(object sender, EventArgs e)
        {
            contentProfil.Visible = true;
            contentPatients.Visible = false;
            contentRequest.Visible = false;
        }

        private void btnPatients_Click(object sender, EventArgs e)
        {
            contentProfil.Visible = false;
            contentPatients.Visible = true;
            contentRequest.Visible = false;

            getPatients();
        }

        private void btnRequests_Click(object sender, EventArgs e)
        {
            contentProfil.Visible = false;
            contentPatients.Visible = false;
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

            InfoPatientForDoctor patientInfo = new InfoPatientForDoctor();

            patientInfo = new InfoPatientForDoctor();
            patientInfo.FullName = "Munteanu Alexandru Ioan";
            patientInfo.Phone = "+40 738474815";
            patientInfo.Email = "alexandru.munteanu6@student.usv.ro";

            infoPatient.Controls.Clear();
            infoPatient.Controls.Add(patientInfo);
        }


        //Requests
        private void getRequests()
        {
            flowLayoutPanel3.RightToLeft = RightToLeft.Yes;
            flowLayoutPanel3.Controls.Clear();

            RequestsForDoctor[] requests = new RequestsForDoctor[5];
            for (int i = 0; i < requests.Length; i++)
            {
                requests[i] = new RequestsForDoctor();
                requests[i].Doctor = "Munteanu Alexandru " + i;

                int randomIndex = random.Next(0, 3);
                requests[i].Status = statuses[randomIndex];

                if (requests[i].Status == "Pending")
                {
                    requests[i].StatusColor();
                    flowLayoutPanel3.Controls.Add(requests[i]);
                }
            }

            foreach (RequestsForDoctor request in requests)
            {

                request.rejectClicked += Request_ButtonRejectClicked;
                request.acceptClicked += Request_ButtonAcceptClicked;
                request.infoClicked += Request_ButtonInfoClicked;

            }
        }
        private void Request_ButtonRejectClicked(object sender, EventArgs e)
        {
            MessageBox.Show("Rejected", "Titlu Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
            patientInfo.Controls.Clear();
        }
        private void Request_ButtonAcceptClicked(object sender, EventArgs e)
        {
            MessageBox.Show("Accepted", "Titlu Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
            patientInfo.Controls.Clear();
        }
        private void Request_ButtonInfoClicked(object sender, EventArgs e)
        {
            getPatientInfo();
        }
        private void getPatientInfo()
        {

            InfoPatientForDoctor infoPatient = new InfoPatientForDoctor();

            infoPatient = new InfoPatientForDoctor();
            infoPatient.FullName = "Munteanu Alexandru Ioan";
            infoPatient.Phone = "+40 738474815";
            infoPatient.Email = "alexandru.munteanu6@student.usv.ro";

            patientInfo.Controls.Clear();
            patientInfo.Controls.Add(infoPatient);

        }

    }
}
