
namespace sistemaDeGestionAutomotriz.UserControls
{
    partial class CotizacionesControl
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvListaCotizaciones = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaCotizaciones)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(47, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cotizaciones Pendientes";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvListaCotizaciones);
            this.panel1.Location = new System.Drawing.Point(20, 67);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(709, 300);
            this.panel1.TabIndex = 1;
            // 
            // dgvListaCotizaciones
            // 
            this.dgvListaCotizaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaCotizaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvListaCotizaciones.Location = new System.Drawing.Point(0, 0);
            this.dgvListaCotizaciones.Name = "dgvListaCotizaciones";
            this.dgvListaCotizaciones.Size = new System.Drawing.Size(709, 300);
            this.dgvListaCotizaciones.TabIndex = 0;
            // 
            // CotizacionesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "CotizacionesControl";
            this.Size = new System.Drawing.Size(749, 510);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaCotizaciones)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvListaCotizaciones;
    }
}
