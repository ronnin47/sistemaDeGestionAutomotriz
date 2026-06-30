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
        }




        private void dgvOrdenesTrabajo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Validar que se haya hecho clic en una fila válida y no en el encabezado
            if (e.RowIndex >= 0)
            {
                // Obtener la fila seleccionada actual
                DataGridViewRow fila = dgvOrdenesTrabajo.Rows[e.RowIndex];

                // Mapear los datos de las celdas a tus Labels o TextBox de abajo
                labelCliente.Text = fila.Cells["Cliente"].Value?.ToString();
                labelDetalle.Text = fila.Cells["Detalle"].Value?.ToString();
                labelAsignado.Text = fila.Cells["Asignado"].Value?.ToString();
                labelEstado.Text = fila.Cells["Estado"].Value?.ToString();

                // Para el DNI y Teléfono, si no están en el DataGridView, 
                // deberás pasárselos desde el Objeto original si usas una Lista como DataSource:
                if (fila.DataBoundItem is OrdenTrabajoDto ordenSeleccionada)
                {
                    // Nota: Agrega Dni y Telefono a tu DTO si la query los incluye más adelante
                    labelDni.Text = "Falta mapear DNI";
                    labelTelefono.Text = "Falta mapear Teléfono";
                    labelDiagnostico.Text = "Falta diagnostico";
                    labelCotizacion.Text = "Falta cotización";
                }
            }
        }























        //eliminar luego
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
