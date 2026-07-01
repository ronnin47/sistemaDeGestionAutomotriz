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
    public partial class VentasControl : UserControl
    {

        private VentaService ventaService = new VentaService();

        public VentasControl()
        {
            InitializeComponent();
            this.Load += CotizacionesControl_Load;
        }







        private void CotizacionesControl_Load(object sender, EventArgs e)
        {

            dgvListaVentas.ReadOnly = true;
            dgvListaVentas.AllowUserToAddRows = false;
            dgvListaVentas.AllowUserToDeleteRows = false;
            dgvListaVentas.AllowUserToResizeRows = false;
            CargarVentas();
        }

        private void CargarVentas()
        {
            try
            {
                dgvListaVentas.AutoGenerateColumns = true;
                dgvListaVentas.DataSource = null;
                dgvListaVentas.DataSource = ventaService.ObtenerVentaInsumos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar lista de ventas: " + ex.Message);
            }
        }



        //labelActivo.Text = _cliente.Activo? "Activo" : "Inactivo";



        //EVENTO DEL BOTON AGREGAR nueva venta

        private void buttonNuevaVenta_Click(object sender, EventArgs e)
        {
            FormNuevaVenta formNuevaVenta = new FormNuevaVenta();

            formNuevaVenta.FormClosed += (s, args) =>
            {
                CargarVentas();
            };

            formNuevaVenta.Show();
        }






    }
}
