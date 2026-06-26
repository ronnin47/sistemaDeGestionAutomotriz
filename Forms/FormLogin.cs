using System;
using System.Windows.Forms;

namespace sistemaDeGestionAutomotriz.Forms
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }




        //Evento de click del boton loging
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string usuario = textBoxUsuario.Text.Trim();
            string contraseña = textBoxPass.Text;

            // Login de prueba
            if (usuario == "admin" && contraseña == "1234")
            {
                // Guarda la sesión
                SessionService.GuardarSesion(1);

                // Oculta el formulario de login
                this.Hide();

                // Abre el formulario principal
                FormPrincipal principal = new FormPrincipal();
                principal.ShowDialog();

                // Cierra el login cuando se cierre el principal
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "Usuario o contraseña incorrectos.",
                    "Error de inicio de sesión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}