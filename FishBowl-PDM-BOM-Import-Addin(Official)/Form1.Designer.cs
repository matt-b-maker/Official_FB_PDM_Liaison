
namespace FishBowl_PDM_BOM_Import_Addin_Official_
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.PRODBox = new System.Windows.Forms.TextBox();
            this.DescriptionBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.IsLoggedIntoPDMCheck = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.HasAssAndCNCCheck = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CheckedInCheck = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.NamedCorrectlyCheck = new System.Windows.Forms.CheckBox();
            this.RunImport = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.ProgBar = new System.Windows.Forms.ProgressBar();
            this.FBUNlabel = new System.Windows.Forms.Label();
            this.FBPasswordLabel = new System.Windows.Forms.Label();
            this.FBUsername = new System.Windows.Forms.TextBox();
            this.FBPassword = new System.Windows.Forms.TextBox();
            this.FBPort = new System.Windows.Forms.TextBox();
            this.FBServer = new System.Windows.Forms.TextBox();
            this.FBPortLabel = new System.Windows.Forms.Label();
            this.FBServerLabel = new System.Windows.Forms.Label();
            this.UpdateWindow = new System.Windows.Forms.TextBox();
            this.BOMItemsWindow = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(29, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "PROD-";
            // 
            // PRODBox
            // 
            this.PRODBox.Location = new System.Drawing.Point(85, 42);
            this.PRODBox.MaxLength = 4;
            this.PRODBox.Name = "PRODBox";
            this.PRODBox.PlaceholderText = "####";
            this.PRODBox.Size = new System.Drawing.Size(73, 23);
            this.PRODBox.TabIndex = 1;
            this.PRODBox.Text = "0317";
            // 
            // DescriptionBox
            // 
            this.DescriptionBox.Location = new System.Drawing.Point(284, 42);
            this.DescriptionBox.Name = "DescriptionBox";
            this.DescriptionBox.PlaceholderText = "Write detailed description of PROD BOM here ";
            this.DescriptionBox.Size = new System.Drawing.Size(459, 23);
            this.DescriptionBox.TabIndex = 3;
            this.DescriptionBox.Text = "Lucky Putt Hex Challenge";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(191, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description:";
            // 
            // IsLoggedIntoPDMCheck
            // 
            this.IsLoggedIntoPDMCheck.AutoSize = true;
            this.IsLoggedIntoPDMCheck.Location = new System.Drawing.Point(298, 93);
            this.IsLoggedIntoPDMCheck.Name = "IsLoggedIntoPDMCheck";
            this.IsLoggedIntoPDMCheck.Size = new System.Drawing.Size(15, 14);
            this.IsLoggedIntoPDMCheck.TabIndex = 4;
            this.IsLoggedIntoPDMCheck.UseVisualStyleBackColor = true;
            this.IsLoggedIntoPDMCheck.CheckedChanged += new System.EventHandler(this.IsLoggedIntoPDMCheck_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(248, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Are you logged into the Creative Works PDM?";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(263, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Does the PROD have an assembly and CNC files?";
            // 
            // HasAssAndCNCCheck
            // 
            this.HasAssAndCNCCheck.AutoSize = true;
            this.HasAssAndCNCCheck.Location = new System.Drawing.Point(298, 124);
            this.HasAssAndCNCCheck.Name = "HasAssAndCNCCheck";
            this.HasAssAndCNCCheck.Size = new System.Drawing.Size(15, 14);
            this.HasAssAndCNCCheck.TabIndex = 6;
            this.HasAssAndCNCCheck.UseVisualStyleBackColor = true;
            this.HasAssAndCNCCheck.CheckedChanged += new System.EventHandler(this.HasAssAndCNCCheck_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(91, 186);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(201, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "Is the entire PROD folder checked in?";
            // 
            // CheckedInCheck
            // 
            this.CheckedInCheck.AutoSize = true;
            this.CheckedInCheck.Location = new System.Drawing.Point(298, 187);
            this.CheckedInCheck.Name = "CheckedInCheck";
            this.CheckedInCheck.Size = new System.Drawing.Size(15, 14);
            this.CheckedInCheck.TabIndex = 10;
            this.CheckedInCheck.UseVisualStyleBackColor = true;
            this.CheckedInCheck.CheckedChanged += new System.EventHandler(this.CheckedInCheck_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(279, 15);
            this.label6.TabIndex = 9;
            this.label6.Text = "Are all hardware and components named correctly?";
            // 
            // NamedCorrectlyCheck
            // 
            this.NamedCorrectlyCheck.AutoSize = true;
            this.NamedCorrectlyCheck.Location = new System.Drawing.Point(298, 156);
            this.NamedCorrectlyCheck.Name = "NamedCorrectlyCheck";
            this.NamedCorrectlyCheck.Size = new System.Drawing.Size(15, 14);
            this.NamedCorrectlyCheck.TabIndex = 8;
            this.NamedCorrectlyCheck.UseVisualStyleBackColor = true;
            this.NamedCorrectlyCheck.CheckedChanged += new System.EventHandler(this.NamedCorrectlyCheck_CheckedChanged);
            // 
            // RunImport
            // 
            this.RunImport.Enabled = false;
            this.RunImport.Location = new System.Drawing.Point(156, 381);
            this.RunImport.Name = "RunImport";
            this.RunImport.Size = new System.Drawing.Size(75, 23);
            this.RunImport.TabIndex = 12;
            this.RunImport.Text = "Run Import";
            this.RunImport.UseVisualStyleBackColor = true;
            this.RunImport.Click += new System.EventHandler(this.RunImport_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(185, 351);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // ProgBar
            // 
            this.ProgBar.Location = new System.Drawing.Point(405, 88);
            this.ProgBar.Maximum = 300;
            this.ProgBar.Name = "ProgBar";
            this.ProgBar.Size = new System.Drawing.Size(270, 14);
            this.ProgBar.Step = 1;
            this.ProgBar.TabIndex = 15;
            // 
            // FBUNlabel
            // 
            this.FBUNlabel.AutoSize = true;
            this.FBUNlabel.Location = new System.Drawing.Point(69, 223);
            this.FBUNlabel.Name = "FBUNlabel";
            this.FBUNlabel.Size = new System.Drawing.Size(110, 15);
            this.FBUNlabel.TabIndex = 16;
            this.FBUNlabel.Text = "FishBowl Username";
            // 
            // FBPasswordLabel
            // 
            this.FBPasswordLabel.AutoSize = true;
            this.FBPasswordLabel.Location = new System.Drawing.Point(72, 253);
            this.FBPasswordLabel.Name = "FBPasswordLabel";
            this.FBPasswordLabel.Size = new System.Drawing.Size(107, 15);
            this.FBPasswordLabel.TabIndex = 17;
            this.FBPasswordLabel.Text = "FishBowl Password";
            // 
            // FBUsername
            // 
            this.FBUsername.Location = new System.Drawing.Point(185, 220);
            this.FBUsername.Name = "FBUsername";
            this.FBUsername.Size = new System.Drawing.Size(100, 23);
            this.FBUsername.TabIndex = 18;
            this.FBUsername.Text = "MattB";
            // 
            // FBPassword
            // 
            this.FBPassword.Location = new System.Drawing.Point(185, 250);
            this.FBPassword.Name = "FBPassword";
            this.FBPassword.PasswordChar = '*';
            this.FBPassword.Size = new System.Drawing.Size(100, 23);
            this.FBPassword.TabIndex = 19;
            this.FBPassword.Text = "MattB123";
            // 
            // FBPort
            // 
            this.FBPort.Location = new System.Drawing.Point(185, 309);
            this.FBPort.Name = "FBPort";
            this.FBPort.ReadOnly = true;
            this.FBPort.Size = new System.Drawing.Size(100, 23);
            this.FBPort.TabIndex = 23;
            this.FBPort.Text = "28192";
            // 
            // FBServer
            // 
            this.FBServer.Location = new System.Drawing.Point(185, 279);
            this.FBServer.Name = "FBServer";
            this.FBServer.ReadOnly = true;
            this.FBServer.Size = new System.Drawing.Size(100, 23);
            this.FBServer.TabIndex = 22;
            this.FBServer.Text = "192.168.2.241";
            // 
            // FBPortLabel
            // 
            this.FBPortLabel.AutoSize = true;
            this.FBPortLabel.Location = new System.Drawing.Point(100, 312);
            this.FBPortLabel.Name = "FBPortLabel";
            this.FBPortLabel.Size = new System.Drawing.Size(79, 15);
            this.FBPortLabel.TabIndex = 21;
            this.FBPortLabel.Text = "FishBowl Port";
            // 
            // FBServerLabel
            // 
            this.FBServerLabel.AutoSize = true;
            this.FBServerLabel.Location = new System.Drawing.Point(90, 282);
            this.FBServerLabel.Name = "FBServerLabel";
            this.FBServerLabel.Size = new System.Drawing.Size(89, 15);
            this.FBServerLabel.TabIndex = 20;
            this.FBServerLabel.Text = "FishBowl Server";
            // 
            // UpdateWindow
            // 
            this.UpdateWindow.Location = new System.Drawing.Point(343, 139);
            this.UpdateWindow.MaximumSize = new System.Drawing.Size(330, 330);
            this.UpdateWindow.MinimumSize = new System.Drawing.Size(150, 280);
            this.UpdateWindow.Multiline = true;
            this.UpdateWindow.Name = "UpdateWindow";
            this.UpdateWindow.ReadOnly = true;
            this.UpdateWindow.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.UpdateWindow.Size = new System.Drawing.Size(222, 299);
            this.UpdateWindow.TabIndex = 24;
            // 
            // BOMItemsWindow
            // 
            this.BOMItemsWindow.Location = new System.Drawing.Point(571, 139);
            this.BOMItemsWindow.MaximumSize = new System.Drawing.Size(330, 330);
            this.BOMItemsWindow.MinimumSize = new System.Drawing.Size(150, 280);
            this.BOMItemsWindow.Multiline = true;
            this.BOMItemsWindow.Name = "BOMItemsWindow";
            this.BOMItemsWindow.ReadOnly = true;
            this.BOMItemsWindow.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.BOMItemsWindow.Size = new System.Drawing.Size(217, 299);
            this.BOMItemsWindow.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(409, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 15);
            this.label7.TabIndex = 26;
            this.label7.Text = "Update Window";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(588, 118);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(149, 15);
            this.label8.TabIndex = 27;
            this.label8.Text = "Items to be added to BOM:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.BOMItemsWindow);
            this.Controls.Add(this.UpdateWindow);
            this.Controls.Add(this.FBPort);
            this.Controls.Add(this.FBServer);
            this.Controls.Add(this.FBPortLabel);
            this.Controls.Add(this.FBServerLabel);
            this.Controls.Add(this.FBPassword);
            this.Controls.Add(this.FBUsername);
            this.Controls.Add(this.FBPasswordLabel);
            this.Controls.Add(this.FBUNlabel);
            this.Controls.Add(this.ProgBar);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.RunImport);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CheckedInCheck);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.NamedCorrectlyCheck);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.HasAssAndCNCCheck);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.IsLoggedIntoPDMCheck);
            this.Controls.Add(this.DescriptionBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PRODBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "FB BOM Import";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PRODBox;
        private System.Windows.Forms.TextBox DescriptionBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox IsLoggedIntoPDMCheck;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox HasAssAndCNCCheck;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox CheckedInCheck;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox NamedCorrectlyCheck;
        private System.Windows.Forms.Button RunImport;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ProgressBar ProgBar;
        private System.Windows.Forms.Label FBUNlabel;
        private System.Windows.Forms.Label FBPasswordLabel;
        private System.Windows.Forms.TextBox FBUsername;
        private System.Windows.Forms.TextBox FBPassword;
        private System.Windows.Forms.TextBox FBPort;
        private System.Windows.Forms.TextBox FBServer;
        private System.Windows.Forms.Label FBPortLabel;
        private System.Windows.Forms.Label FBServerLabel;
        private System.Windows.Forms.TextBox UpdateWindow;
        private System.Windows.Forms.TextBox BOMItemsWindow;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}

