using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoLab
{
    public partial class DetalleDePedido : Form
    {
        //Se instancia las demas clases para mostrar los datos en el combo box
        private readonly cDetallePedido detalles = new cDetallePedido();

        private readonly cPedido pedido = new cPedido();

        private readonly Product producto = new Product();

        public DetalleDePedido()
        {
            InitializeComponent();
        }
        //Metdodo para limpiar los campos 
        private void limpiarCampos()
        {
            textIDdetalleDePedido.Text = "";
            cmbIdPedido.Text = "";
            cmbIDproducto.Text = "";
            texPrecio.Text = "";
            textEstado.Text = "";
            textNumeroLinea.Text = "";
        }
        //Metodo que refresca el grid donde se muestran los datos
        private void refrescar()
        {
            dataGridView1.DataSource = detalles.GetDetallesPedido();
        }
        private void DetalleDePedido_Load_1(object sender, EventArgs e)
        {
            refrescar();
            //Mostrar datos de productos en el comboBox
            cmbIDproducto.ValueMember = "ID del producto";
            cmbIDproducto.DisplayMember = "idProducto";
            DataTable mydt = new DataTable();
            cmbIDproducto.DataSource = (producto.CargarinfoproductoComboBox());

            ////Mostrar datos de pedido en el comboBox
            cmbIdPedido.ValueMember = "ID del pedido";
            cmbIdPedido.DisplayMember = "idPedido";
            cmbIdPedido.DataSource = (pedido.CargarPedidoComboBox());

            limpiarCampos();
        }
        //boton para agregar detalle de pedidos
        private void btnAgregar_Click(object sender, EventArgs e)
        {
          //Se instancian los datos que se ingresan para almacenarlos en la base de datos
            detalles.IdPedido = Convert.ToInt64(cmbIDproducto.Text);
            detalles.IdProducto = Convert.ToInt64(cmbIdPedido.Text);
            detalles.PrecioUnidad = Convert.ToDouble(texPrecio.Text);
            detalles.NumeroLinea = Convert.ToInt16(textNumeroLinea.Text);
            detalles.Estado = "A";
            //Variable que almacena el dato que devuelve el metodo
            long id = detalles.AgregarDetalles();
            if (id > 0)
            {
                MessageBox.Show(" Detalle de pedido Agregado Correctamente");
                refrescar();
                limpiarCampos();
            }
            

        }
        //Boton eliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Se intancia el dato que desea eliminar 
            detalles.Borrar = Convert.ToInt32(textIdDelete.Text);
            string id = dataGridView1.CurrentRow.Cells["IdDetallesPedido"].Value.ToString();
            if (!string.IsNullOrEmpty(id))
            {

                detalles.IdDetallePedido = Convert.ToInt64(id);
                //Variable que almacena el dato que devuelve el metodo
                int affected = detalles.Eliminar();
                if (affected >= 1)
                {
                    //Se confirma que ha eliminado el registro
                    MessageBox.Show("Registro " + textIDdetalleDePedido.Text + " Eliminado");
                    textIdDelete.Text = "";//Dejamos limpio el campo donde ingresa el ID
                    refrescar();//Refrescamos el grid

                }
            }

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            //if (!textIDdetalleDePedido.Text.Equals(""))
            // {
             string id = dataGridView1.CurrentRow.Cells["idDetallesPedido"].Value.ToString();
            if (!string.IsNullOrEmpty(id))
             {
                     detalles.IdDetallePedido = Convert.ToInt64(id);
                    detalles.IdPedido = Convert.ToInt64(cmbIDproducto.Text.Trim());
                    detalles.IdProducto = Convert.ToInt64(cmbIdPedido.Text.Trim());
                    detalles.PrecioUnidad = Convert.ToDouble(texPrecio.Text.Trim());
                    detalles.NumeroLinea = Convert.ToInt16(textNumeroLinea.Text.Trim());
                    detalles.Estado = "A";
                //Variable que almacena el dato que devuelve el metodo
                int Afectados = detalles.ActualizarDetalles();

                if (Afectados >= 1)
                {//Se confirma que ha eliminado el registro
                    MessageBox.Show("Registro Modificado Corectamente");//Dejamos limpio el campo donde ingresa el ID
                    refrescar();//Refrescamos el grid

                }
            }
                
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //Se crea una variable para capturar la fila 
            var fila = dataGridView1.CurrentRow;
            //Se llama a cada celda cuando la selecciona
            textIDdetalleDePedido.Text = fila.Cells[0].Value.ToString();
            cmbIdPedido.Text = fila.Cells["idPedido"].Value.ToString();
            cmbIDproducto.Text = fila.Cells["idProducto"].Value.ToString();
            texPrecio.Text = fila.Cells["precioUnidad"].Value.ToString();
            textNumeroLinea.Text = fila.Cells["numeroLinea"].Value.ToString();
            textEstado.Text = fila.Cells["estado"].Value.ToString();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            //Boton que limpia los campos completos
            limpiarCampos();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //Boton que abre la ventana de login
            LoginVista login = new LoginVista();
            //Muestra el nuevo form 
            login.Show();
            //Esconde el form actual 
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Boton que abre la ventana de clientes
            Clientes detalles = new Clientes();
            //Muestra el nuevo form 
            this.Hide();
            //Esconde el form actual 
            detalles.Show();
        }

        private void btnProducto_Click(object sender, EventArgs e)
        {
            //Boton que abre la ventana de productos
            Productos product = new Productos();
            //Muestra el nuevo form 
            this.Hide();
            //Esconde el form actual 
            product.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Boton que abre la ventana de detalle de pedidos
            RealizarPedidoVista detalles = new RealizarPedidoVista();
            //Muestra el nuevo form 
            this.Hide();
            //Esconde el form actual 
            detalles.Show();
        }
    }
}
