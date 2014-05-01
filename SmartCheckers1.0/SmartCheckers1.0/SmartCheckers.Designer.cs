namespace SmartCheckers1._0
{
    partial class SmartCheckers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SmartCheckers));
            this.colorLbl = new System.Windows.Forms.Label();
            this.difLbl = new System.Windows.Forms.Label();
            this.blackRB = new System.Windows.Forms.RadioButton();
            this.whiteRB = new System.Windows.Forms.RadioButton();
            this.difCmb = new System.Windows.Forms.ComboBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // colorLbl
            // 
            this.colorLbl.AutoSize = true;
            this.colorLbl.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorLbl.Location = new System.Drawing.Point(13, 26);
            this.colorLbl.Name = "colorLbl";
            this.colorLbl.Size = new System.Drawing.Size(151, 30);
            this.colorLbl.TabIndex = 0;
            this.colorLbl.Text = "Choose a color";
            // 
            // difLbl
            // 
            this.difLbl.AutoSize = true;
            this.difLbl.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.difLbl.Location = new System.Drawing.Point(13, 121);
            this.difLbl.Name = "difLbl";
            this.difLbl.Size = new System.Drawing.Size(96, 30);
            this.difLbl.TabIndex = 1;
            this.difLbl.Text = "Difficulty";
            // 
            // blackRB
            // 
            this.blackRB.AutoSize = true;
            this.blackRB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.blackRB.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blackRB.Location = new System.Drawing.Point(61, 69);
            this.blackRB.Name = "blackRB";
            this.blackRB.Size = new System.Drawing.Size(73, 29);
            this.blackRB.TabIndex = 2;
            this.blackRB.TabStop = true;
            this.blackRB.Text = "Black";
            this.blackRB.UseVisualStyleBackColor = true;
            // 
            // whiteRB
            // 
            this.whiteRB.AutoSize = true;
            this.whiteRB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.whiteRB.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.whiteRB.Location = new System.Drawing.Point(156, 69);
            this.whiteRB.Name = "whiteRB";
            this.whiteRB.Size = new System.Drawing.Size(79, 29);
            this.whiteRB.TabIndex = 3;
            this.whiteRB.TabStop = true;
            this.whiteRB.Text = "White";
            this.whiteRB.UseVisualStyleBackColor = true;
            // 
            // difCmb
            // 
            this.difCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.difCmb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.difCmb.FormattingEnabled = true;
            this.difCmb.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.difCmb.Location = new System.Drawing.Point(128, 129);
            this.difCmb.Name = "difCmb";
            this.difCmb.Size = new System.Drawing.Size(54, 21);
            this.difCmb.TabIndex = 4;
            // 
            // startBtn
            // 
            this.startBtn.BackColor = System.Drawing.Color.DarkGreen;
            this.startBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.startBtn.Location = new System.Drawing.Point(194, 182);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(97, 27);
            this.startBtn.TabIndex = 5;
            this.startBtn.Text = "START";
            this.startBtn.UseVisualStyleBackColor = false;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // SmartCheckers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 221);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.difCmb);
            this.Controls.Add(this.whiteRB);
            this.Controls.Add(this.blackRB);
            this.Controls.Add(this.difLbl);
            this.Controls.Add(this.colorLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SmartCheckers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welcome to Smart Checkers 1.0";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SmartCheckers_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label colorLbl;
        private System.Windows.Forms.Label difLbl;
        private System.Windows.Forms.RadioButton blackRB;
        private System.Windows.Forms.RadioButton whiteRB;
        private System.Windows.Forms.ComboBox difCmb;
        private System.Windows.Forms.Button startBtn;
    }
}