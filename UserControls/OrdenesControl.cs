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
            dgvOrdenesTrabajo.ReadOnly = true;
            dgvOrdenesTrabajo.AllowUserToAddRows = false;
            dgvOrdenesTrabajo.AllowUserToDeleteRows = false;
            dgvOrdenesTrabajo.AllowUserToResizeRows = false;
            CargarOrdenes();
        }

        private void CargarOrdenes()
        {
            OrdenTrabajoService servicio = new OrdenTrabajoService();

           //dgvOrdenesTrabajo.DataSource = servicio.ObtenerOrdenesTrabajo();

            List<OrdenTrabajoDto> ordenes = servicio.ObtenerOrdenesTrabajo();

            dgvOrdenesTrabajo.DataSource = ordenes;



            labelActivas.Text = ordenes.Count(o =>
       o.Estado != "Entregado" &&
       o.Estado != "Dado de baja").ToString();

            labelModulos.Text = ordenes.Count(o =>
                o.Categoria == "Módulo").ToString();

            labelCerrajeria.Text = ordenes.Count(o =>
                o.Categoria == "Cerrajería").ToString();

            labelInstalaciones.Text = ordenes.Count(o =>
                o.Categoria == "Instalaciones").ToString();

            labelAlertaStock.Text = "0";





            //-----------


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










        // son estos
        //labelActivas.Text
        //labelAlertasStock.Text
        //labelIntalaciones.Text
        //labelCerrajeria.Text
        //labelModulos.Text










        //eliminar luego
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
