using CapaModelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CapaControlador
{
    public class Controlador
    {
        Sentencias Modelo = new Sentencias();
        
        public DataTable funcObtenerCamposCombobox(string Campo1, string Campo2, string Tabla, string Estado)
        {
            string Comando = string.Format("SELECT " + Campo1 + " ," + Campo2 + " FROM " + Tabla + " WHERE " + Estado + "= 1;");
            return Modelo.funcObtenerCamposCombobox(Comando);
        }
        public OdbcDataReader funcConsultaCombo(string Campo1, string Campo2, string Tabla, string Estado, string Codigo)
        {
            string Comando = string.Format("SELECT " + Campo1 + " FROM " + Tabla + " WHERE " + Estado + "= 1 AND " + Campo2 + " = " + Codigo + ";");
            return Modelo.funcConsulta(Comando);

        }
       
        //Funcion para obtener datos combobox
        public DataTable funcObtenerCamposComboboxPais(string Campo1, string Campo2, string Tabla, string Estado)
        {
            string Comando = string.Format("SELECT " + Campo1 + " ," + Campo2 + " FROM " + Tabla + " WHERE " + Estado + "= 1;");
            return Modelo.funcObtenerCamposCombobox(Comando);
        }
        
    }
}
