using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using sistemaDeGestionAutomotriz.Models;
using sistemaDeGestionAutomotriz.Services;
using sistemaDeGestionAutomotriz.UI;

namespace sistemaDeGestionAutomotriz.UserControls
{
    public partial class VentasControl : UserControl
    {
        // Puente con el backend.
        private readonly VentaService _service = new VentaService();
        // Lista completa en memoria para filtrar sin volver a la base.
        private List<VentaInsumosDto> _ventas = new List<VentaInsumosDto>();

        // Controles armados por código (mismas posiciones que en Clientes).
        private Label _lblTitulo;
        private Label _lblContador;
        private TextBox _txtBuscar;
        private DataGridView _grilla;
        private Label _lblVacio;

        public VentasControl()
        {
            InitializeComponent();
            ConstruirUI();
            CargarVentas();
        }

        /// <summary>Arma la pantalla por código con el mismo molde que las demás.</summary>
        private void ConstruirUI()
        {
            BackColor = Tema.FondoApp;
            Padding = new Padding(Tema.PaddingPantalla);

            // ---- Cuerpo: la grilla (solo lectura por ahora; el back aún no registra ventas) ----
            _grilla = new DataGridView { Dock = DockStyle.Fill };
            Tema.EstiloTabla(_grilla);
            _grilla.AutoGenerateColumns = false;
            _grilla.Columns.Add(Columna("NumeroVenta", "N°"));
            _grilla.Columns.Add(Columna("Cliente", "Cliente"));
            _grilla.Columns.Add(Columna("Insumo", "Insumo"));
            _grilla.Columns.Add(Columna("Cantidad", "Cantidad", DataGridViewContentAlignment.MiddleRight));
            _grilla.Columns.Add(Columna("Total", "Total", DataGridViewContentAlignment.MiddleRight, "C0"));
            _grilla.Columns.Add(Columna("Fecha", "Fecha", null, "dd/MM/yyyy"));

            // ---- Estado vacío ----
            _lblVacio = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Todavía no hay ventas registradas.",
                ForeColor = Tema.TextoSecundario,
                Font = Tema.FuenteCuerpo,
                Visible = false
            };

            // ---- Barra de herramientas: buscador (misma ubicación que en Clientes) ----
            Panel toolbar = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Tema.FondoApp };
            Label lblBuscar = new Label
            {
                Text = "Buscar:",
                AutoSize = true,
                Location = new Point(0, 14),
                ForeColor = Tema.TextoSecundario,
                Font = Tema.FuenteEtiqueta
            };
            _txtBuscar = new TextBox { Location = new Point(54, 10), Width = 300 };
            Tema.EstiloInput(_txtBuscar);
            _txtBuscar.TextChanged += (s, e) => AplicarFiltro();
            toolbar.Controls.Add(lblBuscar);
            toolbar.Controls.Add(_txtBuscar);

            // ---- Encabezado: título + contador (sin botón: el back todavía no registra ventas) ----
            Panel header = new Panel { Dock = DockStyle.Top, Height = 58, BackColor = Tema.FondoApp };
            _lblTitulo = new Label { Text = "Ventas de insumos", Location = new Point(0, 0) };
            Tema.EstiloTituloPantalla(_lblTitulo);
            _lblContador = new Label { Location = new Point(2, 34) };
            Tema.EstiloSubtitulo(_lblContador);
            header.Controls.Add(_lblTitulo);
            header.Controls.Add(_lblContador);

            // Mismo orden de agregado que en Clientes: Fill primero, Top después.
            Controls.Add(_grilla);
            Controls.Add(_lblVacio);
            Controls.Add(toolbar);
            Controls.Add(header);
        }

        /// <summary>Crea una columna mapeada a una propiedad, con alineación y formato opcionales.</summary>
        private DataGridViewTextBoxColumn Columna(string propiedad, string titulo,
            DataGridViewContentAlignment? alineacion = null, string formato = null)
        {
            var col = new DataGridViewTextBoxColumn
            {
                DataPropertyName = propiedad,
                HeaderText = titulo,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            if (alineacion != null) col.DefaultCellStyle.Alignment = alineacion.Value;
            if (formato != null) col.DefaultCellStyle.Format = formato;   // ej. "C0" (moneda) o fecha
            return col;
        }

        /// <summary>Trae las ventas del backend y refresca la pantalla.</summary>
        private void CargarVentas()
        {
            // Si falla, el servicio ya muestra su propio aviso y devuelve lista vacía.
            _ventas = _service.ObtenerVentaInsumos() ?? new List<VentaInsumosDto>();
            AplicarFiltro();
        }

        /// <summary>Filtra por cliente o insumo y vuelca en la grilla.</summary>
        private void AplicarFiltro()
        {
            string q = (_txtBuscar.Text ?? "").Trim().ToLower();
            List<VentaInsumosDto> visibles = _ventas;
            if (q.Length > 0)
            {
                visibles = _ventas.Where(v =>
                    ((v.Cliente ?? "") + " " + (v.Insumo ?? "")).ToLower().Contains(q)).ToList();
            }

            _grilla.DataSource = null;
            _grilla.DataSource = visibles;

            bool sinVentas = _ventas.Count == 0;
            _grilla.Visible = !sinVentas;
            _lblVacio.Visible = sinVentas;

            ActualizarContador(visibles.Count);
        }

        /// <summary>Actualiza el subtítulo con la cantidad de ventas.</summary>
        private void ActualizarContador(int visibles)
        {
            int total = _ventas.Count;
            if (total == visibles)
                _lblContador.Text = total == 1 ? "1 venta registrada" : total + " ventas registradas";
            else
                _lblContador.Text = visibles + " de " + total + " ventas";
        }
    }
}
