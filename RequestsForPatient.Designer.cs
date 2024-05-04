namespace Search_The_GP
{
    partial class RequestsForPatient
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
            this.status = new System.Windows.Forms.Label();
            this.dateOfRequest = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDoctor = new System.Windows.Forms.Label();
            this.fnDoctor = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(232)))), ((int)(((byte)(238)))));
            this.status.Location = new System.Drawing.Point(693, 16);
            this.status.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(76, 22);
            this.status.TabIndex = 83;
            this.status.Text = "Pending";
            this.status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dateOfRequest
            // 
            this.dateOfRequest.AutoSize = true;
            this.dateOfRequest.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateOfRequest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(232)))), ((int)(((byte)(238)))));
            this.dateOfRequest.Location = new System.Drawing.Point(267, 53);
            this.dateOfRequest.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.dateOfRequest.Name = "dateOfRequest";
            this.dateOfRequest.Size = new System.Drawing.Size(102, 22);
            this.dateOfRequest.TabIndex = 82;
            this.dateOfRequest.Text = "07-06-2024";
            this.dateOfRequest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.label1.Location = new System.Drawing.Point(13, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 25);
            this.label1.TabIndex = 81;
            this.label1.Text = "Date of request";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDoctor
            // 
            this.lblDoctor.AutoSize = true;
            this.lblDoctor.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDoctor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.lblDoctor.Location = new System.Drawing.Point(13, 12);
            this.lblDoctor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new System.Drawing.Size(144, 25);
            this.lblDoctor.TabIndex = 79;
            this.lblDoctor.Text = "Family Doctor";
            this.lblDoctor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fnDoctor
            // 
            this.fnDoctor.AutoSize = true;
            this.fnDoctor.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fnDoctor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(232)))), ((int)(((byte)(238)))));
            this.fnDoctor.Location = new System.Drawing.Point(267, 16);
            this.fnDoctor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.fnDoctor.Name = "fnDoctor";
            this.fnDoctor.Size = new System.Drawing.Size(149, 22);
            this.fnDoctor.TabIndex = 77;
            this.fnDoctor.Text = "Full Name Doctor";
            this.fnDoctor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(232)))), ((int)(((byte)(238)))));
            this.btnDelete.Location = new System.Drawing.Point(733, 153);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(91, 30);
            this.btnDelete.TabIndex = 85;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // RequestsForPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(132)))), ((int)(((byte)(226)))));
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.status);
            this.Controls.Add(this.dateOfRequest);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDoctor);
            this.Controls.Add(this.fnDoctor);
            this.Name = "RequestsForPatient";
            this.Size = new System.Drawing.Size(827, 185);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Label dateOfRequest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDoctor;
        private System.Windows.Forms.Label fnDoctor;
        private System.Windows.Forms.Button btnDelete;
    }
}
