using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Generico;
namespace HospitalAutodialerForm
{
    public partial class HospitalAutodialerForm : Form
    {
        private OleDbDataAdapter DataAdapter;
        private DataSet Data;
        private OleDbCommandBuilder CommandBuilder;
        private DataTable Tabla;
        private Log LogHospitalAutodialer;
        private Campaña MiCampaña;
        public HospitalAutodialerForm()
        {
            InitializeComponent();
            toolTipBuscar.SetToolTip(textBoxBuscarCampaña, "Indique el código de la campaña para saber si está finalizada");
        }

        private void botonNuevaCampaña_Click(object sender, EventArgs e)
        {
            try
            {
                MiCampaña = new Campaña(LogHospitalAutodialer);
                LogHospitalAutodialer.Write("CAMPAÑA: ", "Se va a resetear la campaña", HLogLevel.Record);
                MiCampaña.Resetear();
                LogHospitalAutodialer.Write("CAMPAÑA: ", "Se va a crear una campaña", HLogLevel.Record);
                if (MiCampaña.Crear())
                {  
                    LogHospitalAutodialer.Write("CAMPAÑA: ", "Campaña creada", HLogLevel.Record);
                    //MessageBox.Show("Campaña creada correctamente: " + MiCampaña.CodigoCampaña);
                    if (MiCampaña.CrearHorario(MiCampaña.CodigoCampaña))
                    {
                        LogHospitalAutodialer.Write("CAMPAÑA: ", "Horario creado", HLogLevel.Record);
                        //MessageBox.Show("Horario creado correctamente de la campaña: " + MiCampaña.CodigoCampaña);
                        botonCargarCampaña.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Error al añadir el horario a la campaña");
                    }
                }
                else
                {
                    MessageBox.Show("Error al crear la campaña, revisar log");
                }
            }
            catch (Exception E)
            {
                MessageBox.Show("Error al crear la campaña: " + E.Message);
            }
        }

        private void botonCargarCampaña_Click(object sender, EventArgs e)
        {
            try
            {                
                string sConexionCampaña = "Provider=SQLOLEDB.1;Persist Security Info=True;User ID=hermes2;Initial Catalog=hEngine;Data Source=DAVID-PC\\SQLEXPRESS;Password=hermes2;";
                DataAdapter = new OleDbDataAdapter("select FechaCreacion, Codigo, Nombre, Estado from campanyas", sConexionCampaña);
                Data = new DataSet();
                DataAdapter.Fill(Data, "campanyas");
                Tabla = Data.Tables[0];
                CommandBuilder = new OleDbCommandBuilder(DataAdapter);
                dataGridViewCampaña.DataSource = Data;
                dataGridViewCampaña.DataMember = "campanyas";
                dataGridViewCampaña.ReadOnly = false;
            }
            catch (Exception E)
            {
                LogHospitalAutodialer.Write("Error al cargar la campaña: ", E.Message + " " + E.StackTrace, HLogLevel.Error);
                MessageBox.Show("Error al cargar la campaña");
            }
        }

        private void botonCargarListin_Click(object sender, EventArgs e)
        {
            if (MiCampaña == null)
            {
                MessageBox.Show("No has creado la campaña");
            }
            else
            { 
                if (!MiCampaña.TieneListinesCreados(MiCampaña.CodigoCampaña))
                { 
                    if (MiCampaña.CargarListin(MiCampaña.CodigoCampaña))
                    {
                        //MessageBox.Show("Listines cargados correctamente");
                    }
                    else
                    {
                        MessageBox.Show("Error al cargar el listín");
                    }
                }
                else
                {
                    MessageBox.Show("La campaña no tiene listines creados");
                }
            }
        }

        private void botonCargarResultados_Click(object sender, EventArgs e)
        {
            if (MiCampaña == null)
            {
                MessageBox.Show("No has creado la campaña");
            }
            else
            {
                if (Campaña.Terminada(MiCampaña.CodigoCampaña))
                {
                    if (MiCampaña.EscribirResultadosDeLaCampaña(MiCampaña.CodigoCampaña))
                    {
                        //MessageBox.Show("Resultados de la campaña cargados");
                    }
                    else
                    {
                        MessageBox.Show("Error al cargar los resultados de la campaña");
                    }
                }
                else
                {
                    MessageBox.Show("La campaña no ha finalizado");
                }
            }
            /*if (Campaña.LeerResultadosDeLaCampaña("C-20164695443"))
            {
                MessageBox.Show("Resultados de la campaña cargados");
            }
            else
            {
                MessageBox.Show("Error al cargar los resultados de la campaña");
            }*/
        }

        private void botonIniciar_Click(object sender, EventArgs e)
        {
            if (MiCampaña != null)
            {
                if (MiCampaña.Iniciar(MiCampaña.CodigoCampaña))
                {
                    MessageBox.Show("¡¡Campaña iniciada!!");
                }
                else
                {
                    MessageBox.Show("Error al iniciar la campaña");
                }
            }
            else
            {
                MessageBox.Show("No has iniciado ninguna campaña");
            }
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            string sCodigoCampaña = textBoxBuscarCampaña.Text;
            if (sCodigoCampaña == "")
            {
                MessageBox.Show("Debe indicar un código de campaña");
                return;
            }
            else
            {
                if (Campaña.Terminada(sCodigoCampaña))
                {
                    MessageBox.Show("La campaña ha terminado");
                }
                else
                {
                    MessageBox.Show("La campaña aún no ha terminado");
                }
            }
        }

        private void botonBuscarUltima_Click(object sender, EventArgs e)
        {
            string sCodigoDeLaUltimaCampaña = "";
            if (MiCampaña == null)
            {
                sCodigoDeLaUltimaCampaña = Campaña.CodigoDeLaUltimaCampañaCreada();
            }
            else
            {
                sCodigoDeLaUltimaCampaña = MiCampaña.CodigoCampaña;
            }
            // Comprobamos si la campaña ha terminado
            if (Campaña.Terminada(sCodigoDeLaUltimaCampaña))
            {
                MessageBox.Show("La campaña ha terminado");
            }
            else
            {
                MessageBox.Show("La campaña aún no ha terminado");
            }
        }

        private void boton_MouseEnter(object sender, EventArgs e)
        {
            Button BotonDelEvento = (Button)sender;
            BotonDelEvento.BackColor = Color.PaleGreen;
        }
        private void boton_MouseLeave(object sender, EventArgs e)
        {
            Button BotonDelEvento = (Button)sender;
            BotonDelEvento.BackColor = Color.Honeydew;
        }

        private void HospitalAutodialerForm_Load(object sender, EventArgs e)
        {
            LogHospitalAutodialer = new Log();
            LogHospitalAutodialer.Open(HLogLevel.Record, "HospitalAutodialer");
        }

        private void HospitalAutodialerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MiCampaña != null)
            { 
                MiCampaña.CerrarConexion();
            }
            LogHospitalAutodialer.Close();
        }
    }
}
