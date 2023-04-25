namespace Projekt3_Shulga58686
{
    partial class FormularzGlowny
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
            this.osbtnPrzejścieDoLaboratorium = new System.Windows.Forms.Button();
            this.osbtnPrzejścieDoProjektu = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // osbtnPrzejścieDoLaboratorium
            // 
            this.osbtnPrzejścieDoLaboratorium.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.osbtnPrzejścieDoLaboratorium.Location = new System.Drawing.Point(103, 216);
            this.osbtnPrzejścieDoLaboratorium.Name = "osbtnPrzejścieDoLaboratorium";
            this.osbtnPrzejścieDoLaboratorium.Size = new System.Drawing.Size(299, 85);
            this.osbtnPrzejścieDoLaboratorium.TabIndex = 0;
            this.osbtnPrzejścieDoLaboratorium.Text = "LABORATORIUM\r\nWybrane bryły regularne";
            this.osbtnPrzejścieDoLaboratorium.UseVisualStyleBackColor = true;
            this.osbtnPrzejścieDoLaboratorium.Click += new System.EventHandler(this.button1_Click);
            // 
            // osbtnPrzejścieDoProjektu
            // 
            this.osbtnPrzejścieDoProjektu.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.osbtnPrzejścieDoProjektu.Location = new System.Drawing.Point(514, 216);
            this.osbtnPrzejścieDoProjektu.Name = "osbtnPrzejścieDoProjektu";
            this.osbtnPrzejścieDoProjektu.Size = new System.Drawing.Size(293, 85);
            this.osbtnPrzejścieDoProjektu.TabIndex = 1;
            this.osbtnPrzejścieDoProjektu.Text = "PROJEKT NR3\r\nWybrane bryły złożone";
            this.osbtnPrzejścieDoProjektu.UseVisualStyleBackColor = true;
            this.osbtnPrzejścieDoProjektu.Click += new System.EventHandler(this.osbtnPrzejścieDoProjektu_Click);
            // 
            // FormularzGłówny
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(930, 540);
            this.Controls.Add(this.osbtnPrzejścieDoProjektu);
            this.Controls.Add(this.osbtnPrzejścieDoLaboratorium);
            this.Name = "FormularzGłówny";
            this.Text = "Prezenter";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button osbtnPrzejścieDoLaboratorium;
        private System.Windows.Forms.Button osbtnPrzejścieDoProjektu;
    }
}