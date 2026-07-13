using System;
using System.Windows.Forms;
using sistemaDeGestionAutomotriz.Forms;
using sistemaDeGestionAutomotriz.Services;
using sistemaDeGestionAutomotriz.Models;


namespace sistemaDeGestionAutomotriz
{
    static class Program
    {
        [STAThread]



        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (SessionService.HaySesion())
            {
                Session sesion = SessionService.ObtenerSesion();

                UsuarioService usuarioService = new UsuarioService();
                Usuario usuario = usuarioService.ObtenerPorId(sesion.UsuarioId);

                Application.Run(new FormPrincipal(usuario));
            }
            else
            {
                Application.Run(new FormLogin());
            }
        }
    }
}