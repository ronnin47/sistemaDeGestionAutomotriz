using System;
using System.Windows.Forms;
using sistemaDeGestionAutomotriz.Forms;


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
                Application.Run(new FormPrincipal());
            }
            else
            {
                Application.Run(new FormLogin());
                //Application.Run(new sistemaDeGestionAutomotriz.Forms.FormLogin());
            }
        }
    }
}