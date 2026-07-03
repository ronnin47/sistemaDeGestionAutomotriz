
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
            this.labelG = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvListaGarantias = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaGarantias)).BeginInit();
            this.SuspendLayout();
            // 
            // labelG
            // 
            this.labelG.Location = new System.Drawing.Point(53, 35);
            this.labelG.Name = "labelG";
            this.labelG.Size = new System.Drawing.Size(100, 23);
            this.labelG.TabIndex = 0;
            this.labelG.Text = "Garantias";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvListaGarantias);
            this.panel1.Location = new System.Drawing.Point(22, 86);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(731, 319);
            this.panel1.TabIndex = 1;
            // 
            // dgvListaGarantias
            // 
            this.dgvListaGarantias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaGarantias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvListaGarantias.Location = new System.Drawing.Point(0, 0);
            this.dgvListaGarantias.Name = "dgvListaGarantias";
            this.dgvListaGarantias.Size = new System.Drawing.Size(731, 319);
            this.dgvListaGarantias.TabIndex = 0;
            // 
            // GarantiasControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelG);
            this.Name = "GarantiasControl";
            this.Size = new System.Drawing.Size(771, 514);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaGarantias)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelG;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvListaGarantias;
    }
}
