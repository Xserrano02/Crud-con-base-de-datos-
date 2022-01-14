using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLab
{
    class cPedido
    {

        //CAMPOS DE CLASE
        private long idPedido;
        private long idCliente;
        private DateTime fechaPedido;
        private DateTime fechaEsperada;
        private string comentarios;
        private string estado;
        private long buscar;


        //PROPIEDADES
        public long IdPedido { get => idPedido; set => idPedido = value; }
        public long IdCliente { get => idCliente; set => idCliente = value; }
        public string Comentarios { get => comentarios; set => comentarios = value; }
        public string Estado { get => estado; set => estado = value; }
        public DateTime FechaPedido { get => fechaPedido; set => fechaPedido = value; }
        public DateTime FechaEsperada { get => fechaEsperada; set => fechaEsperada = value; }
        public long Buscar { get => buscar; set => buscar = value; }


        //constructor vacio
        public cPedido()
        {
        }



        //Constructor con comentarios
        public cPedido(long idPedido, long idCliente, DateTime fechaPedido, DateTime fechaEsperada, string comentarios, string estado)
        {
            this.idPedido = idPedido;
            this.idCliente = idCliente;
            this.fechaPedido = fechaPedido;
            this.fechaEsperada = fechaEsperada;
            this.comentarios = comentarios;
            this.estado = estado;
        }

        //Constructor sin comentarios
        public cPedido(long idPedido, long idCliente, DateTime fechaPedido, DateTime fechaEsperada, string estado)
        {
            this.idPedido = idPedido;
            this.idCliente = idCliente;
            this.fechaPedido = fechaPedido;
            this.fechaEsperada = fechaEsperada;
            this.estado = estado;
        }

        public DataTable GetPedido()
        {

            DataTable mydt = new DataTable();
            Conexion conectar = new Conexion();

            SqlConnection conect = conectar.conexionServer();

            string sql = "SELECT idPedido,idCliente,fechaPedido,fechaEsperada,comentarios,estado FROM pedidos;";

            SqlCommand cmd = new SqlCommand(sql, conect);
            SqlDataReader mydr = null;

            try
            {
                conect.Open();
                mydr = cmd.ExecuteReader();
                mydt.Load(mydr);
            }
            finally
            {
                cmd.Dispose();
                conect.Close();
            }
            return mydt;
        }

        //metodo para guarda informacion a la base de datos de pedidos
         public long AgregarPedidos()
        {
            //Se declara el valor que va a devolver
            long id;
            //Objeto de tipo tabla 
            Conexion conectar = new Conexion();
            SqlConnection conect = conectar.conexionServer();

            string insertSql = "insert into pedidos(idCliente,fechaPedido, fechaEsperada, comentarios, estado) values (@idCliente,@fechaPedido,@fechaEsperada,@comentarios, @estado); SELECT IDENT_CURRENT('pedidos') as id;";
            SqlCommand cmd = new SqlCommand(insertSql, conect);
            cmd.Parameters.AddWithValue("@idCliente", this.idCliente);
            cmd.Parameters.AddWithValue("@fechaPedido", this.fechaPedido);
            cmd.Parameters.AddWithValue("@fechaEsperada", this.fechaEsperada);
            cmd.Parameters.AddWithValue("@comentarios", this.comentarios);
            cmd.Parameters.AddWithValue("@estado", this.estado);
            
            try
            {
                conect.Open();
                id = Convert.ToInt64(cmd.ExecuteScalar());
            }
            finally
            {
                conect.Close();
                cmd.Dispose();
            }
            return id;
        }

        //METODO QUE EDITA UN PEDIDO
        public int EditarPedido()
        {
            int affected = 0;
            Conexion conectar = new Conexion();
            SqlConnection conect = conectar.conexionServer();

            string updateSql = "UPDATE pedidos SET idCliente=@idCliente, fechaPedido=@fechaPedido, fechaEsperada=@fechaEsperada, comentarios=@comentarios WHERE idPedido = @idPedido;";
            SqlCommand cmd = new SqlCommand(updateSql, conect);

            cmd.Parameters.AddWithValue("@idCliente", this.idCliente);
            cmd.Parameters.AddWithValue("@fechaPedido", this.fechaPedido);
            cmd.Parameters.AddWithValue("@fechaEsperada", this.fechaEsperada);
            cmd.Parameters.AddWithValue("@comentarios", this.comentarios);
            cmd.Parameters.AddWithValue("@idPedido", this.idPedido);
            try
            {
                conect.Open();
                //ejecucion de la actualizacion y captura de los registros modificados
                affected = cmd.ExecuteNonQuery();
            }
            finally
            {
                cmd.Dispose();
                conect.Close();
            }
            return affected;

        }


        //METODO QUE ELIMINA UN PEDIDO
        public int EliminarPedido()
        {
            int affected = 0;
            Conexion conectar = new Conexion();
            SqlConnection conect = conectar.conexionServer();

            string updateSql = "UPDATE pedidos SET estado=@estado WHERE idPedido = @idPedido;";
            SqlCommand cmd = new SqlCommand(updateSql, conect);

            cmd.Parameters.AddWithValue("@idPedido", this.idPedido);
            cmd.Parameters.AddWithValue("@estado", this.estado);
           
            try
            {
                conect.Open();
                //ejecucion de la actualizacion y captura de los registros modificados
                affected = cmd.ExecuteNonQuery();
            }
            finally
            {
                cmd.Dispose();
                conect.Close();
            }
            return affected;

        }
        //METODO PARA BUSCAR POR ID CLIENTE
        public DataTable BuscarPedido()
        {
            DataTable mydt = new DataTable();
            Conexion conectar = new Conexion();
            SqlConnection conn = conectar.conexionServer();
            

            string findSql = "SELECT * FROM pedidos WHERE idPedido=@buscar;";

            SqlCommand comando = new SqlCommand(findSql, conn);
            comando.Parameters.AddWithValue("@buscar", this.Buscar);


            SqlDataReader mydr = null;



            try
            {
                conn.Open();
                mydr = comando.ExecuteReader();
                mydt.Load(mydr);

            }
            finally
            {
                conn.Close();
                comando.Dispose();
            }
            return mydt;

        }

        //Metodod para buscar por ID cliente 
        public DataTable BuscarPedidoPorIdCliente()
        {
            DataTable mydt = new DataTable();
            Conexion conectar = new Conexion();
            SqlConnection conn = conectar.conexionServer();


            string findSql = "SELECT * FROM pedidos WHERE idCliente=@buscar;";

            SqlCommand comando = new SqlCommand(findSql, conn);
            comando.Parameters.AddWithValue("@buscar", this.Buscar);


            SqlDataReader mydr = null;



            try
            {
                conn.Open();
                mydr = comando.ExecuteReader();
                mydt.Load(mydr);

            }
            finally
            {
                conn.Close();
                comando.Dispose();
            }
            return mydt;

        }










        //El comboBox dejalo de ultimo

        public DataTable CargarPedidoComboBox()
        {
            //Objeto de tipo tabla 
            DataTable mydt = new DataTable();
            //Se crea la clase de conexion  
            Conexion conectar = new Conexion();
            SqlConnection conect = conectar.conexionServer();

            //String de consulta que se hace a las base de datos para que devuelva los datos
            string sql = "SELECT idPedido,idCliente FROM pedidos where estado = 'A';";
            SqlCommand cmd = new SqlCommand(sql, conect);

            try
            {
                //abre la conexion con la base
                conect.Open();
                //Ejecuta la consulta hecha 
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(mydt);
                

           
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
