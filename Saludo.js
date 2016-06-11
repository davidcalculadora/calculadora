function Saludar(Nombre)
{
  var Fecha = new Date();
  function ObtenerNombreMes(NombreMes) 
  {
    var NombreDelMes = "";
    switch (NombreMes)
    {
      case 0: NombreDelMes = "Enero"; break;
      case 1: NombreDelMes = "Febrero"; break;
      case 2: NombreDelMes = "Marzo"; break;
      case 3: NombreDelMes = "Abril"; break;
      case 4: NombreDelMes = "Mayo"; break;
      case 5: NombreDelMes = "Junio"; break;
      case 6: NombreDelMes = "Julio"; break;
      case 7: NombreDelMes = "Agosto"; break;
      case 8: NombreDelMes = "Septiembre"; break;
      case 9: NombreDelMes = "Octubre"; break;
      case 10: NombreDelMes = "Noviembre"; break;
      case 11: NombreDelMes = "Diciembre"; break;
    }
    return NombreDelMes;
  }
  //console.log("Hola " + Nombre + ". Hoy es " + Fecha.getTime());
  console.log("Hola " + Nombre + ". Hoy es " + Fecha.getDate() + " de " + ObtenerNombreMes(Fecha.getMonth()) + " de " + Fecha.getFullYear() + " y son las " + Fecha.getHours() + ":" + Fecha.getMinutes() + ":" + Fecha.getSeconds());  
}

Saludar("David");