
namespace sistemaDeGestionAutomotriz
{
    partial class FormPrincipal
    {
      
        private System.ComponentModel.IContainer components = null;

      













        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }










        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            this.buttonLogOut = new System.Windows.Forms.Button();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.buttonOrdenes = new System.Windows.Forms.Button();
            this.buttonVentas = new System.Windows.Forms.Button();
            this.buttonClientes = new System.Windows.Forms.Button();
            this.buttonCotizaciones = new System.Windows.Forms.Button();
            this.buttonGarantias = new System.Windows.Forms.Button();
            this.panelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonLogOut
            // 
            this.buttonLogOut.BackColor = System.Drawing.Color.Red;
            this.buttonLogOut.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonLogOut.Location = new System.Drawing.Point(54, 339);
            this.buttonLogOut.Name = "buttonLogOut";
            this.buttonLogOut.Size = new System.Drawing.Size(87, 23);
            this.buttonLogOut.TabIndex = 0;
            this.buttonLogOut.Text = "Cerrar Sesion";
            this.buttonLogOut.UseVisualStyleBackColor = false;
            this.buttonLogOut.Click += new System.EventHandler(this.buttonLogOut_Click);
            // 
            // panelMenu
            // 
            this.panelMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMenu.Controls.Add(this.buttonGarantias);
            this.panelMenu.Controls.Add(this.buttonCotizaciones);
            this.panelMenu.Controls.Add(this.buttonClientes);
            this.panelMenu.Controls.Add(this.buttonVentas);
            this.panelMenu.Controls.Add(this.buttonOrdenes);
            this.panelMenu.Controls.Add(this.buttonLogOut);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(176, 450);
            this.panelMenu.TabIndex = 1;
            // 
            // panelContenido
            // 
            this.panelContenido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(176, 0);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Size = new System.Drawing.Size(624, 450);
            this.panelContenido.TabIndex = 2;
            // 
            // buttonOrdenes
            // 
            this.buttonOrdenes.Location = new System.Drawing.Point(54, 61);
            this.buttonOrdenes.Name = "buttonOrdenes";
            this.buttonOrdenes.Size = new System.Drawing.Size(75, 23);
            this.buttonOrdenes.TabIndex = 1;
            this.buttonOrdenes.Text = "Órdenes";
            this.buttonOrdenes.UseVisualStyleBackColor = true;
            this.buttonOrdenes.Click += new System.EventHandler(this.buttonOrdenes_Click);
            // 
            // buttonVentas
            // 
            this.buttonVentas.Location = new System.Drawing.Point(54, 110);
            this.buttonVentas.Name = "buttonVentas";
            this.buttonVentas.Size = new System.Drawing.Size(75, 23);
            this.buttonVentas.TabIndex = 2;
            this.buttonVentas.Text = "Ventas";
            this.buttonVentas.UseVisualStyleBackColor = true;
            // 
            // buttonClientes
            // 
            this.buttonClientes.Location = new System.Drawing.Point(54, 161);
            this.buttonClientes.Name = "buttonClientes";
            this.buttonClientes.Size = new System.Drawing.Size(75, 23);
            this.buttonClientes.TabIndex = 3;
            this.buttonClientes.Text = "Clientes";
            this.buttonClientes.UseVisualStyleBackColor = true;
            // 
            // buttonCotizaciones
            // 
            this.buttonCotizaciones.Location = new System.Drawing.Point(54, 209);
            this.buttonCotizaciones.Name = "buttonCotizaciones";
            this.buttonCotizaciones.Size = new System.Drawing.Size(75, 23);
            this.buttonCotizaciones.TabIndex = 4;
            this.buttonCotizaciones.Text = "Cotizaciones";
            this.buttonCotizaciones.UseVisualStyleBackColor = true;
            // 
            // buttonGarantias
            // 
            this.buttonGarantias.Location = new System.Drawing.Point(54, 260);
            this.buttonGarantias.Name = "buttonGarantias";
            this.buttonGarantias.Size = new System.Drawing.Size(75, 23);
            this.buttonGarantias.TabIndex = 5;
            this.buttonGarantias.Text = "Garantías";
            this.buttonGarantias.UseVisualStyleBackColor = true;
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.panelMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPrincipal";
            this.Text = "Gestion Pro";
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.panelMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonLogOut;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelContenido;
        private System.Windows.Forms.Button buttonGarantias;
        private System.Windows.Forms.Button buttonCotizaciones;
        private System.Windows.Forms.Button buttonClientes;
        private System.Windows.Forms.Button buttonVentas;
        private System.Windows.Forms.Button buttonOrdenes;
    }
}

