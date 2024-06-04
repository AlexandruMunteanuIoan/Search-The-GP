using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using Oracle.ManagedDataAccess.Client;

namespace Search_The_GP
{
    public partial class Form3 : Form
    {
        private int userID;
        private bool isEditMode = false;
        private string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=LOCALHOST)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=MORCL)));User Id=SYSTEM;Password=Morbius#070802;";

        public Form3(int userID)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.userID = userID;

            contentPatients.Visible = false;
            contentProfil.Visible = true;
            contentDoctors.Visible = false;
            contentRequest.Visible = false;

            ToggleEditMode(false);
            LoadUserData();
            LoadStatistics();
        }

        private void LoadStatistics()
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Interogare pentru numărul de administratori
                    string queryAdmins = "SELECT COUNT(*) FROM Users WHERE tip_utilizator = 'admin'";
                    using (OracleCommand cmd = new OracleCommand(queryAdmins, con))
                    {
                        nrOfAdmin.Text = "Admin - " + cmd.ExecuteScalar().ToString();
                    }

                    // Interogare pentru numărul de medici
                    string queryDoctors = "SELECT COUNT(*) FROM Users WHERE tip_utilizator = 'medic'";
                    using (OracleCommand cmd = new OracleCommand(queryDoctors, con))
                    {
                        nrOfDoctors.Text = "Medici - " + cmd.ExecuteScalar().ToString();
                    }

                    // Interogare pentru numărul de pacienți
                    string queryPatients = "SELECT COUNT(*) FROM Users WHERE tip_utilizator = 'pacient'";
                    using (OracleCommand cmd = new OracleCommand(queryPatients, con))
                    {
                        nrOfPatients.Text = "Pacienti - " + cmd.ExecuteScalar().ToString();
                    }

                    // Interogare pentru numărul de cereri
                    string queryRequests = "SELECT COUNT(*) FROM Cereri";
                    using (OracleCommand cmd = new OracleCommand(queryRequests, con))
                    {
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
                    string query = "SELECT * FROM Users WHERE id_user = :userID";
                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        cmd.Parameters.Add(":userID", OracleDbType.Int32).Value = userID;
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                fullname.Text = reader["nume"].ToString() + " " + reader["prenume"].ToString();
                                username.Text = reader["username"].ToString();
                                email.Text = reader["email"].ToString();
                                phone.Text = reader["numar_telefon"].ToString();
                                password.Text = reader["password"].ToString();
                                role.Text = reader["tip_utilizator"].ToString();
                                dob.Text = Convert.ToDateTime(reader["data_nasterii"]).ToShortDateString();
                            }
                            else
                            {
                                MessageBox.Show("Valori Editate.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnProfil_Click(object sender, EventArgs e)
        {
            contentPatients.Visible = false;
            contentProfil.Visible = true;
            contentDoctors.Visible = false;
            contentRequest.Visible = false;
        }

        private void btnPatients_Click(object sender, EventArgs e)
        {
            contentPatients.Visible = true;
            contentProfil.Visible = false;
            contentDoctors.Visible = false;
            contentRequest.Visible = false;

            getPatients();
        }

        private void btnDoctors_Click(object sender, EventArgs e)
        {
            contentPatients.Visible = false;
            contentProfil.Visible = false;
            contentDoctors.Visible = true;
            contentRequest.Visible = false;

            getDoctors();
        }

        private void btnRequests_Click(object sender, EventArgs e)
        {
            contentPatients.Visible = false;
            contentProfil.Visible = false;
            contentDoctors.Visible = false;
            contentRequest.Visible = true;

            getRequests();
        }

        private void button2btnEdit_Click(object sender, EventArgs e)
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
                    // Utilizarea unei declarații actualizate (UPDATE) pentru a modifica datele
                    string query = "UPDATE Users SET nume = :nume, prenume = :prenume, username = :username, email = :email, numar_telefon = :phone, password = :password, data_nasterii = :dob WHERE id_user = :userID";
                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        // Separă numele complet în nume și prenume
                        string[] names = fullname.Text.Split(' ');
                        string nume = names[0];
                        string prenume = names.Length > 1 ? names[1] : "";

                        cmd.Parameters.Add(":nume", OracleDbType.Varchar2).Value = nume;
                        cmd.Parameters.Add(":prenume", OracleDbType.Varchar2).Value = prenume;
                        cmd.Parameters.Add(":username", OracleDbType.Varchar2).Value = username.Text;
                        cmd.Parameters.Add(":email", OracleDbType.Varchar2).Value = email.Text;
                        cmd.Parameters.Add(":phone", OracleDbType.Varchar2).Value = phone.Text;
                        cmd.Parameters.Add(":password", OracleDbType.Varchar2).Value = password.Text;
                        cmd.Parameters.Add(":dob", OracleDbType.Date).Value = Convert.ToDateTime(dob.Text);
                        cmd.Parameters.Add(":userID", OracleDbType.Int32).Value = userID;

                        int rowsUpdated = cmd.ExecuteNonQuery();
                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("User data updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No data was updated. Please check the user ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show("An error occurred: " + exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ToggleEditMode(bool editMode)
        {
            fullname.ReadOnly = !editMode;
            username.ReadOnly = !editMode;
            email.ReadOnly = !editMode;
            phone.ReadOnly = !editMode;
            password.ReadOnly = !editMode;
            role.ReadOnly = true; 
            dob.ReadOnly = true;

            if (editMode)
            {
                button2btnEdit.Text = "Save";
                button2btnEdit.BackColor = Color.FromArgb(138, 132, 226);
                password.UseSystemPasswordChar = false;
            }
            else
            {
                button2btnEdit.Text = "Edit";
                button2btnEdit.BackColor = Color.FromArgb(114, 155, 121);
                password.UseSystemPasswordChar = true;
            }
        }


        //Patients
        private void getPatients()
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
                    JOIN Pacienti p ON u.id_user = p.id_user";

                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
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
            InfoPatient patientInfo = new InfoPatient
            {
                FullName = clickedPatient.Patients,
                Phone = clickedPatient.Phone,
                Email = clickedPatient.Email,
                UserName = clickedPatient.UserName,
                Password = clickedPatient.Password,
                Role = clickedPatient.Role,
                DateOfBirth = clickedPatient.DateOfBirth,
                Description = clickedPatient.Description,
                IdPatient = clickedPatient.IdPatient,
                IdUser = clickedPatient.IdUser

            };

            infoPatient.Controls.Clear();
            infoPatient.Controls.Add(patientInfo);

            patientInfo.editClicked += Patient_ButtonEditClicked;
            patientInfo.deleteClicked += Patient_ButtonDeleteClicked;
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
            InfoDoctor doctorInfo = new InfoDoctor
            {
                FullName = clickedDoctor.Doctor,
                Phone = clickedDoctor.Phone,
                Email = clickedDoctor.Email,
                UserName = clickedDoctor.UserName,
                Password = clickedDoctor.Password,
                Role = clickedDoctor.Role,
                DateOfBirth = clickedDoctor.DateOfBirth,
                Adres = clickedDoctor.Adres,
                Description = clickedDoctor.Description,
                NrOfAvailableSeats = clickedDoctor.NrOfAvailableSeats,
                IdMedic = clickedDoctor.IdMedic,
                IdUser = clickedDoctor.IdUser
            };

            infoDoctor.Controls.Clear();
            infoDoctor.Controls.Add(doctorInfo);

            doctorInfo.editClicked += Doctor_ButtonEditClicked;
            doctorInfo.deleteClicked += Doctor_ButtonDeleteClicked;
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
                       c.stare
                FROM Cereri c
                JOIN Pacienti p ON c.id_pacient = p.id_pacient
                JOIN Users u_pacient ON p.id_user = u_pacient.id_user
                JOIN Medici m ON c.id_medic = m.id_medic
                JOIN Users u_medic ON m.id_user = u_medic.id_user";

                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Request request = new Request
                                {
                                    Patient = reader["pacient"].ToString(),
                                    Doctor = reader["medic"].ToString(),
                                    DateOfRequest = Convert.ToDateTime(reader["data_cerere"]).ToShortDateString(),
                                    Status = reader["stare"].ToString(),
                                    IdCerere = reader.GetInt32(reader.GetOrdinal("id_cerere"))
                                };
                                request.StatusColor();

                                if (request.Status == "respinsa")
                                {
                                    flowLayoutPanel3.Controls.Add(request);
                                }
                                else
                                {
                                    flowLayoutPanel4.Controls.Add(request);
                                }

                                request.editClicked += Request_ButtonEditClicked;
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

        //Edit Cereri
        private async void Request_ButtonEditClicked(object sender, EventArgs e)
        {
            Request request = sender as Request;
            if (request != null)
            {
                string displayStatus = request.GetStatusText();
                string newStatus = string.Empty;

                if (displayStatus != "?" && displayStatus != "acceptat" && displayStatus != "respins")
                {
                    MessageBox.Show("Status invalid. Vă rugăm să introduceți un status valid: '?', 'Acceptat', 'Respins'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (displayStatus == "?")
                {
                    newStatus = "in_asteptare";
                }
                else if (displayStatus == "acceptat")
                {
                    newStatus = "acceptata";
                }
                else if (displayStatus == "respins")
                {
                    newStatus = "respinsa";
                }

                string updateQuery = "UPDATE Cereri SET stare = :stare WHERE id_cerere = :id_cerere";

                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    try
                    {
                        await con.OpenAsync();
                        using (OracleTransaction transaction = con.BeginTransaction())
                        {
                            try
                            {
                                using (OracleCommand cmd = new OracleCommand(updateQuery, con))
                                {
                                    cmd.Transaction = transaction;
                                    cmd.CommandTimeout = 3; // Timeout in seconds
                                    cmd.Parameters.Add(new OracleParameter("stare", newStatus));
                                    cmd.Parameters.Add(new OracleParameter("id_cerere", request.IdCerere));

                                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                                    Console.WriteLine($"Rows affected: {rowsAffected}");
                                }
                                transaction.Commit();
                                MessageBox.Show("Starea cererii a fost actualizată cu succes!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Reîmprospătarea interfeței
                                getRequests();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                MessageBox.Show($"A apărut o eroare la actualizarea stării: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Console.WriteLine($"Transaction error: {ex.Message}");
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        MessageBox.Show($"A apărut o eroare la conectarea la baza de date: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine($"Oracle error: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"A apărut o eroare generală: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine($"General error: {ex.Message}");
                    }
                }
            }
        }

        //Delete Cereri
        private void Request_ButtonDeleteClicked(object sender, EventArgs e)
        {
            Request request = sender as Request;
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


        //Edit Pacienti

        private void Patient_ButtonEditClicked(object sender, EventArgs e)
        {
            if (infoPatient.Controls[0] is InfoPatient patientInfo)
            {
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        string updateQuery = @"
                            UPDATE Users SET nume = :nume, prenume = :prenume, numar_telefon = :numar_telefon, email = :email, 
                                             username = :username, password = :password, data_nasterii = :data_nasterii 
                                             WHERE id_user = :id_user";

                        using (OracleCommand cmd = new OracleCommand(updateQuery, con))
                        {
                            cmd.Parameters.Add(new OracleParameter("nume", patientInfo.FullName.Split(' ')[0]));
                            cmd.Parameters.Add(new OracleParameter("prenume", patientInfo.FullName.Split(' ')[1]));
                            cmd.Parameters.Add(new OracleParameter("numar_telefon", patientInfo.Phone));
                            cmd.Parameters.Add(new OracleParameter("email", patientInfo.Email));
                            cmd.Parameters.Add(new OracleParameter("username", patientInfo.UserName));
                            cmd.Parameters.Add(new OracleParameter("password", patientInfo.Password));
                            cmd.Parameters.Add(new OracleParameter("data_nasterii", Convert.ToDateTime(patientInfo.DateOfBirth)));
                            cmd.Parameters.Add(new OracleParameter("id_user", patientInfo.IdUser));

                            cmd.ExecuteNonQuery();
                        }

                        string updatePatientQuery = @"UPDATE Pacienti SET descriere = :descriere WHERE id_pacient = :id_pacient";
                        using (OracleCommand cmd = new OracleCommand(updatePatientQuery, con))
                        {
                            cmd.Parameters.Add(new OracleParameter("descriere", patientInfo.Description));
                            cmd.Parameters.Add(new OracleParameter("id_pacient", patientInfo.IdPatient));
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Pacientul a fost actualizat cu succes!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        getPatients();
                        infoPatient.Controls.Clear();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("A apărut o eroare: " + exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        //Delete Pacient
        private void Patient_ButtonDeleteClicked(object sender, EventArgs e)
        {
            if (infoPatient.Controls[0] is InfoPatient patientInfo)
            {
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        // Ștergere cereri asociate pacientului
                        string deleteRequestsQuery = "DELETE FROM Cereri WHERE id_pacient = :id_pacient";
                        using (OracleCommand cmd = new OracleCommand(deleteRequestsQuery, con))
                        {
                            cmd.Parameters.Add(new OracleParameter("id_pacient", patientInfo.IdPatient));
                            cmd.ExecuteNonQuery();
                        }

                        // Ștergere pacient din tabelul Pacienti
                        string deletePatientQuery = "DELETE FROM Pacienti WHERE id_pacient = :id_pacient";
                        using (OracleCommand cmd = new OracleCommand(deletePatientQuery, con))
                        {
                            cmd.Parameters.Add(new OracleParameter("id_pacient", patientInfo.IdPatient));
                            cmd.ExecuteNonQuery();
                        }

                        // Ștergere utilizator asociat pacientului din tabelul Users
                        string deleteUserQuery = "DELETE FROM Users WHERE id_user = :id_user";
                        using (OracleCommand cmd = new OracleCommand(deleteUserQuery, con))
                        {
                            cmd.Parameters.Add(new OracleParameter("id_user", patientInfo.IdUser));
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Pacientul a fost șters cu succes!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        infoPatient.Controls.Clear();
                        getPatients(); // Refresh patients list
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("A apărut o eroare: " + exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //Edit Doctor
        private void Doctor_ButtonEditClicked(object sender, EventArgs e)
        {
            if (infoDoctor.Controls[0] is InfoDoctor doctorInfo)
            {
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        string updateQuery = @"
                UPDATE Users SET nume = :nume, prenume = :prenume, numar_telefon = :numar_telefon, email = :email, 
                                 username = :username, password = :password, data_nasterii = :data_nasterii 
                WHERE id_user = :id_user";

                        using (OracleCommand cmd = new OracleCommand(updateQuery, con))
                        {
                            cmd.Parameters.Add(new OracleParameter("nume", doctorInfo.FullName.Split(' ')[0]));
                            cmd.Parameters.Add(new OracleParameter("prenume", doctorInfo.FullName.Split(' ')[1]));
                            cmd.Parameters.Add(new OracleParameter("numar_telefon", doctorInfo.Phone));
                            cmd.Parameters.Add(new OracleParameter("email", doctorInfo.Email));
                            cmd.Parameters.Add(new OracleParameter("username", doctorInfo.UserName));
                            cmd.Parameters.Add(new OracleParameter("password", doctorInfo.Password));
                            cmd.Parameters.Add(new OracleParameter("data_nasterii", Convert.ToDateTime(doctorInfo.DateOfBirth)));
                            cmd.Parameters.Add(new OracleParameter("id_user", doctorInfo.IdUser)); // Assuming you have UserID in InfoDoctor

                            cmd.ExecuteNonQuery();
                        }

                        string updateDoctorQuery = @"UPDATE Medici SET descriere = :descriere, adresa_cabinet = :adresa_cabinet, nr_Locuri_Disponibile = :nr_Locuri_Disponibile WHERE id_medic = :id_medic";
                        using (OracleCommand cmd = new OracleCommand(updateDoctorQuery, con))
                        {
                            cmd.Parameters.Add(new OracleParameter("descriere", doctorInfo.Description));
                            cmd.Parameters.Add(new OracleParameter("adresa_cabinet", doctorInfo.Adres));
                            cmd.Parameters.Add(new OracleParameter("nr_Locuri_Disponibile", doctorInfo.NrOfAvailableSeats));
                            cmd.Parameters.Add(new OracleParameter("id_medic", doctorInfo.IdMedic));
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Medicul a fost actualizat cu succes!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        infoDoctor.Controls.Clear();
                        getDoctors();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("A apărut o eroare: " + exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        //Delete Doctor

        private void Doctor_ButtonDeleteClicked(object sender, EventArgs e)
        {
            if (infoDoctor.Controls[0] is InfoDoctor doctorInfo)
            {
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        // Ștergere cereri asociate medicului
                        string deleteRequestsQuery = "DELETE FROM Cereri WHERE id_medic = :id_medic";
                        using (OracleCommand cmd = new OracleCommand(deleteRequestsQuery, con))
                        {
                            cmd.Parameters.Add(new OracleParameter("id_medic", doctorInfo.IdMedic));
                            cmd.ExecuteNonQuery();
                        }

                        // Ștergere medicului din tabelul medici
                        string deletePatientQuery = "DELETE FROM Medici WHERE id_medic = :id_medic";
                        using (OracleCommand cmd = new OracleCommand(deletePatientQuery, con))
                        {
                            cmd.Parameters.Add(new OracleParameter("id_medic", doctorInfo.IdMedic));
                            cmd.ExecuteNonQuery();
                        }

                        // Ștergere utilizator asociat medicului din tabelul Users
                        string deleteUserQuery = "DELETE FROM Users WHERE id_user = :id_user";
                        using (OracleCommand cmd = new OracleCommand(deleteUserQuery, con))
                        {
                            cmd.Parameters.Add(new OracleParameter("id_user", doctorInfo.IdUser));
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Medic a fost șters cu succes!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        infoDoctor.Controls.Clear();
                        getDoctors();
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
