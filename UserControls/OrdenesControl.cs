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
using sistemaDeGestionAutomotriz.Models;

namespace sistemaDeGestionAutomotriz.UserControls
{
    public partial class OrdenesControl : UserControl
    {
        public OrdenesControl()
        {
            InitializeComponent();
        }

        private void OrdenesControl_Load(object sender, EventArgs e)
        {
            CargarOrdenes();
        }

        private void CargarOrdenes()
        {
            OrdenTrabajoService servicio = new OrdenTrabajoService();

            dgvOrdenesTrabajo.DataSource = servicio.ObtenerOrdenesTrabajo();


            //forma para renderizar lo que quiera ocultar columnas

            dgvOrdenesTrabajo.Columns["Detalle"].Visible = false;
            dgvOrdenesTrabajo.Columns["Diagnostico"].Visible = false;
            dgvOrdenesTrabajo.Columns["Telefono"].Visible = false;
            dgvOrdenesTrabajo.Columns["Precio"].Visible = false;
            dgvOrdenesTrabajo.Columns["Garantia"].Visible = false;
        }





        //esto para que se pueda vel el panel de abajo
        private void dgvOrdenesTrabajo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvOrdenesTrabajo.Rows[e.RowIndex].DataBoundItem is OrdenTrabajoDto orden)
            {
                labelCliente.Text = orden.Cliente;
                labelNumeroOrdenTipo.Text = $"{orden.NumeroOrden}-{orden.TipoModulo}";
                labelDni.Text = orden.Dni;
                labelDetalle.Text = orden.Detalle;
                labelAsignado.Text = orden.TecnicoAsignado;
                labelEstado.Text = orden.Estado;

                labelTelefono.Text = orden.Telefono;
                labelDiagnostico.Text = orden.Diagnostico;
                labelCotizacion.Text = orden.Precio.ToString("C");

                // Cuando agregues el DNI:
                // labelDni.Text = orden.Dni;
            }
        }























        //eliminar luego
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
