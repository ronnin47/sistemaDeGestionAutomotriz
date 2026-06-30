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
        // El servicio es el "puente" con el backend del compañero.
        private readonly ClienteService _service = new ClienteService();
        // Guardamos la lista completa en memoria para poder filtrar sin volver a la base.
        private List<Cliente> _clientes = new List<Cliente>();

        // Controles que armamos por código.
        private Label _lblTitulo;
        private Label _lblContador;
        private Button _btnNuevo;
        private TextBox _txtBuscar;
        private DataGridView _grilla;
        private Label _lblVacio;

        public ClientesControl()
        {
            InitializeComponent();
            ConstruirUI();
            CargarClientes();
        }

        /// <summary>Arma toda la pantalla por código, aplicando el tema y el molde común.</summary>
        private void ConstruirUI()
        {
            BackColor = Tema.FondoApp;
            Padding = new Padding(Tema.PaddingPantalla);

            // ---- Cuerpo: la grilla (se agrega primero para que el Fill ocupe lo que sobra) ----
            _grilla = new DataGridView { Dock = DockStyle.Fill };
            Tema.EstiloTabla(_grilla);
            _grilla.AutoGenerateColumns = false;   // definimos nosotros las columnas, no todas las del modelo
            _grilla.Columns.Add(ColumnaTexto("Nombre", "Nombre"));
            _grilla.Columns.Add(ColumnaTexto("Apellido", "Apellido"));
            _grilla.Columns.Add(ColumnaTexto("Telefono", "Teléfono"));
            _grilla.Columns.Add(ColumnaTexto("Email", "Email"));
            _grilla.Columns.Add(ColumnaTexto("Dni", "DNI"));

            // ---- Estado vacío (mismo lugar que la grilla; mostramos uno u otro) ----
            _lblVacio = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Todavía no hay clientes.\r\nTocá \"Nuevo cliente\" para agregar el primero.",
                ForeColor = Tema.TextoSecundario,
                Font = Tema.FuenteCuerpo,
                Visible = false
            };

            // ---- Barra de herramientas: buscador ----
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

            // ---- Encabezado: título + contador + botón "Nuevo cliente" ----
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
            // Mantener el botón pegado a la derecha cuando cambia el tamaño de la pantalla.
            header.Resize += (s, e) => { _btnNuevo.Left = header.ClientSize.Width - _btnNuevo.Width; };

            // Orden de agregado: el Fill primero; los Top después (el último queda más arriba).
            Controls.Add(_grilla);
            Controls.Add(_lblVacio);
            Controls.Add(toolbar);
            Controls.Add(header);
        }

        /// <summary>Crea una columna de texto de la grilla mapeada a una propiedad del Cliente.</summary>
        private DataGridViewTextBoxColumn ColumnaTexto(string propiedad, string titulo)
        {
            return new DataGridViewTextBoxColumn
            {
                DataPropertyName = propiedad,   // de qué campo del Cliente saca el valor
                HeaderText = titulo,            // qué muestra en el encabezado
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
        }

        /// <summary>Trae los clientes del backend y refresca la pantalla.</summary>
        private void CargarClientes()
        {
            // Si falla la conexión, el servicio del compañero ya muestra su propio aviso
            // y devuelve una lista vacía, así que acá no hace falta otro try/catch.
            _clientes = _service.ObtenerClientes() ?? new List<Cliente>();
            AplicarFiltro();
        }

        /// <summary>Filtra la lista según el buscador y la vuelca en la grilla.</summary>
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

            // Si no hay ningún cliente, mostramos el estado vacío en lugar de la grilla.
            bool sinClientes = _clientes.Count == 0;
            _grilla.Visible = !sinClientes;
            _lblVacio.Visible = sinClientes;

            ActualizarContador(visibles.Count);
        }

        /// <summary>Actualiza el subtítulo con la cantidad de clientes (o cuántos coinciden con el filtro).</summary>
        private void ActualizarContador(int visibles)
        {
            int total = _clientes.Count;
            if (total == visibles)
                _lblContador.Text = total == 1 ? "1 cliente registrado" : total + " clientes registrados";
            else
                _lblContador.Text = visibles + " de " + total + " clientes";
        }

        /// <summary>Abre el formulario de alta y, si se confirma, lo agrega vía el backend.</summary>
        private void NuevoCliente()
        {
            using (FormCliente form = new FormCliente())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // AgregarCliente ya muestra su propio aviso de éxito/error.
                    _service.AgregarCliente(form.Cliente);
                    CargarClientes();   // refrescamos para ver el nuevo cliente
                }
            }
        }
    }
}
