using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sistemaDeGestionAutomotriz.Services;
using sistemaDeGestionAutomotriz.Forms;
using sistemaDeGestionAutomotriz.Models;

namespace sistemaDeGestionAutomotriz.UserControls
{
    public partial class GarantiasControl : UserControl
    {
        private GarantiaService garantiaService = new GarantiaService();

        public GarantiasControl()
        {
            InitializeComponent();

            this.Load += GarantiasControl_Load;
        }
        private void GarantiasControl_Load(object sender, EventArgs e)
        {

            dgvListaGarantias.ReadOnly = true;
            dgvListaGarantias.AllowUserToAddRows = false;
            dgvListaGarantias.AllowUserToDeleteRows = false;
            dgvListaGarantias.AllowUserToResizeRows = false;
            CargarGarantias();
        }

        private void CargarGarantias()
        {
            try
            {
                dgvListaGarantias.AutoGenerateColumns = true;
                dgvListaGarantias.DataSource = null;
                dgvListaGarantias.DataSource = garantiaService.ObtenerTodasLasGarantias();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los clientes: " + ex.Message);
            }
        }


    }

}
