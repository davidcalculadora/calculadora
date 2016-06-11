using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Configuration;
namespace Generico
{
    public class Campaña
    {
        private string _sCodigoCampaña;
        private string _sNombreCampaña;
        private static string _sConexionCampaña = "Provider=SQLOLEDB.1;Persist Security Info=True;User ID=hermes2;Initial Catalog=hEngine;Data Source=DAVID-PC\\SQLEXPRESS;Password=hermes2;";
        private static OleDbConnection _ConexionCampaña = new OleDbConnection(_sConexionCampaña);
        private static Log _LogDeLaCampaña;

        // Constructor
        public Campaña(Log LogParametro)
        {
            string sFechaHora = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            _sCodigoCampaña = "C-" + sFechaHora;
            _sNombreCampaña = "Campaña-" + sFechaHora;
            _LogDeLaCampaña = LogParametro;
            AbrirConexion();
        }

        // Propiedad: código de campaña
        public string CodigoCampaña
        {
            get
            {
                return _sCodigoCampaña;
            }
        }
        // Propiedad: conexión (para saber si se ha abierto o no y poder capturar la excepción)
        public OleDbConnection ConexionCampaña
        {
            get
            {
                return _ConexionCampaña;
            }
        }

        #region Métodos

        // Métodos para abrir y cerrar conexiones
        public void AbrirConexion()
        {
            try
            {
                if (_ConexionCampaña.State == ConnectionState.Closed)
                { 
                    _ConexionCampaña.Open();
                }
            }
            catch (Exception E)
            {
                _LogDeLaCampaña.Write("ERROR AL ABRIR CONEXIONES: ", E.StackTrace, HLogLevel.Error);
            }
        }
        public void CerrarConexion()
        {
            try
            {
                if (_ConexionCampaña.State == ConnectionState.Open)
                {
                    _ConexionCampaña.Close();
                }
            }
            catch (Exception E)
            {
                _LogDeLaCampaña.Write("ERROR AL CERRAR CONEXIONES: ", E.StackTrace, HLogLevel.Error);
            }
        }

        // Crea una nueva campaña 
        public bool Crear()
        {
            bool fCrear = false;
            try
            {
                if (_ConexionCampaña.State == ConnectionState.Closed)
                {
                    _ConexionCampaña.Open();
                }
                string sFinalizacion = ConfigurationManager.AppSettings["Finalizacion"];
                int iTNocontesta = Int32.Parse(ConfigurationManager.AppSettings["TNocontesta"]);
                int iMaxLineas = Int32.Parse(ConfigurationManager.AppSettings["MaxLineas"]);
                string sMensajeWav = ConfigurationManager.AppSettings["MensajeWav"];
                int iMaxTEspera = Int32.Parse(ConfigurationManager.AppSettings["MaxTEspera"]);
                int iMaxReintentos = Int32.Parse(ConfigurationManager.AppSettings["MaxReintentos"]);
                int iTEntreIntNoContesta = Int32.Parse(ConfigurationManager.AppSettings["TEntreIntNoContesta"]);
                int iTEntreIntComunica = Int32.Parse(ConfigurationManager.AppSettings["TEntreIntComunica"]);
                int iTEntreIntErrorLinea = Int32.Parse(ConfigurationManager.AppSettings["TEntreIntErrorLinea"]);
                int iIntensidad = Int32.Parse(ConfigurationManager.AppSettings["Intensidad"]);
                string sEstado = ConfigurationManager.AppSettings["Estado"];
                string sTransferToExtension = ConfigurationManager.AppSettings["TransferToExtension"];
                string sModo = ConfigurationManager.AppSettings["Modo"];
                string sSQLInsertarCampaña = "insert into Campanyas values (GETDATE(), '" + _sCodigoCampaña + "', '" + _sNombreCampaña +
                                             "', '" + sFinalizacion + "', " + iTNocontesta + 
                                             ", " + iMaxLineas + ", '" + sMensajeWav + "', " + iMaxTEspera + ", " + iMaxReintentos + 
                                             ", " + iTEntreIntNoContesta + ", " + iTEntreIntComunica + ", " + iTEntreIntErrorLinea + 
                                             ", " + iIntensidad + ", '" + sEstado + "', '" + sTransferToExtension + "', '" + sModo + "')";
                OleDbCommand CommandInsertarCampaña = new OleDbCommand(sSQLInsertarCampaña, _ConexionCampaña);
                CommandInsertarCampaña.ExecuteNonQuery();
                fCrear = true;
            }
            catch (Exception E)
            {
                _LogDeLaCampaña.Write("ERROR AL CREAR LA CAMPAÑA: ", E.Message + " " + E.StackTrace, HLogLevel.Error);
            }
            return fCrear;
        }

