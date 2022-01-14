using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProyectoLab
{
    class Conexion
    {
        public SqlConnection conexionServer()
        {
            SqlConnection conect;

            try
            {
                //Solo descomenta la tuya y comenta la mia
                string cadenaConexion = "Data Source=OLAISOLAPC\\SQLEXPRESS;Initial Catalog=Tienda;Integrated Security=True";
                //string cadenaConexion = "Data Source=DESKTOP-1TULQTD\\SQLEXPRESS;Initial Catalog=Tienda;Integrated Security=True";
                conect = new SqlConnection(cadenaConexion);
            }
            catch (Exception ex)
            {
                //Error que devuelve en casi que la conexion halla fallado
                throw new ArgumentException("Error al conectar", ex);
            }
            return conect;
        }
    }
}
