using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;



namespace ProyectoLab
{
    class Product
    {

        //Se declaran las variables a utilizar
        private long idProductos;
        private string producto;
        private string descripcion;
        private int cantidadStock;
        private decimal precio;
        private string estado;
        private string borrar;
        private string buscar;


        //Constructor vacio
        public Product()
        {

        }

        //Constructor instanciando las variables
        public Product(long idProductos, string producto, string descripcion, int cantidadStock, decimal precio, string estado, string buscar)
        {
            this.idProductos = idProductos;
            this.producto = producto;
            this.descripcion = descripcion;
            this.cantidadStock = cantidadStock;
            this.precio = precio;
            this.estado = estado;
            this.buscar = buscar;
        }

        //******************** Aqui se setean los datos
        public long IdProductos
        {
            get
            {
                return idProductos;
            }
            set
            {
                idProductos = value;
            }
        }


        public string Producto
        {
            get
            {
                return producto;
            }
            set
            {
                producto = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return descripcion;
            }
            set
            {
                descripcion = value;
            }
        }

        public int CantidadStock
        {
            get
            {
                return cantidadStock;
            }
            set
            {
                cantidadStock = value;
            }
        }


        public decimal Precio
        {
            get
            {
                return precio;
            }
            set
            {
                precio = value;
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

        public string Buscar
        {
            get
            {
                return buscar;
            }
            set
            {
                buscar = value;
            }
        }


        public DataTable GetProduct()
        {
            //Objeto de tipo tabla 

            DataTable mydt = new DataTable();
            //Se crea la clase de conexion  
            Conexion conectar = new Conexion();

            SqlConnection conect = conectar.conexionServer();

            //String de consulta que se hace a las base de datos para que devuelva los datos
            string sql = "SELECT idProducto,producto,descripcion,cantidadEnStock,precioVenta,estado FROM productos where estado = 'A';";

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
                //Se cierran las conexiones
                cmd.Dispose();
                conect.Close();
            }
            return mydt;//Valor que devuelve
        }

        public long AgregarProduct()
        {
            long id; //Se declara el valor que va a devolver
            Conexion conectar = new Conexion(); //Objeto de tipo tabla 
            SqlConnection conect = conectar.conexionServer();
            //Se hace la la sentencia sql para agregar los datos
            string insertSql = "insert into productos(producto,descripcion, cantidadEnStock, precioVenta, estado) values (@producto,@descripcion,@cantidadEnStock,@precioVenta,@estado); SELECT IDENT_CURRENT('productos') as id;";
            SqlCommand cmd = new SqlCommand(insertSql, conect);
            //Se agregan los parametros
            cmd.Parameters.AddWithValue("@producto", this.producto);
            cmd.Parameters.AddWithValue("@descripcion", this.descripcion);
            cmd.Parameters.AddWithValue("@cantidadEnStock", this.cantidadStock);
            cmd.Parameters.AddWithValue("@precioVenta", this.precio);
            cmd.Parameters.AddWithValue("@estado", this.estado);
            try
            {
                //Se abre la conexion 
                conect.Open();
                //Se ejecuta la sentencia sql
                id = Convert.ToInt64(cmd.ExecuteScalar());
            }
            finally
            {
                //Se cierran conexiones
                conect.Close();
                cmd.Dispose();
            }
            return id;
        }

        public int Eliminar()
        {
            int affected = 0;//Se declara el valor que va a devolver
            Conexion conectar = new Conexion();//Objeto de tipo tabla 
            SqlConnection conn = conectar.conexionServer();//Se conecta a la base
             //Se hace la la sentencia sql para agregar los datos
            string deleteSql = "update productos set estado = 'I' where idProducto = @borrar ";
            SqlCommand cmd = new SqlCommand(deleteSql, conn);
            //Se agregan los valores
            cmd.Parameters.AddWithValue("@Borrar", this.borrar);

            try
            {
                //Se abre la conexion
                conn.Open();
                //Se ejeucta la sentencia sql
                affected = cmd.ExecuteNonQuery();

            }
            finally
            {
                //Se cierran las conexiones
                conn.Close();
                cmd.Dispose();
            }
            return affected;
        }


        public int ActualizarProducto()
        {
            int afectada = 0;//Se declara el valor que va a devolver
            Conexion conectar = new Conexion();//Objeto de tipo tabla 
            SqlConnection conn = conectar.conexionServer();
            //Se hace la la sentencia sql para agregar los datos
            string updateSql = "update productos set producto=@producto,descripcion=@descripcion,cantidadEnStock=@CantidadEnStock,precioVenta=@precioVenta," +
                "estado=@estado WHERE idProducto = @idProducto;";
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            //Se agregan los valores necesarios
            cmd.Parameters.AddWithValue("@producto", this.producto);
            cmd.Parameters.AddWithValue("@descripcion", this.descripcion);
            cmd.Parameters.AddWithValue("@cantidadEnStock", this.cantidadStock);
            cmd.Parameters.AddWithValue("@precioVenta", this.precio);
            cmd.Parameters.AddWithValue("@estado", this.estado);
            cmd.Parameters.AddWithValue("@idProducto", this.idProductos);


            try
            {//Se abre la conexion
                conn.Open();
                //Se ejeucta la sentencia y captura los registrosa actualizados
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

        public DataTable BuscarProducto()
        {
            DataTable mydt = new DataTable();//Objeto de tipo tabla 
            Conexion conectar = new Conexion();
            SqlConnection conn = conectar.conexionServer();//Se crea la clase de conexion 
            // String de consulta que se hace a las base de datos para que devuelva los datos
            string findSql = "select * from productos where producto=@buscar;";

            SqlCommand comando = new SqlCommand(findSql, conn);
            //Se parametrizan el dato a buscar
            comando.Parameters.AddWithValue("@buscar", this.buscar);

            //Hace lectura de la tabla
            SqlDataReader mydr = null; 
            


            try
            {
                //Se abre la conexion
                conn.Open();
                //Ejecuta la consulta hecha 
                mydr = comando.ExecuteReader();
                //muestra los datos de la lectura hecha
                mydt.Load(mydr);
                
            }
            finally
            {
                //Se cierran las conexiones
                conn.Close();
                comando.Dispose();
            }
            return mydt;

        }

        //Metodo para cargar informacion al combo box de productos
        public DataTable CargarinfoproductoComboBox()
        {
            //Objeto de tipo tabla 
            DataTable mydt = new DataTable();
            //Se crea la clase de conexion  
            Conexion conectar = new Conexion();
            SqlConnection conect = conectar.conexionServer();

            //String de consulta que se hace a las base de datos para que devuelva los datos
            string sql = "SELECT idProducto,producto FROM productos where estado = 'A';";
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
