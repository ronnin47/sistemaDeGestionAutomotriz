using System;
using System.Drawing;
using System.Windows.Forms;
using sistemaDeGestionAutomotriz.Models;
using sistemaDeGestionAutomotriz.UI;

namespace sistemaDeGestionAutomotriz.Forms
{
    /// <summary>
    /// Ventana de alta y edición de un cliente. Reúne los datos, los valida y los
    /// deja disponibles en <see cref="Cliente"/>. No habla con la base: quien la
    /// abre decide si llama a Agregar o a Modificar (separación UI / lógica).
    /// </summary>
    public class FormCliente : Form
    {
        private TextBox _txtNombre, _txtApellido, _txtDni, _txtTelefono, _txtEmail, _txtDireccion;
        private Button _btnGuardar, _btnCancelar;
        private readonly ErrorProvider _errores = new ErrorProvider();
        private readonly int _clienteIdEdicion;   // 0 = es un cliente nuevo

        /// <summary>El cliente cargado. Tiene valor cuando la ventana cierra con OK.</summary>
        public Cliente Cliente { get; private set; }

        public FormCliente() : this(null) { }

        public FormCliente(Cliente clienteExistente)
        {
            _clienteIdEdicion = clienteExistente != null ? clienteExistente.ClienteId : 0;
            ConstruirUI(clienteExistente != null);
            if (clienteExistente != null) Precargar(clienteExistente);
        }

        /// <summary>Arma el formulario por código, aplicando el tema.</summary>
        private void ConstruirUI(bool esEdicion)
        {
            Text = esEdicion ? "Editar cliente" : "Nuevo cliente";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(360, 312);
            BackColor = Tema.FondoApp;
            Font = Tema.FuenteCuerpo;

            // Fila 1: Nombre y Apellido
            AgregarCampo("Nombre", 24, 20, 150, out _txtNombre);
            AgregarCampo("Apellido", 186, 20, 150, out _txtApellido);
            // Fila 2: DNI y Teléfono
            AgregarCampo("DNI", 24, 76, 150, out _txtDni);
            AgregarCampo("Teléfono", 186, 76, 150, out _txtTelefono);
            // Fila 3: Email (ancho completo)
            AgregarCampo("Email", 24, 132, 312, out _txtEmail);
            // Fila 4: Dirección (ancho completo)
            AgregarCampo("Dirección", 24, 188, 312, out _txtDireccion);

            // Botones
            _btnGuardar = new Button { Text = "Guardar", Size = new Size(96, 34), Location = new Point(240, 258), Anchor = AnchorStyles.Bottom | AnchorStyles.Right };
            Tema.EstiloBotonPrimario(_btnGuardar);
            _btnGuardar.Click += (s, e) => Guardar();

            _btnCancelar = new Button { Text = "Cancelar", Size = new Size(96, 34), Location = new Point(136, 258), Anchor = AnchorStyles.Bottom | AnchorStyles.Right, DialogResult = DialogResult.Cancel };
            Tema.EstiloBotonSecundario(_btnCancelar);

            Controls.Add(_btnGuardar);
            Controls.Add(_btnCancelar);

            // Enter = Guardar, Esc = Cancelar
            AcceptButton = _btnGuardar;
            CancelButton = _btnCancelar;
        }

        /// <summary>Crea una etiqueta + su caja de texto y los agrega al formulario.</summary>
        private void AgregarCampo(string titulo, int x, int y, int ancho, out TextBox caja)
        {
            Label etiqueta = new Label
            {
                Text = titulo,
                AutoSize = true,
                Location = new Point(x, y),
                ForeColor = Tema.TextoSecundario,
                Font = Tema.FuenteEtiqueta
            };
            caja = new TextBox { Location = new Point(x, y + 18), Width = ancho };
            Tema.EstiloInput(caja);
            Controls.Add(etiqueta);
            Controls.Add(caja);
        }

        /// <summary>Rellena los campos cuando estamos editando un cliente existente.</summary>
        private void Precargar(Cliente c)
        {
            _txtNombre.Text = c.Nombre;
            _txtApellido.Text = c.Apellido;
            _txtDni.Text = c.Dni;
            _txtTelefono.Text = c.Telefono;
            _txtEmail.Text = c.Email;
            _txtDireccion.Text = c.Direccion;
        }

        /// <summary>Valida los campos obligatorios y, si está todo bien, arma el Cliente y cierra con OK.</summary>
        private void Guardar()
        {
            _errores.Clear();
            bool valido = true;

            // Validación: marcamos el campo con un ícono rojo (patrón de feedback), sin popups.
            if (string.IsNullOrWhiteSpace(_txtNombre.Text)) { _errores.SetError(_txtNombre, "El nombre es obligatorio"); valido = false; }
            if (string.IsNullOrWhiteSpace(_txtApellido.Text)) { _errores.SetError(_txtApellido, "El apellido es obligatorio"); valido = false; }
            if (string.IsNullOrWhiteSpace(_txtDni.Text)) { _errores.SetError(_txtDni, "El DNI es obligatorio"); valido = false; }

            if (!valido) return;   // no cerramos: el usuario corrige y reintenta

            Cliente = new Cliente
            {
                ClienteId = _clienteIdEdicion,
                Nombre = _txtNombre.Text.Trim(),
                Apellido = _txtApellido.Text.Trim(),
                Dni = _txtDni.Text.Trim(),
                Telefono = _txtTelefono.Text.Trim(),
                Email = _txtEmail.Text.Trim(),
                Direccion = _txtDireccion.Text.Trim()
            };

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
