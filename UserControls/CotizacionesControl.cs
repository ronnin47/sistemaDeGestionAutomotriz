using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sistemaDeGestionAutomotriz.Services;
using sistemaDeGestionAutomotriz.Forms;
using sistemaDeGestionAutomotriz.Models;

namespace sistemaDeGestionAutomotriz.UserControls
{
    public partial class CotizacionesControl : UserControl
    {

        private CotizacionService cotizacionService = new CotizacionService();

        public CotizacionesControl()
        {
            InitializeComponent();
            this.Load += CotizacionesControl_Load;
        }




       
      

        private void CotizacionesControl_Load(object sender, EventArgs e)
        {

            dgvListaCotizaciones.ReadOnly = true;
            dgvListaCotizaciones.AllowUserToAddRows = false;
            dgvListaCotizaciones.AllowUserToDeleteRows = false;
            dgvListaCotizaciones.AllowUserToResizeRows = false;
            CargarCotizaciones();
        }

        private void CargarCotizaciones()
        {
            try
            {
                dgvListaCotizaciones.AutoGenerateColumns = true;
                dgvListaCotizaciones.DataSource = null;
                dgvListaCotizaciones.DataSource = cotizacionService.ObtenerClientesPendientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar lista de cotizaciones: " + ex.Message);
            }
        }



        //labelActivo.Text = _cliente.Activo? "Activo" : "Inactivo";



        //EVENTO DEL BOTON AGREGAR Cotizaciones
        private void buttonNuevoCliente_Click(object sender, EventArgs e)
        {
            FormNuevoCliente formNuevoCliente = new FormNuevoCliente();

            formNuevoCliente.FormClosed += (s, args) =>
            {
                CargarCotizaciones();
            };

            formNuevoCliente.Show();
        }




    }
}
