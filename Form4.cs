using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace Search_The_GP
{
    public partial class Form4 : Form
    {
        private int userId;
        private bool isEditMode = false;
        Random random = new Random();
        string[] statuses = { "Accepted", "Rejected", "Pending" };

        private string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=LOCALHOST)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=MORCL)));User Id=SYSTEM;Password=Morbius#070802;";

        public Form4(int userId)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.userId = userId;

            contentProfil.Visible = true;
            contentDoctors.Visible = false;
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
                    string query = "SELECT u.*, p.descriere FROM Users u " +
                                   "INNER JOIN Pacienti p ON u.id_user = p.id_user " +
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
            contentDoctors.Visible = false;
            contentRequest.Visible = false;
        }

        private void btnDoctors_Click(object sender, EventArgs e)
        {
            contentProfil.Visible = false;
            contentDoctors.Visible = true;
            contentRequest.Visible = false;

            getDoctors();
        }

        private void btnRequests_Click(object sender, EventArgs e)
        {
            contentProfil.Visible = false;
            contentDoctors.Visible = false;
            contentRequest.Visible = true;

            getRequests();
        }

        private void ToggleEditMode(bool editMode)
        {
            fullname.ReadOnly = !editMode;
            username.ReadOnly = !editMode;
            email.ReadOnly = !editMode;
            phone.ReadOnly = !editMode;
            password.ReadOnly = !editMode;
            dob.Enabled = editMode;
            description.ReadOnly = !editMode;

            if (editMode)
            {
                btnEditProfil.Text = "Save";
                btnEditProfil.BackColor = Color.FromArgb(138, 132, 226);
                password.UseSystemPasswordChar = false;
            }
            else
            {
                btnEditProfil.Text = "Edit";
                btnEditProfil.BackColor = Color.FromArgb(114, 155, 121);
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
