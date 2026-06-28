using System;
using System.Windows.Forms;
using sistemaDeGestionAutomotriz.Models;
using sistemaDeGestionAutomotriz.Services;
using sistemaDeGestionAutomotriz.Data;




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


                FormPrincipal principal = new FormPrincipal();
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
                    textBoxUsuario.Text = "administradorDemo@gmail.com";
                    textBoxPass.Text = "1234";
                }
                else if (comboBoxDemoUsuarios.SelectedItem.ToString() == "Técnico") {

                    textBoxUsuario.Text = "tecnicoDemo@gmail.com";
                    textBoxPass.Text = "1234";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var con = DataBase.ObtenerConexion())
                {
                    con.Open();
                    MessageBox.Show("¡Conexión exitosa con Supabase!",
                                  "OK",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                               "Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
        }
    }
}