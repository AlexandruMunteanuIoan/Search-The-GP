namespace Search_The_GP
{
    partial class DoctorList
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DoctorList));
            this.read = new System.Windows.Forms.Button();
            this.description = new System.Windows.Forms.Label();
            this.email = new System.Windows.Forms.Label();
            this.phone = new System.Windows.Forms.Label();
            this.fullname = new System.Windows.Forms.Label();
            this.adres = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // read
            // 
            this.read.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(155)))), ((int)(((byte)(121)))));
            this.read.FlatAppearance.BorderSize = 0;
            this.read.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.read.ForeColor = System.Drawing.Color.White;
            this.read.Location = new System.Drawing.Point(532, 112);
            this.read.Name = "read";
            this.read.Size = new System.Drawing.Size(75, 25);
            this.read.TabIndex = 10;
            this.read.Text = "Read More";
            this.read.UseVisualStyleBackColor = false;
            this.read.Click += new System.EventHandler(this.read_Click);
            // 
            // description
            // 
            this.description.ForeColor = System.Drawing.Color.White;
            this.description.Location = new System.Drawing.Point(292, 47);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(315, 60);
            this.description.TabIndex = 9;
            this.description.Text = resources.GetString("description.Text");
            // 
            // email
            // 
            this.email.AutoSize = true;
            this.email.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.email.ForeColor = System.Drawing.Color.White;
            this.email.Location = new System.Drawing.Point(13, 61);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(128, 15);
            this.email.TabIndex = 8;
            this.email.Text = "example.@gmail.com";
            // 
            // phone
            // 
            this.phone.AutoSize = true;
            this.phone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phone.ForeColor = System.Drawing.Color.White;
            this.phone.Location = new System.Drawing.Point(13, 77);
            this.phone.Name = "phone";
            this.phone.Size = new System.Drawing.Size(94, 15);
            this.phone.TabIndex = 7;
            this.phone.Text = "+40 712345678";
            // 
            // fullname
            // 
            this.fullname.AutoSize = true;
            this.fullname.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fullname.ForeColor = System.Drawing.Color.White;
            this.fullname.Location = new System.Drawing.Point(13, 12);
            this.fullname.Name = "fullname";
            this.fullname.Size = new System.Drawing.Size(109, 25);
            this.fullname.TabIndex = 6;
            this.fullname.Text = "Full Name";
            this.fullname.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // adres
            // 
            this.adres.AutoSize = true;
            this.adres.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adres.ForeColor = System.Drawing.Color.White;
            this.adres.Location = new System.Drawing.Point(13, 45);
            this.adres.Name = "adres";
            this.adres.Size = new System.Drawing.Size(176, 15);
            this.adres.TabIndex = 11;
            this.adres.Text = "Suceava, str. Universitatii nr. 13";
            // 
            // DoctorList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(132)))), ((int)(((byte)(226)))));
            this.Controls.Add(this.adres);
            this.Controls.Add(this.read);
            this.Controls.Add(this.description);
            this.Controls.Add(this.email);
            this.Controls.Add(this.phone);
            this.Controls.Add(this.fullname);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "DoctorList";
            this.Size = new System.Drawing.Size(620, 150);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button read;
        private System.Windows.Forms.Label description;
        private System.Windows.Forms.Label email;
        private System.Windows.Forms.Label phone;
        private System.Windows.Forms.Label fullname;
        private System.Windows.Forms.Label adres;
    }
}
