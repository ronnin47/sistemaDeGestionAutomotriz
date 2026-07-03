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
    public partial class NuevaOrdenCerrajeriaControl : UserControl
    {


        private readonly UsuarioService _usuarioService = new UsuarioService();
        private readonly OrdenTrabajoService _ordenService = new OrdenTrabajoService();



        public NuevaOrdenCerrajeriaControl()
        {
            InitializeComponent();
        }


        private void CargarUsuarios()
        {
            comboBoxUsuarios.DataSource = _usuarioService.ObtenerUsuarios();
            comboBoxUsuarios.DisplayMember = "NombreCompleto";
            comboBoxUsuarios.ValueMember = "UsuarioId";
            comboBoxUsuarios.SelectedIndex = -1;
        }

        private void NuevaOrdenCerrajeriaControl_Load(object sender, EventArgs e)
        {

            CargarUsuarios();

        }



        private void buttonCrearOrden_Click(object sender, EventArgs e)
        {
            if (comboBoxUsuarios.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un usuario.");
                return;
            }

            try
            {


                OrdenTrabajoCerrajeria nuevaOrden = new OrdenTrabajoCerrajeria
                {
                    //cliente
                    NombreCliente = textBoxNombre.Text.Trim(),//ok
                    ApellidoCliente = textBoxApellido.Text.Trim(),//ok
                    Dni = textBoxDni.Text.Trim(),//ok
                    Telefono = textBoxTelefono.Text.Trim(),//ok
                    Email = textBoxEmail.Text.Trim(),//ok 
                    Direccion = textBoxDireccion.Text.Trim(),//ok 


                    //detalle
                    TipoServicio = comboBoxTipoServicio.Text,//ok
                    Marca =textBoxModeloVehiculo.Text,//ok
                 

                    IdUsuarioAsignado = (int)comboBoxUsuarios.SelectedValue,//ok
                    Observaciones = textBoxObservaciones.Text.Trim(),//ok

                    Garantia = checkBoxGarantia.Checked,




                    //instancia solito
                    FechaIngreso = DateTime.Now,
                    Estado = "Pendiente"
                };




                bool exito = _ordenService.CrearNuevaOrdenCerrajeria(nuevaOrden);

                if (exito)
                {
                    MessageBox.Show("Orden registrada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    Form formulario = this.FindForm();
                    if (formulario != null)
                    {
                        formulario.Close();
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo registrar la orden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }





            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



























        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

