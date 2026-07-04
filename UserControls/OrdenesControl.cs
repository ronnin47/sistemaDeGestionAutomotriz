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
    public partial class OrdenesControl : UserControl
    {
        private readonly OrdenTrabajoService _service = new OrdenTrabajoService();
        private List<OrdenTrabajoDto> _ordenes = new List<OrdenTrabajoDto>();

        private Label _lblTitulo, _lblContador;
        private Button _btnNueva;
        private TextBox _txtBuscar;
        private ComboBox _cboCategoria;
        private DataGridView _grilla;
        private Label _lblVacio;
        private DataGridViewButtonColumn _colBaja;

        private Label _stActivas, _stModulo, _stCerrajeria, _stInstalacion;
        private Label _dCliente, _dDni, _dTelefono, _dVehiculo, _dTecnico, _dEstado, _dDiagnostico, _dObservaciones;

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

            // Cuerpo
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
            _colBaja = ColumnaAccion(Tema.Iconos.Eliminar, Tema.CerradoTexto);
            _grilla.Columns.Add(_colBaja);
            _grilla.CellFormatting += Grilla_CellFormatting;
            _grilla.CellContentClick += Grilla_CellContentClick;
            _grilla.SelectionChanged += (s, e) => MostrarDetalle(_grilla.CurrentRow != null ? _grilla.CurrentRow.DataBoundItem as OrdenTrabajoDto : null);

            _lblVacio = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "No hay órdenes para mostrar.",
                ForeColor = Tema.TextoSecundario,
                Font = Tema.FuenteCuerpo,
                Visible = false
            };

            Panel panelDetalle = ConstruirPanelDetalle();

            // Barra de herramientas: buscador + filtro por categoría
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

            // Tarjetas de resumen
            Panel stats = new Panel { Dock = DockStyle.Top, Height = 66, BackColor = Tema.FondoApp };
            _stActivas = TarjetaStat(stats, "Activas", 0);
            _stModulo = TarjetaStat(stats, "Módulo", 162);
            _stCerrajeria = TarjetaStat(stats, "Cerrajería", 324);
            _stInstalacion = TarjetaStat(stats, "Instalaciones", 486);

            // Encabezado
            Panel header = new Panel { Dock = DockStyle.Top, Height = 58, BackColor = Tema.FondoApp };
            _lblTitulo = new Label { Text = "Órdenes de trabajo", Location = new Point(0, 0) };
            Tema.EstiloTituloPantalla(_lblTitulo);
            _lblContador = new Label { Location = new Point(2, 34) };
            Tema.EstiloSubtitulo(_lblContador);
            _btnNueva = new Button { Text = "+  Nueva orden", Size = new Size(140, 36) };
            Tema.EstiloBotonPrimario(_btnNueva);
            _btnNueva.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _btnNueva.Click += (s, e) => NuevaOrden();
            header.Controls.Add(_lblTitulo);
            header.Controls.Add(_lblContador);
            header.Controls.Add(_btnNueva);
            header.Resize += (s, e) => { _btnNueva.Left = header.ClientSize.Width - _btnNueva.Width; };

            // Orden de agregado (define el apilado del docking).
            Controls.Add(_grilla);
            Controls.Add(_lblVacio);
            Controls.Add(panelDetalle);
            Controls.Add(toolbar);
            Controls.Add(stats);
            Controls.Add(header);
        }

        private Label TarjetaStat(Panel contenedor, string titulo, int x)
        {
            Panel card = new Panel { Location = new Point(x, 4), Size = new Size(150, 58), BackColor = Tema.Superficie };
            card.Controls.Add(new Label { Text = titulo, Location = new Point(10, 8), AutoSize = true, ForeColor = Tema.TextoSecundario, Font = Tema.FuenteMenor });
            Label valor = new Label { Text = "0", Location = new Point(10, 24), AutoSize = true, ForeColor = Tema.TextoPrincipal, Font = new Font("Segoe UI", 16F, FontStyle.Bold) };
            card.Controls.Add(valor);
            contenedor.Controls.Add(card);
            return valor;
        }

        private Panel ConstruirPanelDetalle()
        {
            Panel p = new Panel { Dock = DockStyle.Bottom, Height = 138, BackColor = Tema.Superficie };

            Panel cuerpo = new Panel { Dock = DockStyle.Fill, BackColor = Tema.Superficie };

            // Lado derecho: Diagnóstico | Observaciones (rellenan el espacio, 50/50)
            Panel derecha = new Panel { Dock = DockStyle.Fill, BackColor = Tema.Superficie, Padding = new Padding(24, 34, 16, 12) };
            TableLayoutPanel notas = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, BackColor = Tema.Superficie };
            notas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            notas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            notas.Controls.Add(BloqueNota("Diagnóstico", out _dDiagnostico), 0, 0);
            notas.Controls.Add(BloqueNota("Observaciones", out _dObservaciones), 1, 0);
            derecha.Controls.Add(notas);

            Panel divisor = new Panel { Dock = DockStyle.Left, Width = 1, BackColor = Tema.Borde };

            // Lado izquierdo: datos del cliente
            Panel izquierda = new Panel { Dock = DockStyle.Left, Width = 620, BackColor = Tema.Superficie };
            izquierda.Controls.Add(new Label { Text = "DETALLE DE LA ORDEN SELECCIONADA", Location = new Point(16, 10), AutoSize = true, ForeColor = Tema.TextoSecundario, Font = Tema.FuenteMenor });
            _dCliente = DatoStacked(izquierda, "Cliente", 16, 34);
            _dTecnico = DatoStacked(izquierda, "Técnico", 220, 34);
            _dEstado = DatoStacked(izquierda, "Estado", 424, 34);
            _dTelefono = DatoStacked(izquierda, "Teléfono", 16, 78);
            _dVehiculo = DatoStacked(izquierda, "Vehículo", 220, 78);
            _dDni = DatoStacked(izquierda, "DNI", 424, 78);

            cuerpo.Controls.Add(derecha);
            cuerpo.Controls.Add(divisor);
            cuerpo.Controls.Add(izquierda);

            p.Controls.Add(cuerpo);
            p.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 1, BackColor = Tema.Borde }); // separador con la tabla
            return p;
        }

        private Panel BloqueNota(string caption, out Label valor)
        {
            Panel b = new Panel { Dock = DockStyle.Fill, BackColor = Tema.Superficie, Padding = new Padding(0, 0, 16, 0) };
            valor = new Label { Dock = DockStyle.Fill, AutoSize = false, ForeColor = Tema.TextoPrincipal, Font = Tema.FuenteCuerpo, Text = "" };
            Label cap = new Label { Dock = DockStyle.Top, Height = 18, Text = caption, ForeColor = Tema.TextoSecundario, Font = Tema.FuenteMenor };
            b.Controls.Add(valor);
            b.Controls.Add(cap);
            return b;
        }

        private Label DatoStacked(Panel p, string caption, int x, int y, int ancho = 0)
        {
            p.Controls.Add(new Label { Text = caption, Location = new Point(x, y), AutoSize = true, ForeColor = Tema.TextoSecundario, Font = Tema.FuenteMenor });
            Label v = new Label { Location = new Point(x, y + 17), ForeColor = Tema.TextoPrincipal, Font = Tema.FuenteCuerpo, Text = "" };
            if (ancho > 0) { v.AutoSize = false; v.Size = new Size(ancho, 30); }
            else v.AutoSize = true;
            p.Controls.Add(v);
            return v;
        }

        private void MostrarDetalle(OrdenTrabajoDto o)
        {
            if (o == null)
            {
                foreach (Label l in new[] { _dCliente, _dTecnico, _dEstado, _dDni, _dTelefono, _dVehiculo, _dDiagnostico, _dObservaciones })
                    l.Text = "";
                return;
            }
            _dCliente.Text = o.Cliente;
            _dTecnico.Text = o.TecnicoAsignado;
            _dEstado.Text = o.Estado;
            _dDni.Text = o.Dni;
            _dTelefono.Text = o.Telefono;
            _dVehiculo.Text = o.Vehiculo;
            _dDiagnostico.Text = o.Diagnostico;
            _dObservaciones.Text = o.Observaciones;
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
            ActualizarStats();
            AplicarFiltro();
        }

        private void ActualizarStats()
        {
            _stActivas.Text = _ordenes.Count(o => !EsFinal(o.Estado)).ToString();
            _stModulo.Text = _ordenes.Count(o => Es(o.Categoria, "Módulo")).ToString();
            _stCerrajeria.Text = _ordenes.Count(o => Es(o.Categoria, "Cerrajería")).ToString();
            _stInstalacion.Text = _ordenes.Count(o => Es(o.Categoria, "Instalaciones")).ToString();
        }

        private bool EsFinal(string estado)
        {
            return estado == "Entregado" || estado == "Dado de baja" || estado == "Rechazado por cliente";
        }

        private bool Es(string valor, string esperado)
        {
            return string.Equals(valor, esperado, StringComparison.OrdinalIgnoreCase);
        }

        private void AplicarFiltro()
        {
            string q = (_txtBuscar.Text ?? "").Trim().ToLower();
            string cat = _cboCategoria.SelectedItem != null ? _cboCategoria.SelectedItem.ToString() : "Todas";

            List<OrdenTrabajoDto> visibles = _ordenes.Where(o =>
                (cat == "Todas" || Es(o.Categoria, cat)) &&
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

        private void NuevaOrden()
        {
            using (FormOrden form = new FormOrden())
            {
                if (form.ShowDialog() == DialogResult.OK)
                    CargarOrdenes();
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
            OrdenTrabajoDto orden = _grilla.Rows[e.RowIndex].DataBoundItem as OrdenTrabajoDto;
            if (orden == null) return;

            if (e.ColumnIndex == _colBaja.Index) DarDeBajaOrden(orden);
        }

        private void DarDeBajaOrden(OrdenTrabajoDto orden)
        {
            bool confirma = Avisos.Confirmar(
                "¿Dar de baja la orden N° " + orden.NumeroOrden + " de " + orden.Cliente +
                "?\r\nDejará de aparecer en la lista.");
            if (!confirma) return;

            _service.AnularOrden(orden.NumeroOrden);
            CargarOrdenes();
        }
    }
}
