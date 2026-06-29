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










namespace sistemaDeGestionAutomotriz
{
    public partial class FormPrincipal : Form
    {

        public FormPrincipal()
        {

         
            InitializeComponent();

        }



        private void FormPrincipal_Load(object sender, EventArgs e)
        {

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










        //Navegacion por botones
        private void MostrarPantallaControl(UserControl control)
        {
            panelContenido.Controls.Clear();

            control.Dock = DockStyle.Fill;

            panelContenido.Controls.Add(control);
        }

        //boton evento navegacion Ordenes
        private void buttonOrdenes_Click(object sender, EventArgs e)
        {

            OrdenesControl  pantallaOrdenes = new OrdenesControl();

            MostrarPantallaControl( pantallaOrdenes );

        }

        private void buttonVentas_Click(object sender, EventArgs e)
        {
            VentasControl pantallaVentas = new VentasControl();

            MostrarPantallaControl(pantallaVentas);
        }

        private void buttonClientes_Click(object sender, EventArgs e)
        {
            ClientesControl pantallaClientes = new ClientesControl();

            MostrarPantallaControl(pantallaClientes);
        }

        private void buttonCotizaciones_Click(object sender, EventArgs e)
        {
           CotizacionesControl pantallaCotizaciones = new CotizacionesControl();

            MostrarPantallaControl(pantallaCotizaciones);
        }

        private void buttonGarantias_Click(object sender, EventArgs e)
        {

            GarantiasControl pantallaGarantias = new GarantiasControl();

            MostrarPantallaControl(pantallaGarantias);

        }
    }
}
