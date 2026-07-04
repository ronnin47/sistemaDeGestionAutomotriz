using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using sistemaDeGestionAutomotriz.Models;
using sistemaDeGestionAutomotriz.Services;
using sistemaDeGestionAutomotriz.Forms;
using sistemaDeGestionAutomotriz.UI;

namespace sistemaDeGestionAutomotriz.UserControls
{
    public partial class VentasControl : UserControl
    {
        private readonly VentaService _service = new VentaService();
        private List<VentaInsumosDto> _ventas = new List<VentaInsumosDto>();

        private Label _lblTitulo;
        private Label _lblContador;
        private Button _btnNueva;
        private TextBox _txtBuscar;
        private DataGridView _grilla;
        private Label _lblVacio;
        private DataGridViewButtonColumn _colBaja;

        public VentasControl()
        {
            InitializeComponent();
            ConstruirUI();
            CargarVentas();
        }

        private void ConstruirUI()
        {
            BackColor = Tema.FondoApp;
            Padding = new Padding(Tema.PaddingPantalla);

            _grilla = new DataGridView { Dock = DockStyle.Fill };
            Tema.EstiloTabla(_grilla);
            _grilla.AutoGenerateColumns = false;
            _grilla.Columns.Add(Columna("NumeroVenta", "N°"));
            _grilla.Columns.Add(Columna("Cliente", "Cliente"));
            _grilla.Columns.Add(Columna("Insumo", "Insumo"));
            _grilla.Columns.Add(Columna("Cantidad", "Cantidad", DataGridViewContentAlignment.MiddleRight));
            _grilla.Columns.Add(Columna("Total", "Total", DataGridViewContentAlignment.MiddleRight, "C0"));
            _grilla.Columns.Add(Columna("Fecha", "Fecha", null, "dd/MM/yyyy"));
            _colBaja = ColumnaAccion(Tema.Iconos.Eliminar, Tema.CerradoTexto);
            _grilla.Columns.Add(_colBaja);
            _grilla.CellContentClick += Grilla_CellContentClick;

            _lblVacio = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Todavía no hay ventas registradas.",
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
            _lblTitulo = new Label { Text = "Ventas de insumos", Location = new Point(0, 0) };
            Tema.EstiloTituloPantalla(_lblTitulo);
            _lblContador = new Label { Location = new Point(2, 34) };
            Tema.EstiloSubtitulo(_lblContador);
            _btnNueva = new Button { Text = "+  Nueva venta", Size = new Size(140, 36) };
            Tema.EstiloBotonPrimario(_btnNueva);
            _btnNueva.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _btnNueva.Click += (s, e) => NuevaVenta();
            header.Controls.Add(_lblTitulo);
            header.Controls.Add(_lblContador);
            header.Controls.Add(_btnNueva);
            header.Resize += (s, e) => { _btnNueva.Left = header.ClientSize.Width - _btnNueva.Width; };

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

        private void CargarVentas()
        {
            // Las ventas anuladas (baja) no se muestran.
            _ventas = (_service.ObtenerVentaInsumos() ?? new List<VentaInsumosDto>())
                .Where(v => !string.Equals(v.Estado, "Anulada", StringComparison.OrdinalIgnoreCase)).ToList();
            AplicarFiltro();
        }

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

        private void ActualizarContador(int visibles)
        {
            int total = _ventas.Count;
            if (total == visibles)
                _lblContador.Text = total == 1 ? "1 venta registrada" : total + " ventas registradas";
            else
                _lblContador.Text = visibles + " de " + total + " ventas";
        }

        private void NuevaVenta()
        {
            using (FormVenta form = new FormVenta())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // RegistrarNuevaVenta descuenta stock y devuelve true/false;
                    // en éxito no muestra cartel, así que refrescamos en silencio.
                    if (_service.RegistrarNuevaVenta(form.Venta))
                        CargarVentas();
                }
            }
        }

        private DataGridViewButtonColumn ColumnaAccion(string glifo, Color color)
        {
            var col = new DataGridViewButtonColumn
            {
                Text = glifo,
                UseColumnTextForButtonValue = true,
                HeaderText = "",
                Width = 44,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                FlatStyle = FlatStyle.Flat
            };
            col.DefaultCellStyle.Font = Tema.FuenteIcono(11F);
            col.DefaultCellStyle.ForeColor = color;
            col.DefaultCellStyle.SelectionForeColor = color;
            return col;
        }

        private void Grilla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            VentaInsumosDto venta = _grilla.Rows[e.RowIndex].DataBoundItem as VentaInsumosDto;
            if (venta == null) return;

            if (e.ColumnIndex == _colBaja.Index) DarDeBajaVenta(venta);
        }

        private void DarDeBajaVenta(VentaInsumosDto venta)
        {
            bool confirma = Avisos.Confirmar(
                "¿Dar de baja la venta N° " + venta.NumeroVenta + " de " + venta.Cliente +
                "?\r\nSe repondrá el stock del insumo.");
            if (!confirma) return;

            _service.AnularVenta(venta.NumeroVenta, venta.idKit, venta.Cantidad);
            CargarVentas();
        }
    }
}