        // Crea el horario
        public bool CrearHorario(string sCodigoCampaña)
        {
            bool fCrearHorario = false;
            try
            {
                if (_ConexionCampaña.State == ConnectionState.Closed)
                {
                    _ConexionCampaña.Open();
                }
                // Leemos las variables que componen el horario
                int iActivoLunesViernes = Int32.Parse(ConfigurationManager.AppSettings["ActivoLunesViernes"]);
                string sHoraInicioLunesViernes1 = ConfigurationManager.AppSettings["HoraInicioLunesViernes1"].Replace(":", "");
                string sHoraFinLunesViernes1 = ConfigurationManager.AppSettings["HoraFinLunesViernes1"].Replace(":", "");
                string sHoraInicioLunesViernes2 = ConfigurationManager.AppSettings["HoraInicioLunesViernes2"].Replace(":", "");
                string sHoraFinLunesViernes2 = ConfigurationManager.AppSettings["HoraFinLunesViernes2"].Replace(":", "");
                int iActivoSabado = Int32.Parse(ConfigurationManager.AppSettings["ActivoSabado"]);
                string sHoraInicioSabado1 = ConfigurationManager.AppSettings["HoraInicioSabado1"].Replace(":", "");
                string sHoraFinSabado1 = ConfigurationManager.AppSettings["HoraFinSabado1"].Replace(":", "");
                string sHoraInicioSabado2 = ConfigurationManager.AppSettings["HoraInicioSabado2"].Replace(":", "");
                string sHoraFinSabado2 = ConfigurationManager.AppSettings["HoraFinSabado2"].Replace(":", "");
                int iActivoDomingo = Int32.Parse(ConfigurationManager.AppSettings["ActivoDomingo"]);
                string sHoraInicioDomingo1 = ConfigurationManager.AppSettings["HoraInicioDomingo1"].Replace(":", "");
                string sHoraFinDomingo1 = ConfigurationManager.AppSettings["HoraFinDomingo1"].Replace(":", "");
                string sHoraInicioDomingo2 = ConfigurationManager.AppSettings["HoraInicioDomingo2"].Replace(":", "");
                string sHoraFinDomingo2 = ConfigurationManager.AppSettings["HoraFinDomingo2"].Replace(":", "");
                string sFestivos = ConfigurationManager.AppSettings["Festivos"];
                string sInsertarHorarios = "insert into Horario values ('" + sCodigoCampaña + "', '" + sHoraInicioLunesViernes1 + 
                "', '" + sHoraFinLunesViernes1 + "', " + iActivoLunesViernes + ", '" + sHoraInicioSabado1 + "', '" + sHoraFinSabado1 + 
                "', '" + iActivoSabado + "', '" + sHoraInicioDomingo1 + "', '" + sHoraFinDomingo2 + "', " + iActivoDomingo + 
                ", '" + sFestivos + "', '" + sHoraInicioLunesViernes2 + "', '" + sHoraFinLunesViernes2 + "', '" + sHoraInicioSabado2 + 
                "', '" + sHoraFinSabado2 + "', '" + sHoraInicioDomingo2 + "', '" + sHoraFinDomingo2 + "')";
                OleDbCommand CommandInsertarHorarios = new OleDbCommand(sInsertarHorarios, _ConexionCampaña);
                CommandInsertarHorarios.ExecuteNonQuery();
                fCrearHorario = true;
            }
            catch (Exception E)
            {
                _LogDeLaCampaña.Write("ERROR AL CREAR EL HORARIO DE LA CAMPAÑA: ", E.Message + " " + E.StackTrace, HLogLevel.Error);
            }
            return fCrearHorario;
        }

