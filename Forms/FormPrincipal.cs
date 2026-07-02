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

        public FormPrincipal()
        {
            InitializeComponent();
        }

        public FormPrincipal(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            AplicarTema();
        }

        // En el evento Load para que aplique con cualquiera de los dos constructores.
        // Por código (no en el Designer) para que Visual Studio no lo pise.
        private void AplicarTema()
        {
            panelContenido.BackColor = Tema.FondoApp;
            panelContenido.BorderStyle = BorderStyle.None;

            panelMenu.BackColor = Tema.PrimarioOscuro;
            panelMenu.BorderStyle = BorderStyle.None;
            panelMenu.Width = 200;

            Label titulo = new Label
            {
                Text = "Gestión Pro",
                ForeColor = Tema.TextoSobrePrimario,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(18, 0, 0, 0),
                Size = new Size(panelMenu.Width, 56),
                Location = new Point(0, 0)
            };
            panelMenu.Controls.Add(titulo);

            Button[] navegacion = { buttonOrdenes, buttonClientes, buttonVentas, buttonCotizaciones, buttonGarantias };
            int y = 72;
            foreach (Button boton in navegacion)
            {
                Tema.EstiloBotonMenu(boton);
                boton.Size = new Size(panelMenu.Width, 46);
                boton.Location = new Point(0, y);
                y += 46;
            }

            Tema.EstiloBotonMenu(buttonLogOut);
            buttonLogOut.Text = "Cerrar sesión";
            buttonLogOut.ForeColor = ColorTranslator.FromHtml("#F2B8B8");
            buttonLogOut.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#A32D2D");
            buttonLogOut.Size = new Size(panelMenu.Width, 46);
            buttonLogOut.Location = new Point(0, panelMenu.Height - 58);
            buttonLogOut.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
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
            MostrarPantallaControl(new OrdenesControl());
        }

        private void buttonVentas_Click(object sender, EventArgs e)
        {
            MostrarPantallaControl(new VentasControl());
        }

        private void buttonClientes_Click(object sender, EventArgs e)
        {
            MostrarPantallaControl(new ClientesControl());
        }

        private void buttonCotizaciones_Click(object sender, EventArgs e)
        {
            MostrarPantallaControl(new CotizacionesControl());
        }

        private void buttonGarantias_Click(object sender, EventArgs e)
        {
            MostrarPantallaControl(new GarantiasControl());
        }
    }
}
