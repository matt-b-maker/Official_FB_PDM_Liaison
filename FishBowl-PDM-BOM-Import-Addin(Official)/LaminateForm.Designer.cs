
namespace FishBowl_PDM_BOM_Import_Addin_Official_
{
    partial class LaminateForm
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
            this.LaminateValidateButton = new System.Windows.Forms.Button();
            this.LamBox1 = new System.Windows.Forms.ComboBox();
            this.ValBox1 = new System.Windows.Forms.NumericUpDown();
            this.AddLam = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ValBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // LaminateValidateButton
            // 
            this.LaminateValidateButton.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.LaminateValidateButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.LaminateValidateButton.Location = new System.Drawing.Point(160, 77);
            this.LaminateValidateButton.Name = "LaminateValidateButton";
            this.LaminateValidateButton.Size = new System.Drawing.Size(75, 23);
            this.LaminateValidateButton.TabIndex = 1;
            this.LaminateValidateButton.Text = "Submit";
            this.LaminateValidateButton.UseVisualStyleBackColor = true;
            this.LaminateValidateButton.Click += new System.EventHandler(this.LaminateValidateButton_Click);
            // 
            // LamBox1
            // 
            this.LamBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LamBox1.FormattingEnabled = true;
            this.LamBox1.Items.AddRange(new object[] {
            "Xanadu",
            "Walnut Heights",
            "Violine",
            "Tutti Frutti",
            "Pewter",
            "Pearl Wave",
            "Passionfruit",
            "Oiled Soapstone",
            "Midnight",
            "Lemon Lime",
            "Honey Plantain",
            "Hollyberry",
            "Hibiscus Tea",
            "Hibiscus",
            "Classic Marbleized",
            "Carribean Blue",
            "Capri",
            "Brighton Walnut",
            "Barrel Herringbone"});
            this.LamBox1.Location = new System.Drawing.Point(26, 27);
            this.LamBox1.Name = "LamBox1";
            this.LamBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LamBox1.Size = new System.Drawing.Size(144, 23);
            this.LamBox1.TabIndex = 2;
            // 
            // ValBox1
            // 
            this.ValBox1.Location = new System.Drawing.Point(190, 27);
            this.ValBox1.Name = "ValBox1";
            this.ValBox1.Size = new System.Drawing.Size(45, 23);
            this.ValBox1.TabIndex = 4;
            // 
            // AddLam
            // 
            this.AddLam.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AddLam.Location = new System.Drawing.Point(55, 77);
            this.AddLam.Name = "AddLam";
            this.AddLam.Size = new System.Drawing.Size(75, 23);
            this.AddLam.TabIndex = 5;
            this.AddLam.Text = "Add Laminate";
            this.AddLam.UseVisualStyleBackColor = true;
            this.AddLam.Click += new System.EventHandler(this.AddLam_Click);
            // 
            // LaminateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 418);
            this.Controls.Add(this.AddLam);
            this.Controls.Add(this.ValBox1);
            this.Controls.Add(this.LamBox1);
            this.Controls.Add(this.LaminateValidateButton);
            this.Name = "LaminateForm";
            this.Text = "LaminateForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LaminateForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.ValBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button LaminateValidateButton;
        private System.Windows.Forms.ComboBox LamBox1;
        private System.Windows.Forms.NumericUpDown ValBox1;
        private System.Windows.Forms.Button AddLam;
    }
}