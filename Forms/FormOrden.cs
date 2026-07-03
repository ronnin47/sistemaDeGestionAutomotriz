using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using sistemaDeGestionAutomotriz.Models;
using sistemaDeGestionAutomotriz.Services;
using sistemaDeGestionAutomotriz.UI;

namespace sistemaDeGestionAutomotriz.Forms
{
    // Alta de una orden de trabajo. Selector de grupo (Módulo / Cerrajería / Instalación)
    // con botones; los campos específicos cambian según el grupo elegido.
    public class FormOrden : Form
    {
        private readonly OrdenTrabajoService _ordenService = new OrdenTrabajoService();

        private Button _btnModulo, _btnCerrajeria, _btnInstalacion;
        private string _grupo = "Módulo";
        private ComboBox _cboCliente, _cboTecnico;
        private Panel _panelEspecifico;
        private TextBox _txtObservaciones;
        private Button _btnGuardar, _btnCancelar;
        private readonly Dictionary<string, TextBox> _campos = new Dictionary<string, TextBox>();

        public FormOrden()
        {
            ConstruirUI();
            CargarCombos();
            SeleccionarGrupo("Módulo");
        }

        private void ConstruirUI()
        {
            Text = "Nueva orden";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(458, 470);
            BackColor = Tema.FondoApp;
            Font = Tema.FuenteCuerpo;

            Controls.Add(new Label { Text = "Nueva orden", Location = new Point(24, 16), AutoSize = true, Font = Tema.FuenteTitulo, ForeColor = Tema.TextoPrincipal });

            // Selector de grupo con 3 botones (segmentado).
            Etiqueta("Grupo", 24, 54);
            _btnModulo = BotonGrupo("Módulo", 24, 132);
            _btnCerrajeria = BotonGrupo("Cerrajería", 163, 132);
            _btnInstalacion = BotonGrupo("Instalación", 302, 132);

            Etiqueta("Cliente", 24, 112);
            _cboCliente = Combo(24, 130, 200);
            Etiqueta("Técnico asignado", 240, 112);
            _cboTecnico = Combo(240, 130, 194);

            _panelEspecifico = new Panel { Location = new Point(24, 170), Size = new Size(410, 158), BackColor = Tema.FondoApp };
            Controls.Add(_panelEspecifico);

            Etiqueta("Observaciones", 24, 338);
            _txtObservaciones = new TextBox { Location = new Point(24, 356), Width = 410, Height = 50, Multiline = true };
            Tema.EstiloInput(_txtObservaciones);
            Controls.Add(_txtObservaciones);

            _btnGuardar = new Button { Text = "Guardar", Size = new Size(96, 34), Location = new Point(338, 422), Anchor = AnchorStyles.Bottom | AnchorStyles.Right };
            Tema.EstiloBotonPrimario(_btnGuardar);
            _btnGuardar.Click += (s, e) => Guardar();
            _btnCancelar = new Button { Text = "Cancelar", Size = new Size(96, 34), Location = new Point(234, 422), Anchor = AnchorStyles.Bottom | AnchorStyles.Right, DialogResult = DialogResult.Cancel };
            Tema.EstiloBotonSecundario(_btnCancelar);
            Controls.Add(_btnGuardar);
            Controls.Add(_btnCancelar);
            AcceptButton = _btnGuardar;
            CancelButton = _btnCancelar;
        }

        private Button BotonGrupo(string texto, int x, int ancho)
        {
            Button b = new Button { Text = texto, Location = new Point(x, 72), Size = new Size(ancho, 34) };
            b.Click += (s, e) => SeleccionarGrupo(texto);
            Controls.Add(b);
            return b;
        }

        // Marca el grupo elegido (botón primario) y rearma los campos específicos.
        private void SeleccionarGrupo(string grupo)
        {
            _grupo = grupo;
            Tema.EstiloBotonSecundario(_btnModulo);
            Tema.EstiloBotonSecundario(_btnCerrajeria);
            Tema.EstiloBotonSecundario(_btnInstalacion);
            if (grupo == "Módulo") Tema.EstiloBotonPrimario(_btnModulo);
            else if (grupo == "Cerrajería") Tema.EstiloBotonPrimario(_btnCerrajeria);
            else Tema.EstiloBotonPrimario(_btnInstalacion);

            ArmarCamposGrupo();
        }

        private void CargarCombos()
        {
            var clientes = new ClienteService().ObtenerClientesActivos() ?? new List<Cliente>();
            var items = new List<ItemCliente>();
            foreach (Cliente c in clientes) items.Add(new ItemCliente { Cliente = c });
            _cboCliente.DataSource = items;

            var usuarios = new UsuarioService().ObtenerUsuarios() ?? new List<Usuario>();
            _cboTecnico.DisplayMember = "Nombre";
            _cboTecnico.ValueMember = "UsuarioId";
            _cboTecnico.DataSource = usuarios;
        }

