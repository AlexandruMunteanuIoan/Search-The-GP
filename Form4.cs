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
        private int idMedic;
        private int idPacient;
        Random random = new Random();
        string[] statuses = { "Accepted", "Rejected", "Pending" };

        private string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=LOCALHOST)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=MORCL)));User Id=SYSTEM;Password=Morbius#070802;";

        public Form4(int userId)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.userId = userId;
            idPacient = GetPacientId(userId);

            contentProfil.Visible = true;
            contentDoctors.Visible = false;
            contentRequest.Visible = false;

            ToggleEditMode(false);
            LoadStatistics();
            LoadUserData();
        }
        private int GetPacientId(int userId)
        {
            int pacientId = -1; // Initializez cu o valoare care indică lipsa unui id_pacient valid

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT id_pacient FROM Pacienti WHERE id_user = :userId";

                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        cmd.Parameters.Add(":userId", OracleDbType.Int32).Value = userId;
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            pacientId = Convert.ToInt32(result);
                        }
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show("An error occurred while fetching pacient ID: " + exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return pacientId;
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

        private void LoadStatistics()
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string queryDoctors = "SELECT COUNT(*) FROM Users WHERE tip_utilizator = 'medic'";
                    using (OracleCommand cmd = new OracleCommand(queryDoctors, con))
                    {
                        nrOfDoctors.Text = "Medici - " + cmd.ExecuteScalar().ToString();
                    }


                    string queryRequests = "SELECT COUNT(*) FROM Cereri WHERE id_pacient = :idPacient";
                    using (OracleCommand cmd = new OracleCommand(queryRequests, con))
                    {
                        cmd.Parameters.Add(":idPacient", OracleDbType.Int32).Value = idPacient;
                        var result = cmd.ExecuteScalar();
                        nrOfRequests.Text = "Cereri - " + result.ToString();
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show("An error occurred while loading statistics: " + exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void SaveUserData()
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string queryUser = @"
        UPDATE Users 
        SET nume = :nume, prenume = :prenume, username = :username, email = :email, numar_telefon = :phone, password = :password, data_nasterii = :dob 
        WHERE id_user = :userID";

                    string queryPacient = @"
        UPDATE Pacienti 
        SET descriere = :descriere 
        WHERE id_user = :userID";

                    using (OracleTransaction transaction = con.BeginTransaction())
                    {
                        using (OracleCommand cmdUser = new OracleCommand(queryUser, con))
                        {
                            cmdUser.Parameters.Add(":nume", OracleDbType.Varchar2).Value = fullname.Text.Split(' ')[0];
                            cmdUser.Parameters.Add(":prenume", OracleDbType.Varchar2).Value = fullname.Text.Split(' ').Length > 1 ? fullname.Text.Split(' ')[1] : "";
                            cmdUser.Parameters.Add(":username", OracleDbType.Varchar2).Value = username.Text;
                            cmdUser.Parameters.Add(":email", OracleDbType.Varchar2).Value = email.Text;
                            cmdUser.Parameters.Add(":phone", OracleDbType.Varchar2).Value = phone.Text;
                            cmdUser.Parameters.Add(":password", OracleDbType.Varchar2).Value = password.Text;
                            cmdUser.Parameters.Add(":dob", OracleDbType.Date).Value = Convert.ToDateTime(dob.Text);
                            cmdUser.Parameters.Add(":userID", OracleDbType.Int32).Value = userId;

                            cmdUser.ExecuteNonQuery();
                        }

                        using (OracleCommand cmdPacient = new OracleCommand(queryPacient, con))
                        {
                            cmdPacient.Parameters.Add(":descriere", OracleDbType.Clob).Value = description.Text;
                            cmdPacient.Parameters.Add(":userID", OracleDbType.Int32).Value = userId;

                            cmdPacient.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("User data updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            dob.ReadOnly = true;
            description.ReadOnly = !editMode;
            role.ReadOnly = true;

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
            if (isEditMode)
            {
                SaveUserData();
            }
            isEditMode = !isEditMode;
            ToggleEditMode(isEditMode);
        }

        //Doctors
        private void getDoctors()
        {
            flowLayoutPanel2.Controls.Clear();

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = @"
                SELECT u.id_user, u.nume, u.prenume, u.numar_telefon, u.email, u.username, u.password, u.tip_utilizator, u.data_nasterii,
                       m.descriere, m.adresa_cabinet, m.nr_Locuri_Disponibile,m.id_medic
                FROM Users u
                JOIN Medici m ON u.id_user = m.id_user";

                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DoctorList doctor = new DoctorList
                                {
                                    IdUser = reader.GetInt32(reader.GetOrdinal("id_user")),
                                    IdMedic = reader.GetInt32(reader.GetOrdinal("id_medic")),
                                    Doctor = reader["nume"].ToString() + " " + reader["prenume"].ToString(),
                                    Phone = reader["numar_telefon"].ToString(),
                                    Email = reader["email"].ToString(),
                                    Description = reader["descriere"].ToString(),
                                    Adres = reader["adresa_cabinet"].ToString(),
                                    NrOfAvailableSeats = reader["nr_Locuri_Disponibile"].ToString(),
                                    UserName = reader["username"].ToString(),
                                    Password = reader["password"].ToString(),
                                    Role = reader["tip_utilizator"].ToString(),
                                    DateOfBirth = Convert.ToDateTime(reader["data_nasterii"]).ToShortDateString()
                                };

                                flowLayoutPanel2.Controls.Add(doctor);
                                doctor.readClicked += DoctorList_ButtonClicked;
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

        private void DoctorList_ButtonClicked(object sender, EventArgs e)
        {
            DoctorList clickedDoctor = sender as DoctorList;
            if (clickedDoctor != null)
            {
                getInfoDoctor(clickedDoctor);
            }
        }

        private void getInfoDoctor(DoctorList clickedDoctor)
        {
            InfoDoctorForPatient doctorInfo = new InfoDoctorForPatient
            {
                FullName = clickedDoctor.Doctor,
                Phone = clickedDoctor.Phone,
                Email = clickedDoctor.Email,
                DateOfBirth = clickedDoctor.DateOfBirth,
                Adres = clickedDoctor.Adres,
                Description = clickedDoctor.Description,
                IdMedic = clickedDoctor.IdMedic,
                IdUser = clickedDoctor.IdUser,
                NrOfAvailableSeats = clickedDoctor.NrOfAvailableSeats
            };

            infoDoctor.Controls.Clear();
            infoDoctor.Controls.Add(doctorInfo);

            idMedic = doctorInfo.IdMedic;
            doctorInfo.applyClicked += Apply_ButtonClicked;

        }

        private void Apply_ButtonClicked(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    con.Open();
                    string query = @"
                INSERT INTO Cereri (id_cerere, id_pacient, id_medic, data_cerere, stare)
                VALUES (seq_cereri.NEXTVAL, :id_pacient, :id_medic, SYSDATE, 'in_asteptare')";

                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {

                        cmd.Parameters.Add(":id_pacient", OracleDbType.Int32).Value = idPacient;
                        cmd.Parameters.Add(":id_medic", OracleDbType.Int32).Value = idMedic;

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Applied", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ai aplicat deja la acest medic", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        //Requests
        private void getRequests()
        {
            flowLayoutPanel3.RightToLeft = RightToLeft.Yes;
            flowLayoutPanel3.Controls.Clear();
            flowLayoutPanel4.Controls.Clear();

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = @"
                SELECT c.id_cerere, 
                       u_pacient.nume || ' ' || u_pacient.prenume AS pacient, 
                       u_medic.nume || ' ' || u_medic.prenume AS medic, 
                       c.data_cerere, 
                       c.stare,
                       p.id_pacient
                FROM Cereri c
                JOIN Pacienti p ON c.id_pacient = p.id_pacient
                JOIN Users u_pacient ON p.id_user = u_pacient.id_user
                JOIN Medici m ON c.id_medic = m.id_medic
                JOIN Users u_medic ON m.id_user = u_medic.id_user
                WHERE p.id_pacient = :idPacient";

                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        cmd.Parameters.Add(":idPacient", OracleDbType.Int32).Value = idPacient;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RequestsForPatient request = new RequestsForPatient();
                                request.IdCerere = Convert.ToInt32(reader["id_cerere"]);
                                request.Doctor = reader["medic"].ToString();
                                request.DateOfRequest = Convert.ToDateTime(reader["data_cerere"]).ToShortDateString();
                                request.Status = reader["stare"].ToString();
                                request.StatusColor();

                                if (flowLayoutPanel3.Controls.Count < 5) // Limităm numărul de cereri pentru fiecare flowLayoutPanel
                                {
                                    flowLayoutPanel3.Controls.Add(request);
                                }
                                else
                                {
                                    flowLayoutPanel4.Controls.Add(request);
                                }

                                request.deleteClicked += Request_ButtonDeleteClicked;

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


        private void Request_ButtonDeleteClicked(object sender, EventArgs e)
        {
            RequestsForPatient request = sender as RequestsForPatient;
            if (request != null)
            {
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        string deleteQuery = "DELETE FROM Cereri WHERE id_cerere = :id_cerere";
                        using (OracleCommand cmd = new OracleCommand(deleteQuery, con))
                        {
                            cmd.Parameters.Add(new OracleParameter("id_cerere", request.IdCerere));
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Cererea a fost ștearsă cu succes!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flowLayoutPanel3.Controls.Remove(request);
                        flowLayoutPanel4.Controls.Remove(request);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("A apărut o eroare: " + exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

    }
}
