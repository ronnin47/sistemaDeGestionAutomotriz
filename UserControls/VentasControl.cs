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

                // Forma para renderizar lo que quiera ocultar columnas
                dgvListaVentas.Columns["idCliente"].Visible = false;
                dgvListaVentas.Columns["idKit"].Visible = false;
              
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



        
        // EVENTO DE DOBLE CLIC EN UNA CELDA DE LA TABLA
        private void dgvListaVentas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificamos que el doble clic sea en una fila real y no en los títulos de arriba (los encabezados)
            if (e.RowIndex >= 0)
            {
                // 1. Obtenemos la fila en la que el usuario hizo doble clic
                DataGridViewRow filaSeleccionada = dgvListaVentas.Rows[e.RowIndex];

                // 2. PODEMOS CONSEGUIR EL ID DE LA VENTA
                // int clienteId = Convert.ToInt32(filaSeleccionada.Cells["ClienteId"].Value);


                VentaInsumosDto venta = (VentaInsumosDto)filaSeleccionada.DataBoundItem;

                FormEditarVenta formEditar = new FormEditarVenta(venta);



                // 4. Cuando se cierre la ventana de edición, actualizamos la lista automáticamente
                formEditar.FormClosed += (s, args) =>
                {
                    CargarVentas();
                };

                // 5. Lo mostramos como ventana emergente (ShowDialog bloquea la de atrás para evitar clics extra)
                formEditar.ShowDialog();
            }
        }

       

    }
}
