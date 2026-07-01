using System;
using System.Drawing;
using System.Windows.Forms;
using sistemaDeGestionAutomotriz.Models;
using sistemaDeGestionAutomotriz.Services;
using sistemaDeGestionAutomotriz.UI;




namespace sistemaDeGestionAutomotriz.Forms
{
    public partial class FormLogin : Form
    {
        private UsuarioService usuarioService = new UsuarioService();
        private Panel _card;   // la tarjeta centrada del login

        public FormLogin()
        {
            InitializeComponent();
            AplicarTema();
        }

        /// <summary>
        /// Rearma el login como una tarjeta centrada, siguiendo el sistema de diseño
        /// (paleta, tipografía, espaciado 4/8/16/24/32 e íconos del Tema). Va por
        /// código para no pelearse con el diseñador de Visual Studio.
        /// </summary>
        private void AplicarTema()
        {
            BackColor = Tema.FondoApp;
            Text = "Gestión Pro — Iniciar sesión";
            textBoxPass.UseSystemPasswordChar = true;   // ocultar la contraseña

            int margen = Tema.EspacioLg;          // 24
            int anchoCard = 330;
            int x = margen;                       // margen interno izquierdo
            int ancho = anchoCard - margen * 2;   // ancho útil del contenido (282)

            _card = new Panel { Width = anchoCard, BackColor = Tema.Superficie, BorderStyle = BorderStyle.FixedSingle };

            int y = Tema.EspacioLg;               // arranca a 24 del borde superior

            // Ícono (fuente Segoe MDL2 Assets): llave / reparación
            Label icono = new Label
            {
                Text = char.ConvertFromUtf32(0xE90F),
                Font = Tema.FuenteIcono(26F),
                ForeColor = Tema.Primario,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(ancho, 44),
                Location = new Point(x, y)
            };
            y += 44 + Tema.EspacioSm;

            Label titulo = new Label
            {
                Text = "Gestión Pro",
                Font = Tema.FuenteTitulo,
                ForeColor = Tema.TextoPrincipal,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(ancho, 30),
                Location = new Point(x, y)
            };
            y += 30;

            Label subtitulo = new Label
            {
                Text = "Iniciá sesión para continuar",
                Font = Tema.FuenteEtiqueta,
                ForeColor = Tema.TextoSecundario,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(ancho, 20),
                Location = new Point(x, y)
            };
            y += 20 + Tema.EspacioLg;

            // Campo Usuario
            EstiloEtiqueta(labelUsuario, "Usuario", x, y);
            y += 18;
            Tema.EstiloInput(textBoxUsuario);
            textBoxUsuario.Location = new Point(x, y);
            textBoxUsuario.Width = ancho;
            y += textBoxUsuario.Height + Tema.EspacioMd;

            // Campo Contraseña
            EstiloEtiqueta(labelPass, "Contraseña", x, y);
            y += 18;
            Tema.EstiloInput(textBoxPass);
            textBoxPass.Location = new Point(x, y);
            textBoxPass.Width = ancho;
            y += textBoxPass.Height + Tema.EspacioLg;

            // Botón Ingresar (ancho completo de la card)
            Tema.EstiloBotonPrimario(buttonLogin);
            buttonLogin.Text = "Ingresar";
            buttonLogin.Location = new Point(x, y);
            buttonLogin.Size = new Size(ancho, 38);
            y += 38 + Tema.EspacioMd;

            // Separador "acceso rápido (demo)"
            Label demo = new Label
            {
                Text = "acceso rápido (demo)",
                Font = Tema.FuenteMenor,
                ForeColor = Tema.TextoSecundario,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(ancho, 16),
                Location = new Point(x, y)
            };
            y += 16 + Tema.EspacioSm;

            // Combo de usuario demo
            Tema.EstiloCombo(comboBoxDemoUsuarios);
            comboBoxDemoUsuarios.Location = new Point(x, y);
            comboBoxDemoUsuarios.Width = ancho;
            y += comboBoxDemoUsuarios.Height + Tema.EspacioLg;

            _card.Height = y;

            // Movemos todos los controles dentro de la card
            _card.Controls.Add(icono);
            _card.Controls.Add(titulo);
            _card.Controls.Add(subtitulo);
            _card.Controls.Add(labelUsuario);
            _card.Controls.Add(textBoxUsuario);
            _card.Controls.Add(labelPass);
            _card.Controls.Add(textBoxPass);
            _card.Controls.Add(buttonLogin);
            _card.Controls.Add(demo);
            _card.Controls.Add(comboBoxDemoUsuarios);
            Controls.Add(_card);

            CentrarCard();
            Resize += (s, e) => CentrarCard();
        }

        /// <summary>Da estilo a una etiqueta de campo y la ubica.</summary>
        private void EstiloEtiqueta(Label etiqueta, string texto, int x, int y)
        {
            etiqueta.Text = texto;
            etiqueta.AutoSize = true;
            etiqueta.ForeColor = Tema.TextoSecundario;
            etiqueta.Font = Tema.FuenteEtiqueta;
            etiqueta.Location = new Point(x, y);
        }

        /// <summary>Mantiene la tarjeta centrada en la ventana (también al maximizar).</summary>
        private void CentrarCard()
        {
            if (_card == null) return;
            _card.Left = Math.Max(0, (ClientSize.Width - _card.Width) / 2);
            _card.Top = Math.Max(0, (ClientSize.Height - _card.Height) / 2);
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