
namespace sistemaDeGestionAutomotriz.UserControls
{
    partial class GarantiasControl
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
            this.dgvListaGarantias = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaGarantias)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(286, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Garantias";
            // 
            // dgvListaGarantias
            // 
            this.dgvListaGarantias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaGarantias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvListaGarantias.Location = new System.Drawing.Point(0, 0);
            this.dgvListaGarantias.Name = "dgvListaGarantias";
            this.dgvListaGarantias.Size = new System.Drawing.Size(579, 424);
            this.dgvListaGarantias.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvListaGarantias);
            this.panel1.Location = new System.Drawing.Point(22, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(579, 424);
            this.panel1.TabIndex = 2;
            // 
            // GarantiasControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "GarantiasControl";
            this.Size = new System.Drawing.Size(668, 514);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaGarantias)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvListaGarantias;
        private System.Windows.Forms.Panel panel1;
    }
}
