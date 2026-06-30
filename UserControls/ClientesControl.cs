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
    public partial class ClientesControl : UserControl
    {
        private ClienteService clienteService = new ClienteService();

        public ClientesControl()
        {
            InitializeComponent();
         
            this.Load += ClientesControl_Load;
        }

        private void ClientesControl_Load(object sender, EventArgs e)
        {

            dgvListaClientes.ReadOnly = true;
            dgvListaClientes.AllowUserToAddRows = false;
            dgvListaClientes.AllowUserToDeleteRows = false;
            dgvListaClientes.AllowUserToResizeRows = false;
            CargarClientes();
        }

        private void CargarClientes()
        {
            try
            {
                dgvListaClientes.AutoGenerateColumns = true;
                dgvListaClientes.DataSource = null;
                dgvListaClientes.DataSource = clienteService.ObtenerClientesActivos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los clientes: " + ex.Message);
            }
        }



        //labelActivo.Text = _cliente.Activo? "Activo" : "Inactivo";



        //EVENTO DEL BOTON AGREGAR CLIENTE
        private void buttonNuevoCliente_Click(object sender, EventArgs e)
        {
            FormNuevoCliente formNuevoCliente = new FormNuevoCliente();

            formNuevoCliente.FormClosed += (s, args) =>
            {
                CargarClientes();
            };

            formNuevoCliente.Show();
        }





        //DGV LLAMAR VENTANA MODIFICAR

        // EVENTO DE DOBLE CLIC EN UNA CELDA DE LA TABLA
        private void dgvListaClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificamos que el doble clic sea en una fila real y no en los títulos de arriba (los encabezados)
            if (e.RowIndex >= 0)
            {
                // 1. Obtenemos la fila en la que el usuario hizo doble clic
                DataGridViewRow filaSeleccionada = dgvListaClientes.Rows[e.RowIndex];

                // 2. Extraemos el ID del cliente (reemplaza "ClientId" o el índice por el nombre real de tu columna de ID)
               // int clienteId = Convert.ToInt32(filaSeleccionada.Cells["ClienteId"].Value);

                Cliente cliente = (Cliente)filaSeleccionada.DataBoundItem;

                FormEditarCliente formEditar = new FormEditarCliente(cliente);

              

                // 4. Cuando se cierre la ventana de edición, actualizamos la lista automáticamente
                formEditar.FormClosed += (s, args) =>
                {
                    CargarClientes();
                };

                // 5. Lo mostramos como ventana emergente (ShowDialog bloquea la de atrás para evitar clics extra)
                formEditar.ShowDialog();
            }
        }


    }
}