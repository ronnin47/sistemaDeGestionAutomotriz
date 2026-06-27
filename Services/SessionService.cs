using System;
using System.IO;
using Newtonsoft.Json;
using sistemaDeGestionAutomotriz.Models;


namespace sistemaDeGestionAutomotriz
{
    public static class SessionService
    {







































        //Persistencia en memoria local de la sesion 
        private static readonly string Carpeta =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "SistemaGestionAutomotriz");

        private static readonly string Archivo =
            Path.Combine(Carpeta, "session.json");

        public static bool HaySesion()
        {
            return File.Exists(Archivo);
        }
   
        public static void GuardarSesion(int usuarioId)
        {
            if (!Directory.Exists(Carpeta))
                Directory.CreateDirectory(Carpeta);

            Session sesion = new Session
            {
                UsuarioId = usuarioId
            };

            string json = JsonConvert.SerializeObject(sesion);

            File.WriteAllText(Archivo, json);
        }

        public static Session ObtenerSesion()
        {
            if (!File.Exists(Archivo))
                return null;

            string json = File.ReadAllText(Archivo);

            return JsonConvert.DeserializeObject<Session>(json);
        }

        public static void CerrarSesion()
        {
            if (File.Exists(Archivo))
                File.Delete(Archivo);
        }
    }
}


