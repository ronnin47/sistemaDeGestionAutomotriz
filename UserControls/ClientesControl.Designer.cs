
namespace sistemaDeGestionAutomotriz.UserControls
{
    partial class ClientesControl
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
            this.dgvListaClientes = new System.Windows.Forms.DataGridView();
            this.buttonNuevoCliente = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaClientes)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(242, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pantalla Clientes";
            // 
            // dgvListaClientes
            // 
            this.dgvListaClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaClientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvListaClientes.Location = new System.Drawing.Point(0, 0);
            this.dgvListaClientes.Name = "dgvListaClientes";
            this.dgvListaClientes.Size = new System.Drawing.Size(942, 267);
            this.dgvListaClientes.TabIndex = 1;
            // 
            // buttonNuevoCliente
            // 
            this.buttonNuevoCliente.Location = new System.Drawing.Point(83, 83);
            this.buttonNuevoCliente.Name = "buttonNuevoCliente";
            this.buttonNuevoCliente.Size = new System.Drawing.Size(102, 23);
            this.buttonNuevoCliente.TabIndex = 2;
            this.buttonNuevoCliente.Text = "+ Nuevo Cliente";
            this.buttonNuevoCliente.UseVisualStyleBackColor = true;
            this.buttonNuevoCliente.Click += new System.EventHandler(this.buttonNuevoCliente_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvListaClientes);
            this.panel1.Location = new System.Drawing.Point(18, 162);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(942, 267);
            this.panel1.TabIndex = 3;
            // 
            // ClientesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonNuevoCliente);
            this.Controls.Add(this.label1);
            this.Name = "ClientesControl";
            this.Size = new System.Drawing.Size(987, 501);
            this.Load += new System.EventHandler(this.ClientesControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaClientes)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvListaClientes;
        private System.Windows.Forms.Button buttonNuevoCliente;
        private System.Windows.Forms.Panel panel1;
    }
}
