using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ProyectoLab
{
    public partial class LoginVista : Form
    {
        public LoginVista()
        {
            InitializeComponent();
        }
        SqlConnection conexion = new SqlConnection("server = OLAISOLAPC\\SQLEXPRESS;database=Usuarios; integrated security = true");
        //SqlConnection conexion = new SqlConnection("server = DESKTOP-1TULQTD\\SQLEXPRESS;database=Usuarios; integrated security = true");

        private void button1_Click(object sender, EventArgs e)
        {
            conexion.Open(); 
            string consulta = "select usuario, contrasena from usuario where usuario ='"+textUsuario.Text+"' and Contrasena='"+textPass.Text+"'";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            SqlDataReader lector;
            lector = comando.ExecuteReader();

            if(lector.HasRows == true)
            {
                Clientes ingClientes = new Clientes();
                this.Hide();
                ingClientes.Show();
            }
            else
            {
                MessageBox.Show("Credenciales no validas");
            }
            conexion.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit()
;        }
    }
}
