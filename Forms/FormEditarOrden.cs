using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using sistemaDeGestionAutomotriz.Models;
using sistemaDeGestionAutomotriz.Services;
using sistemaDeGestionAutomotriz.UI;

namespace sistemaDeGestionAutomotriz.Forms
{
    // Edición de los campos operativos de una orden (los que actualiza ActualizarOrden).
    public class FormEditarOrden : Form
    {
        private readonly OrdenTrabajoDto _orden;

        private ComboBox _cboEstado, _cboTecnico;
        private NumericUpDown _numPrecio;
        private TextBox _txtTelefono, _txtMotivo, _txtDiagnostico, _txtObservaciones;
        private CheckBox _chkReparable, _chkGarantia;

        private static readonly string[] Estados =
        {
            "Pendiente", "En diagnóstico", "Esperando aprobación", "Aprobado",
            "En reparación", "Reparado", "Entregado", "Rechazado por cliente", "Dado de baja"
        };

        public OrdenTrabajoDto Orden { get { return _orden; } }

        public FormEditarOrden(OrdenTrabajoDto orden)
        {
            _orden = orden;
            ConstruirUI();
            CargarDatos();
        }

        private void ConstruirUI()
        {
            Text = "Editar orden";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(470, 500);
            BackColor = Tema.FondoApp;
            Font = Tema.FuenteCuerpo;

            Controls.Add(new Label { Text = "Editar orden N° " + _orden.NumeroOrden, Location = new Point(24, 16), AutoSize = true, Font = Tema.FuenteTitulo, ForeColor = Tema.TextoPrincipal });

            // Contexto (solo lectura)
            Panel ctx = new Panel { Location = new Point(24, 48), Size = new Size(422, 56), BackColor = Tema.Superficie };
            ctx.Controls.Add(Info("Cliente: " + _orden.Cliente, 12, 8));
            ctx.Controls.Add(Info("Servicio: " + _orden.TipoServicio, 215, 8));
            ctx.Controls.Add(Info("Vehículo: " + _orden.Vehiculo, 12, 30));
            ctx.Controls.Add(Info("Categoría: " + _orden.Categoria, 215, 30));
            Controls.Add(ctx);

            Etiqueta("Estado", 24, 114);
            _cboEstado = Combo(24, 132, 195);
            Etiqueta("Técnico asignado", 239, 114);
            _cboTecnico = Combo(239, 132, 195);

            Etiqueta("Precio", 24, 170);
            _numPrecio = new NumericUpDown { Location = new Point(24, 188), Width = 195, Maximum = 100000000, DecimalPlaces = 0, ThousandsSeparator = true, Font = Tema.FuenteCuerpo };
            Controls.Add(_numPrecio);
            Etiqueta("Teléfono", 239, 170);
            _txtTelefono = new TextBox { Location = new Point(239, 188), Width = 195 };
            Tema.EstiloInput(_txtTelefono);
            Controls.Add(_txtTelefono);

            _chkReparable = Check("Es reparable", 24, 226);
            _chkGarantia = Check("Garantía", 170, 226);
            _chkGarantia.CheckedChanged += (s, e) => _txtMotivo.Enabled = _chkGarantia.Checked;

            Etiqueta("Motivo de garantía", 24, 252);
            _txtMotivo = new TextBox { Location = new Point(24, 270), Width = 410 };
            Tema.EstiloInput(_txtMotivo);
            Controls.Add(_txtMotivo);

            Etiqueta("Diagnóstico", 24, 302);
            _txtDiagnostico = new TextBox { Location = new Point(24, 320), Width = 410, Height = 44, Multiline = true };
            Tema.EstiloInput(_txtDiagnostico);
            Controls.Add(_txtDiagnostico);

            Etiqueta("Observaciones", 24, 374);
            _txtObservaciones = new TextBox { Location = new Point(24, 392), Width = 410, Height = 44, Multiline = true };
            Tema.EstiloInput(_txtObservaciones);
            Controls.Add(_txtObservaciones);

            Button btnGuardar = new Button { Text = "Guardar", Size = new Size(96, 34), Location = new Point(338, 452) };
            Tema.EstiloBotonPrimario(btnGuardar);
            btnGuardar.Click += (s, e) => Guardar();
            Button btnCancelar = new Button { Text = "Cancelar", Size = new Size(96, 34), Location = new Point(234, 452), DialogResult = DialogResult.Cancel };
            Tema.EstiloBotonSecundario(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            AcceptButton = btnGuardar;
            CancelButton = btnCancelar;
        }

        private void CargarDatos()
        {
            _cboEstado.Items.AddRange(Estados);
            if (!string.IsNullOrEmpty(_orden.Estado) && !_cboEstado.Items.Contains(_orden.Estado))
                _cboEstado.Items.Insert(0, _orden.Estado);
            _cboEstado.SelectedItem = _orden.Estado;

            List<Usuario> usuarios = new UsuarioService().ObtenerUsuarios() ?? new List<Usuario>();
            _cboTecnico.DisplayMember = "NombreCompleto";
            _cboTecnico.ValueMember = "UsuarioId";
            _cboTecnico.DataSource = usuarios;
            _cboTecnico.SelectedValue = _orden.IdUsuario;

            if (_orden.Precio < 0) _numPrecio.Value = 0;
            else if (_orden.Precio > _numPrecio.Maximum) _numPrecio.Value = _numPrecio.Maximum;
            else _numPrecio.Value = _orden.Precio;

            _txtTelefono.Text = _orden.Telefono;
            _chkReparable.Checked = _orden.EsReparable;
            _chkGarantia.Checked = _orden.Garantia;
            _txtMotivo.Text = _orden.MotivoGarantia;
            _txtMotivo.Enabled = _orden.Garantia;
            _txtDiagnostico.Text = _orden.Diagnostico;
            _txtObservaciones.Text = _orden.Observaciones;
        }

        private void Guardar()
        {
            _orden.Estado = _cboEstado.SelectedItem != null ? _cboEstado.SelectedItem.ToString() : _orden.Estado;
            if (_cboTecnico.SelectedValue != null) _orden.IdUsuario = (int)_cboTecnico.SelectedValue;
            _orden.Precio = _numPrecio.Value;
            _orden.Telefono = _txtTelefono.Text.Trim();
            _orden.EsReparable = _chkReparable.Checked;
            _orden.Garantia = _chkGarantia.Checked;
            _orden.MotivoGarantia = _chkGarantia.Checked ? _txtMotivo.Text.Trim() : "";
            _orden.Diagnostico = _txtDiagnostico.Text.Trim();
            _orden.Observaciones = _txtObservaciones.Text.Trim();

            DialogResult = DialogResult.OK;
            Close();
        }

        private Label Info(string texto, int x, int y)
        {
            return new Label { Text = texto, AutoSize = true, Location = new Point(x, y), ForeColor = Tema.TextoSecundario, Font = Tema.FuenteMenor };
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

        private CheckBox Check(string texto, int x, int y)
        {
            CheckBox chk = new CheckBox { Text = texto, Location = new Point(x, y), AutoSize = true, ForeColor = Tema.TextoPrincipal, Font = Tema.FuenteCuerpo };
            Controls.Add(chk);
            return chk;
        }
    }
}
