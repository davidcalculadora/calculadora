using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
namespace Generico
{
    public class WSWriter
    {
        private static string _sWSUrl;
        private static string _sFicheroDeResultados;
        private static Log _LogDelWSWriter;

        // Constructor. Lo crea con un path donde escribir (en el real, la url del webservice)
        public WSWriter(Log LogParametro)
        {
            _sWSUrl = ConfigurationManager.AppSettings["WSUrl"];
            _sFicheroDeResultados = ConfigurationManager.AppSettings["Resultados"];
            _LogDelWSWriter = LogParametro;
        }

        // Propiedades: Path del webservice y nombre del fichero que escribr
        public string WSUrl
        {
            get
            {
                return _sWSUrl;
            }
        }
        public string FicheroDeResultados
        {
            get
            {
                return _sFicheroDeResultados;
            }
        }

        // Escribir resultados recibe como parámetro un ArrayList con los resultados leídos y lo escribe en un fichero de texto
        // En la aplicación real no será un fichero de texto, será un método de un webservice y los parámetros serán otros
        public bool EscribirResultados(ArrayList ListaDeResultados)
        {
            try
            {
                StreamWriter FicheroDeResultados = new StreamWriter(_sWSUrl + _sFicheroDeResultados, true);
                foreach (string sResultadoDeLaLista in ListaDeResultados)
                {
                    try
                    { 
                        FicheroDeResultados.WriteLine(sResultadoDeLaLista);
                    }
                    catch (Exception E)
                    {
                        _LogDelWSWriter.Write("ERROR AL ESCRIBIR UN RESULTADO: ", E.Message + " " + E.StackTrace, HLogLevel.Error);
                    }
                }
                FicheroDeResultados.Close();
                _LogDelWSWriter.Write("Escritura de resultados finalizada", "", HLogLevel.Record);
                return true;
            }
            catch (Exception E)
            {
                _LogDelWSWriter.Write("ERROR AL EMPEZAR LA ESCRITURA DE RESULTADOS: ", E.Message + " " + E.StackTrace, HLogLevel.Error);
                return false;
            }
        }
    }
}
