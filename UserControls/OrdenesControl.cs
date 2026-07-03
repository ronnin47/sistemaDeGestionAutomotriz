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
using sistemaDeGestionAutomotriz.Forms;
using System.Globalization;


namespace sistemaDeGestionAutomotriz.UserControls
{
    public partial class OrdenesControl : UserControl
    {
        private List<OrdenTrabajoDto> _ordenes = new List<OrdenTrabajoDto>();
        public OrdenesControl()
        {
            InitializeComponent();
        }

        private void OrdenesControl_Load(object sender, EventArgs e)
        {
            dgvOrdenesTrabajo.ReadOnly = true;
            dgvOrdenesTrabajo.AllowUserToAddRows = false;
            dgvOrdenesTrabajo.AllowUserToDeleteRows = false;
            dgvOrdenesTrabajo.AllowUserToResizeRows = false;
            CargarOrdenes();

            comboBoxFilterTipo.SelectedItem = "Todos";
            textBoxBuscador.TextChanged += textBoxBuscador_TextChanged;
        }

        private void CargarOrdenes()
        {
            OrdenTrabajoService servicio = new OrdenTrabajoService();
            // 1. Instanciamos tu servicio existente de insumos
            InsumoService insumoServicio = new InsumoService();


            _ordenes = servicio.ObtenerOrdenesTrabajo();

            dgvOrdenesTrabajo.DataSource = _ordenes;

            // 1. CONTADOR DE ACTIVAS: Excluimos de forma correcta "Entregado" y "Anulada"
            labelActivas.Text = _ordenes.Count(o =>
                o.Estado != "Entregado" &&
                o.Estado != "Anulada").ToString();

            // 2. CONTADORES DE CATEGORÍAS: Sumamos la condición para que SOLO cuente las que están activas (no entregadas ni anuladas)
            labelModulos.Text = _ordenes.Count(o =>
                o.Categoria == "Módulo" &&
                o.Estado != "Entregado" &&
                o.Estado != "Anulada").ToString();

            labelCerrajeria.Text = _ordenes.Count(o =>
                o.Categoria == "Cerrajería" &&
                o.Estado != "Entregado" &&
                o.Estado != "Anulada").ToString();

            labelInstalaciones.Text = _ordenes.Count(o =>
                o.Categoria == "Instalaciones" &&
                o.Estado != "Entregado" &&
                o.Estado != "Anulada").ToString();

            //labelAlertaStock.Text = "0";

            // 2. LEEMOS TODOS LOS INSUMOS Y CONTAMOS LOS QUE TIENEN STOCK BAJO CON LINQ
            List<Insumo> todosLosInsumos = insumoServicio.ObtenerTodosLosInsumos();
            int alertas = todosLosInsumos.Count(i => i.Stock <= 5);
            labelAlertaStock.Text = alertas.ToString();


            // Forma para renderizar lo que quiera ocultar columnas
            //dgvOrdenesTrabajo.Columns["Detalle"].Visible = false;
            dgvOrdenesTrabajo.Columns["Diagnostico"].Visible = false;
            dgvOrdenesTrabajo.Columns["Telefono"].Visible = false;
            dgvOrdenesTrabajo.Columns["Precio"].Visible = false;
            dgvOrdenesTrabajo.Columns["Garantia"].Visible = false;
            dgvOrdenesTrabajo.Columns["IdUsuario"].Visible = false;
            dgvOrdenesTrabajo.Columns["Dni"].Visible = false;
            dgvOrdenesTrabajo.Columns["Vehiculo"].Visible = false;
            dgvOrdenesTrabajo.Columns["TipoModulo"].Visible = false;
            dgvOrdenesTrabajo.Columns["EsReparable"].Visible = false;
            dgvOrdenesTrabajo.Columns["MotivoGarantia"].Visible = false;
            dgvOrdenesTrabajo.Columns["Observaciones"].Visible = false;
        }