        // Crea los listines
        public bool CargarListin(string sCodigoCampaña)
        {
            bool fCargarListin = false;
            try
            {
                if (_ConexionCampaña.State == ConnectionState.Closed)
                {
                    _ConexionCampaña.Open();
                }
                WSReader LectorDeListines = new WSReader(_LogDeLaCampaña);
                ArrayList Listin = LectorDeListines.LeerListin();
                foreach (string sListin in Listin)
                {
                    try
                    {
                        string[] sPartesDelListin = sListin.Split(',');
                        string sTelefonoDelListin = sPartesDelListin[0];
                        string sCodigoDelListin = sPartesDelListin[1];
                        string sNombreDelListin = sPartesDelListin[2];
                        string sDato1 = sPartesDelListin[3];
                        string sDato2 = sPartesDelListin[4];
                        string sDato3 = sPartesDelListin[5];
                        string sDato4 = sPartesDelListin[6];
                        /*string sTelefonoDelListin = sPartesDelListin[0].Replace("'", "");
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
                        }*/
                        // Insertamos en la tabla de Clientes_Telefonos
                        OleDbCommand CommandInsertarTelefonosDeLosClientes = new OleDbCommand("insert into Clientes_Telefonos values ('" + sCodigoDelListin + "', '" + sTelefonoDelListin + "', 1)", _ConexionCampaña);
                        CommandInsertarTelefonosDeLosClientes.ExecuteNonQuery();
                        // Insertamos en la tabla de Listines
                        string sInsertarListines = "insert into Listines (FechaCreacion, CodCampanya, CodCliente, NomCliente, HorasDeContacto," +
                        " Dato1, Dato2, Dato3, Dato4) values (GETDATE(), '" + sCodigoCampaña + "', '" + sCodigoDelListin +
                        "', '" + sNombreDelListin + "', '', '" + sDato1 + "', '" + sDato2 + "', '" + sDato3 + "', '" + sDato4 + "')";
                        OleDbCommand CommandInsertarListines = new OleDbCommand(sInsertarListines, _ConexionCampaña);
                        CommandInsertarListines.ExecuteNonQuery();
                    }
                    catch (Exception E)
                    {
                        _LogDeLaCampaña.Write("Error al insertar en el listín: ", E.Message + " " + E.StackTrace, HLogLevel.Error);
                    }
                }
                fCargarListin = true;
            }
            catch (Exception E)
            {
                _LogDeLaCampaña.Write("Error al crear el listín: ", E.Message + " " + E.StackTrace, HLogLevel.Error);
            }
            return fCargarListin;
        }

        // Inicia una campaña
        public bool Iniciar(string sCodigoCampaña)
        {
            bool fIniciar = false;
            try
            {
                if (_ConexionCampaña.State == ConnectionState.Closed)
                {
                    _ConexionCampaña.Open();
                }
                // Cambiamos el estado de la campaña
                OleDbCommand CommandIniciarCampaña = new OleDbCommand("update Campanyas set Estado='Ejecucion' where Codigo='" + sCodigoCampaña + "'", _ConexionCampaña);
                CommandIniciarCampaña.ExecuteNonQuery();
                fIniciar = true;
            }
            catch (Exception E)
            {
                _LogDeLaCampaña.Write("ERROR AL INICIAR LA CAMPAÑA: ", E.Message + " " + E.StackTrace, HLogLevel.Error);
            }
            return fIniciar;
        }

