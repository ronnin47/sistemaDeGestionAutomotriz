# Documentación de diseño — Sistema de Gestión Automotriz

**Proyecto:** Sistema de gestión para laboratorio de electrónica automotriz
**Alcance de este documento:** interfaz de usuario (UI) y experiencia (UX)

> Este documento describe el sistema de diseño de la aplicación en su **estado vigente**. Reúne las definiciones visuales y de interacción que rigen toda la interfaz. Ante cualquier cambio, el documento se actualiza para reflejar el estado final; no conserva el historial del proceso.

---

## 1. Principios de diseño

La aplicación es una herramienta de trabajo de uso prolongado. El diseño prioriza:

- **Bajo cansancio visual:** no se utiliza blanco puro ni negro puro; las superficies grandes son neutras y el color saturado se reserva para acentos pequeños.
- **Claridad:** la información relevante (estados, alertas) se distingue de un vistazo.
- **Consistencia:** todas las pantallas comparten la misma paleta, tipografía, espaciado y componentes.
- **Proporción 60/30/10:** 60 % neutros, 30 % color de marca, 10 % acentos.

---

## 2. Paleta de colores

Identidad visual "azul acero / pizarra". Cada color tiene una versión para modo claro y otra para modo oscuro.

### Neutros
| Rol | Claro | Oscuro |
|---|---|---|
| Fondo de la aplicación | `#F4F6F8` | `#0F1720` |
| Superficie (tarjetas, tablas) | `#FFFFFF` | `#1A2430` |
| Bordes | `#E1E5EA` | `#2A3744` |
| Texto principal | `#1F2933` | `#E4E8EC` |
| Texto secundario | `#5B6672` | `#9AA5B1` |

### Marca
| Rol | Claro | Oscuro |
|---|---|---|
| Primario (botones, acción) | `#2D6A8E` | `#4A9CC4` |
| Primario oscuro (menú lateral) | `#1F4E68` | `#16202B` |
| Primario claro (selección, hover suave) | `#E8F1F6` | `#1E3A4C` |

---

## 3. Estados de una orden

Los estados se agrupan en cinco familias de color según su significado operativo (mismo significado, mismo color):

| Familia | Color | Estados incluidos |
|---|---|---|
| En cola | Gris `#E1E5EA` / `#3A4450` | Ingresado |
| En proceso (el laboratorio trabaja) | Azul `#E6F1FB` / `#0C447C` | En diagnóstico, En reparación |
| Requiere acción | Ámbar `#FAEEDA` / `#854F0B` | Esperando aprobación, Aprobado |
| Terminado | Verde `#E1F5EE` / `#0F6E56` | Reparado, Entregado |
| Cerrado sin servicio | Rojo `#FCEBEB` / `#A32D2D` | Rechazado por cliente, Dado de baja |

En modo oscuro cada familia invierte la relación: fondo oscuro de la familia con texto claro.

---

## 4. Tipografía

Fuente única: **Segoe UI** (nativa de Windows). Dos pesos: normal y negrita. Escala de cinco roles:

| Rol | Tamaño | Uso |
|---|---|---|
| Título | 18 pt, negrita | título de pantalla |
| Sección | 12 pt, negrita | encabezado de tarjeta o bloque |
| Cuerpo | 9,75 pt | texto general, celdas de tabla, inputs |
| Etiqueta | 9 pt | rótulos de campos y columnas |
| Menor | 8,25 pt | contadores, ayudas, badges |

---

## 5. Espaciado

Ritmo único en píxeles para márgenes, paddings y separaciones: **4 / 8 / 16 / 24 / 32**. Padding interno estándar de pantalla: 20 px. Alto de fila de tabla, input y botón: 34 px.

---

## 6. Componentes base

- **Botones:** primario (azul lleno, acción principal), secundario (borde, acciones menores) y de peligro (rojo con borde, acciones destructivas).
- **Campos:** cajas de texto con borde simple y listas desplegables planas.
- **Tabla (DataGridView):** encabezado gris suave, solo líneas horizontales, fila seleccionada en azul claro, columnas que ocupan el ancho disponible.
- **Badges de estado:** etiqueta de color que aplica la familia correspondiente al estado.

---

## 7. Plantilla de pantalla

Todas las pantallas comparten un mismo esqueleto:

1. **Encabezado:** título + contador a la izquierda; acción principal a la derecha.
2. **Barra de herramientas (opcional):** buscador y filtros.
3. **Cuerpo:** contenido principal, habitualmente una tabla.

---

## 8. Patrones de feedback

- **Confirmación:** diálogo Sí/No antes de acciones destructivas (por ejemplo, eliminar).
- **Validación:** el campo con error se marca y se muestra un mensaje contiguo, sin recurrir a ventanas emergentes.
- **Éxito:** aviso breve de operación realizada.
- **Estado vacío:** cuando no hay datos, se muestra un mensaje orientativo en lugar de una tabla en blanco.

---

## 9. Iconografía

Íconos provistos por la fuente **Segoe MDL2 Assets**, incluida en Windows 10 y 11. No requiere instalación de recursos ni dependencias externas.

---

## 10. Modo claro y oscuro

La interfaz contempla ambos modos. Cada color de la paleta define su valor para claro y para oscuro; el cambio de modo conmuta todos los colores manteniendo el contraste (el texto siempre contrasta con su fondo).

---

## 11. Organización en el código

- `UI/Tema.cs`: centraliza la paleta, la tipografía, el espaciado, los íconos y los estilos de los componentes.
- `UI/Avisos.cs`: mensajes estándar al usuario (confirmación, éxito, error).
- Los estilos se aplican por código (no desde el diseñador visual), de modo que se mantengan consistentes y no se sobrescriban al regenerarse los formularios.
