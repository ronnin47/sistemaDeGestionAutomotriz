
namespace sistemaDeGestionAutomotriz.UserControls
{
    partial class OrdenesControl
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
            this.buttonNuevaOrden = new System.Windows.Forms.Button();
            this.comboBoxFilterTipo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxBuscador = new System.Windows.Forms.TextBox();
            this.labelActivas = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelModulos = new System.Windows.Forms.Label();
            this.labelCerrajeria = new System.Windows.Forms.Label();
            this.labelInstalaciones = new System.Windows.Forms.Label();
            this.labelAlertaStock = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvOrdenesTrabajo = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelNumeroOrdenTipo = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.labelCotizacion = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.labelEstado = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.labelDiagnostico = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.labelDetalle = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.labelAsignado = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.labelTelefono = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelDni = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.labelCliente = new System.Windows.Forms.Label();
            this.labelD = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenesTrabajo)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(19, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Órdenes de Trabajo";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // buttonNuevaOrden
            // 
            this.buttonNuevaOrden.Location = new System.Drawing.Point(41, 57);
            this.buttonNuevaOrden.Name = "buttonNuevaOrden";
            this.buttonNuevaOrden.Size = new System.Drawing.Size(88, 23);
            this.buttonNuevaOrden.TabIndex = 1;
            this.buttonNuevaOrden.Text = "+ Nueva Orden";
            this.buttonNuevaOrden.UseVisualStyleBackColor = true;
            this.buttonNuevaOrden.Click += new System.EventHandler(this.buttonNuevaOrden_Click);
            // 
            // comboBoxFilterTipo
            // 
            this.comboBoxFilterTipo.FormattingEnabled = true;
            this.comboBoxFilterTipo.Items.AddRange(new object[] {
            "Todos",
            "Módulo",
            "Cerrajería",
            "Instalaciones"});
            this.comboBoxFilterTipo.Location = new System.Drawing.Point(198, 59);
            this.comboBoxFilterTipo.Name = "comboBoxFilterTipo";
            this.comboBoxFilterTipo.Size = new System.Drawing.Size(121, 21);
            this.comboBoxFilterTipo.TabIndex = 2;
            this.comboBoxFilterTipo.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterTipo_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(164, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tipo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(350, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Buscar";
            // 
            // textBoxBuscador
            // 
            this.textBoxBuscador.Location = new System.Drawing.Point(397, 59);
            this.textBoxBuscador.Name = "textBoxBuscador";
            this.textBoxBuscador.Size = new System.Drawing.Size(118, 20);
            this.textBoxBuscador.TabIndex = 5;
            // 
            // labelActivas
            // 
            this.labelActivas.AutoSize = true;
            this.labelActivas.Location = new System.Drawing.Point(49, 136);
            this.labelActivas.Name = "labelActivas";
            this.labelActivas.Size = new System.Drawing.Size(16, 13);
            this.labelActivas.TabIndex = 6;
            this.labelActivas.Text = "...";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(127, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Módulos";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(214, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Cerrajería";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(313, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Instalaciones";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(430, 109);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Alertas Stock";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Activas";
            // 
            // labelModulos
            // 
            this.labelModulos.AutoSize = true;
            this.labelModulos.Location = new System.Drawing.Point(130, 135);
            this.labelModulos.Name = "labelModulos";
            this.labelModulos.Size = new System.Drawing.Size(16, 13);
            this.labelModulos.TabIndex = 12;
            this.labelModulos.Text = "...";
            // 
            // labelCerrajeria
            // 
            this.labelCerrajeria.AutoSize = true;
            this.labelCerrajeria.Location = new System.Drawing.Point(217, 136);
            this.labelCerrajeria.Name = "labelCerrajeria";
            this.labelCerrajeria.Size = new System.Drawing.Size(16, 13);
            this.labelCerrajeria.TabIndex = 13;
            this.labelCerrajeria.Text = "...";
            // 
            // labelInstalaciones
            // 
            this.labelInstalaciones.AutoSize = true;
            this.labelInstalaciones.Location = new System.Drawing.Point(316, 136);
            this.labelInstalaciones.Name = "labelInstalaciones";
            this.labelInstalaciones.Size = new System.Drawing.Size(16, 13);
            this.labelInstalaciones.TabIndex = 14;
            this.labelInstalaciones.Text = "...";
            // 
            // labelAlertaStock
            // 
            this.labelAlertaStock.AutoSize = true;
            this.labelAlertaStock.Location = new System.Drawing.Point(433, 136);
            this.labelAlertaStock.Name = "labelAlertaStock";
            this.labelAlertaStock.Size = new System.Drawing.Size(16, 13);
            this.labelAlertaStock.TabIndex = 15;
            this.labelAlertaStock.Text = "...";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvOrdenesTrabajo);
            this.panel1.Location = new System.Drawing.Point(22, 168);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(780, 194);
            this.panel1.TabIndex = 16;
            // 
            // dgvOrdenesTrabajo
            // 
            this.dgvOrdenesTrabajo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenesTrabajo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrdenesTrabajo.Location = new System.Drawing.Point(0, 0);
            this.dgvOrdenesTrabajo.Name = "dgvOrdenesTrabajo";
            this.dgvOrdenesTrabajo.Size = new System.Drawing.Size(780, 194);
            this.dgvOrdenesTrabajo.TabIndex = 0;
            this.dgvOrdenesTrabajo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrdenesTrabajo_CellClick);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.labelNumeroOrdenTipo);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Controls.Add(this.labelCotizacion);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.labelEstado);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.labelDiagnostico);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.labelDetalle);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.labelAsignado);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.labelTelefono);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.labelDni);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.labelCliente);
            this.panel2.Controls.Add(this.labelD);
            this.panel2.Location = new System.Drawing.Point(22, 368);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(780, 173);
            this.panel2.TabIndex = 17;
            // 
            // labelNumeroOrdenTipo
            // 
            this.labelNumeroOrdenTipo.AutoSize = true;
            this.labelNumeroOrdenTipo.Location = new System.Drawing.Point(81, 4);
            this.labelNumeroOrdenTipo.Name = "labelNumeroOrdenTipo";
            this.labelNumeroOrdenTipo.Size = new System.Drawing.Size(16, 13);
            this.labelNumeroOrdenTipo.TabIndex = 17;
            this.labelNumeroOrdenTipo.Text = "...";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(24, 4);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(46, 13);
            this.label16.TabIndex = 16;
            this.label16.Text = "Detalle -";
            // 
            // labelCotizacion
            // 
            this.labelCotizacion.AutoSize = true;
            this.labelCotizacion.Location = new System.Drawing.Point(28, 138);
            this.labelCotizacion.Name = "labelCotizacion";
            this.labelCotizacion.Size = new System.Drawing.Size(16, 13);
            this.labelCotizacion.TabIndex = 15;
            this.labelCotizacion.Text = "...";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(21, 125);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 13);
            this.label15.TabIndex = 14;
            this.label15.Text = "Cotización";
            // 
            // labelEstado
            // 
            this.labelEstado.AutoSize = true;
            this.labelEstado.Location = new System.Drawing.Point(338, 100);
            this.labelEstado.Name = "labelEstado";
            this.labelEstado.Size = new System.Drawing.Size(16, 13);
            this.labelEstado.TabIndex = 13;
            this.labelEstado.Text = "...";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(334, 87);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(40, 13);
            this.label14.TabIndex = 12;
            this.label14.Text = "Estado";
            // 
            // labelDiagnostico
            // 
            this.labelDiagnostico.AutoSize = true;
            this.labelDiagnostico.Location = new System.Drawing.Point(196, 100);
            this.labelDiagnostico.Name = "labelDiagnostico";
            this.labelDiagnostico.Size = new System.Drawing.Size(16, 13);
            this.labelDiagnostico.TabIndex = 11;
            this.labelDiagnostico.Text = "...";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(196, 87);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(89, 13);
            this.label13.TabIndex = 10;
            this.label13.Text = "Req. Diagnostico";
            // 
            // labelDetalle
            // 
            this.labelDetalle.AutoSize = true;
            this.labelDetalle.Location = new System.Drawing.Point(22, 100);
            this.labelDetalle.Name = "labelDetalle";
            this.labelDetalle.Size = new System.Drawing.Size(16, 13);
            this.labelDetalle.TabIndex = 9;
            this.labelDetalle.Text = "...";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(22, 87);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Detalle";
            // 
            // labelAsignado
            // 
            this.labelAsignado.AutoSize = true;
            this.labelAsignado.Location = new System.Drawing.Point(334, 60);
            this.labelAsignado.Name = "labelAsignado";
            this.labelAsignado.Size = new System.Drawing.Size(16, 13);
            this.labelAsignado.TabIndex = 7;
            this.labelAsignado.Text = "...";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(323, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Asignado";
            // 
            // labelTelefono
            // 
            this.labelTelefono.AutoSize = true;
            this.labelTelefono.Location = new System.Drawing.Point(196, 60);
            this.labelTelefono.Name = "labelTelefono";
            this.labelTelefono.Size = new System.Drawing.Size(16, 13);
            this.labelTelefono.TabIndex = 5;
            this.labelTelefono.Text = "...";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(190, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Telefono";
            // 
            // labelDni
            // 
            this.labelDni.AutoSize = true;
            this.labelDni.Location = new System.Drawing.Point(103, 60);
            this.labelDni.Name = "labelDni";
            this.labelDni.Size = new System.Drawing.Size(16, 13);
            this.labelDni.TabIndex = 3;
            this.labelDni.Text = "...";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(99, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Dni";
            // 
            // labelCliente
            // 
            this.labelCliente.AutoSize = true;
            this.labelCliente.Location = new System.Drawing.Point(22, 60);
            this.labelCliente.Name = "labelCliente";
            this.labelCliente.Size = new System.Drawing.Size(16, 13);
            this.labelCliente.TabIndex = 1;
            this.labelCliente.Text = "...";
            // 
            // labelD
            // 
            this.labelD.AutoSize = true;
            this.labelD.Location = new System.Drawing.Point(21, 42);
            this.labelD.Name = "labelD";
            this.labelD.Size = new System.Drawing.Size(39, 13);
            this.labelD.TabIndex = 0;
            this.labelD.Text = "Cliente";
            // 
            // OrdenesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelAlertaStock);
            this.Controls.Add(this.labelInstalaciones);
            this.Controls.Add(this.labelCerrajeria);
            this.Controls.Add(this.labelModulos);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelActivas);
            this.Controls.Add(this.textBoxBuscador);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxFilterTipo);
            this.Controls.Add(this.buttonNuevaOrden);
            this.Controls.Add(this.label1);
            this.Name = "OrdenesControl";
            this.Size = new System.Drawing.Size(825, 582);
            this.Load += new System.EventHandler(this.OrdenesControl_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenesTrabajo)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonNuevaOrden;
        private System.Windows.Forms.ComboBox comboBoxFilterTipo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxBuscador;
        private System.Windows.Forms.Label labelActivas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelModulos;
        private System.Windows.Forms.Label labelCerrajeria;
        private System.Windows.Forms.Label labelInstalaciones;
        private System.Windows.Forms.Label labelAlertaStock;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvOrdenesTrabajo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelDni;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelCliente;
        private System.Windows.Forms.Label labelD;
        private System.Windows.Forms.Label labelDetalle;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label labelAsignado;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label labelTelefono;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelCotizacion;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label labelEstado;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label labelDiagnostico;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labelNumeroOrdenTipo;
        private System.Windows.Forms.Label label16;
    }
}
