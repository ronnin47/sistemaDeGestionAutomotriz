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
    public partial class GarantiasControl : UserControl
    {
        private readonly GarantiaService _service = new GarantiaService();
        private List<GarantiaDto> _garantias = new List<GarantiaDto>();

        private Label _lblTitulo;
        private Label _lblContador;
        private TextBox _txtBuscar;
        private DataGridView _grilla;
        private Label _lblVacio;

        public GarantiasControl()
        {
            InitializeComponent();
            ConstruirUI();
            CargarGarantias();
        }

        private void ConstruirUI()
        {
            BackColor = Tema.FondoApp;
            Padding = new Padding(Tema.PaddingPantalla);

            _grilla = new DataGridView { Dock = DockStyle.Fill };
            Tema.EstiloTabla(_grilla);
            _grilla.AutoGenerateColumns = false;
            _grilla.Columns.Add(Columna("IdOrden", "N° Orden"));
            _grilla.Columns.Add(Columna("Cliente", "Cliente"));
            _grilla.Columns.Add(Columna("Detalle", "Detalle"));
            _grilla.Columns.Add(Columna("Asignado", "Asignado"));
            _grilla.Columns.Add(Columna("Estado", "Estado"));
            _grilla.Columns.Add(Columna("Condicion", "Condición"));
            _grilla.CellFormatting += Grilla_CellFormatting;

            _lblVacio = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "No hay garantías registradas.",
                ForeColor = Tema.TextoSecundario,
                Font = Tema.FuenteCuerpo,
                Visible = false
            };

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

            Panel header = new Panel { Dock = DockStyle.Top, Height = 58, BackColor = Tema.FondoApp };
            _lblTitulo = new Label { Text = "Garantías", Location = new Point(0, 0) };
            Tema.EstiloTituloPantalla(_lblTitulo);
            _lblContador = new Label { Location = new Point(2, 34) };
            Tema.EstiloSubtitulo(_lblContador);
            header.Controls.Add(_lblTitulo);
            header.Controls.Add(_lblContador);

            Controls.Add(_grilla);
            Controls.Add(_lblVacio);
            Controls.Add(toolbar);
            Controls.Add(header);
        }

        private DataGridViewTextBoxColumn Columna(string propiedad, string titulo)
        {
            return new DataGridViewTextBoxColumn
            {
                DataPropertyName = propiedad,
                HeaderText = titulo,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
        }

        private void Grilla_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (_grilla.Columns[e.ColumnIndex].DataPropertyName == "Estado" && e.Value != null)
            {
                Color fondo, texto;
                Tema.ColoresEstado(e.Value.ToString(), out fondo, out texto);
                e.CellStyle.BackColor = fondo;
                e.CellStyle.ForeColor = texto;
                e.CellStyle.SelectionBackColor = fondo;
                e.CellStyle.SelectionForeColor = texto;
            }
        }

        private void CargarGarantias()
        {
            _garantias = _service.ObtenerTodasLasGarantias() ?? new List<GarantiaDto>();
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            string q = (_txtBuscar.Text ?? "").Trim().ToLower();
            List<GarantiaDto> visibles = _garantias;
            if (q.Length > 0)
            {
                visibles = _garantias.Where(g =>
                    ((g.Cliente ?? "") + " " + (g.Asignado ?? "")).ToLower().Contains(q)).ToList();
            }

            _grilla.DataSource = null;
            _grilla.DataSource = visibles;

            bool sinDatos = _garantias.Count == 0;
            _grilla.Visible = !sinDatos;
            _lblVacio.Visible = sinDatos;

            ActualizarContador(visibles.Count);
        }

        private void ActualizarContador(int visibles)
        {
            int total = _garantias.Count;
            if (total == visibles)
                _lblContador.Text = total == 1 ? "1 garantía" : total + " garantías";
            else
                _lblContador.Text = visibles + " de " + total + " garantías";
        }
    }
}
