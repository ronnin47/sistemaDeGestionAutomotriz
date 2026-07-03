using System.Drawing;
using System.Windows.Forms;

namespace sistemaDeGestionAutomotriz.UI
{
    // Paleta, tipografía, espaciado, íconos y estilos centralizados.
    // Soporta modo claro y oscuro (cada color tiene sus dos versiones).
    public static class Tema
    {
        public static bool ModoOscuro { get; set; } = false;

        private static Color C(string claro, string oscuro)
        {
            return ColorTranslator.FromHtml(ModoOscuro ? oscuro : claro);
        }

        // Neutros
        public static Color FondoApp => C("#F4F6F8", "#0F1720");
        public static Color Superficie => C("#FFFFFF", "#1A2430");
        public static Color Borde => C("#E1E5EA", "#2A3744");
        public static Color TextoPrincipal => C("#1F2933", "#E4E8EC");
        public static Color TextoSecundario => C("#5B6672", "#9AA5B1");

        // Marca
        public static Color Primario => C("#2D6A8E", "#4A9CC4");
        public static Color PrimarioOscuro => C("#1F4E68", "#16202B");
        public static Color PrimarioClaro => C("#E8F1F6", "#1E3A4C");
        public static Color TextoSobrePrimario => C("#FFFFFF", "#FFFFFF");

        // Estados de una orden (mismo significado, mismo color)
        public static Color EnColaFondo => C("#E1E5EA", "#2C2C2A");          // Ingresado
        public static Color EnColaTexto => C("#3A4450", "#B4B2A9");
        public static Color EnCursoFondo => C("#C7DEF5", "#103049");         // En diagnóstico / En reparación
        public static Color EnCursoTexto => C("#0C447C", "#9CC9EC");
        public static Color RequiereAccionFondo => C("#FAEEDA", "#4A3A12");  // Esperando aprobación / Aprobado
        public static Color RequiereAccionTexto => C("#854F0B", "#F0C779");
        public static Color TerminadoFondo => C("#E1F5EE", "#0E3D31");       // Reparado / Entregado
        public static Color TerminadoTexto => C("#0F6E56", "#7FD3B4");
        public static Color CerradoFondo => C("#FCEBEB", "#501313");         // Rechazado por cliente / Dado de baja
        public static Color CerradoTexto => C("#A32D2D", "#F09595");

        // Tipografía (Segoe UI, nativa de Windows)
        private const string Familia = "Segoe UI";
        public static readonly Font FuenteTitulo = new Font(Familia, 18F, FontStyle.Bold);
        public static readonly Font FuenteSeccion = new Font(Familia, 12F, FontStyle.Bold);
        public static readonly Font FuenteCuerpo = new Font(Familia, 9.75F, FontStyle.Regular);
        public static readonly Font FuenteEtiqueta = new Font(Familia, 9F, FontStyle.Regular);
        public static readonly Font FuenteMenor = new Font(Familia, 8.25F, FontStyle.Regular);
        public static readonly Font FuenteBoton = new Font(Familia, 9.75F, FontStyle.Bold);

        // Espaciado (px)
        public const int EspacioXs = 4;
        public const int EspacioSm = 8;
        public const int EspacioMd = 16;
        public const int EspacioLg = 24;
        public const int EspacioXl = 32;
        public const int PaddingPantalla = 20;
        public const int AltoFila = 34;

        // Íconos: fuente Segoe MDL2 Assets (Windows 10/11), sin dependencias.
        public static Font FuenteIcono(float tam = 12F)
        {
            return new Font("Segoe MDL2 Assets", tam);
        }

        /// <summary>Glifos de íconos (fuente Segoe MDL2 Assets).</summary>
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
            public static readonly string Ordenes = char.ConvertFromUtf32(0xE90F);
            public static readonly string Ventas = char.ConvertFromUtf32(0xE7BF);
            public static readonly string Cotizaciones = char.ConvertFromUtf32(0xE8A5);
            public static readonly string Garantias = char.ConvertFromUtf32(0xEB95);
            public static readonly string ModoOscuro = char.ConvertFromUtf32(0xE708);
            public static readonly string ModoClaro = char.ConvertFromUtf32(0xE706);
            public static readonly string CerrarSesion = char.ConvertFromUtf32(0xF3B1);
        }

