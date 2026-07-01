
namespace sistemaDeGestionAutomotriz.UserControls
{
    partial class VentasControl
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
            this.buttonNuevaVenta = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvListaVentas = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaVentas)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Venta directa de Insumos";
            // 
            // buttonNuevaVenta
            // 
            this.buttonNuevaVenta.Location = new System.Drawing.Point(31, 116);
            this.buttonNuevaVenta.Name = "buttonNuevaVenta";
            this.buttonNuevaVenta.Size = new System.Drawing.Size(97, 23);
            this.buttonNuevaVenta.TabIndex = 1;
            this.buttonNuevaVenta.Text = "+ Nueva Venta";
            this.buttonNuevaVenta.UseVisualStyleBackColor = true;
            this.buttonNuevaVenta.Click += new System.EventHandler(this.buttonNuevaVenta_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvListaVentas);
            this.panel1.Location = new System.Drawing.Point(14, 213);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(712, 220);
            this.panel1.TabIndex = 2;
            // 
            // dgvListaVentas
            // 
            this.dgvListaVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaVentas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvListaVentas.Location = new System.Drawing.Point(0, 0);
            this.dgvListaVentas.Name = "dgvListaVentas";
            this.dgvListaVentas.Size = new System.Drawing.Size(712, 220);
            this.dgvListaVentas.TabIndex = 0;
            // 
            // VentasControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonNuevaVenta);
            this.Controls.Add(this.label1);
            this.Name = "VentasControl";
            this.Size = new System.Drawing.Size(749, 472);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaVentas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonNuevaVenta;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvListaVentas;
    }
}
