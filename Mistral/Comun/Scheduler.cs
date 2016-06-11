using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
namespace Generico
{
    // Esta clase permite gestionar planificaciones horarias a lo largo de la semana.
    // La planificaci�n viene dada por una cadena con esta sint�xis ejemplo:
    // L:2000;X:2300;D:1400,2030
    // La sintaxis es muy estricta: letras en may�sculas, sin espacios en blanco, etc.

    class HScheduler
    {
        #region Atributos privados

        // D�a y hora del �ltimo "OK" (Dia: "L".."D", Hora: HHMM)
        private string sLastDay;
        private string sLastTime;

        // Lista (hashtable) de d�as, que a su vez contiene una lista (sortedlist) 
        // de horas ordenadas. 
        // La lista (hastable) de d�as usa como clave "L","M","X",..,"D". 
        // Para cada d�a la lista (sortedlist) ordenada de horas en forma HHMM.
        private Hashtable LDays;

        #endregion

        #region M�todo p�blicos

        // Constructor
        public HScheduler()
        {
            // Construimos la lista de d�as que a su vez tienen la lista
            // de horas (ordenadas)
            LDays = new Hashtable();
            LDays.Add("L", new SortedList());
            LDays.Add("M", new SortedList());
            LDays.Add("X", new SortedList());
            LDays.Add("J", new SortedList());
            LDays.Add("V", new SortedList());
            LDays.Add("S", new SortedList());
            LDays.Add("D", new SortedList());
            sLastDay = "";
            sLastTime = "";
        }

        // M�todo para asignar la cadena de planificaci�n
        public void AssignSchedule(string sSchedule, Log LogScheduler)
        {
            // La cadena de planificaci�n es algo as�:
            // L:2000;X:2300;D:1400,2030
            // (ejecutar lunes a las 20:00, mi�rcoles a las 20:00 y domingo
            // a las 14:00 y 20:30)
            // Si la sintaxis no es correcta, lanza una excepci�n

            // Descomponemos la cadena que nos han pasado y rellenamos la 
            // estructura LDays.
            // Nos es muy �til la funci�n split, que separa un string definiendo
            // un caracter separador. Lo usaremos doblemente: con ";" para separar los
            // dias y con "," para separar las horas. 
            // Ojo: split no devuelve un ArrayList, sino un string[]
            // Si la sint�xis no es correcta (una inicial que no es v�lida o una hora
            // que no es HHMM), lanza una exc�pci�n... y la planificaci�n se quedar� vac�a.

            // Vaciamos la planificaci�n actual. Las HastTables se recorren de una
            // forma un poco "rara", usando DictionaryEntry "�tems"
            foreach (DictionaryEntry item in LDays)
            {
                SortedList L = (item.Value) as SortedList;
                L.Clear();
            }

            try
            {
                // Reiniciamos los atributos que indican el �ltimo "OK"
                sLastDay = "";
                sLastTime = "";

                // Descomponemos en d�as
                string[] Days = sSchedule.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                // Para cada d�a ...
                foreach (string sDaySchedule in Days)
                {
                    // Los dos primeros caracteres deben ser "A:", siendo A la inicial del d�a
                    string sDay = sDaySchedule.Substring(0, 1);
                    string sSeparator = sDaySchedule.Substring(1, 1);
                    if (!CheckDaySintax(sDay) || (sSeparator != ":"))
                    {
                        throw new Exception("Sint�xis incorrecta : " + sDaySchedule);
                    }

                    // Obtenemos la SortedList con la planificaci�n de este d�a (que est� vac�a)
                    SortedList L = LDays[sDay] as SortedList;

                    // Obtenemos la planificaci�n del d�a como un string[]
                    string[] Times = sDaySchedule.Substring(2).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string sTime in Times)
                    {
                        // Comprobamos que la hora especificada sea correcta (HHMM)
                        if (!CheckHourSintax(sTime))
                        {
                            throw new Exception("Sint�xis incorrecta : " + sDaySchedule);
                        }

                        // A�adimos la hora a la sortedlist. La clave de ordenaci�n y el valor son
                        // lo mismo, pero como entero o string.
                        L.Add(Int32.Parse(sTime), sTime);
                    }
                }
            }
            catch (Exception E)
            {
                // Si se produce un error, vaciamos la planificaci�n (no habr� ninguna!)
                // y relanzamos el error
                // Las HastTables se recorren de una
                // forma un poco "rara", usando DictionaryEntry "�tems"
                foreach (DictionaryEntry item in LDays)
                {
                    SortedList L = (item.Value) as SortedList;
                    L.Clear();
                }
                LogScheduler.Write("Hubo un error al iniciar el organizador de campa�as y el servicio no se pudo iniciar, revisar el fichero de configuraci�n", "", HLogLevel.Error);
                //System.Windows.Forms.MessageBox.Show("El organizador de campa�as no se ha inicializado correctamente, revisar el fichero de configuraci�n");
                throw E;
            }

