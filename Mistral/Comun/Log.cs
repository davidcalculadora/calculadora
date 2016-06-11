using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms; // Para Application.ExecutablePath !
namespace Generico
{
    public enum HLogLevel
    {
        Detail,
        Record,
        Warning,
        Error
    }

    public class Log
    {
        // Directorio para el log
        private string sLogPath;

        // Ruta+nombre del fichero en el que se almacena el log
        private string sFile;

        // Base para el nombre del fichero 
        private string sBaseName;

        // Streamer de escritura en el fichero
        private StreamWriter fFile;

        // Nivel de escritura en el log
        private HLogLevel LogLevel;

        private string GetLogPath()
        {
            // Obtiene el path (sin terminar en \) del ejecutable
            string sExecutable = Application.ExecutablePath;
            int iSlashPos = sExecutable.LastIndexOf('\\');
            return sExecutable.Substring(0, iSlashPos) + "\\Log";
        }

        private string GetExecutableName()
        {
            // Obtiene el nombre del ejecutable (sin ".exe")
            string sExecutable = Application.ExecutablePath;
            int iSlashPos = sExecutable.LastIndexOf('\\');
            return sExecutable.Substring(iSlashPos+1, sExecutable.Length - (4 + iSlashPos));
        }

        public Log()
        {
            // Inicializamos los atributos a vacío
            sFile = "";
            sLogPath = "";
            fFile = null;
            sBaseName = "";
            LogLevel = HLogLevel.Record;
        }

        public void Open(HLogLevel LogLevel, string sBaseName)
        {
            Monitor.Enter(this);
            try
            {
                // Nivel de escritura en el log
                this.LogLevel = LogLevel;

                // Directorio en el que reside el ejecutable
                sLogPath = GetLogPath();

                // Si no se ha especificado, el nombre se compone con el propio ejecutable
                if (sBaseName == "")
                {
                    this.sBaseName = GetExecutableName();
                }
                else
                {
                    this.sBaseName = sBaseName;
                }

                // Fichero en el que almacenaremos el log ahora
                sFile = sLogPath + "\\" + this.sBaseName + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + ".log";

                // Si el directorio no existe ...
                if (!Directory.Exists(sLogPath))
                {
                    Directory.CreateDirectory(sLogPath);
                }

                // Creamos el streamer de escritura
                fFile = File.AppendText(sFile);

                // Añadimos un primer registro al log
                PrivateWrite("Log.Open", DateTime.Now.ToLongDateString(), HLogLevel.Record);
            }
            finally
            {
                Monitor.Exit(this);
            }
        }

        public void Close()
        {
            // Método para cerrar el fichero
            Monitor.Enter(this);
            try
            {
                // Añadimos un primer registro al log
                PrivateWrite("Log.Close", DateTime.Now.ToLongDateString(), HLogLevel.Record);

                // Cerramos el fichero
                fFile.Close();
                fFile = null;
            }
            finally
            {
                Monitor.Exit(this);
            }
        }

        public void SetLogLevel(HLogLevel LogLevel)
        {
            // Método para modificar el nivel de detalle del log
            Monitor.Enter(this);
            try
            {
                this.LogLevel = LogLevel;
            }
            finally
            {
                Monitor.Exit(this);
            }
        }

        public void Write(string sEvent, string sData, HLogLevel Level)
        {
            // Método para escribir en el fichero de log
            Monitor.Enter(this);
            try
            {
                // Si el log está abierto y el nivel de detalle es adecuado ...
                if ( (fFile != null) && (Level >= LogLevel) )
                {
                    // Obtenemos el nombre del fichero en el que deberíamos escribir
                    string aux = sLogPath + "\\" + this.sBaseName + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + ".log";

                    // Si no es el fichero en el que estamos escribiendo ...
                    if (aux != sFile)
                    {
                        // Hemos cambiado de día.
                        sFile = aux;
                        fFile.Close();
                        File.Delete(sFile);
                        fFile = File.AppendText(sFile);
                        PrivateWrite("Log.Open", DateTime.Now.ToLongDateString(), HLogLevel.Record);
                    }

                    // Escribimos en el fichero
                    PrivateWrite(sEvent, sData, Level);
                }
            }
            finally
            {
                Monitor.Exit(this);
            }
        }

        private void PrivateWrite(string sEvent, string sData, HLogLevel Level)
        {
            // Escritura en el fichero
            fFile.WriteLine
            (
                DateTime.Now.ToLongTimeString() + ", " + Level.ToString() + ", " + sEvent + sData// + "," + sData
            );

            // Nos aseguramos de que no se quede en el buffer
            fFile.Flush(); 
        }
    }
}
