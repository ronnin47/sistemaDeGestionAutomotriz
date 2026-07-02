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
    public partial class NuevaOrdenModuloControl : UserControl
    {


        private readonly UsuarioService _usuarioService = new UsuarioService();
        private readonly OrdenTrabajoService _ordenService = new OrdenTrabajoService();



        public NuevaOrdenModuloControl()
        {
            InitializeComponent();
        }


        private void CargarUsuarios()
        {
            comboBoxUsuarios.DataSource = _usuarioService.ObtenerUsuarios();
            comboBoxUsuarios.DisplayMember = "NombreCompleto";
            comboBoxUsuarios.ValueMember = "UsuarioId"; // ¡Faltaba esto para poder registrar la venta!
            comboBoxUsuarios.SelectedIndex = -1;
        }

        private void NuevaOrdenModuloControl_Load(object sender, EventArgs e)
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

                //hacer clase modelo
                OrdenTrabajo nuevaOrden = new OrdenTrabajo
                {
                    //cliente
                    NombreCliente = textBoxNombre.Text.Trim(),//ok
                    ApellidoCliente = textBoxApellido.Text.Trim(),//ok
                    Dni = textBoxDni.Text.Trim(),//ok
                    Telefono = textBoxTelefono.Text.Trim(),//ok
                    Email = textBoxEmail.Text.Trim(),//ok 
                    Direccion = textBoxDireccion.Text.Trim(),//ok 


                    //detalle
                    TipoModulo = comboBoxTipoModulo.Text,//ok
                    Modelo = textBoxModelo.Text.Trim(),//ok
                    Marca = textBoxMarca.Text.Trim(),//ok
                    TipoVehiculo = comboBoxVehiculo.Text,//ok
                    Combustible = comboBoxCombustible.Text,//ok


                    IdUsuarioAsignado = (int)comboBoxUsuarios.SelectedValue,//ok
                    Observaciones = textBoxObservaciones.Text.Trim(),//ok

                    Garantia = checkBoxGarantia.Checked,


 

                    //instancia solito
                    FechaIngreso = DateTime.Now,
                    Estado = "Pendiente"
                };

                //OrdenTrabajoService service = new OrdenTrabajoService();
               

                bool exito = _ordenService.CrearNuevaOrden(nuevaOrden);

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
    }
}
