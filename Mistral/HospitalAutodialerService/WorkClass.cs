using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Timers;
using System.Configuration;
using Generico;
namespace HospitalAutodialerService
{
    public class WorkClass
    {

        #region Atributos privados

        private Log LogHospitalAutodialer;
        private Campaña MiCampaña;
        private HScheduler OrganizadorDeInicioDeCampaña;
        private HScheduler OrganizadorDeCargaDeResultados;
        private string sHorasInicioCampaña = "";
        private string sHorasCargaResultados = "";
        private System.Timers.Timer MiReloj;

        #endregion

        #region Atributos públicos
        #endregion

        #region Métodos públicos

        // Constructor
        public WorkClass()
        {
            LogHospitalAutodialer = new Log();
            OrganizadorDeInicioDeCampaña = new HScheduler();
            sHorasInicioCampaña = ConfigurationManager.AppSettings["HorasInicioCampaña"];
            //OrganizadorDeInicioDeCampaña.AssignSchedule(sHorasInicioCampaña);
            OrganizadorDeCargaDeResultados = new HScheduler();
            sHorasCargaResultados = ConfigurationManager.AppSettings["HorasCargaResultados"];
            //OrganizadorDeCargaDeResultados.AssignSchedule(sHorasCargaResultados);
            MiReloj = new System.Timers.Timer();
            MiReloj.Interval = 60000;
            MiReloj.Enabled = false;
            MiReloj.Elapsed += new System.Timers.ElapsedEventHandler(OnMiTimerEvent);
        }

        public void Start()
        {
            LogHospitalAutodialer.Open(HLogLevel.Record, "Hospital2Autodialer");
            LogHospitalAutodialer.Write("", "Servicio empezado", HLogLevel.Record);
            //LogHospitalAutodialer.Write("Hora de inicio de campaña: ", sHorasInicioCampaña, HLogLevel.Record);
            try
            {
                OrganizadorDeInicioDeCampaña.AssignSchedule(sHorasInicioCampaña, LogHospitalAutodialer);
                OrganizadorDeCargaDeResultados.AssignSchedule(sHorasCargaResultados, LogHospitalAutodialer);
                MiReloj.Start();
            }
            catch (Exception E)
            {
                //LogHospitalAutodialer.Write("Hubo un error al iniciar el organizador de campañas y el servicio no se pudo iniciar, revisar el fichero de configuración", "", HLogLevel.Error);
                //LogHospitalAutodialer.Close();
                System.Windows.Forms.MessageBox.Show("El organizador de campañas no se ha inicializado correctamente y el servicio no se pudo iniciar, revisar el fichero de configuración");
            }
        }

        public void Stop()
        {
            if (MiCampaña != null)
            { 
                if (MiCampaña.ConexionCampaña.State == ConnectionState.Open)
                {
                    MiCampaña.CerrarConexion();
                }
            }
            LogHospitalAutodialer.Write("", "Servicio detenido", HLogLevel.Record);
            LogHospitalAutodialer.Close();
            MiReloj.Stop();
        }

        #endregion

        #region Métodos privados

        private void OnMiTimerEvent(object source, ElapsedEventArgs e)
        {
            if (OrganizadorDeInicioDeCampaña.Check())
            {
                CrearNuevaCampaña();
            }
            if (OrganizadorDeCargaDeResultados.Check())
            {
                CargarResultadosDeLaCampaña();
            }
        }

        private void CrearNuevaCampaña()
        {
            MiCampaña = new Campaña(LogHospitalAutodialer);
            if (MiCampaña.ConexionCampaña.State == ConnectionState.Open)
            {
                if (MiCampaña.Resetear())
                {
                    if (MiCampaña.Crear())
                    {
                        if (MiCampaña.CrearHorario(MiCampaña.CodigoCampaña))
                        {
                            if (MiCampaña.CargarListin(MiCampaña.CodigoCampaña))
                            {
                                if (MiCampaña.Iniciar(MiCampaña.CodigoCampaña))
                                {
                                    LogHospitalAutodialer.Write("Campaña iniciada", "", HLogLevel.Record);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                LogHospitalAutodialer.Write("No se pudo conectar con la base de datos y no se creó la campaña", "", HLogLevel.Error);
            }
        }

        private void CargarResultadosDeLaCampaña()
        {
            if (MiCampaña != null)
            {
                if (MiCampaña.ConexionCampaña.State == ConnectionState.Open)
                {
                    if (Campaña.Terminada(MiCampaña.CodigoCampaña))
                    {
                        if (MiCampaña.EscribirResultadosDeLaCampaña(MiCampaña.CodigoCampaña))
                        {
                            LogHospitalAutodialer.Write("Datos de la campaña cargados. Finalización de la rutina", "", HLogLevel.Record);
                        }
                    }
                    else
                    {
                        LogHospitalAutodialer.Write("La campaña aún no ha terminado", "", HLogLevel.Record);
                    }
                }
            }
        }

        #endregion
    }
}
