namespace slxJobServiceWhisperer
{
    partial class frmJobs
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Jobs = new System.Windows.Forms.Label();
            this.lstJobs = new System.Windows.Forms.ListBox();
            this.lstTriggers = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lstExecutions = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtURI = new System.Windows.Forms.TextBox();
            this.lblURI = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.rtbDetails = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRunMe = new System.Windows.Forms.Button();
            this.btnResult = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Jobs
            // 
            this.Jobs.AutoSize = true;
            this.Jobs.Location = new System.Drawing.Point(12, 30);
            this.Jobs.Name = "Jobs";
            this.Jobs.Size = new System.Drawing.Size(32, 13);
            this.Jobs.TabIndex = 0;
            this.Jobs.Text = "Jobs:";
            // 
            // lstJobs
            // 
            this.lstJobs.FormattingEnabled = true;
            this.lstJobs.Location = new System.Drawing.Point(12, 46);
            this.lstJobs.Name = "lstJobs";
            this.lstJobs.Size = new System.Drawing.Size(243, 251);
            this.lstJobs.TabIndex = 1;
            this.lstJobs.SelectedIndexChanged += new System.EventHandler(this.lstJobs_SelectedIndexChanged);
            // 
            // lstTriggers
            // 
            this.lstTriggers.FormattingEnabled = true;
            this.lstTriggers.Location = new System.Drawing.Point(260, 46);
            this.lstTriggers.Name = "lstTriggers";
            this.lstTriggers.Size = new System.Drawing.Size(207, 251);
            this.lstTriggers.TabIndex = 3;
            this.lstTriggers.SelectedIndexChanged += new System.EventHandler(this.lstTriggers_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(258, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Triggers:";
            // 
            // lstExecutions
            // 
            this.lstExecutions.FormattingEnabled = true;
            this.lstExecutions.Location = new System.Drawing.Point(473, 46);
            this.lstExecutions.Name = "lstExecutions";
            this.lstExecutions.Size = new System.Drawing.Size(264, 251);
            this.lstExecutions.TabIndex = 5;
            this.lstExecutions.SelectedIndexChanged += new System.EventHandler(this.lstExecutions_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(492, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Executions:";
            // 
            // txtURI
            // 
            this.txtURI.Location = new System.Drawing.Point(12, 535);
            this.txtURI.Name = "txtURI";
            this.txtURI.ReadOnly = true;
            this.txtURI.Size = new System.Drawing.Size(725, 20);
            this.txtURI.TabIndex = 6;
            // 
            // lblURI
            // 
            this.lblURI.AutoSize = true;
            this.lblURI.Location = new System.Drawing.Point(12, 519);
            this.lblURI.Name = "lblURI";
            this.lblURI.Size = new System.Drawing.Size(109, 13);
            this.lblURI.TabIndex = 7;
            this.lblURI.Text = "URI Being Submitted:";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(12, 4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(109, 23);
            this.btnReset.TabIndex = 8;
            this.btnReset.Text = "Reset to formload";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // rtbDetails
            // 
            this.rtbDetails.Location = new System.Drawing.Point(12, 351);
            this.rtbDetails.Name = "rtbDetails";
            this.rtbDetails.Size = new System.Drawing.Size(725, 161);
            this.rtbDetails.TabIndex = 9;
            this.rtbDetails.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 335);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Details of whatever you selected:";
            // 
            // btnRunMe
            // 
            this.btnRunMe.Enabled = false;
            this.btnRunMe.Location = new System.Drawing.Point(12, 303);
            this.btnRunMe.Name = "btnRunMe";
            this.btnRunMe.Size = new System.Drawing.Size(75, 23);
            this.btnRunMe.TabIndex = 11;
            this.btnRunMe.Text = "RunMe";
            this.btnRunMe.UseVisualStyleBackColor = true;
            this.btnRunMe.Click += new System.EventHandler(this.btnRunMe_Click);
            // 
            // btnResult
            // 
            this.btnResult.Location = new System.Drawing.Point(495, 303);
            this.btnResult.Name = "btnResult";
            this.btnResult.Size = new System.Drawing.Size(75, 23);
            this.btnResult.TabIndex = 12;
            this.btnResult.Text = "Show Result";
            this.btnResult.UseVisualStyleBackColor = true;
            this.btnResult.Click += new System.EventHandler(this.btnResult_Click);
            // 
            // frmJobs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 558);
            this.Controls.Add(this.btnResult);
            this.Controls.Add(this.btnRunMe);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rtbDetails);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblURI);
            this.Controls.Add(this.txtURI);
            this.Controls.Add(this.lstExecutions);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstTriggers);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstJobs);
            this.Controls.Add(this.Jobs);
            this.Name = "frmJobs";
            this.Text = "All your Jobs are belong to us";
            this.Load += new System.EventHandler(this.frmJobs_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Jobs;
        private System.Windows.Forms.ListBox lstJobs;
        private System.Windows.Forms.ListBox lstTriggers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstExecutions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtURI;
        private System.Windows.Forms.Label lblURI;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.RichTextBox rtbDetails;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRunMe;
        private System.Windows.Forms.Button btnResult;
    }
}

