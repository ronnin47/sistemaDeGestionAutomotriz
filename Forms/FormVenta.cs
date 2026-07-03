using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using sistemaDeGestionAutomotriz.Models;
using sistemaDeGestionAutomotriz.Services;
using sistemaDeGestionAutomotriz.UI;

namespace sistemaDeGestionAutomotriz.Forms
{
    // Alta de una venta de insumo: elige cliente e insumo, cantidad, y calcula el total.
    public class FormVenta : Form
    {
        private ComboBox _cboCliente, _cboInsumo;
        private NumericUpDown _numCantidad;
        private Label _lblTotalValor;
        private Button _btnGuardar, _btnCancelar;

        public Venta Venta { get; private set; }

        public FormVenta()
        {
            ConstruirUI();
            CargarCombos();
        }

        private void ConstruirUI()
        {
            Text = "Nueva venta";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(360, 260);
            BackColor = Tema.FondoApp;
            Font = Tema.FuenteCuerpo;

            Etiqueta("Cliente", 24, 18);
            _cboCliente = Combo(24, 36, 312);

            Etiqueta("Insumo", 24, 72);
            _cboInsumo = Combo(24, 90, 312);
            _cboInsumo.SelectedIndexChanged += (s, e) => ActualizarTotal();

            Etiqueta("Cantidad", 24, 126);
            _numCantidad = new NumericUpDown
            {
                Location = new Point(24, 144),
                Width = 100,
                Minimum = 1,
                Maximum = 9999,
                Value = 1,
                BorderStyle = BorderStyle.FixedSingle,
                Font = Tema.FuenteCuerpo
            };
            _numCantidad.ValueChanged += (s, e) => ActualizarTotal();
            Controls.Add(_numCantidad);

            Etiqueta("Total", 150, 126);
            _lblTotalValor = new Label
            {
                Location = new Point(150, 146),
                AutoSize = true,
                Font = Tema.FuenteSeccion,
                ForeColor = Tema.TextoPrincipal,
                Text = "$0"
            };
            Controls.Add(_lblTotalValor);

            _btnGuardar = new Button { Text = "Guardar", Size = new Size(96, 34), Location = new Point(240, 206), Anchor = AnchorStyles.Bottom | AnchorStyles.Right };
            Tema.EstiloBotonPrimario(_btnGuardar);
            _btnGuardar.Click += (s, e) => Guardar();

            _btnCancelar = new Button { Text = "Cancelar", Size = new Size(96, 34), Location = new Point(136, 206), Anchor = AnchorStyles.Bottom | AnchorStyles.Right, DialogResult = DialogResult.Cancel };
            Tema.EstiloBotonSecundario(_btnCancelar);

            Controls.Add(_btnGuardar);
            Controls.Add(_btnCancelar);
            AcceptButton = _btnGuardar;
            CancelButton = _btnCancelar;
        }

        private void Etiqueta(string texto, int x, int y)
        {
            Controls.Add(new Label
            {
                Text = texto,
                AutoSize = true,
                Location = new Point(x, y),
                ForeColor = Tema.TextoSecundario,
                Font = Tema.FuenteEtiqueta
            });
        }

        private ComboBox Combo(int x, int y, int ancho)
        {
            ComboBox cbo = new ComboBox
            {
                Location = new Point(x, y),
                Width = ancho,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            Tema.EstiloCombo(cbo);
            Controls.Add(cbo);
            return cbo;
        }

        private void CargarCombos()
        {
            // Clientes: mostramos "Nombre Apellido" y guardamos el id.
            var clientes = new ClienteService().ObtenerClientesActivos() ?? new List<Cliente>();
            var itemsCliente = new List<object>();
            foreach (Cliente c in clientes)
                itemsCliente.Add(new { Id = c.ClienteId, Texto = c.Nombre + " " + c.Apellido });
            _cboCliente.DisplayMember = "Texto";
            _cboCliente.ValueMember = "Id";
            _cboCliente.DataSource = itemsCliente;

            // Insumos: los envolvemos para mostrar el stock al lado del nombre.
            var insumos = new InsumoService().ObtenerTodosLosInsumos() ?? new List<Insumo>();
            var itemsInsumo = new List<ItemInsumo>();
            foreach (Insumo i in insumos) itemsInsumo.Add(new ItemInsumo { Insumo = i });
            _cboInsumo.DataSource = itemsInsumo;

            ActualizarTotal();
        }

        private void ActualizarTotal()
        {
            Insumo ins = InsumoSeleccionado();
            decimal total = ins != null ? ins.Precio * _numCantidad.Value : 0;
            _lblTotalValor.Text = total.ToString("C0");
        }

        private void Guardar()
        {
            Insumo ins = InsumoSeleccionado();
            if (_cboCliente.SelectedValue == null || ins == null)
            {
                Avisos.Error("Elegí un cliente y un insumo.");
                return;
            }

            int cantidad = (int)_numCantidad.Value;
            if (cantidad > ins.Stock)
            {
                Avisos.Error("No hay stock suficiente. Disponible: " + ins.Stock + ".");
                return;
            }

            Venta = new Venta
            {
                IdCliente = (int)_cboCliente.SelectedValue,
                IdKit = ins.IdKit,
                Cantidad = cantidad,
                Total = ins.Precio * cantidad
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private Insumo InsumoSeleccionado()
        {
            ItemInsumo item = _cboInsumo.SelectedItem as ItemInsumo;
            return item != null ? item.Insumo : null;
        }

        // Envuelve un insumo para mostrar "Nombre (stock: N)" en el combo.
        private class ItemInsumo
        {
            public Insumo Insumo;
            public override string ToString()
            {
                return Insumo.Nombre + "  (stock: " + Insumo.Stock + ")";
            }
        }
    }
}
