using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sistemaDeGestionAutomotriz.UserControls;

namespace sistemaDeGestionAutomotriz.Forms
{
    public partial class FormNuevaOrden : Form
    {
        public FormNuevaOrden()
        {
            InitializeComponent();
        }











        //Navegacion por botones
        private void MostrarPantallaControl(UserControl control)
        {
            panelContenido.Controls.Clear();

            control.Dock = DockStyle.Fill;

            panelContenido.Controls.Add(control);
        }




        //boton evento navegacion Ordenes
        private void buttonModulo_Click(object sender, EventArgs e)
        {

            NuevaOrdenModuloControl pantallaModulo = new NuevaOrdenModuloControl();

            MostrarPantallaControl(pantallaModulo);

        }

        private void buttonCerrajeria_Click(object sender, EventArgs e)
        {
            NuevaOrdenCerrajeriaControl pantallaCerrajeria = new NuevaOrdenCerrajeriaControl();

            MostrarPantallaControl(pantallaCerrajeria);
        }

        private void buttonInstalacion_Click(object sender, EventArgs e)
        {
            NuevaOrdenInstalacionControl pantallaInstalacion = new NuevaOrdenInstalacionControl();

            MostrarPantallaControl(pantallaInstalacion);
        }
    }
}
