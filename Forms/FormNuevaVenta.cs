using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using sistemaDeGestionAutomotriz.Data;
using sistemaDeGestionAutomotriz.Models;
using sistemaDeGestionAutomotriz.Services;

namespace sistemaDeGestionAutomotriz.Forms
{
    public partial class FormNuevaVenta : Form
    {
        public FormNuevaVenta()
        {
            InitializeComponent();

           

        }

        private void FormNuevaVenta_Load(object sender, EventArgs e)
        {
                CargarInsumos();
                CargarClientes();
          
        }


        private void CargarInsumos()
        {
            InsumoService servicio = new InsumoService();

            comboBoxInsumosVender.DataSource = servicio.ObtenerTodosLosInsumos();
            comboBoxInsumosVender.DisplayMember = "Nombre"; // Lo que ve el usuario
            comboBoxInsumosVender.ValueMember = "IdKit";    // El valor asociado
        }

        private void CargarClientes()
        {
            ClienteService servicio = new  ClienteService();

            comboBoxClientes.DataSource = servicio.ObtenerClientesActivos();
            comboBoxClientes.DisplayMember = "NombreCompleto";
        }
    }
}
