using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ProyectoLab
{
    class cDetallePedido
    {
        //Se declaran las variables que se necesitaran para pasar los datos desde el formulario
        private long idDetallePedido;
        private long idPedido;
        private long idProducto;
        private double precioUnidad;
        private int numeroLinea;
        private string estado;
        private int borrar;

        //Se agregan los get y set donde se setearan los datso que vienen del form
        public long IdDetallePedido { get => idDetallePedido; set => idDetallePedido = value; }
        public long IdProducto { get => idProducto; set => idProducto = value; }

        public long IdPedido { get => idPedido; set => idPedido = value; }
        public double PrecioUnidad { get => precioUnidad; set => precioUnidad = value; }
        public int NumeroLinea { get => numeroLinea; set => numeroLinea = value; }
        public string Estado { get => estado; set => estado = value; }

        public int Borrar { get => borrar; set => borrar = value; }

        //constructor vacio
        public cDetallePedido()
        {
        }

        //Constructor instanciando 
        public cDetallePedido(long idDetallePedido, long idPedido, long idProducto,double precioUnidad, int numeroLinea ,string estado)
        {
            this.idDetallePedido = idDetallePedido;
            this.idPedido = idPedido;
            this.idProducto = idProducto;
            this.precioUnidad = precioUnidad;
            this.numeroLinea = numeroLinea;
            this.estado = estado;
        }

        //Recupera los datos almacenados en la base de datos y los muestra en pantalla
        public DataTable GetDetallesPedido()
        {
            //Objeto de tipo tabla 
            DataTable mydt = new DataTable();
            //Se crea la clase de conexion  
            Conexion conectar = new Conexion();

            SqlConnection conect = conectar.conexionServer();

            //String de consulta que se hace a las base de datos para que devuelva los datos
            string sql = "SELECT * FROM detalle_pedido where estado = 'A';";


            SqlCommand cmd = new SqlCommand(sql, conect);
            //Hace lectura de la tabla
            SqlDataReader mydr = null;

            try
            {//abre la conexion con la base
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

        public long AgregarDetalles()
        {
            //Se declara el valor que va a devolver
            long id;
            //Objeto de tipo tabla 
            Conexion conectar = new Conexion();
            SqlConnection conect = conectar.conexionServer();
            //String de consulta que se hace a las base de datos para que devuelva los datos
            string insertSql = "insert into detalle_pedido(idPedido, idProducto,precioUnidad,numeroLinea, estado) values (@idPedido,@idProducto,@precioUnidad,@numeroLinea,@estado); SELECT IDENT_CURRENT('detalle_pedido') as id;";
            SqlCommand cmd = new SqlCommand(insertSql, conect);
            //Se asignan los campos que se utilizaran
            cmd.Parameters.AddWithValue("@idPedido", this.idPedido);
            cmd.Parameters.AddWithValue("@idProducto", this.idProducto);
            cmd.Parameters.AddWithValue("@precioUnidad", this.precioUnidad);
            cmd.Parameters.AddWithValue("@numeroLinea", this.numeroLinea);
            cmd.Parameters.AddWithValue("@estado", this.estado);
            try
            {
                //Se abre conexion a la base de datos
                conect.Open();
                //Se ejecuta la peticion
                id = Convert.ToInt64(cmd.ExecuteScalar());
            }
            finally
            {
                  //Se cierran las conexiones
                conect.Close();
                cmd.Dispose();
            }
            return id;
        }

        //Clase eliminar
        public int Eliminar()
        {
            //Se declara una variable para determinar si se elimina el dato de la base de datos
            int affected = 0;
            //Se intancia la clase para hacer conexion con ella
            Conexion conectar = new Conexion();
            SqlConnection conn = conectar.conexionServer();

            //Se hace unsa sentencia sql para eliminar registros
            string deleteSql = "update detalle_pedido set estado = 'I' where idDetallesPedido = @borrar ";
            SqlCommand cmd = new SqlCommand(deleteSql, conn);
            //Se intancia los parametros necesarios
            cmd.Parameters.AddWithValue("@Borrar", this.borrar);

            try
            {
                //Se abre la conexion a la base
                conn.Open();
                //Se ejecuta la petecion 
                affected = cmd.ExecuteNonQuery();

            }
            finally
            {
                //Se cierran conexiones
                conn.Close();
                cmd.Dispose();
            }
            return affected;
        }

        public int ActualizarDetalles()
        {
            //Se declara una variable para determinar si se elimina el dato de la base de datos
            int afectada = 0;
            //Se intancia la clase para hacer conexion con ella
            Conexion conectar = new Conexion();
            SqlConnection conn = conectar.conexionServer();
            //Se hace unsa sentencia sql para actualizar los detalles registros
            string updateSql = "update detalle_pedido set idPedido=@idPedido,idProducto=@idProducto,precioUnidad=@precioUnidad,numeroLinea=@numeroLinea," +
               "estado=@estado WHERE idDetallesPedido = @idDetallesPedido;";
            //Se intancia los parametros necesarios
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            cmd.Parameters.AddWithValue("@idPedido", this.idPedido);
            cmd.Parameters.AddWithValue("@idProducto", this.idProducto);
            cmd.Parameters.AddWithValue("@precioUnidad", this.precioUnidad);
            cmd.Parameters.AddWithValue("@numeroLinea", this.numeroLinea);
            cmd.Parameters.AddWithValue("@estado", this.estado);
            cmd.Parameters.AddWithValue("@idDetallesPedido", this.idDetallePedido);


            try
            {
                //Se abre la conexion a la base
                conn.Open();
                //Se ejecuta la petecion 
                afectada = cmd.ExecuteNonQuery();


            }
            finally
            {
                //Se cierran conexiones
                conn.Close();
                cmd.Dispose();
            }
            //Valor que devuelve
            return afectada;

        }

    }
}
