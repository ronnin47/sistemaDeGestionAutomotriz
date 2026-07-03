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
    public partial class FormEditarOrdenes : Form
    {

        private OrdenTrabajoDto _orden;



        public FormEditarOrdenes(OrdenTrabajoDto orden)
        {
            InitializeComponent();

             _orden  =orden;

            //no editables
            labelNumeroOrden.Text = _orden.NumeroOrden.ToString();
            labelCategoria.Text = _orden.Categoria;
            labelTipoServicio.Text = _orden.TipoServicio;
            labelCliente.Text = _orden.Cliente;
            labelDni.Text = _orden.Dni;
            labelTipoModulo.Text = _orden.TipoModulo;
            labelVehiculo.Text = _orden.Vehiculo;
            labelFechaIngreso.Text = _orden.FechaIngreso.ToString();

            //editables
            textBoxTelefono.Text = _orden.Telefono;
            textBoxPrecio.Text = _orden.Precio.ToString("F2");
            textBoxDiagnostico.Text = _orden.Diagnostico;
            textBoxObservaciones.Text = _orden.Observaciones;
            textBoxMotivoGarantia.Text = _orden.MotivoGarantia;
            checkBoxGarantia.Checked = _orden.Garantia;
            checkBoxEsReparable.Checked = _orden.EsReparable;


       



     
            UsuarioService usuarioServicio = new UsuarioService();
          

            comboBoxTecnicoAsignado.DataSource = usuarioServicio.ObtenerUsuarios();
            comboBoxTecnicoAsignado.DisplayMember = "NombreCompleto";
            comboBoxTecnicoAsignado.ValueMember = "UsuarioId";
            comboBoxTecnicoAsignado.SelectedIndex = -1;

            //este es el que quiero que este seleciconado 
            //comboBoxTecnicoAsignado.SelectedIndex = comboBoxTecnicoAsignado.FindStringExact(_orden.TecnicoAsignado);
            // comboBoxTecnicoAsignado.Text = _orden.TecnicoAsignado;
            comboBoxTecnicoAsignado.SelectedValue = _orden.IdUsuario;

            comboBoxEstado.Items.Clear();
            comboBoxEstado.Items.Add("Pendiente");
            comboBoxEstado.Items.Add("En revisión");
            comboBoxEstado.Items.Add("En reparación");
            comboBoxEstado.Items.Add("Listo para entregar");
            comboBoxEstado.Items.Add("Entregado");
            comboBoxEstado.Items.Add("Dado de baja");
            comboBoxEstado.Text = _orden.Estado;



        }


        private void buttonEliminar_Click(object sender, EventArgs e)
        {


           
            if (_orden.Estado == "Anulada")
            {
                MessageBox.Show("Esta órden ya se encuentra anulada en el sistema. No es posible volver a anularla.",
                                "Acción Denegada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // El return frena el método acá y no deja que avance al código de abajo
            }

            DialogResult resultado = MessageBox.Show(
                    "¿Está seguro de que desea anula esta órden?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                OrdenTrabajoService servicio = new OrdenTrabajoService();
                // servicio.AnularOrden(_orden.NumeroOrden, _orden.idKit, _orden.Cantidad);
                bool ok=servicio.AnularOrden(_orden.NumeroOrden);
                MessageBox.Show("Cliente dado de baja correctamente.");

                this.Close();
            }
        }

        private void buttonEditar_Click(object sender, EventArgs e)
        {
            _orden.Telefono = textBoxTelefono.Text.Trim();

            // Parseo seguro del precio decimal (Evita caídas por comas o puntos)
            string precioTexto = textBoxPrecio.Text.Replace('.', ',');
            if (!decimal.TryParse(precioTexto, out decimal precio))
            {
                MessageBox.Show("Por favor, ingrese un precio numérico válido.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _orden.Precio = precio;

            _orden.Diagnostico = textBoxDiagnostico.Text.Trim();
            _orden.Observaciones = textBoxObservaciones.Text.Trim();
            _orden.MotivoGarantia = textBoxMotivoGarantia.Text.Trim();
            _orden.Garantia = checkBoxGarantia.Checked;
            _orden.EsReparable = checkBoxEsReparable.Checked;

            // 1. VALIDACIÓN DEL COMBO ESTADO
            if (comboBoxEstado.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un estado válido para la orden.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _orden.Estado = comboBoxEstado.SelectedItem.ToString();

            // 2. SOLUCIÓN COMPLETA PARA EL TÉCNICO (Capturamos el ID real de la base de datos)
            if (comboBoxTecnicoAsignado.SelectedValue != null)
            {
                // Guardamos el ID numérico exacto (evita el error de la columna id_usuario obligatoria)
                _orden.IdUsuario = Convert.ToInt32(comboBoxTecnicoAsignado.SelectedValue);
                _orden.TecnicoAsignado = comboBoxTecnicoAsignado.Text;
            }
            else
            {
                // Como tu base de datos exige que la columna id_usuario NO sea nula,
                // no podemos dejar avanzar el guardado si el combo se quedó vacío.
                MessageBox.Show("Debe asignar un técnico obligatorio a la orden de trabajo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Ejecución del servicio transaccional
            OrdenTrabajoService service = new OrdenTrabajoService();
            bool exito = service.ActualizarOrden(_orden);

            if (exito)
            {
                MessageBox.Show("Edición de orden actualizada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }






    }
}
