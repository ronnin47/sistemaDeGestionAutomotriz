using System.Windows.Forms;

namespace sistemaDeGestionAutomotriz.UI
{
    /// <summary>
    /// Mensajes estándar al usuario, centralizados para que todas las pantallas
    /// avisen de la misma forma (confirmar, éxito, error). Así no repetimos
    /// MessageBox sueltos con textos y estilos distintos en cada lado.
    /// </summary>
    public static class Avisos
    {
        /// <summary>
        /// Pregunta de confirmación (Sí/No) antes de algo importante o destructivo
        /// (ej. eliminar). Devuelve true si el usuario eligió "Sí".
        /// </summary>
        public static bool Confirmar(string mensaje, string titulo = "Confirmar")
        {
            return MessageBox.Show(
                mensaje, titulo,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        /// <summary>Aviso de que algo salió bien (ej. "Cliente guardado").</summary>
        public static void Exito(string mensaje, string titulo = "Listo")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>Aviso de error (ej. no se pudo conectar a la base).</summary>
        public static void Error(string mensaje, string titulo = "Error")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
