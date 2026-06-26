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
}
}