        private void ArmarCamposGrupo()
        {
            _panelEspecifico.Controls.Clear();
            _campos.Clear();

            if (_grupo == "Módulo")
            {
                CampoEspecifico("TipoModulo", "Tipo de módulo", 0, 0);
                CampoEspecifico("Marca", "Marca", 215, 0);
                CampoEspecifico("Modelo", "Modelo", 0, 56);
                CampoEspecifico("TipoVehiculo", "Tipo de vehículo", 215, 56);
                CampoEspecifico("Combustible", "Combustible", 0, 112);
            }
            else // Cerrajería / Instalación: mismos campos
            {
                CampoEspecifico("TipoServicio", "Tipo de servicio", 0, 0);
                CampoEspecifico("Marca", "Marca", 215, 0);
            }
        }

        private void CampoEspecifico(string clave, string titulo, int x, int y)
        {
            _panelEspecifico.Controls.Add(new Label { Text = titulo, AutoSize = true, Location = new Point(x, y), ForeColor = Tema.TextoSecundario, Font = Tema.FuenteEtiqueta });
            TextBox t = new TextBox { Location = new Point(x, y + 18), Width = 195 };
            Tema.EstiloInput(t);
            _panelEspecifico.Controls.Add(t);
            _campos[clave] = t;
        }

        private string Campo(string clave)
        {
            return _campos.ContainsKey(clave) ? _campos[clave].Text.Trim() : "";
        }

        private void Guardar()
        {
            ItemCliente ic = _cboCliente.SelectedItem as ItemCliente;
            if (ic == null || _cboTecnico.SelectedValue == null)
            {
                Avisos.Error("Elegí un cliente y un técnico.");
                return;
            }

            Cliente cli = ic.Cliente;
            int idTecnico = (int)_cboTecnico.SelectedValue;
            bool ok;

            if (_grupo == "Módulo")
            {
                ok = _ordenService.CrearNuevaOrden(new OrdenTrabajo
                {
                    IdCliente = cli.ClienteId,
                    NombreCliente = cli.Nombre,
                    ApellidoCliente = cli.Apellido,
                    Dni = cli.Dni,
                    Telefono = cli.Telefono,
                    Email = cli.Email,
                    Direccion = cli.Direccion,
                    IdUsuarioAsignado = idTecnico,
                    TipoModulo = Campo("TipoModulo"),
                    Marca = Campo("Marca"),
                    Modelo = Campo("Modelo"),
                    TipoVehiculo = Campo("TipoVehiculo"),
                    Combustible = Campo("Combustible"),
                    Observaciones = _txtObservaciones.Text.Trim(),
                    Estado = "Ingresado",
                    Garantia = false
                });
            }
            else if (_grupo == "Cerrajería")
            {
                ok = _ordenService.CrearNuevaOrdenCerrajeria(new OrdenTrabajoCerrajeria
                {
                    IdCliente = cli.ClienteId,
                    NombreCliente = cli.Nombre,
                    ApellidoCliente = cli.Apellido,
                    Dni = cli.Dni,
                    Telefono = cli.Telefono,
                    Email = cli.Email,
                    Direccion = cli.Direccion,
                    IdUsuarioAsignado = idTecnico,
                    TipoServicio = Campo("TipoServicio"),
                    Marca = Campo("Marca"),
                    Observaciones = _txtObservaciones.Text.Trim(),
                    Estado = "Ingresado",
                    Garantia = false
                });
            }
            else
            {
                ok = _ordenService.CrearNuevaOrdenInstalacion(new OrdenTrabajoInstalacion
                {
                    IdCliente = cli.ClienteId,
                    NombreCliente = cli.Nombre,
                    ApellidoCliente = cli.Apellido,
                    Dni = cli.Dni,
                    Telefono = cli.Telefono,
                    Email = cli.Email,
                    Direccion = cli.Direccion,
                    IdUsuarioAsignado = idTecnico,
                    TipoServicio = Campo("TipoServicio"),
                    Marca = Campo("Marca"),
                    Observaciones = _txtObservaciones.Text.Trim(),
                    Estado = "Ingresado",
                    Garantia = false
                });
            }

            if (ok)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private class ItemCliente
        {
            public Cliente Cliente;
            public override string ToString()
            {
                return Cliente.Nombre + " " + Cliente.Apellido;
            }
        }

        private void Etiqueta(string texto, int x, int y)
        {
            Controls.Add(new Label { Text = texto, AutoSize = true, Location = new Point(x, y), ForeColor = Tema.TextoSecundario, Font = Tema.FuenteEtiqueta });
        }

        private ComboBox Combo(int x, int y, int ancho)
        {
            ComboBox cbo = new ComboBox { Location = new Point(x, y), Width = ancho, DropDownStyle = ComboBoxStyle.DropDownList };
            Tema.EstiloCombo(cbo);
            Controls.Add(cbo);
            return cbo;
        }
    }
}
