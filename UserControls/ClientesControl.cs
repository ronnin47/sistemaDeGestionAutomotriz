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
    public partial class ClientesControl : UserControl
    {
        private readonly ClienteService _service = new ClienteService();
        private List<Cliente> _clientes = new List<Cliente>();

        private Label _lblTitulo;
        private Label _lblContador;
        private Button _btnNuevo;
        private TextBox _txtBuscar;
        private DataGridView _grilla;
        private Label _lblVacio;
        private DataGridViewButtonColumn _colEditar;
        private DataGridViewButtonColumn _colEliminar;

        public ClientesControl()
        {
            InitializeComponent();
            ConstruirUI();
            CargarClientes();
        }

        private void ConstruirUI()
        {
            BackColor = Tema.FondoApp;
            Padding = new Padding(Tema.PaddingPantalla);

            _grilla = new DataGridView { Dock = DockStyle.Fill };
            Tema.EstiloTabla(_grilla);
            _grilla.AutoGenerateColumns = false;
            _grilla.Columns.Add(ColumnaTexto("Nombre", "Nombre"));
            _grilla.Columns.Add(ColumnaTexto("Apellido", "Apellido"));
            _grilla.Columns.Add(ColumnaTexto("Telefono", "Teléfono"));
            _grilla.Columns.Add(ColumnaTexto("Email", "Email"));
            _grilla.Columns.Add(ColumnaTexto("Dni", "DNI"));
            _colEditar = ColumnaAccion(Tema.Iconos.Editar);
            _colEliminar = ColumnaAccion(Tema.Iconos.Eliminar);
            _grilla.Columns.Add(_colEditar);
            _grilla.Columns.Add(_colEliminar);
            _grilla.CellContentClick += Grilla_CellContentClick;

            _lblVacio = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Todavía no hay clientes.\r\nTocá \"Nuevo cliente\" para agregar el primero.",
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
            _lblTitulo = new Label { Text = "Clientes", Location = new Point(0, 0) };
            Tema.EstiloTituloPantalla(_lblTitulo);
            _lblContador = new Label { Location = new Point(2, 34) };
            Tema.EstiloSubtitulo(_lblContador);
            _btnNuevo = new Button { Text = "+  Nuevo cliente", Size = new Size(150, 36) };
            Tema.EstiloBotonPrimario(_btnNuevo);
            _btnNuevo.Click += (s, e) => NuevoCliente();
            header.Controls.Add(_lblTitulo);
            header.Controls.Add(_lblContador);
            header.Controls.Add(_btnNuevo);
            header.Resize += (s, e) => { _btnNuevo.Left = header.ClientSize.Width - _btnNuevo.Width; };

            // Fill primero; los Top después (el último queda arriba de todo).
            Controls.Add(_grilla);
            Controls.Add(_lblVacio);
            Controls.Add(toolbar);
            Controls.Add(header);
        }

        private DataGridViewTextBoxColumn ColumnaTexto(string propiedad, string titulo)
        {
            return new DataGridViewTextBoxColumn
            {
                DataPropertyName = propiedad,
                HeaderText = titulo,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
        }

        private DataGridViewButtonColumn ColumnaAccion(string glifo)
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
            col.DefaultCellStyle.ForeColor = Tema.TextoSecundario;
            return col;
        }

        private void CargarClientes()
        {
            _clientes = _service.ObtenerClientesActivos() ?? new List<Cliente>();
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            string q = (_txtBuscar.Text ?? "").Trim().ToLower();
            List<Cliente> visibles = _clientes;
            if (q.Length > 0)
            {
                visibles = _clientes.Where(c =>
                    ((c.Nombre ?? "") + " " + (c.Apellido ?? "") + " " +
                     (c.Dni ?? "") + " " + (c.Telefono ?? "")).ToLower().Contains(q)).ToList();
            }

            _grilla.DataSource = null;
            _grilla.DataSource = visibles;

            bool sinClientes = _clientes.Count == 0;
            _grilla.Visible = !sinClientes;
            _lblVacio.Visible = sinClientes;

            ActualizarContador(visibles.Count);
        }

        private void ActualizarContador(int visibles)
        {
            int total = _clientes.Count;
            if (total == visibles)
                _lblContador.Text = total == 1 ? "1 cliente registrado" : total + " clientes registrados";
            else
                _lblContador.Text = visibles + " de " + total + " clientes";
        }

        private void NuevoCliente()
        {
            using (FormCliente form = new FormCliente())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _service.AgregarCliente(form.Cliente);
                    CargarClientes();
                }
            }
        }

        private void Grilla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Cliente cliente = _grilla.Rows[e.RowIndex].DataBoundItem as Cliente;
            if (cliente == null) return;

            if (e.ColumnIndex == _colEditar.Index) EditarCliente(cliente);
            else if (e.ColumnIndex == _colEliminar.Index) DarDeBaja(cliente);
        }

        private void EditarCliente(Cliente cliente)
        {
            using (FormCliente form = new FormCliente(cliente))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _service.ActualizarCliente(form.Cliente);
                    CargarClientes();
                }
            }
        }

        private void DarDeBaja(Cliente cliente)
        {
            bool confirma = Avisos.Confirmar(
                "¿Dar de baja a " + cliente.Nombre + " " + cliente.Apellido +
                "?\r\nDejará de aparecer en la lista de clientes activos.");
            if (!confirma) return;

            _service.DarDeBajaCliente(cliente.ClienteId);
            CargarClientes();
        }
    }
}
