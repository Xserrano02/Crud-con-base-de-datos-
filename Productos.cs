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
    public partial class Productos : Form
    {
        private readonly Product producto = new Product();
        public Productos()
        {
            InitializeComponent();
        }

       

        private void btnSalir_Click(object sender, EventArgs e)
        {
            LoginVista log = new LoginVista();
            this.Hide();
            log.Show();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            Clientes clientes = new Clientes();
            this.Hide();
            clientes.Show();
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            refrescar();
        }

        private void refrescar()
        {
            dataGridView1.DataSource = producto.GetProduct();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
           // if (!string.IsNullOrEmpty(textProducto.Text) && !string.IsNullOrEmpty(textDescripcion.Text) && !string.IsNullOrEmpty(textCantidad.Text) && !string.IsNullOrEmpty(textPrecio.Text) && !string.IsNullOrEmpty(textEstado.Text))
         //   {
                producto.Producto = textProducto.Text;
                producto.Descripcion = textDescripcion.Text;
                producto.CantidadStock = Convert.ToInt32(textCantidad.Text);
                producto.Precio = Convert.ToDecimal(textPrecio.Text);
                producto.Estado = "A";


                long id = producto.AgregarProduct();
                if (id > 0)
                {
                    MessageBox.Show(" Producto Agregado Correctamente");
                    refrescar();
                }
          //  }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var fila = dataGridView1.CurrentRow;
            textIdProducto.Text = fila.Cells[0].Value.ToString();
            textProducto.Text = fila.Cells["producto"].Value.ToString();
            textDescripcion.Text = fila.Cells["descripcion"].Value.ToString();
            textCantidad.Text = fila.Cells["cantidadEnStock"].Value.ToString();
            textPrecio.Text = fila.Cells["precioVenta"].Value.ToString();
            textEstado.Text = fila.Cells["estado"].Value.ToString();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!textIdProducto.Text.Equals(""))
            {
                producto.Borrar = textDelete.Text;
                string id = dataGridView1.CurrentRow.Cells["idProducto"].Value.ToString();
                if (!string.IsNullOrEmpty(id))
                {
                    producto.IdProductos = Convert.ToInt64(id);
                    int affected = producto.Eliminar();
                    if (affected >= 1)
                    {
                        MessageBox.Show("Regisrtro "+ "" + textIdProducto.Text +"" + "Eliminado");
                        refrescar();
                    }
                }
            }
            else
            {
                MessageBox.Show("'ERROR:' No se pudo Eliminar el ID: " + " " + textDelete +" ");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!textIdProducto.Text.Equals(""))
            {
                string id = dataGridView1.CurrentRow.Cells["idProducto"].Value.ToString();
                if (!string.IsNullOrEmpty(id))
                {
                    producto.IdProductos = Convert.ToInt64(id);
                    producto.Producto = textProducto.Text.Trim();
                    producto.Descripcion = textDescripcion.Text.Trim();
                    producto.CantidadStock = Convert.ToInt32(textCantidad.Text);
                    producto.Precio = Convert.ToDecimal(textPrecio.Text);
                    producto.Estado = textEstado.Text.Trim();


                    int Afectados = producto.ActualizarProducto();

                    if (Afectados >= 1)
                    {
                        MessageBox.Show("Registro Modificado Correctamente");
                        refrescar();
                    }

                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            textIdProducto.Text = "";
            textProducto.Text = "";
            textDescripcion.Text = "";
            textCantidad.Text = "";
            textPrecio.Text = "";
            textEstado.Text = "";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!textIdProducto.Text.Equals(""))
            {
                producto.Buscar = textBuscar.Text;

                dataGridView1.DataSource = producto.BuscarProducto();
            }
            else
            {
                MessageBox.Show("'ERROR:' No se pudo Eliminar el ID: " + " " + textDelete + "" + " ");
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            refrescar();
        }

        //BOTON DE REALIZAR PEDIDOS
        private void button3_Click(object sender, EventArgs e)
        {
            RealizarPedidoVista detalles = new RealizarPedidoVista();
            this.Hide();
            detalles.Show();
        }

        //BOTON DETALLE DE PEDIDO
        private void btnPedido_Click(object sender, EventArgs e)
        {
            DetalleDePedido detalles = new DetalleDePedido();
            this.Hide();
            detalles.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Productos product = new Productos();
            this.Hide();
            product.Show();
        }
    }
}
