
namespace sistemaDeGestionAutomotriz.Forms
{
    partial class FormNuevaOrden
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonModulo = new System.Windows.Forms.Button();
            this.buttonCerrajeria = new System.Windows.Forms.Button();
            this.buttonInstalacion = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tipo de Servicio";
            // 
            // buttonModulo
            // 
            this.buttonModulo.Location = new System.Drawing.Point(78, 23);
            this.buttonModulo.Name = "buttonModulo";
            this.buttonModulo.Size = new System.Drawing.Size(75, 23);
            this.buttonModulo.TabIndex = 1;
            this.buttonModulo.Text = "Módulo";
            this.buttonModulo.UseVisualStyleBackColor = true;
            this.buttonModulo.Click += new System.EventHandler(this.buttonModulo_Click);
            // 
            // buttonCerrajeria
            // 
            this.buttonCerrajeria.Location = new System.Drawing.Point(278, 23);
            this.buttonCerrajeria.Name = "buttonCerrajeria";
            this.buttonCerrajeria.Size = new System.Drawing.Size(75, 23);
            this.buttonCerrajeria.TabIndex = 2;
            this.buttonCerrajeria.Text = "Cerrajería";
            this.buttonCerrajeria.UseVisualStyleBackColor = true;
            this.buttonCerrajeria.Click += new System.EventHandler(this.buttonCerrajeria_Click);
            // 
            // buttonInstalacion
            // 
            this.buttonInstalacion.Location = new System.Drawing.Point(507, 23);
            this.buttonInstalacion.Name = "buttonInstalacion";
            this.buttonInstalacion.Size = new System.Drawing.Size(75, 23);
            this.buttonInstalacion.TabIndex = 3;
            this.buttonInstalacion.Text = "Instalación";
            this.buttonInstalacion.UseVisualStyleBackColor = true;
            this.buttonInstalacion.Click += new System.EventHandler(this.buttonInstalacion_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonModulo);
            this.panel1.Controls.Add(this.buttonInstalacion);
            this.panel1.Controls.Add(this.buttonCerrajeria);
            this.panel1.Location = new System.Drawing.Point(51, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(689, 62);
            this.panel1.TabIndex = 4;
            // 
            // panelContenido
            // 
            this.panelContenido.Location = new System.Drawing.Point(51, 93);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Size = new System.Drawing.Size(983, 447);
            this.panelContenido.TabIndex = 5;
            // 
            // FormNuevaOrden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 538);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "FormNuevaOrden";
            this.Text = "FormNuevaOrden";
            this.Load += new System.EventHandler(this.FormNuevaOrden_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonModulo;
        private System.Windows.Forms.Button buttonCerrajeria;
        private System.Windows.Forms.Button buttonInstalacion;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelContenido;
    }
}