        // Comprueba si tiene los listines creados
        public bool TieneListinesCreados(string sCodigoCampaña)
        {
            bool fTieneListinesCreados = false;
            try
            {
                if (_ConexionCampaña.State == ConnectionState.Closed)
                {
                    _ConexionCampaña.Open();
                }
                // Miramos si en la tabla de Listines y de Clientes_Telefonos hay alguna campaña con ese nombre
                OleDbDataReader ReaderVerListines = null;
                OleDbCommand CommandVerListines = new OleDbCommand("select id from Listines where CodCampanya='" + sCodigoCampaña + "'", _ConexionCampaña);
                ReaderVerListines = CommandVerListines.ExecuteReader();
                if (ReaderVerListines.Read())
                {
                    fTieneListinesCreados = true;
                }
                else
                {
                    _LogDeLaCampaña.Write("No hay listines creados", "", HLogLevel.Warning);
                }
            }
            catch (Exception E)
            {
                _LogDeLaCampaña.Write("ERROR AL COMPROBAR SI HAY LISTINES CREADOS: ", E.Message + " " + E.StackTrace, HLogLevel.Error);
            }
            return fTieneListinesCreados;
        }

        // Método para escribir los resultados de una campaña
        public bool EscribirResultadosDeLaCampaña(string sCodigoCampaña)
        {
            bool fEscribirResultadosDeLaCampaña = false;
            try
            {
                if (_ConexionCampaña.State == ConnectionState.Closed)
                {
                    _ConexionCampaña.Open();
                }
                ArrayList ListaDeResultados = new ArrayList();
                try
                {
                    OleDbDataReader ReaderLeerResultadosDeLaCampaña = null;
                    OleDbCommand CommandLeerResultadosDeLaCampaña = new OleDbCommand("select CodCampanya, CodCliente, Telefono, Resultado, Transferencia, ExtensionRing, ExtensionAtendida from StatsCampanyas", _ConexionCampaña);
                    ReaderLeerResultadosDeLaCampaña = CommandLeerResultadosDeLaCampaña.ExecuteReader();
                    while (ReaderLeerResultadosDeLaCampaña.Read())
                    {
                        string sCodCampanya = ReaderLeerResultadosDeLaCampaña.GetString(0);
                        string sCodCliente = ReaderLeerResultadosDeLaCampaña.GetString(1);
                        string sTelefono = ReaderLeerResultadosDeLaCampaña.GetString(2);
                        string sResultado = ReaderLeerResultadosDeLaCampaña.GetString(3);
                        string sTransferencia = ReaderLeerResultadosDeLaCampaña.GetString(4);
                        string sExtensionRing = ReaderLeerResultadosDeLaCampaña.GetString(5);
                        string sExtensionAtendida = ReaderLeerResultadosDeLaCampaña.GetString(6);
                        string sResultadoDeLaLista = sCodCampanya + "," + sCodCliente + "," + sTelefono + "," + sTelefono + "," + sResultado + "," + sTransferencia + "," + sExtensionRing + "," + sExtensionAtendida;
                        ListaDeResultados.Add(sResultadoDeLaLista);
                    }
                    ReaderLeerResultadosDeLaCampaña.Close();
                }
                catch (Exception E)
                {
                    _LogDeLaCampaña.Write("ERROR DURANTE LA LECTURA DE RESULTADOS", E.Message + " " + E.StackTrace, HLogLevel.Error);
                }
                WSWriter EscritorDeResultados = new WSWriter(_LogDeLaCampaña);
                if (EscritorDeResultados.EscribirResultados(ListaDeResultados))
                {
                    fEscribirResultadosDeLaCampaña = true;
                }
            }
            catch (Exception E)
            {
                _LogDeLaCampaña.Write("ERROR AL EMPEZAR LA LECTURA DE RESULTADOS: ", E.Message + " " + E.StackTrace, HLogLevel.Error);
            }
            return fEscribirResultadosDeLaCampaña;
        }

