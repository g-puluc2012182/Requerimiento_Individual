
using CapaControlador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace CapaVista
{
    public partial class frmAbonoCuentas : Form
    {
       // clsMovInventariosCon Controlador = new clsMovInventariosCon();
        ControladorJM jm = new ControladorJM();
        VariableGlobal glo = new VariableGlobal();
        Controlador con = new Controlador();
        clsValidaciones vali = new clsValidaciones();
        double saldoanterior;
        public frmAbonoCuentas()
        {
            InitializeComponent();
            mostrarmorosos();
            // CargarCombobox();
        }

        //private void CargarCombobox()
        //{

        //llenado de combobox de metodo pago
        // cmbmetodopago.DisplayMember = "descripcionMetodo";
        // cmbmetodopago.ValueMember = "pkIdMetodoPago";
        // cmbmetodopago.DataSource = con.funcObtenerCamposComboboxPais("pkIdMetodoPago", "descripcionMetodo", "metodopago", "estadoMetodo");
        //cmbmetodopago.SelectedIndex = -1;

        //}
        private void mostrarmorosos()
        {
            dgvmorosos.Rows.Clear();
            OdbcDataReader mostrar = jm.funcSelectllenardgvhistorico();
            try
            {
                while (mostrar.Read())
                {
                    dgvmorosos.Rows.Add(mostrar.GetString(0), mostrar.GetString(1), mostrar.GetString(2), mostrar.GetString(3));
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        private void mostrarmorosos1()
        {
            dgvmorosos.Rows.Clear();
            OdbcDataReader mostrar = jm.funcSelectTabla("compraencabezado");
            try
            {
                while (mostrar.Read())
                {
                    dgvmorosos.Rows.Add(mostrar.GetString(0), mostrar.GetString(1), mostrar.GetString(2), mostrar.GetString(3), mostrar.GetString(4), mostrar.GetString(5), mostrar.GetString(6), mostrar.GetString(7), mostrar.GetString(8));
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void frmAbonoCuentas_Load(object sender, EventArgs e)
        {

        }

        private void dgvmorosos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // private void cmbmetodopago_SelectedIndexChanged(object sender, EventArgs e)
        //{
        // if (cmbmetodopago.SelectedIndex != -1)
        //  {
        // txtmetodopago.Text = cmbmetodopago.SelectedValue.ToString();
        // }
        // }

        //private void txtmetodopago_TextChanged(object sender, EventArgs e)
        // {
        // if (txtmetodopago.Text != "")
        // {
        // OdbcDataReader reader = jm.FieldComboboxtxt("descripcionMetodo", "metodopago", "estadoMetodo", "pkIdMetodoPago", txtmetodopago.Text);
        //   if (reader.Read())
        //  {
        // cmbmetodopago.Text = reader.GetString(0);
        // }
        //  else
        // {
        // cmbmetodopago.SelectedIndex = -1;
        //}
        // }
        // }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txtcodigo.Enabled = true;
                txtabono.Enabled = true;
                button2.Enabled = true;
                button4.Enabled = true;
                textBox1.Enabled = true;
            }
            else
            {

                txtcodigo.Enabled = false;
                textBox1.Enabled = false;
                txtabono.Enabled = false;
                button2.Enabled = false;
                button4.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mostrarmorosos();
        }
        private void limpiar()
        {

            dgvhistorico.Rows.Clear();
            txtcodigo.Text = "";
            textBox1.Text = "";
            txtabono.Text = "";
            txtordencompra.Text = "";
            checkBox1.Checked = false;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            limpiar();


        }
        private void buscarsaldoyhistorico()
        {
            dgvhistorico.Rows.Clear();
            dgvmorosos.Rows.Clear();
            if (txtordencompra.Text != "")
            {
                OdbcDataReader mostrar = jm.funcSelectllenardgvmorososfiltro(txtordencompra.Text);
                try
                {
                    while (mostrar.Read())
                    {
                        dgvmorosos.Rows.Add(mostrar.GetString(0), mostrar.GetString(1), mostrar.GetString(2), mostrar.GetString(3));
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }

                OdbcDataReader mostrar1 = jm.funcSelectllenardgvmorososfiltro2(txtordencompra.Text);
                try
                {
                    while (mostrar1.Read())
                    {
                        dgvhistorico.Rows.Add(mostrar1.GetString(0), mostrar1.GetString(1), mostrar1.GetString(2), mostrar1.GetString(3), mostrar1.GetString(4), mostrar1.GetString(5));
                        saldoanterior = mostrar1.GetDouble(6);
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }




            }





        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            buscarsaldoyhistorico();
        }
        private void realizarpago()
        {

            string fecha, saldonuevostring;
            double saldonuevo, abono,cargo,saldonuevo1;
            abono = Double.Parse(txtabono.Text);
            cargo = Double.Parse(textBox1.Text);
            if (abono == 0 || txtabono.Text=="")
                {
                saldonuevo1 = saldoanterior + cargo;
                saldonuevostring = Convert.ToString(saldonuevo1);
                fecha = DateTime.Now.ToString("dd/MM/yyyy");
                if (dgvhistorico.Rows.Count > 1)
                {

                    OdbcDataReader consulta3 = jm.funcInsertarsaldocomprasdefinitivo(txtcodigo.Text, saldonuevostring);
                    OdbcDataReader consulta2 = jm.funcInsertarSaldocompras2(fecha, txtcodigo.Text, textBox1.Text, Convert.ToString(saldoanterior), txtabono.Text);
                    MessageBox.Show("CARGO REALIZADO CON EXITO");
                }
                limpiar();

            }
            else
            {
                saldonuevo = saldoanterior - abono;
                saldonuevostring = Convert.ToString(saldonuevo);
                fecha = DateTime.Now.ToString("dd/MM/yyyy");
                if (dgvhistorico.Rows.Count > 1)
                {

                    OdbcDataReader consulta3 = jm.funcInsertarsaldocomprasdefinitivo(txtcodigo.Text, saldonuevostring);
                    OdbcDataReader consulta2 = jm.funcInsertarSaldocompras2(fecha, txtcodigo.Text, textBox1.Text, Convert.ToString(saldoanterior), txtabono.Text);
                    MessageBox.Show("ABONO REALIZADO CON EXITO");
                }
                limpiar();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (saldoanterior >= Double.Parse(txtabono.Text))
            {
                realizarpago();
            }
            else
            {
                MessageBox.Show("EL MONTO INGRESADO EXCEDE EL SALDO");


            }
        }

        private void txtordencompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            vali.CampoNumerico(e);
        }

        private void txtordencompra_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtcodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtcodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            vali.CampoNumerico(e);
        }

        private void txtabono_KeyPress(object sender, KeyPressEventArgs e)
        {
            vali.CampoNumerico(e);
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dgvhistorico_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Reporte_Cierre formulario = new Reporte_Cierre();
            formulario.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
            System.Diagnostics.Process.Start(@"C:\Users\Epa Peri\source\repos\Inventarios_Cierre\CapaVista\Ayuda Cierre Inventarios.pdf");
        }
    }
}
