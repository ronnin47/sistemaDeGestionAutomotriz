using System.Windows.Forms;

namespace sistemaDeGestionAutomotriz.UI
{
    // Mensajes estándar al usuario, centralizados para que toda la app avise igual.
    public static class Avisos
    {
        /// <summary>Pregunta Sí/No antes de una acción importante. Devuelve true si eligió "Sí".</summary>
        public static bool Confirmar(string mensaje, string titulo = "Confirmar")
        {
            return MessageBox.Show(
                mensaje, titulo,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        /// <summary>Aviso de operación exitosa.</summary>
        public static void Exito(string mensaje, string titulo = "Listo")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>Aviso de error.</summary>
        public static void Error(string mensaje, string titulo = "Error")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
