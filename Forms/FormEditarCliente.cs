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
    public partial class FormEditarCliente : Form
    {
        private Cliente _cliente;
        public FormEditarCliente(Cliente cliente)
        {
          
            InitializeComponent();
            _cliente = cliente;


            labelId.Text = _cliente.ClienteId.ToString();
            textBoxNombre.Text = _cliente.Nombre;
            textBoxApellido.Text = _cliente.Apellido;
            textBoxDni.Text = _cliente.Dni;
            textBoxEmail.Text = _cliente.Email;
            textBoxTelefono.Text = _cliente.Telefono;
            textBoxDireccion.Text = _cliente.Direccion;
            labelActivo.Text = _cliente.Activo ? "Activo" : "Inactivo";



        }


        private void buttonEditar_Click(object sender, EventArgs e)
        {
            _cliente.Nombre = textBoxNombre.Text;
            _cliente.Apellido = textBoxApellido.Text;
            _cliente.Dni = textBoxDni.Text;
            _cliente.Email = textBoxEmail.Text;
            _cliente.Telefono = textBoxTelefono.Text;
            _cliente.Direccion = textBoxDireccion.Text;

            ClienteService service = new ClienteService();
            service.ActualizarCliente(_cliente);

            MessageBox.Show("Cliente actualizado correctamente.");

            this.Close();
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de que desea dar de baja este cliente?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                ClienteService servicio = new ClienteService();
                servicio.DarDeBajaCliente(_cliente.ClienteId);

                MessageBox.Show("Cliente dado de baja correctamente.");

                this.Close();
            }
        }



    }
}