        /// <summary>Título de una pantalla.</summary>
        public static void EstiloTituloPantalla(Label titulo)
        {
            titulo.Font = FuenteTitulo;
            titulo.ForeColor = TextoPrincipal;
            titulo.AutoSize = true;
        }

        /// <summary>Subtítulo / contador debajo del título.</summary>
        public static void EstiloSubtitulo(Label sub)
        {
            sub.Font = FuenteEtiqueta;
            sub.ForeColor = TextoSecundario;
            sub.AutoSize = true;
        }

        /// <summary>Botón del menú lateral (plano, texto claro, hover azul).</summary>
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

        /// <summary>Botón de acción principal (azul lleno).</summary>
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

        /// <summary>Botón secundario (blanco con borde, texto azul).</summary>
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

        /// <summary>Botón de peligro (borde y texto rojo).</summary>
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

        /// <summary>Botón solo-ícono (ej. editar/eliminar en una fila).</summary>
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

        /// <summary>Pone un ícono (glifo Segoe MDL2) a la izquierda del texto del botón.</summary>
        public static void PonerIcono(Button boton, string glifo, bool centrado = false, float tam = 13F)
        {
            if (boton.Image != null) boton.Image.Dispose();
            boton.Image = GlifoAImagen(glifo, boton.ForeColor, tam);
            boton.ImageAlign = centrado ? ContentAlignment.MiddleCenter : ContentAlignment.MiddleLeft;
            boton.TextAlign = centrado ? ContentAlignment.MiddleCenter : ContentAlignment.MiddleLeft;
            boton.TextImageRelation = TextImageRelation.ImageBeforeText;
        }

        private static Bitmap GlifoAImagen(string glifo, Color color, float tam)
        {
            int lado = (int)(tam * 1.7F);
            Bitmap bmp = new Bitmap(lado, lado);
            using (Graphics g = Graphics.FromImage(bmp))
            using (Font f = FuenteIcono(tam))
            using (Brush b = new SolidBrush(color))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString(glifo, f, b, new RectangleF(0, 0, lado, lado), sf);
            }
            return bmp;
        }

        /// <summary>Estilo de una caja de texto.</summary>
        public static void EstiloInput(TextBox caja)
        {
            caja.BorderStyle = BorderStyle.FixedSingle;
            caja.BackColor = Superficie;
            caja.ForeColor = TextoPrincipal;
            caja.Font = FuenteCuerpo;
        }

        /// <summary>Estilo de una lista desplegable.</summary>
        public static void EstiloCombo(ComboBox combo)
        {
            combo.FlatStyle = FlatStyle.Flat;
            combo.BackColor = Superficie;
            combo.ForeColor = TextoPrincipal;
            combo.Font = FuenteCuerpo;
        }

        /// <summary>Estilo de la tabla: encabezado gris, líneas horizontales, fila seleccionada azul.</summary>
        public static void EstiloTabla(DataGridView tabla)
        {
            tabla.BackgroundColor = Superficie;
            tabla.BorderStyle = BorderStyle.None;
            tabla.GridColor = Borde;
            tabla.EnableHeadersVisualStyles = false;
            tabla.RowHeadersVisible = false;
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

        /// <summary>Devuelve los colores (fondo y texto) de la familia de un estado.</summary>
        public static void ColoresEstado(string estado, out Color fondo, out Color texto)
        {
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
                default:
                    fondo = EnColaFondo; texto = EnColaTexto; break;
            }
        }

        /// <summary>Pinta una etiqueta como badge del estado (según su familia de color).</summary>
        public static void EstiloBadgeEstado(Label etiqueta, string estado)
        {
            Color fondo, texto;
            ColoresEstado(estado, out fondo, out texto);
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
