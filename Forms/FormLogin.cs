using System;
using System.Windows.Forms;
using sistemaDeGestionAutomotriz.Models;
using sistemaDeGestionAutomotriz.Services;




namespace sistemaDeGestionAutomotriz.Forms
{
    public partial class FormLogin : Form
    {
        private UsuarioService usuarioService = new UsuarioService();

        public FormLogin()
        {
            InitializeComponent();
             
        }




        //Evento de click del boton login
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string email = textBoxUsuario.Text.Trim();
            string pass = textBoxPass.Text;


            //manda a hacer el login 
            Usuario usuario = usuarioService.Login(email, pass);
            


            if (usuario != null)
            {
                // Guarda la sesión con el id real del usuario
                SessionService.GuardarSesion(usuario.UsuarioId);


                this.Hide();


                FormPrincipal principal = new FormPrincipal(usuario);
                principal.ShowDialog();


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

        private void comboBoxDemoUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDemoUsuarios.SelectedItem != null)
            {
                if (comboBoxDemoUsuarios.SelectedItem.ToString() == "Administrador")
                {
                    textBoxUsuario.Text = "administradordemo@gmail.com";
                    textBoxPass.Text = "1234";
                }
                else if (comboBoxDemoUsuarios.SelectedItem.ToString() == "Técnico") {

                    textBoxUsuario.Text = "tecnicodemo@gmail.com";
                    textBoxPass.Text = "1234";
                }
                else if (comboBoxDemoUsuarios.SelectedItem.ToString() == "Instalador")
                {

                    textBoxUsuario.Text = "instaladordemo@gmail.com";
                    textBoxPass.Text = "1234";
                }
                else if (comboBoxDemoUsuarios.SelectedItem.ToString() == "Cotizador")
                {

                    textBoxUsuario.Text = "cotizadordemo@gmail.com";
                    textBoxPass.Text = "1234";
                }
                else if (comboBoxDemoUsuarios.SelectedItem.ToString() == "Deposito")
                {

                    textBoxUsuario.Text = "depositodemo@gmail.com";
                    textBoxPass.Text = "1234";
                }
            }
        }
    }
}