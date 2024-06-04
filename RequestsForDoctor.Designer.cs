namespace Search_The_GP
{
    partial class RequestsForDoctor
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
            this.btnReject = new System.Windows.Forms.Button();
            this.status = new System.Windows.Forms.Label();
            this.dateOfRequest = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPatient = new System.Windows.Forms.Label();
            this.fnPatient = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnInfo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnReject
            // 
            this.btnReject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnReject.FlatAppearance.BorderSize = 0;
            this.btnReject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReject.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(232)))), ((int)(((byte)(238)))));
            this.btnReject.Location = new System.Drawing.Point(546, 120);
            this.btnReject.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(68, 24);
            this.btnReject.TabIndex = 93;
            this.btnReject.Text = "Reject";
            this.btnReject.UseVisualStyleBackColor = false;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(232)))), ((int)(((byte)(238)))));
            this.status.Location = new System.Drawing.Point(460, 9);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(76, 22);
            this.status.TabIndex = 92;
            this.status.Text = "Pending";
            this.status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dateOfRequest
            // 
            this.dateOfRequest.AutoSize = true;
            this.dateOfRequest.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateOfRequest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(232)))), ((int)(((byte)(238)))));
            this.dateOfRequest.Location = new System.Drawing.Point(196, 39);
            this.dateOfRequest.Name = "dateOfRequest";
            this.dateOfRequest.Size = new System.Drawing.Size(102, 22);
            this.dateOfRequest.TabIndex = 91;
            this.dateOfRequest.Text = "07-06-2024";
            this.dateOfRequest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.label1.Location = new System.Drawing.Point(6, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 25);
            this.label1.TabIndex = 90;
            this.label1.Text = "Date of request";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPatient
            // 
            this.lblPatient.AutoSize = true;
            this.lblPatient.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.lblPatient.Location = new System.Drawing.Point(6, 6);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(79, 25);
            this.lblPatient.TabIndex = 89;
            this.lblPatient.Text = "Patient";
            this.lblPatient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fnPatient
            // 
            this.fnPatient.AutoSize = true;
            this.fnPatient.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fnPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(232)))), ((int)(((byte)(238)))));
            this.fnPatient.Location = new System.Drawing.Point(196, 9);
            this.fnPatient.Name = "fnPatient";
            this.fnPatient.Size = new System.Drawing.Size(152, 22);
            this.fnPatient.TabIndex = 87;
            this.fnPatient.Text = "Full Name Patient";
            this.fnPatient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnAccept
            // 
            this.btnAccept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(155)))), ((int)(((byte)(121)))));
            this.btnAccept.FlatAppearance.BorderSize = 0;
            this.btnAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccept.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(232)))), ((int)(((byte)(238)))));
            this.btnAccept.Location = new System.Drawing.Point(466, 120);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(68, 24);
            this.btnAccept.TabIndex = 94;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.btnInfo.FlatAppearance.BorderSize = 0;
            this.btnInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInfo.ForeColor = System.Drawing.Color.White;
            this.btnInfo.Location = new System.Drawing.Point(10, 120);
            this.btnInfo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(139, 24);
            this.btnInfo.TabIndex = 95;
            this.btnInfo.Text = "Information about patient";
            this.btnInfo.UseVisualStyleBackColor = false;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // RequestsForDoctor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(132)))), ((int)(((byte)(226)))));
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnReject);
            this.Controls.Add(this.status);
            this.Controls.Add(this.dateOfRequest);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblPatient);
            this.Controls.Add(this.fnPatient);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "RequestsForDoctor";
            this.Size = new System.Drawing.Size(620, 150);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Label dateOfRequest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.Label fnPatient;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnInfo;
    }
}
