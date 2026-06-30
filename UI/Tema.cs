using System.Drawing;
using System.Windows.Forms;

namespace sistemaDeGestionAutomotriz.UI
{
    /// <summary>
    /// Paleta de colores, tipografía, espaciado, íconos y estilos del sistema,
    /// todo centralizado en un solo lugar. Si hay que cambiar algo del look,
    /// se cambia ACÁ una vez y se actualiza toda la app.
    ///
    /// Soporta modo claro y oscuro: cada color devuelve el valor que corresponde
    /// según <see cref="ModoOscuro"/>. Por ahora el "interruptor" en vivo no está
    /// cableado (queda para cuando haya pantallas reales); el mecanismo sí está listo.
    /// </summary>
    public static class Tema
    {
        /// <summary>Si está en true, los colores devuelven su versión oscura.</summary>
        public static bool ModoOscuro { get; set; } = false;

        // Devuelve el color claro o el oscuro según el modo actual.
        private static Color C(string claro, string oscuro)
        {
            return ColorTranslator.FromHtml(ModoOscuro ? oscuro : claro);
        }

        // ===== Neutros: la base, ~60% de la pantalla =====
        // Nunca blanco ni negro puro: cansan la vista en jornadas largas.
        public static Color FondoApp => C("#F4F6F8", "#0F1720");
        public static Color Superficie => C("#FFFFFF", "#1A2430");   // tarjetas y tablas
        public static Color Borde => C("#E1E5EA", "#2A3744");
        public static Color TextoPrincipal => C("#1F2933", "#E4E8EC");
        public static Color TextoSecundario => C("#5B6672", "#9AA5B1");

        // ===== Marca: azul acero, ~30% =====
        public static Color Primario => C("#2D6A8E", "#4A9CC4");        // botones de acción
        public static Color PrimarioOscuro => C("#1F4E68", "#16202B");  // menú lateral
        public static Color PrimarioClaro => C("#E8F1F6", "#1E3A4C");   // fila seleccionada / hover suave
        public static Color TextoSobrePrimario => C("#FFFFFF", "#FFFFFF");

        // ===== Estados de una orden, ~10% =====
        // Cada familia es UN significado. En oscuro se invierte (fondo oscuro + texto claro).
        public static Color EnColaFondo => C("#E1E5EA", "#2C2C2A");          // Ingresado
        public static Color EnColaTexto => C("#3A4450", "#B4B2A9");
        public static Color EnCursoFondo => C("#E6F1FB", "#103049");         // En diagnóstico / En reparación
        public static Color EnCursoTexto => C("#0C447C", "#9CC9EC");
        public static Color RequiereAccionFondo => C("#FAEEDA", "#4A3A12");  // Esperando aprobación / Aprobado
        public static Color RequiereAccionTexto => C("#854F0B", "#F0C779");
        public static Color TerminadoFondo => C("#E1F5EE", "#0E3D31");       // Reparado / Entregado
        public static Color TerminadoTexto => C("#0F6E56", "#7FD3B4");
        public static Color CerradoFondo => C("#FCEBEB", "#501313");         // Rechazado por cliente / Dado de baja
        public static Color CerradoTexto => C("#A32D2D", "#F09595");

        // ===== Tipografía: 5 roles fijos =====
        // Segoe UI es la fuente nativa de Windows (ya está en todas las máquinas).
        // Solo dos pesos: normal y negrita.
        private const string Familia = "Segoe UI";
        public static readonly Font FuenteTitulo = new Font(Familia, 18F, FontStyle.Bold);     // título de pantalla
        public static readonly Font FuenteSeccion = new Font(Familia, 12F, FontStyle.Bold);    // encabezado de bloque
        public static readonly Font FuenteCuerpo = new Font(Familia, 9.75F, FontStyle.Regular); // texto general, celdas, inputs
        public static readonly Font FuenteEtiqueta = new Font(Familia, 9F, FontStyle.Regular);  // rótulos de campos y columnas
        public static readonly Font FuenteMenor = new Font(Familia, 8.25F, FontStyle.Regular);  // contadores, ayudas, badges
        public static readonly Font FuenteBoton = new Font(Familia, 9.75F, FontStyle.Bold);     // texto de botones

