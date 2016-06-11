using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
namespace Generico
{
    public class WSReader
    {
        private static string _sWSUrl;
        private static string _sNombreListin;
        private static Log _LogDelWSReader;

        // Constructor. Lo crea con un path donde leer (en el real, la url del webservice)
        public WSReader(Log LogParametro)
        {
            _sWSUrl = ConfigurationManager.AppSettings["WSUrl"];
            _sNombreListin = ConfigurationManager.AppSettings["Listin"];
            _LogDelWSReader = LogParametro;
        }

        // Propiedades: Path del webservice y nombre del fichero que lee
        public string WSUrl
        {
            get
            {
                return _sWSUrl;
            }
        }
        public string NombreListin
        {
            get
            {
                return _sNombreListin;
            }
        }

        // LeerListin recibe la url con el listin, lo lee y lo transforma en alguna estructura de datos, por ejemplo un array de strings
        // Luego llama al método privado CargarListin, que con esa estructura inserta en base de datos
        // Realmente llamará a un webservice y los resultados irán en cualquier estructura, seguramente un string, luego lo meterá en un
        // ArrayList
        public ArrayList LeerListin()
        {
            try
            {
                ArrayList Listin = new ArrayList();
                StreamReader Lector = new StreamReader(_sWSUrl + _sNombreListin);
                while (!Lector.EndOfStream)
                {
                    string sFila = Lector.ReadLine();
                    string[] sPartesDelListin = sFila.Split(',');
                    string sTelefonoDelListin = sPartesDelListin[0].Replace("'", "");
                    string sCodigoDelListin = sPartesDelListin[1].Replace("'", "");
                    string sNombreDelListin = sPartesDelListin[2].Replace("'", "");
                    string sDato1 = "", sDato2 = "", sDato3 = "", sDato4 = "";
                    if (sPartesDelListin.Length >= 4)
                    {
                        sDato1 = sPartesDelListin[3].Replace("'", "");
                    }
                    if (sPartesDelListin.Length >= 5)
                    {
                        sDato2 = sPartesDelListin[4].Replace("'", "");
                    }
                    if (sPartesDelListin.Length >= 6)
                    {
                        sDato3 = sPartesDelListin[5].Replace("'", "");
                    }
                    if (sPartesDelListin.Length >= 7)
                    {
                        sDato4 = sPartesDelListin[6].Replace("'", "");
                    }
                    string sFilaFormateada = sTelefonoDelListin + "," + sCodigoDelListin + "," + sNombreDelListin + "," + sDato1 + "," + sDato2 + "," + sDato3 + "," + sDato4;
                    Listin.Add(sFilaFormateada);
                }
                Listin.RemoveAt(0);
                return Listin;
            }
            catch (Exception E)
            {
                _LogDelWSReader.Write("Error al leer el listín: ", E.Message + " " + E.StackTrace, HLogLevel.Error);
                return null;
            }
        }
    }
}
