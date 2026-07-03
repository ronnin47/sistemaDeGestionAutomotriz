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
    public partial class OrdenesControl : UserControl
    {
        private readonly OrdenTrabajoService _service = new OrdenTrabajoService();
        private List<OrdenTrabajoDto> _ordenes = new List<OrdenTrabajoDto>();

        private Label _lblTitulo;
        private Label _lblContador;
        private TextBox _txtBuscar;
        private ComboBox _cboCategoria;
        private DataGridView _grilla;
        private Label _lblVacio;

        public OrdenesControl()
        {
            InitializeComponent();
            ConstruirUI();
            CargarOrdenes();
        }

        private void ConstruirUI()
        {
            BackColor = Tema.FondoApp;
            Padding = new Padding(Tema.PaddingPantalla);

            _grilla = new DataGridView { Dock = DockStyle.Fill };
            Tema.EstiloTabla(_grilla);
            _grilla.AutoGenerateColumns = false;
            _grilla.Columns.Add(Columna("NumeroOrden", "N°"));
            _grilla.Columns.Add(Columna("Categoria", "Categoría"));
            _grilla.Columns.Add(Columna("TipoServicio", "Servicio"));
            _grilla.Columns.Add(Columna("Cliente", "Cliente"));
            _grilla.Columns.Add(Columna("TecnicoAsignado", "Técnico"));
            _grilla.Columns.Add(Columna("FechaIngreso", "Ingreso", null, "dd/MM/yyyy"));
            _grilla.Columns.Add(Columna("Estado", "Estado"));
            _grilla.Columns.Add(Columna("Precio", "Precio", DataGridViewContentAlignment.MiddleRight, "C0"));
            _grilla.CellFormatting += Grilla_CellFormatting;

            _lblVacio = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "No hay órdenes para mostrar.",
                ForeColor = Tema.TextoSecundario,
                Font = Tema.FuenteCuerpo,
                Visible = false
            };

            Panel toolbar = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Tema.FondoApp };
            Label lblBuscar = new Label { Text = "Buscar:", AutoSize = true, Location = new Point(0, 14), ForeColor = Tema.TextoSecundario, Font = Tema.FuenteEtiqueta };
            _txtBuscar = new TextBox { Location = new Point(54, 10), Width = 260 };
            Tema.EstiloInput(_txtBuscar);
            _txtBuscar.TextChanged += (s, e) => AplicarFiltro();
            Label lblCat = new Label { Text = "Categoría:", AutoSize = true, Location = new Point(334, 14), ForeColor = Tema.TextoSecundario, Font = Tema.FuenteEtiqueta };
            _cboCategoria = new ComboBox { Location = new Point(404, 10), Width = 170, DropDownStyle = ComboBoxStyle.DropDownList };
            Tema.EstiloCombo(_cboCategoria);
            _cboCategoria.Items.AddRange(new object[] { "Todas", "Módulo", "Cerrajería", "Instalaciones" });
            _cboCategoria.SelectedIndex = 0;
            _cboCategoria.SelectedIndexChanged += (s, e) => AplicarFiltro();
            toolbar.Controls.Add(lblBuscar);
            toolbar.Controls.Add(_txtBuscar);
            toolbar.Controls.Add(lblCat);
            toolbar.Controls.Add(_cboCategoria);

            Panel header = new Panel { Dock = DockStyle.Top, Height = 58, BackColor = Tema.FondoApp };
            _lblTitulo = new Label { Text = "Órdenes de trabajo", Location = new Point(0, 0) };
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
            if (formato != null) col.DefaultCellStyle.Format = formato;
            return col;
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

        private void CargarOrdenes()
        {
            _ordenes = _service.ObtenerOrdenesTrabajo() ?? new List<OrdenTrabajoDto>();
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            string q = (_txtBuscar.Text ?? "").Trim().ToLower();
            string cat = _cboCategoria.SelectedItem != null ? _cboCategoria.SelectedItem.ToString() : "Todas";

            List<OrdenTrabajoDto> visibles = _ordenes.Where(o =>
                (cat == "Todas" || string.Equals(o.Categoria, cat, StringComparison.OrdinalIgnoreCase)) &&
                (q.Length == 0 ||
                 ((o.Cliente ?? "") + " " + (o.TecnicoAsignado ?? "") + " " +
                  (o.TipoServicio ?? "") + " " + o.NumeroOrden).ToLower().Contains(q))
            ).ToList();

            _grilla.DataSource = null;
            _grilla.DataSource = visibles;

            bool sinDatos = _ordenes.Count == 0;
            _grilla.Visible = !sinDatos;
            _lblVacio.Visible = sinDatos;

            ActualizarContador(visibles.Count);
        }

        private void ActualizarContador(int visibles)
        {
            int total = _ordenes.Count;
            if (total == visibles)
                _lblContador.Text = total == 1 ? "1 orden" : total + " órdenes";
            else
                _lblContador.Text = visibles + " de " + total + " órdenes";
        }
    }
}
