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
    public partial class NuevaOrdenInstalacionControl : UserControl
    {


        private readonly UsuarioService _usuarioService = new UsuarioService();
        private readonly OrdenTrabajoService _ordenService = new OrdenTrabajoService();



        public NuevaOrdenInstalacionControl()
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

        private void NuevaOrdenInstalacionControl_Load(object sender, EventArgs e)
        {
            //cargar el combo
            comboBoxTipoServicio.Items.Clear();
            
            comboBoxTipoServicio.Items.Add("Instalación de Alarmas");
            comboBoxTipoServicio.Items.Add("Instalación de Estereos");
            comboBoxTipoServicio.Items.Add("Reparación de cerradura");
            comboBoxTipoServicio.Items.Add("Instalación de Cámaras de reserva");
            comboBoxTipoServicio.Items.Add("Instalación de Sensores de Estacionamiento");
            comboBoxTipoServicio.Items.Add("Instalación de Luces LED");

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
                int tipoServicio = 0;

                switch (comboBoxTipoServicio.Text)
                {
                    

                    case "Instalación de Alarmas":
                        tipoServicio = 11;
                        break;

                    case "Instalación de Estereos":
                        tipoServicio = 12;
                        break;

                    case "Instalación de Cámaras de reserva":
                        tipoServicio = 13;
                        break;
             

                    case "Instalación de Sensores de Estacionamiento":
                        tipoServicio = 14;
                        break;

                    case "Instalación de Luces LED":
                        tipoServicio = 15;
                        break;

                    default:
                        MessageBox.Show("Seleccione un tipo de servicio.");
                        return;
                }

                OrdenTrabajoInstalacion nuevaOrden = new OrdenTrabajoInstalacion
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
                    TipoServicioId = tipoServicio,// aca guardamos el id del combo
                    Marca = textBoxModeloVehiculo.Text,//ok


                    IdUsuarioAsignado = (int)comboBoxUsuarios.SelectedValue,//ok
                    Observaciones = textBoxObservaciones.Text.Trim(),//ok

                    Garantia = checkBoxGarantia.Checked,




                    //instancia solito
                    FechaIngreso = DateTime.Now,
                    Estado = "Pendiente"
                };




                bool exito = _ordenService.CrearNuevaOrdenInstalacion(nuevaOrden);

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