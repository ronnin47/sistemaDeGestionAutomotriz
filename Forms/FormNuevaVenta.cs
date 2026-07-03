

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using sistemaDeGestionAutomotriz.Models;
using sistemaDeGestionAutomotriz.Services;

namespace sistemaDeGestionAutomotriz.Forms
{
    public partial class FormNuevaVenta : Form
    {
        // Instanciamos los servicios de forma global en el formulario
        private readonly InsumoService _insumoService = new InsumoService();
        private readonly ClienteService _clienteService = new ClienteService();
        private readonly VentaService _ventaService = new VentaService(); // Asegúrate de tener este servicio

        public FormNuevaVenta()
        {
            InitializeComponent();
        }

        private void FormNuevaVenta_Load(object sender, EventArgs e)
        {
            CargarInsumos();
            CargarClientes();

            // Suscribir eventos para recalcular el total en tiempo real
            comboBoxInsumosVender.SelectedIndexChanged += CalcularTotal;
            textBoxCantidad.TextChanged += CalcularTotal;
        }

        private void CargarInsumos()
        {
            // Desvincular eventos temporalmente para evitar errores al cargar datos
            comboBoxInsumosVender.SelectedIndexChanged -= CalcularTotal;

            comboBoxInsumosVender.DataSource = _insumoService.ObtenerTodosLosInsumos();
            comboBoxInsumosVender.DisplayMember = "Nombre";
            comboBoxInsumosVender.ValueMember = "IdKit"; // Mapea con id_kit de Supabase
            comboBoxInsumosVender.SelectedIndex = -1; // Inicia vacío

            comboBoxInsumosVender.SelectedIndexChanged += CalcularTotal;
        }

        private void CargarClientes()
        {
            comboBoxClientes.DataSource = _clienteService.ObtenerClientesActivos();
            comboBoxClientes.DisplayMember = "NombreCompleto";
            comboBoxClientes.ValueMember = "ClienteId"; // ¡Faltaba esto para poder registrar la venta!
            comboBoxClientes.SelectedIndex = -1;
        }

        private void CalcularTotal(object sender, EventArgs e)
        {
            // Validar que haya un insumo seleccionado y una cantidad válida
            if (comboBoxInsumosVender.SelectedItem is Insumo modeloInsumo &&
                int.TryParse(textBoxCantidad.Text, out int cantidad) && cantidad > 0)
            {
                // Verifica que tu modelo 'Insumo' o 'Kit' tenga la propiedad Precio
                decimal total = modeloInsumo.Precio * cantidad;
                labelTotal.Text = $"Total: ${total:N2}";
            }
            else
            {
                labelTotal.Text = "Total: $0.00";
            }
        }


   
    

        private void buttonRegistarVenta_Click(object sender, EventArgs e)
        {


            // 1. Validaciones de interfaz de usuario
            if (comboBoxClientes.SelectedValue == null)
            {
                MessageBox.Show("Por favor, seleccione un cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBoxInsumosVender.SelectedValue == null)
            {
                MessageBox.Show("Por favor, seleccione un insumo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(textBoxCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida mayor a cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar stock antes de enviar a Supabase
            var insumoSeleccionado = (Insumo)comboBoxInsumosVender.SelectedItem;
            if (cantidad > insumoSeleccionado.Stock)
            {
                MessageBox.Show($"Stock insuficiente. Solo quedan {insumoSeleccionado.Stock} unidades.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Mapear datos al modelo Venta
                Venta nuevaVenta = new Venta
                {
                    IdCliente = (int)comboBoxClientes.SelectedValue,
                    IdKit = (int)comboBoxInsumosVender.SelectedValue,
                    Cantidad = cantidad,
                    Total = insumoSeleccionado.Precio * cantidad,
                    Fecha = DateTime.Now
                };

                // 3. Guardar en Base de Datos mediante el servicio
                bool exito = _ventaService.RegistrarNuevaVenta(nuevaVenta);

                if (exito)
                {
                    MessageBox.Show("Venta registrada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // O limpiar campos
                }
                else
                {
                    MessageBox.Show("No se pudo registrar la venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en el sistema: {ex.Message}", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}










































/*

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using sistemaDeGestionAutomotriz.Data;
using sistemaDeGestionAutomotriz.Models;
using sistemaDeGestionAutomotriz.Services;

namespace sistemaDeGestionAutomotriz.Forms
{
    public partial class FormNuevaVenta : Form
    {
        public FormNuevaVenta()
        {
            InitializeComponent();

           

        }

        private void FormNuevaVenta_Load(object sender, EventArgs e)
        {
                CargarInsumos();
                CargarClientes();
          
        }


        private void CargarInsumos()
        {
            InsumoService servicio = new InsumoService();

            comboBoxInsumosVender.DataSource = servicio.ObtenerTodosLosInsumos();
            comboBoxInsumosVender.DisplayMember = "Nombre"; // Lo que ve el usuario
            comboBoxInsumosVender.ValueMember = "IdKit";    // El valor asociado
        }

        private void CargarClientes()
        {
            ClienteService servicio = new  ClienteService();

            comboBoxClientes.DataSource = servicio.ObtenerClientesActivos();
            comboBoxClientes.DisplayMember = "NombreCompleto";
        }
    }
}
*/