        //esto para que se pueda vel el panel de abajo
        private void dgvOrdenesTrabajo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvOrdenesTrabajo.Rows[e.RowIndex].DataBoundItem is OrdenTrabajoDto orden)
            {
                labelCliente.Text = orden.Cliente;
                labelNumeroOrdenTipo.Text = $"{orden.NumeroOrden}-{orden.TipoModulo}";
                labelDni.Text = orden.Dni;
                labelDetalle.Text = orden.Detalle;
                labelAsignado.Text = orden.TecnicoAsignado;
                labelEstado.Text = orden.Estado;

                labelTelefono.Text = orden.Telefono;
                labelDiagnostico.Text = orden.Diagnostico;
                labelCotizacion.Text = orden.Precio.ToString("C");

                // Cuando agregues el DNI:
                // labelDni.Text = orden.Dni;
            }
        }



     


        private void comboBoxFilterTipo_SelectedIndexChanged(object sender, EventArgs e)
        {

            string categoria = Normalizar(comboBoxFilterTipo.Text);

            if (comboBoxFilterTipo.Text == "Todos")
            {
                dgvOrdenesTrabajo.DataSource = _ordenes;
            }
            else
            {
                dgvOrdenesTrabajo.DataSource = _ordenes
                 .Where(o => Normalizar(o.Categoria) == categoria)
                 .ToList();
            }
        }






        //Buscador
        private void textBoxBuscador_TextChanged(object sender, EventArgs e)
        {
            string busqueda = Normalizar(textBoxBuscador.Text);

            if (string.IsNullOrWhiteSpace(busqueda))
            {
                dgvOrdenesTrabajo.DataSource = null;
                dgvOrdenesTrabajo.DataSource = _ordenes;
                return;
            }

            dgvOrdenesTrabajo.DataSource = null;

            dgvOrdenesTrabajo.DataSource = _ordenes.Where(o =>
                   Normalizar(o.NumeroOrden.ToString()).Contains(busqueda) ||
                   Normalizar(o.Categoria).Contains(busqueda) ||
                   Normalizar(o.TipoServicio).Contains(busqueda) ||
                   Normalizar(o.Cliente).Contains(busqueda) ||
                   Normalizar(o.Dni).Contains(busqueda) ||
                   Normalizar(o.Telefono).Contains(busqueda) ||
                   Normalizar(o.Vehiculo).Contains(busqueda) ||
                   Normalizar(o.TipoModulo).Contains(busqueda) ||
                   Normalizar(o.Detalle).Contains(busqueda) ||
                   Normalizar(o.Diagnostico).Contains(busqueda) ||
                   Normalizar(o.TecnicoAsignado).Contains(busqueda) ||
                   Normalizar(o.FechaIngreso.ToString("dd/MM/yyyy")).Contains(busqueda) ||
                   Normalizar(o.Estado).Contains(busqueda) ||
                   Normalizar(o.Precio.ToString()).Contains(busqueda) ||
                   Normalizar(o.Observaciones).Contains(busqueda) ||
                   Normalizar(o.EsReparable.ToString()).Contains(busqueda) ||
                   Normalizar(o.Garantia.ToString()).Contains(busqueda) ||
                   Normalizar(o.MotivoGarantia).Contains(busqueda)
            ).ToList();
        }

        private string Normalizar(string texto)
        {
            string normalizado = texto.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in normalizado)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC).ToLower().Trim();
        }









       
        private void buttonNuevaOrden_Click(object sender, EventArgs e)
        {
            FormNuevaOrden formNuevaOrden = new FormNuevaOrden();

            formNuevaOrden.FormClosed += (s, args) =>
            {
                CargarOrdenes();
            };

            formNuevaOrden.Show();
        }








        private void dgvOrdenesTrabajo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificamos que el doble clic sea en una fila real y no en los títulos de arriba (los encabezados)
            if (e.RowIndex >= 0)
            {
                // 1. Obtenemos la fila en la que el usuario hizo doble clic
                DataGridViewRow filaSeleccionada = dgvOrdenesTrabajo.Rows[e.RowIndex];

                // 2. Extraemos el ID del cliente (reemplaza "ClientId" o el índice por el nombre real de tu columna de ID)
                // int clienteId = Convert.ToInt32(filaSeleccionada.Cells["ClienteId"].Value);

                OrdenTrabajoDto orden = (OrdenTrabajoDto)filaSeleccionada.DataBoundItem;

                FormEditarOrdenes formEditar = new FormEditarOrdenes(orden);



                // 4. Cuando se cierre la ventana de edición, actualizamos la lista automáticamente
                formEditar.FormClosed += (s, args) =>
                {
                    CargarOrdenes();
                };

                // 5. Lo mostramos como ventana emergente (ShowDialog bloquea la de atrás para evitar clics extra)
                formEditar.ShowDialog();
            }
        }



        //eliminar luego
        private void label1_Click(object sender, EventArgs e)
        {

        }

       
    }
}
