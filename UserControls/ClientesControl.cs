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
            CargarClientes();
        }

        private void CargarClientes()
        {
            try
            {
                dgvListaClientes.AutoGenerateColumns = true;
                dgvListaClientes.DataSource = null;
                dgvListaClientes.DataSource = clienteService.ObtenerClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los clientes: " + ex.Message);
            }
        }





        /*
            private void buttonNuevoCliente_Click(object sender, EventArgs e)
            {
                FormNuevoCliente formNuevoCliente = new FormNuevoCliente();
                formNuevoCliente.Show();
            }
        */

        private void buttonNuevoCliente_Click(object sender, EventArgs e)
        {
            FormNuevoCliente formNuevoCliente = new FormNuevoCliente();

            formNuevoCliente.FormClosed += (s, args) =>
            {
                CargarClientes();
            };

            formNuevoCliente.Show();
        }

    }
}