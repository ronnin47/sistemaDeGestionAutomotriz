using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sistemaDeGestionAutomotriz.Models;
using sistemaDeGestionAutomotriz.Services;

namespace sistemaDeGestionAutomotriz.Forms
{
    public partial class FormEditarVenta : Form
    {

        private VentaInsumosDto _venta;
        public FormEditarVenta( VentaInsumosDto venta)
        {
            InitializeComponent();
            _venta = venta;


       
            labelIdVenta.Text = _venta.NumeroVenta.ToString();
            textBoxClienteNombreApellido.Text = _venta.Cliente;
            labelIdCliente.Text = _venta.idCliente.ToString();
            labelFecha.Text = _venta.Fecha.ToString();
            labelInsumo.Text = _venta.Insumo;
            labelCantidad.Text = _venta.Cantidad.ToString();
            labelTotal.Text = _venta.Total.ToString();
            
             


          
        }




        /*
         private void buttonEditar_Click(object sender, EventArgs e)
            {
                _venta.Cliente = textBoxClienteNombreApellido.Text;
               

                VentaService service = new VentaService();
                service.ActualizarVenta(_venta);

                MessageBox.Show("Edicion de venta actualizada.");

                this.Close();
            }
        */
            private void buttonEliminar_Click(object sender, EventArgs e)
            {


            // Compara contra tu propiedad del DTO (ajusta si se llama 'Estado' con mayúscula o minúscula)
            if (_venta.Estado == "Anulada")
            {
                MessageBox.Show("Esta venta ya se encuentra anulada en el sistema. No es posible volver a anularla.",
                                "Acción Denegada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // El return frena el método acá y no deja que avance al código de abajo
            }

            DialogResult resultado = MessageBox.Show(
                    "¿Está seguro de que desea anula esta venta?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    VentaService servicio = new VentaService();
                    servicio.AnularVenta(_venta.NumeroVenta, _venta.idKit, _venta.Cantidad );

                    MessageBox.Show("Cliente dado de baja correctamente.");

                    this.Close();
                }
            }


       



    }
}
