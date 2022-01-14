using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoLab
{
    class CClientes
    {
        //Se declaran las variables que se necesitaran para pasar los datos desde el formulario
        private long idClientes;
        private string nombres;
        private string apellidos;
        private string telefono;
        private string direccion;
        private string ciudad;
        private string departamento;
        private string estado;
        private string borrar;

        //Constructor vacio
        public CClientes()
        {

        }

       //Constructor seteando las variables
        public CClientes(long idClientes, string nombres, string apellidos, string telefono, string direccion, string ciudad, string departamento, string estado, string borrar)
        {
            this.idClientes = idClientes;
            this.nombres = nombres;
            this.apellidos = apellidos;
            this.telefono = telefono;
            this.direccion = direccion;
            this.ciudad = ciudad;
            this.departamento = departamento;
            this.estado = estado;
            this.borrar = borrar;
        }

        //Se agregan los get y set donde se setearan los datso que vienen del form
        public string Borrar
        {
            get
            {
                return borrar;
            }
            set
            {
                borrar = value;
            }
        }


        public long IdClientes
        {
            get
            {
                return idClientes;
            }
            set
            {
                idClientes = value;
            }
        }

        public string Nombres
        {
            get
            {
                return nombres;
            }
            set
            {
                nombres = value;
            }
        }

        public string Apellidos
        {
            get
            {
                return apellidos;
            }
            set
            {
                apellidos = value;
            }
        }


        public string Telefono
        {
            get
            {
                return telefono;
            }
            set
            {
                telefono = value;
            }
        }


        public string Direccion
        {
            get
            {
                return direccion;
            }
            set
            {
                direccion = value;
            }
        }

        public string Ciudad
        {
            get
            {
                return ciudad;
            }
            set
            {
                ciudad = value;
            }
        }

        public string Departamento
        {
            get
            {
                return departamento;
            }
            set
            {
                departamento = value;
            }
        }

        public string Estado
        {
            get
            {
                return estado;
            }
            set
            {
                estado = value;
            }
        }

        //Recupera los datos almacenados en la base de datos y los muestra en pantalla
        public DataTable GetClientes()
        {
            //Objeto de tipo tabla 
            DataTable mydt = new DataTable();
            //Se crea la clase de conexion  
            Conexion conectar = new Conexion();
            SqlConnection conect = conectar.conexionServer();

            //String de consulta que se hace a las base de datos para que devuelva los datos
            string sql = "SELECT idCliente,nombres,apellidos,telefono,direccion,ciudad,departamento,estado FROM clientes where estado = 'A';";
            SqlCommand cmd = new SqlCommand(sql, conect);
            //Hace lectura de la tabla
            SqlDataReader mydr = null;

            try
            {
                //abre la conexion con la base
                conect.Open();
                //Ejecuta la consulta hecha 
                mydr = cmd.ExecuteReader();
                //muestra los datos de la lectura hecha
                mydt.Load(mydr);
            }
            finally
            {
                //cierra conexiones
                cmd.Dispose();
                conect.Close();
            }
            return mydt;
        }


        //Agrega los clientes nuevos
        public long AgregarClientes()
        {
            //Se declara el valor que va a devolver
            long id;
            //Objeto de tipo tabla 
            Conexion conectar = new Conexion();
            SqlConnection conect = conectar.conexionServer();
            //Se hace la la sentencia sql para agregar los datos
            string insertSql = "insert into clientes(nombres, apellidos,telefono, direccion, ciudad, departamento, estado) values (@nombres,@apellidos,@telefono,@direccion,@ciudad, @departamento, @estado); SELECT IDENT_CURRENT('clientes') as id;";
            SqlCommand cmd = new SqlCommand(insertSql, conect);
            //Se agregan los parametros
            cmd.Parameters.AddWithValue("@nombres", this.nombres);
            cmd.Parameters.AddWithValue("@apellidos", this.apellidos);
            cmd.Parameters.AddWithValue("@telefono", this.telefono);
            cmd.Parameters.AddWithValue("@direccion", this.direccion);
            cmd.Parameters.AddWithValue("@ciudad", this.ciudad);
            cmd.Parameters.AddWithValue("@departamento", this.departamento);
            cmd.Parameters.AddWithValue("@estado", this.estado);
            try
            {
                //Se abre la conexion
                conect.Open();
                //Se ejecuta la sentencia
                id = Convert.ToInt64(cmd.ExecuteScalar());
            }
            finally
            {
                conect.Close();
                cmd.Dispose();
            }
            return id;
        }

        //Clase para "Eliminar" 
        public int Eliminar()
        {//Se declara el valor que va a devolver
            int affected = 0;

            //Se instancia la clase conexion
            Conexion conectar = new Conexion();
            SqlConnection conn = conectar.conexionServer();
            //Se hace la la sentencia sql para eliminar los datos
            string deleteSql = "update clientes set estado = 'I' where idCliente = @borrar ";
            SqlCommand cmd = new SqlCommand(deleteSql, conn);
            //Se agregan los parametros
            cmd.Parameters.AddWithValue("@Borrar", this.borrar);

            try
            {//Se abre la conexion
                conn.Open();
                //Se ejecuta la sentencia
                affected = cmd.ExecuteNonQuery();

            }
            finally
            {
                //Se cierran las cinecciones
                conn.Close();
                cmd.Dispose();
            }
            return affected;
        }

        public int ActualizarClientes()
        {
            int afectada = 0;
            //Se instancia la clase conexion
            Conexion conectar = new Conexion();
            SqlConnection conn = conectar.conexionServer();
            //Se hace la la sentencia sql para actualizar los datos
            string updateSql = "update clientes set nombres=@nombres,apellidos=@apellidos,telefono=@telefono,direccion=@direccion," +
                "ciudad=@ciudad,departamento=@departamento,estado=@estado WHERE idCliente = @idCliente;";
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            //Se agregan los parametros
            cmd.Parameters.AddWithValue("@nombres", this.nombres);
            cmd.Parameters.AddWithValue("@apellidos", this.apellidos);
            cmd.Parameters.AddWithValue("@telefono", this.telefono);
            cmd.Parameters.AddWithValue("@direccion", this.direccion);
            cmd.Parameters.AddWithValue("@ciudad", this.ciudad);
            cmd.Parameters.AddWithValue("@departamento", this.departamento);
            cmd.Parameters.AddWithValue("@estado", this.estado);
            cmd.Parameters.AddWithValue("@idCliente", this.idClientes);


            try
            {//Se abre la conexion
                conn.Open();
                //Se ejecuta la sentencia
                afectada = cmd.ExecuteNonQuery();


            }
            finally
            {
                //Se cierran las conexiones
                conn.Close();
                cmd.Dispose();
            }

            return afectada;

        }

        public DataTable CargarClienteComboBox()
        {
            //Objeto de tipo tabla 
            DataTable mydt = new DataTable();
            //Se crea la clase de conexion  
            Conexion conectar = new Conexion();
            SqlConnection conect = conectar.conexionServer();

            //String de consulta que se hace a las base de datos para que devuelva los datos
            string sql = "SELECT idCliente,nombres FROM clientes where estado = 'A';";
            SqlCommand cmd = new SqlCommand(sql, conect);

           

            //Hace lectura de la tabla
            SqlDataReader mydr = null;

            try
            {
                //abre la conexion con la base
                conect.Open();
                //Ejecuta la consulta hecha 
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(mydt);
                DataRow fila = mydt.NewRow();
               
                fila["nombres"] = "Selecciona un cliente";
                mydt.Rows.InsertAt(fila, 0);
            }
            finally
            {
                //cierra conexiones
                cmd.Dispose();
                conect.Close();
            }
            return mydt;
        }
    }
}