        // Busca si una campaña está terminada
        public static bool Terminada(string sCodigoCampaña)
        {
            bool fTerminada = false;
            try
            {
                if (_ConexionCampaña.State == ConnectionState.Closed)
                {
                    _ConexionCampaña.Open();
                }
                OleDbDataReader ReaderEstadoCampaña = null;
                OleDbCommand CommandEstadoCampaña = new OleDbCommand("select estado from Campanyas where codigo='" + sCodigoCampaña + "'", _ConexionCampaña);
                ReaderEstadoCampaña = CommandEstadoCampaña.ExecuteReader();
                if (ReaderEstadoCampaña.HasRows)
                {
                    if (ReaderEstadoCampaña.Read())
                    {
                        string sEstado = ReaderEstadoCampaña.GetString(0);
                        if (sEstado == "Finalizada")
                        {
                            fTerminada = true;
                        }
                    }
                }
                else
                {
                    _LogDeLaCampaña.Write("EXCEPCION AL BUSCAR EL ESTADO DE LA CAMPAÑA: ", "La campaña con código " + sCodigoCampaña + " no existe", HLogLevel.Warning);
                }
            }
            catch (Exception E)
            {
                _LogDeLaCampaña.Write("ERROR AL BUSCAR EL ESTADO DE LA CAMPAÑA " + sCodigoCampaña + ": ", E.Message + " " + E.StackTrace, HLogLevel.Error);
            }
            return fTerminada;
        }

        // Busca el código de la última campaña creada
        public static string CodigoDeLaUltimaCampañaCreada()
        {
            string sCodigoDeLaUltimaCampañaCreada = "";
            try
            {
                if (_ConexionCampaña.State == ConnectionState.Closed)
                {
                    _ConexionCampaña.Open();
                }
                OleDbDataReader ReaderCodigoDeLaUltimaCampañaCreada = null;
                OleDbCommand CommandCodigoDeLaUltimaCampañaCreada = new OleDbCommand("select top 1 codigo from Campanyas order by id desc", _ConexionCampaña);
                ReaderCodigoDeLaUltimaCampañaCreada = CommandCodigoDeLaUltimaCampañaCreada.ExecuteReader();
                if (ReaderCodigoDeLaUltimaCampañaCreada.HasRows)
                {
                    if (ReaderCodigoDeLaUltimaCampañaCreada.Read())
                    {
                        sCodigoDeLaUltimaCampañaCreada = ReaderCodigoDeLaUltimaCampañaCreada.GetString(0);
                    }
                }
                else
                {
                    _LogDeLaCampaña.Write("EXCEPCION AL BUSCAR EL ESTADO DE LA ÚLTIMA CAMPAÑA: ", "No hay campañas creadas", HLogLevel.Warning);
                }
            }
            catch (Exception E)
            {
                _LogDeLaCampaña.Write("ERROR AL BUSCAR EL ESTADO DE LA ÚLTIMA CAMPAÑA: ", E.Message + " " + E.StackTrace, HLogLevel.Error);
            }
            return sCodigoDeLaUltimaCampañaCreada;
        }

        // Este método se ejecuta al principio del todo y borra los listines y actualiza el estado de las campañas
        public bool Resetear()
        {
            bool fResetear = false;
            try
            {
                if (_ConexionCampaña.State == ConnectionState.Closed)
                {
                    _ConexionCampaña.Open();
                }
                // Borrar tabla de Listines y de Clientes_Telefonos
                OleDbCommand CommandBorrarListines = new OleDbCommand("delete from Listines", _ConexionCampaña);
                CommandBorrarListines.ExecuteNonQuery();
                OleDbCommand CommandBorrarClientesTelefonos = new OleDbCommand("delete from Clientes_Telefonos", _ConexionCampaña);
                CommandBorrarClientesTelefonos.ExecuteNonQuery();
                // Actualizar el estado de las campañas a Finalizado
                OleDbCommand CommandActualizarCampañas = new OleDbCommand("update Campanyas set Estado='Resuelta'", _ConexionCampaña);
                CommandActualizarCampañas.ExecuteNonQuery();
                fResetear = true;
            }
            catch (Exception E)
            {
                _LogDeLaCampaña.Write("ERROR AL RESETEAR LAS CAMPAÑAS: ", E.Message + " " + E.StackTrace, HLogLevel.Error);
            }
            return fResetear;
        }
        #endregion
    }
}