            if (LDays == null) return;
        }

        // M�todo para checkear si se ha cumplido alguna tarea
        public bool Check()
        {
            // Obtenemos el d�a de hoy ("L",..,"D")
            string sToday = Day2Dia(DateTime.Now.DayOfWeek);

            // Obtenemos la hora actual en formato HHMM 
            // Ojo: HH = hora de 0..23, hh = hora de 0..12, mm = minutos, MM = mes
            string sNow = DateTime.Now.ToString("HHmm"); 

            // Si es la primera vez que chequeamos con este planning ...
            if ((sLastDay == "") && (sLastTime == ""))
            {
                // Inicializamos los valores de �ltimo OK (aunque sea falso,
                // pero para que sirvan como punto de partida y evitar que se cumplan
                // planning pasados)
                sLastTime = sNow;
                sLastDay = sToday;
            }

            // Si el �ltimo "OK" se di� en un d�a distinto, fijamos a 0000 la hora del
            // �ltimo OK (porque hemos cambiado de d�a)...
            if (sLastDay != sToday)
            {
                sLastTime = "0000";
                sLastDay = sToday;
            }

            // Obtenemos la lista de horas correspondientes a ese d�a
            SortedList L = LDays[sToday] as SortedList;

            // Recorremos la lista de horas en busca de la primera que supere la
            // hora actual y que no sea anterior o igual a la �ltima que YA di� OK (claro, 
            // no queremos que una misma hora nos d� varios "OK")
            for (int i = 0; i < L.Count; i++)
            {
                // Obtenemos la i-�sima hora de planificaci�n
                string sTime = L.GetByIndex(i) as string;

                // Convertimos a valores enteros para la comparaci�n (podemos
                // convertir horas de formato "HHMM" a un valor num�rico, y seguir�n
                // siendo v�lidos las relaciones de mayor-menor)
                int iTime = Int32.Parse(sTime);
                int iNow = Int32.Parse(sNow);
                int iLastTime = Int32.Parse(sLastTime);
                // Si la hora actual es mayor o igual a la de planificaci�n,
                // y dicha planificaci�n no es inferior al �ltimo OK 
                if ( (iNow >= iTime) && (iTime > iLastTime) )
                {
                    // Retornamos OK
                    sLastDay = sToday;
                    sLastTime = sTime;
                    return true;
                }
            }
            
            return false;
        }

        #endregion

        #region M�todos privados

        private string Day2Dia(DayOfWeek Day)
        {
            // Convierte el valor del enumerado DayOfWeek a su inicial castellana
            string sDia = "";
            switch (Day)
            {
                case DayOfWeek.Monday: sDia = "L"; break;
                case DayOfWeek.Tuesday: sDia = "M"; break;
                case DayOfWeek.Wednesday: sDia = "X"; break;
                case DayOfWeek.Thursday: sDia = "J"; break;
                case DayOfWeek.Friday: sDia = "V"; break;
                case DayOfWeek.Saturday: sDia = "S"; break;
                case DayOfWeek.Sunday: sDia = "D"; break;
            }
            return sDia;
        }

        private bool CheckDaySintax(string sDay)
        {
            // Comprueba si la inicial proporcionada corresponde a la de un d�a
            if ((sDay == "L") || (sDay == "M") || (sDay == "X") || (sDay == "J") ||
                 (sDay == "V") || (sDay == "S") || (sDay == "D"))
            {
                return true;
            }
            return false;
        }

        private bool CheckHourSintax(string sHour)
        {
            // Comprueba que el valor tenga el forma HHNN: 4 d�gitos, los dos
            // primeros una hora, los dos segundos unos minutos, y que no haya letras
            bool rc = true;
            try
            {
                if (sHour.Length == 4)
                {
                    int iHour = Int32.Parse(sHour.Substring(0, 2));
                    int iMinuts = Int32.Parse(sHour.Substring(2, 2));
                    if ((iHour < 0) || (iHour > 23))
                    {
                        rc = false;
                    }
                    else if ((iMinuts < 0) || (iMinuts > 59))
                    {
                        rc = false;
                    }
                }
                else
                {
                    rc = false;
                }
            }
            catch (Exception E)
            {
                // Si salta una excepci�n, es porque hab�a letras!
                rc = false;
            }
            return rc;
        }

        #endregion
    }
}
