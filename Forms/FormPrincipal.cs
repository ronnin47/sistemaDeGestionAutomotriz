using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sistemaDeGestionAutomotriz.Forms;
using sistemaDeGestionAutomotriz.UserControls;
using sistemaDeGestionAutomotriz.Models;
using sistemaDeGestionAutomotriz.UI;

namespace sistemaDeGestionAutomotriz
{
    public partial class FormPrincipal : Form
    {
        private Usuario _usuario;
        private Label _tituloMenu;
        private Button _btnModo;
        private Func<UserControl> _pantallaActual;   // para poder recrear la pantalla al cambiar de tema



        /*
        public FormPrincipal()
        {
            InitializeComponent();
        }
        */


        public FormPrincipal(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {

            Navegar(() => new OrdenesControl());
            AplicarTema();
        }

        // Aplica el tema al menú. Es idempotente: se puede volver a llamar al cambiar de modo.
        private void AplicarTema()
        {
            panelContenido.BackColor = Tema.FondoApp;
            panelContenido.BorderStyle = BorderStyle.None;

            panelMenu.BackColor = Tema.PrimarioOscuro;
            panelMenu.BorderStyle = BorderStyle.None;
            panelMenu.Width = 200;

            if (_tituloMenu == null)
            {
                _tituloMenu = new Label
                {
                    Text = "Gestión Pro",
                    ForeColor = Tema.TextoSobrePrimario,
                    Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(18, 0, 0, 0),
                    Size = new Size(panelMenu.Width, 56),
                    Location = new Point(0, 0)
                };
                panelMenu.Controls.Add(_tituloMenu);
            }

            Button[] navegacion = { buttonOrdenes, buttonClientes, buttonVentas, buttonCotizaciones, buttonGarantias };
            string[] iconos = { Tema.Iconos.Ordenes, Tema.Iconos.Cliente, Tema.Iconos.Ventas, Tema.Iconos.Cotizaciones, Tema.Iconos.Garantias };
            int y = 72;
            for (int i = 0; i < navegacion.Length; i++)
            {
                Tema.EstiloBotonMenu(navegacion[i]);
                Tema.PonerIcono(navegacion[i], iconos[i]);
                navegacion[i].Size = new Size(panelMenu.Width, 46);
                navegacion[i].Location = new Point(0, y);
                y += 46;
            }

            if (_btnModo == null)
            {
                _btnModo = new Button();
                _btnModo.Click += (s, e) => AlternarModo();
                panelMenu.Controls.Add(_btnModo);
            }
            Tema.EstiloBotonMenu(_btnModo);
            _btnModo.Text = Tema.ModoOscuro ? "Modo claro" : "Modo oscuro";
            Tema.PonerIcono(_btnModo, Tema.ModoOscuro ? Tema.Iconos.ModoClaro : Tema.Iconos.ModoOscuro);
            _btnModo.Size = new Size(panelMenu.Width, 46);
            _btnModo.Location = new Point(0, panelMenu.Height - 104);
            _btnModo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            Tema.EstiloBotonMenu(buttonLogOut);
            buttonLogOut.Text = "Cerrar sesión";
            buttonLogOut.ForeColor = ColorTranslator.FromHtml("#F2B8B8");
            Tema.PonerIcono(buttonLogOut, Tema.Iconos.CerrarSesion);
            buttonLogOut.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#A32D2D");
            buttonLogOut.Size = new Size(panelMenu.Width, 46);
            buttonLogOut.Location = new Point(0, panelMenu.Height - 58);
            buttonLogOut.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        }

        private void AlternarModo()
        {
            Tema.ModoOscuro = !Tema.ModoOscuro;
            AplicarTema();
            if (_pantallaActual != null) MostrarPantallaControl(_pantallaActual());
        }

        // Recuerda cómo crear la pantalla actual para poder repintarla al cambiar de tema.
        private void Navegar(Func<UserControl> crear)
        {
            _pantallaActual = crear;
            MostrarPantallaControl(crear());
        }

        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Desea cerrar la sesión?",
                "Cerrar sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                SessionService.CerrarSesion();

                this.Hide();

                FormLogin login = new FormLogin();
                login.ShowDialog();

                this.Close();
            }
        }

        private void MostrarPantallaControl(UserControl control)
        {
            panelContenido.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panelContenido.Controls.Add(control);
        }

        private void buttonOrdenes_Click(object sender, EventArgs e)
        {
            Navegar(() => new OrdenesControl());
        }

        private void buttonVentas_Click(object sender, EventArgs e)
        {
            Navegar(() => new VentasControl());
        }

        private void buttonClientes_Click(object sender, EventArgs e)
        {
            Navegar(() => new ClientesControl());
        }

        private void buttonCotizaciones_Click(object sender, EventArgs e)
        {
            Navegar(() => new CotizacionesControl());
        }

        private void buttonGarantias_Click(object sender, EventArgs e)
        {
            Navegar(() => new GarantiasControl());
        }
    }
}