        // ===== Espaciado: un solo ritmo (en píxeles) =====
        public const int EspacioXs = 4;
        public const int EspacioSm = 8;
        public const int EspacioMd = 16;
        public const int EspacioLg = 24;
        public const int EspacioXl = 32;
        public const int PaddingPantalla = 20;  // padding interno estándar de cada pantalla
        public const int AltoFila = 34;          // alto cómodo de fila / input / botón

        // ===== Iconografía =====
        // Usamos "Segoe MDL2 Assets": viene en Windows 10 y 11 (la versión "Fluent"
        // es su gemela de Win11). Así tenemos íconos sin instalar nada ni usar imágenes.
        public static Font FuenteIcono(float tam = 12F)
        {
            return new Font("Segoe MDL2 Assets", tam);
        }

        /// <summary>
        /// Códigos de los íconos que usamos (glifos de la fuente Segoe MDL2 Assets).
        /// Los generamos desde su número de código para no pegar caracteres invisibles.
        /// </summary>
        public static class Iconos
        {
            public static readonly string Buscar = char.ConvertFromUtf32(0xE721);
            public static readonly string Nuevo = char.ConvertFromUtf32(0xE710);
            public static readonly string Editar = char.ConvertFromUtf32(0xE70F);
            public static readonly string Eliminar = char.ConvertFromUtf32(0xE74D);
            public static readonly string Guardar = char.ConvertFromUtf32(0xE74E);
            public static readonly string Cancelar = char.ConvertFromUtf32(0xE711);
            public static readonly string Refrescar = char.ConvertFromUtf32(0xE72C);
            public static readonly string Cliente = char.ConvertFromUtf32(0xE77B);
        }

        // ===== Helpers de estilo =====

        /// <summary>Título de una pantalla (texto grande arriba a la izquierda).</summary>
        public static void EstiloTituloPantalla(Label titulo)
        {
            titulo.Font = FuenteTitulo;
            titulo.ForeColor = TextoPrincipal;
            titulo.AutoSize = true;
        }

        /// <summary>Subtítulo / contador debajo del título (texto chico y gris).</summary>
        public static void EstiloSubtitulo(Label sub)
        {
            sub.Font = FuenteEtiqueta;
            sub.ForeColor = TextoSecundario;
            sub.AutoSize = true;
        }

        /// <summary>
        /// Botón del menú lateral: plano, texto claro alineado a la izquierda,
        /// con resaltado azul al pasar el mouse.
        /// </summary>
        public static void EstiloBotonMenu(Button boton)
        {
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.BackColor = PrimarioOscuro;
            boton.ForeColor = TextoSobrePrimario;
            boton.FlatAppearance.MouseOverBackColor = Primario;
            boton.TextAlign = ContentAlignment.MiddleLeft;
            boton.Padding = new Padding(18, 0, 0, 0);
            boton.Font = FuenteCuerpo;
            boton.Cursor = Cursors.Hand;
        }

        /// <summary>Botón de acción principal (azul lleno, texto blanco).</summary>
        public static void EstiloBotonPrimario(Button boton)
        {
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.BackColor = Primario;
            boton.ForeColor = TextoSobrePrimario;
            boton.FlatAppearance.MouseOverBackColor = PrimarioOscuro;
            boton.Font = FuenteBoton;
            boton.Cursor = Cursors.Hand;
        }

        /// <summary>Botón secundario (blanco con borde, texto azul). Para acciones menos importantes.</summary>
        public static void EstiloBotonSecundario(Button boton)
        {
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 1;
            boton.FlatAppearance.BorderColor = C("#C9D2DA", "#3A4A57");
            boton.BackColor = Superficie;
            boton.ForeColor = Primario;
            boton.FlatAppearance.MouseOverBackColor = C("#EEF2F5", "#243240");
            boton.Font = FuenteBoton;
            boton.Cursor = Cursors.Hand;
        }

        /// <summary>Botón de peligro (ej. "Eliminar"): blanco con borde y texto rojo.</summary>
        public static void EstiloBotonPeligro(Button boton)
        {
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 1;
            boton.FlatAppearance.BorderColor = C("#E0A6A6", "#6B2A2A");
            boton.BackColor = Superficie;
            boton.ForeColor = CerradoTexto;
            boton.FlatAppearance.MouseOverBackColor = CerradoFondo;
            boton.Font = FuenteBoton;
            boton.Cursor = Cursors.Hand;
        }

