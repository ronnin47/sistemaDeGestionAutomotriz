using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sistemaDeGestionAutomotriz.Models;
using sistemaDeGestionAutomotriz.Services;

namespace sistemaDeGestionAutomotriz.Forms
{

 
   
    public partial class FormNuevoCliente : Form
    {

        private ClienteService clienteService = new ClienteService();

        public FormNuevoCliente()
        {
            InitializeComponent();
        }

        private void FormNuevoCliente_Load(object sender, EventArgs e)
        {

        }







        private void buttonCargarCliente_Click(object sender, EventArgs e)
        {
            Cliente nuevoCliente = new Cliente
            {
                Nombre = textBoxNombre.Text,
                Apellido = textBoxApellido.Text,
                Dni = textBoxDni.Text,
                Telefono = textBoxTelefono.Text,
                Email = textBoxEmail.Text,
                Direccion = textBoxDireccion.Text,
                Activo = true
            };

           // ClienteService clienteService = new ClienteService();
            clienteService.AgregarCliente(nuevoCliente);

            //List<Cliente> listaClientes = clienteService.ObtenerClientes();
            this.Close();
        }
    }
}
