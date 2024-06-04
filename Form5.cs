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
            LoadStatistics(GetMedicId(userId));
        }
        private int GetMedicId(int userId)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT id_medic FROM Medici WHERE id_user = :userId";

                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        cmd.Parameters.Add(new OracleParameter("userId", userId));
                        object result = cmd.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int medicId))
                        {
                            return medicId;
                        }
                        else
                        {
                            throw new Exception("Medic ID not found for the given User ID.");
                        }
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show("An error occurred while fetching the Medic ID: " + exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
        }


        private void LoadStatistics(int loggedInMedicId)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Interogare pentru numărul de pacienți ai medicului logat
                    string queryPatients = @"
                SELECT COUNT(*) 
                FROM Pacienti P
                JOIN Cereri C ON P.id_pacient = C.id_pacient
                WHERE C.id_medic = :loggedInMedicId
                AND stare = 'acceptata'";

                    using (OracleCommand cmd = new OracleCommand(queryPatients, con))
                    {
                        cmd.Parameters.Add(new OracleParameter("loggedInMedicId", loggedInMedicId));
                        nrOfPatients.Text = "Pacienti - " + cmd.ExecuteScalar().ToString();
                    }

                    // Interogare pentru numărul de cereri în așteptare pentru medicul logat
                    string queryRequests = @"
                SELECT COUNT(*) 
                FROM Cereri 
                WHERE id_medic = :loggedInMedicId AND stare = 'in_asteptare'";

                    using (OracleCommand cmd = new OracleCommand(queryRequests, con))
                    {
                        cmd.Parameters.Add(new OracleParameter("loggedInMedicId", loggedInMedicId));
                        nrOfRequests.Text = "Cereri - " + cmd.ExecuteScalar().ToString();
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show("An error occurred while loading statistics: " + exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

            getPatients(GetMedicId(userId));
        }

        private void btnRequests_Click(object sender, EventArgs e)
        {
            contentProfil.Visible = false;
            contentPatients.Visible = false;
            contentRequest.Visible = true;

            getRequests(GetMedicId(userId));
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
                dob.ReadOnly = true;
                description.ReadOnly = false;
                nrLocuriDisponibile.ReadOnly = false;
                Adres.ReadOnly = false;


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
                nrLocuriDisponibile.ReadOnly = true;
                Adres.ReadOnly = true;

                // Modificăm textul și culoarea butonului
                btnEditProfil.Text = "Edit";
                btnEditProfil.BackColor = btnEditProfil.BackColor = Color.FromArgb(114, 155, 121);

                // Facem câmpul de parolă invizibil
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
        private void SaveUserData()
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Utilizarea unei declarații actualizate (UPDATE) pentru a modifica datele în tabelul Users
                    string queryUser = @"
                UPDATE Users 
                SET nume = :nume, prenume = :prenume, username = :username, email = :email, numar_telefon = :phone, password = :password, data_nasterii = :dob 
                WHERE id_user = :userID";

                    // Utilizarea unei declarații actualizate (UPDATE) pentru a modifica datele în tabelul Medici
                    string queryMedic = @"
                UPDATE Medici 
                SET descriere = :descriere, adresa_cabinet = :adresaCabinet, nr_Locuri_Disponibile = :nrLocuriDisponibile 
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

                        using (OracleCommand cmdMedic = new OracleCommand(queryMedic, con))
                        {
                            cmdMedic.Parameters.Add(":descriere", OracleDbType.Clob).Value = description.Text;
                            cmdMedic.Parameters.Add(":adresaCabinet", OracleDbType.Varchar2).Value = Adres.Text;
                            cmdMedic.Parameters.Add(":nrLocuriDisponibile", OracleDbType.Int32).Value = int.Parse(nrLocuriDisponibile.Text);
                            cmdMedic.Parameters.Add(":userID", OracleDbType.Int32).Value = userId;

                            cmdMedic.ExecuteNonQuery();
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


        //Patients
        private void getPatients(int loggedInMedicId)
        {
            flowLayoutPanel1.Controls.Clear();

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = @"
            SELECT u.id_user, u.nume, u.prenume, u.numar_telefon, u.email, u.username, u.password, u.tip_utilizator, u.data_nasterii, p.descriere, p.id_pacient
            FROM Users u
            JOIN Pacienti p ON u.id_user = p.id_user
            JOIN Cereri c ON p.id_pacient = c.id_pacient
            WHERE c.id_medic = :loggedInMedicId AND c.stare = 'acceptata'";

                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        cmd.Parameters.Add(":loggedInMedicId", OracleDbType.Int32).Value = loggedInMedicId;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PatientList patient = new PatientList
                                {
                                    IdUser = reader.GetInt32(reader.GetOrdinal("id_user")),
                                    IdPatient = reader.GetInt32(reader.GetOrdinal("id_pacient")),
                                    Patients = reader["nume"].ToString() + " " + reader["prenume"].ToString(),
                                    Phone = reader["numar_telefon"].ToString(),
                                    Email = reader["email"].ToString(),
                                    Description = reader["descriere"].ToString(),
                                    UserName = reader["username"].ToString(),
                                    Password = reader["password"].ToString(),
                                    Role = reader["tip_utilizator"].ToString(),
                                    DateOfBirth = Convert.ToDateTime(reader["data_nasterii"]).ToShortDateString()
                                };

                                flowLayoutPanel1.Controls.Add(patient);
                                patient.readClicked += PatientList_ButtonClicked;
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

        private void PatientList_ButtonClicked(object sender, EventArgs e)
        {
            PatientList clickedPatient = sender as PatientList;
            if (clickedPatient != null)
            {
                getInfoPatient(clickedPatient);
            }
        }

        private void getInfoPatient(PatientList clickedPatient)
        {
            InfoPatientForDoctor patientInfo = new InfoPatientForDoctor
            {
                FullName = clickedPatient.Patients,
                Phone = clickedPatient.Phone,
                Email = clickedPatient.Email,    
                DateOfBirth = clickedPatient.DateOfBirth,
                Description = clickedPatient.Description,
                IdPatient = clickedPatient.IdPatient,
                IdUser = clickedPatient.IdUser
            };

            infoPatient.Controls.Clear();
            infoPatient.Controls.Add(patientInfo);

            patientInfo.editClicked += Patient_ButtonEditClicked;
        }

        //Edit Pacient
        private void Patient_ButtonEditClicked(object sender, EventArgs e)
        {
            if (infoPatient.Controls[0] is InfoPatientForDoctor patientInfo)
            {
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        

                        string updatePatientQuery = @"UPDATE Pacienti SET descriere = :descriere WHERE id_pacient = :id_pacient";
                        using (OracleCommand cmd = new OracleCommand(updatePatientQuery, con))
                        {
                            cmd.Parameters.Add(new OracleParameter("descriere", patientInfo.Description));
                            cmd.Parameters.Add(new OracleParameter("id_pacient", patientInfo.IdPatient));
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Pacientul a fost actualizat cu succes!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        getPatients(GetMedicId(userId));
                        infoPatient.Controls.Clear();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("A apărut o eroare: " + exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        //Requests
        private void getRequests(int loggedInMedicId)
        {
            flowLayoutPanel3.RightToLeft = RightToLeft.Yes;
            flowLayoutPanel3.Controls.Clear();

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
            WHERE c.id_medic = :loggedInMedicId";

                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        cmd.Parameters.Add(":loggedInMedicId", OracleDbType.Int32).Value = loggedInMedicId;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RequestsForDoctor request = new RequestsForDoctor
                                {
                                    IdCerere = reader.GetInt32(reader.GetOrdinal("id_cerere")),
                                    IdPatient = reader.GetInt32(reader.GetOrdinal("id_pacient")),
                                    Patient = reader["pacient"].ToString(),
                                    DateOfRequest = Convert.ToDateTime(reader["data_cerere"]).ToShortDateString(),
                                    Status = reader["stare"].ToString()
                                };
                                request.StatusColor();

                                if (request.Status == "in_asteptare")
                                {
                                    flowLayoutPanel3.Controls.Add(request);
                                }

                                request.rejectClicked += Request_ButtonRejectClicked;
                                request.acceptClicked += Request_ButtonAcceptClicked;
                                request.infoClicked += Request_ButtonInfoClicked;
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

        private void Request_ButtonRejectClicked(object sender, EventArgs e)
        {
            RequestsForDoctor clickedRequest = sender as RequestsForDoctor;
            if (clickedRequest != null)
            {
                UpdateRequestStatus(clickedRequest.IdCerere, "respinsa");
                MessageBox.Show("Rejected", "Titlu Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                patientInfo.Controls.Clear();
                getRequests(GetMedicId(userId)); // Reîncarcă cererile după actualizare
            }
        }

        private void Request_ButtonAcceptClicked(object sender, EventArgs e)
        {
            RequestsForDoctor clickedRequest = sender as RequestsForDoctor;
            if (clickedRequest != null)
            {
                UpdateRequestStatus(clickedRequest.IdCerere, "acceptata");
                MessageBox.Show("Accepted", "Titlu Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                patientInfo.Controls.Clear();
                getRequests(GetMedicId(userId)); // Reîncarcă cererile după actualizare
            }
        }

        private void Request_ButtonInfoClicked(object sender, EventArgs e)
        {
            RequestsForDoctor clickedRequest = sender as RequestsForDoctor;
            if (clickedRequest != null)
            {
                getPatientInfo(clickedRequest.IdPatient);
            }
        }

        private void getPatientInfo(int patientId)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = @"
                        SELECT u.nume || ' ' || u.prenume AS fullname, 
                               u.numar_telefon, 
                               u.email,
                               u.data_nasterii,
                               p.descriere,
                               p.id_pacient,
                               u.id_user
                        FROM Users u
                        JOIN Pacienti p ON u.id_user = p.id_user
                        WHERE p.id_pacient = :patientId";

                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        cmd.Parameters.Add(":patientId", OracleDbType.Int32).Value = patientId;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                InfoPatientForDoctor infoPatient = new InfoPatientForDoctor
                                {
                                    FullName = reader["fullname"].ToString(),
                                    Phone = reader["numar_telefon"].ToString(),
                                    Email = reader["email"].ToString(),
                                    DateOfBirth = Convert.ToDateTime(reader["data_nasterii"]).ToShortDateString(),
                                    Description = reader["descriere"].ToString(),
                                    IdPatient = reader.GetInt32(reader.GetOrdinal("id_pacient")),
                                    IdUser = reader.GetInt32(reader.GetOrdinal("id_user"))
                                };

                                patientInfo.Controls.Clear();
                                patientInfo.Controls.Add(infoPatient);
                            }
                            else
                            {
                                MessageBox.Show("Patient information not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        private void UpdateRequestStatus(int requestId, string newStatus)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "UPDATE Cereri SET stare = :newStatus WHERE id_cerere = :requestId";

                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        cmd.Parameters.Add(":newStatus", OracleDbType.Varchar2).Value = newStatus;
                        cmd.Parameters.Add(":requestId", OracleDbType.Int32).Value = requestId;

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show("An error occurred: " + exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



    }
}