        /// <summary>Botón solo-ícono (ej. editar/eliminar en una fila de tabla).</summary>
        public static void EstiloBotonIcono(Button boton, string glifo)
        {
            boton.Text = glifo;
            boton.Font = FuenteIcono(11F);
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.BackColor = Superficie;
            boton.ForeColor = TextoSecundario;
            boton.FlatAppearance.MouseOverBackColor = PrimarioClaro;
            boton.Cursor = Cursors.Hand;
            boton.Size = new Size(32, 28);
        }

        /// <summary>Estilo de una caja de texto: borde simple, fondo blanco, fuente de cuerpo.</summary>
        public static void EstiloInput(TextBox caja)
        {
            caja.BorderStyle = BorderStyle.FixedSingle;
            caja.BackColor = Superficie;
            caja.ForeColor = TextoPrincipal;
            caja.Font = FuenteCuerpo;
        }

        /// <summary>Estilo de una lista desplegable (combo): plana, fondo blanco.</summary>
        public static void EstiloCombo(ComboBox combo)
        {
            combo.FlatStyle = FlatStyle.Flat;
            combo.BackColor = Superficie;
            combo.ForeColor = TextoPrincipal;
            combo.Font = FuenteCuerpo;
        }

        /// <summary>
        /// Estilo de la tabla (DataGridView), pieza central de casi todas las pantallas:
        /// encabezado gris suave, solo líneas horizontales, fila seleccionada en azul
        /// clarito y columnas que llenan el ancho.
        /// </summary>
        public static void EstiloTabla(DataGridView tabla)
        {
            tabla.BackgroundColor = Superficie;
            tabla.BorderStyle = BorderStyle.None;
            tabla.GridColor = Borde;
            tabla.EnableHeadersVisualStyles = false;   // para que tome NUESTROS colores de encabezado
            tabla.RowHeadersVisible = false;           // ocultamos la columna gris de la izquierda
            tabla.AllowUserToAddRows = false;
            tabla.AllowUserToResizeRows = false;
            tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tabla.MultiSelect = false;
            tabla.ReadOnly = true;
            tabla.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tabla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            tabla.ColumnHeadersHeight = 38;
            tabla.RowTemplate.Height = AltoFila;

            tabla.ColumnHeadersDefaultCellStyle.BackColor = FondoApp;
            tabla.ColumnHeadersDefaultCellStyle.ForeColor = TextoSecundario;
            tabla.ColumnHeadersDefaultCellStyle.Font = FuenteEtiqueta;
            tabla.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
            tabla.ColumnHeadersDefaultCellStyle.SelectionBackColor = FondoApp;

            tabla.DefaultCellStyle.BackColor = Superficie;
            tabla.DefaultCellStyle.ForeColor = TextoPrincipal;
            tabla.DefaultCellStyle.Font = FuenteCuerpo;
            tabla.DefaultCellStyle.SelectionBackColor = PrimarioClaro;
            tabla.DefaultCellStyle.SelectionForeColor = TextoPrincipal;
            tabla.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
        }

        /// <summary>
        /// Convierte una etiqueta en un "badge" de estado: la pinta con el color de
        /// la familia que corresponde según el texto del estado (reutiliza el sistema
        /// de 5 familias).
        /// </summary>
        public static void EstiloBadgeEstado(Label etiqueta, string estado)
        {
            Color fondo, texto;
            switch (estado)
            {
                case "En diagnóstico":
                case "En reparación":
                    fondo = EnCursoFondo; texto = EnCursoTexto; break;
                case "Esperando aprobación":
                case "Aprobado":
                    fondo = RequiereAccionFondo; texto = RequiereAccionTexto; break;
                case "Reparado":
                case "Entregado":
                    fondo = TerminadoFondo; texto = TerminadoTexto; break;
                case "Rechazado por cliente":
                case "Dado de baja":
                    fondo = CerradoFondo; texto = CerradoTexto; break;
                default:  // "Ingresado" y cualquier otro caen en "en cola"
                    fondo = EnColaFondo; texto = EnColaTexto; break;
            }
            etiqueta.Text = estado;
            etiqueta.BackColor = fondo;
            etiqueta.ForeColor = texto;
            etiqueta.Font = FuenteMenor;
            etiqueta.AutoSize = true;
            etiqueta.TextAlign = ContentAlignment.MiddleCenter;
            etiqueta.Padding = new Padding(8, 3, 8, 3);
        }
    }
}
