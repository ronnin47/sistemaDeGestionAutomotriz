using System;
using System.Drawing;
using System.Windows.Forms;
using sistemaDeGestionAutomotriz.Models;
using sistemaDeGestionAutomotriz.UI;

namespace sistemaDeGestionAutomotriz.Forms
{
    // Ventana de alta y edición de un cliente. Junta y valida los datos y los deja
    // en la propiedad Cliente; quien la abre decide si agrega o actualiza.
    public class FormCliente : Form
    {
        private TextBox _txtNombre, _txtApellido, _txtDni, _txtTelefono, _txtEmail, _txtDireccion;
        private Button _btnGuardar, _btnCancelar;
        private readonly ErrorProvider _errores = new ErrorProvider();
        private readonly int _clienteIdEdicion;   // 0 = cliente nuevo

        public Cliente Cliente { get; private set; }

        public FormCliente() : this(null) { }

        public FormCliente(Cliente clienteExistente)
        {
            _clienteIdEdicion = clienteExistente != null ? clienteExistente.ClienteId : 0;
            ConstruirUI(clienteExistente != null);
            if (clienteExistente != null) Precargar(clienteExistente);
        }

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

            AgregarCampo("Nombre", 24, 20, 150, out _txtNombre);
            AgregarCampo("Apellido", 186, 20, 150, out _txtApellido);
            AgregarCampo("DNI", 24, 76, 150, out _txtDni);
            AgregarCampo("Teléfono", 186, 76, 150, out _txtTelefono);
            AgregarCampo("Email", 24, 132, 312, out _txtEmail);
            AgregarCampo("Dirección", 24, 188, 312, out _txtDireccion);

            _btnGuardar = new Button { Text = "Guardar", Size = new Size(96, 34), Location = new Point(240, 258), Anchor = AnchorStyles.Bottom | AnchorStyles.Right };
            Tema.EstiloBotonPrimario(_btnGuardar);
            _btnGuardar.Click += (s, e) => Guardar();

            _btnCancelar = new Button { Text = "Cancelar", Size = new Size(96, 34), Location = new Point(136, 258), Anchor = AnchorStyles.Bottom | AnchorStyles.Right, DialogResult = DialogResult.Cancel };
            Tema.EstiloBotonSecundario(_btnCancelar);

            Controls.Add(_btnGuardar);
            Controls.Add(_btnCancelar);

            AcceptButton = _btnGuardar;
            CancelButton = _btnCancelar;
        }

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

        private void Precargar(Cliente c)
        {
            _txtNombre.Text = c.Nombre;
            _txtApellido.Text = c.Apellido;
            _txtDni.Text = c.Dni;
            _txtTelefono.Text = c.Telefono;
            _txtEmail.Text = c.Email;
            _txtDireccion.Text = c.Direccion;
        }

        private void Guardar()
        {
            _errores.Clear();
            bool valido = true;

            if (string.IsNullOrWhiteSpace(_txtNombre.Text)) { _errores.SetError(_txtNombre, "El nombre es obligatorio"); valido = false; }
            if (string.IsNullOrWhiteSpace(_txtApellido.Text)) { _errores.SetError(_txtApellido, "El apellido es obligatorio"); valido = false; }
            if (string.IsNullOrWhiteSpace(_txtDni.Text)) { _errores.SetError(_txtDni, "El DNI es obligatorio"); valido = false; }

            if (!valido) return;

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
