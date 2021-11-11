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
    public class ControladorJM
    {
        Sentencias Modelo = new Sentencias();
        //Get data from table to cb
        public DataTable FieldCombobox(string field1, string field2, string table, string status)
        {
            string Comando = string.Format("SELECT " + field1 + " ," + field2 + " FROM " + table + " WHERE " + status + "= 1;");
            return Modelo.funcObtenerCamposCombobox(Comando);
        }
        public DataTable FieldComboboxCondition(string field1, string field2, string table, string conditionfield, string conditiondata, string status)
        {
            string Comando = string.Format("SELECT " + field1 + " ," + field2 + " FROM " + table + " WHERE " + conditionfield + "=" + conditiondata + " AND " + status + "= 1;");
            return Modelo.funcObtenerCamposCombobox(Comando);
        }
        public OdbcDataReader FieldComboboxtxt(string field1, string table, string status, string field2, string condition)
        {
            string Comando = string.Format("SELECT " + field1 + " FROM " + table + " WHERE " + status + "= 1 AND " + field2 + " = '" + condition + "';");
            return Modelo.funcConsulta(Comando);
        }
        public OdbcDataReader funcSelect(string Tabla)
        {
            string Consulta = "SELECT * FROM " + Tabla + ";";
            return Modelo.funcConsulta(Consulta);
        }

        public OdbcDataReader funcSelectTabla(string Tabla)
        {
            string Consulta = "SELECT * FROM " + Tabla + ";";
            return Modelo.funcConsulta(Consulta);
        }




        public OdbcDataReader funcActualizarestado(string idencabezadoestado, string estado)
        {
            string Consulta = "UPDATE `compraencabezado` SET `fktipocompra` = " + estado + " WHERE(`pkNoDocumentoEnca` = " + idencabezadoestado + ")";
            return Modelo.funcInsertar(Consulta);
        }
        
        public OdbcDataReader funcInsertarSaldocompras(string idcabezado, string saldo)
        {
            string Consulta = "INSERT INTO `saldoscomrpa` (`fkNoDocumentoEnca`, `saldo`) VALUES ('" + idcabezado + "', '" + saldo + "');";
            return Modelo.funcInsertar(Consulta);
        }
        public OdbcDataReader funcSelectllenardgvmorosos()
        {
            string Consulta = "SELECT ce.pkNoDocumentoEnca, pro.direccion, ce.fechaCompra, ce.totalCompra FROM compraencabezado ce inner join saldoscomrpa sc on sc.fkNoDocumentoEnca = ce.pkNoDocumentoEnca inner join proveedor pro on pro.IdProveedor = ce.fkIdProveedor WHERE ce.estadoCompra ='1' AND sc.saldo > 0;";
            return Modelo.funcConsulta(Consulta);
        }
        public OdbcDataReader funcSelectllenardgvmorososfiltro(string idencabezado)
        {
            string Consulta = "SELECT ce.pkNoDocumentoEnca, pro.direccion, ce.fechaCompra, ce.totalCompra FROM compraencabezado ce inner join saldoscomrpa sc on sc.fkNoDocumentoEnca = ce.pkNoDocumentoEnca inner join proveedor pro on pro.IdProveedor = ce.fkIdProveedor WHERE ce.estadoCompra ='1' AND sc.saldo > 0 AND ce.pkNoDocumentoEnca = '" + idencabezado + "';";
            return Modelo.funcConsulta(Consulta);
        }
        public OdbcDataReader funcSelectllenardgvhistorico()
        {
            string Consulta = "SELECT ce.pkNoDocumentoEnca, pro.direccion, ce.fechaCompra, ce.totalCompra FROM compraencabezado ce inner join saldoscomrpa sc on sc.fkNoDocumentoEnca = ce.pkNoDocumentoEnca inner join proveedor pro on pro.idProveedor = ce.fkIdProveedor WHERE ce.estadoCompra ='1' AND sc.saldo > 0 ;";
            return Modelo.funcConsulta(Consulta);
        }
        public OdbcDataReader funcInsertarSaldocompras2(string fecha, string idencabezado, string cargo, string saldo, string abono)
        {
            string Consulta = "INSERT INTO saldohistoricocompra (`fechamovimientocompra`, `fkNoDocumentoEnca`, `cargo`, `saldoanterior`, `abono`) VALUES ('" + fecha + "', " + idencabezado + "," + cargo + "," + saldo + "," + abono + ");";
            return Modelo.funcInsertar(Consulta);
        }
        public OdbcDataReader funcSelectllenardgvmorososfiltro2(string idencabezado)
        {
            string Consulta = "SELECT sh.pksaldohistoricocompra, sh.fechamovimientocompra, sh.fkNoDocumentoEnca,sh.saldoanterior,sh.cargo,(sh.saldoanterior + sh.cargo), sh.abono, (sh.saldoanterior - sh.abono)AS saldoactual, sc.saldo FROM saldohistoricocompra sh inner join saldoscomrpa sc on sh.fkNoDocumentoEnca = sc.fkNoDocumentoEnca WHERE sh.fkNoDocumentoEnca = " + idencabezado + ";";
            return Modelo.funcConsulta(Consulta);
        }


        public OdbcDataReader funcInsertarsaldocomprasdefinitivo(string saldo, string abono)
        {
            string Consulta = "UPDATE`saldoscomrpa` SET `saldo` = " + abono + " WHERE (`fkNoDocumentoEnca` = " + saldo + ");";
            return Modelo.funcInsertar(Consulta);

        }
        
    }
}
