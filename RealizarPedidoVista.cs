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
    public partial class RealizarPedidoVista : Form
    {
        //instanciando y dejandolo como solo lectura clase cPedido
        private readonly cPedido pedido = new cPedido();

        //instanciando y dejandolo como solo lectura clase cCliente
        private readonly CClientes clientes = new CClientes();
        //instanciando y dejandolo como solo lectura clase cCliente
        private readonly Product producto = new Product();

        //Metodo para refrescar el grid
        private void refrescar()
        {
            //completamos el datafridView con el DataTable
            dataGridView1.DataSource = pedido.GetPedido();
        }
        public RealizarPedidoVista()
        {
            InitializeComponent();
            refrescar();
            
        }
        

        //Este metodo pertenece al boton salir y realiza el log out del usuario actual
        private void btnSalir_Click(object sender, EventArgs e)
        {
            LoginVista loggin = new LoginVista();
            this.Hide();
            loggin.Show();
        }

        //Este metodo es del boton agregar producto realiza una llamada a la forma para ingresar producto
        private void btnProducto_Click(object sender, EventArgs e)
        {
            Productos product = new Productos();
            this.Hide();
            product.Show();
        }
        //Este boton pertenece al boton ir a la forma  ingresar clientes
        private void button1_Click(object sender, EventArgs e)
        {
            Clientes detalles = new Clientes();
            this.Hide();
            detalles.Show();
        }

        //este metodo nos ayuda a calcular el tiempo de entrega para un producto
        private void CalculaTiempoEntrega(DateTime fechaSeleccionada)
        {
            if (fechaSeleccionada.DayOfWeek == DayOfWeek.Friday || fechaSeleccionada.DayOfWeek == DayOfWeek.Saturday || fechaSeleccionada.DayOfWeek == DayOfWeek.Sunday)
            {
                //estima tres dias para la entrega 
                lblfechaEstimadaEntrega.Text = fechaSeleccionada.AddDays(3).ToLongDateString();

            }
            else
            {
                //caso contrario estima, solo dos dias para la entrega
                lblfechaEstimadaEntrega.Text = fechaSeleccionada.AddDays(2).ToLongDateString();
            }

        }

        //codigo del evento valued changed perteneciente al objeto date time picker 
        private void fechaDePedidoDTP_ValueChanged(object sender, EventArgs e)
        {
            DateTime fechaPedido = fechaDePedidoDTP.Value;
            CalculaTiempoEntrega(fechaPedido);
        }

        //metodo nos permite que ajustar el objeto tipo datetime picker a los valores deseados
        private void RealizarPedidoVista_Load(object sender, EventArgs e)
        {
            //el usuario no puede seleccionar dias antes de hoy
            fechaDePedidoDTP.MinDate = DateTime.Today;
            //el usuario solo puede seleccionar dias de este año
            fechaDePedidoDTP.MaxDate = DateTime.Today.AddYears(1);
            //aplico informacion al combo box que tiene informacion del cliente
            cmbdeCliente.ValueMember = "ID del usuario";
            cmbdeCliente.DisplayMember="idCliente";
            DataTable mydt = new DataTable();
           // mydt = clientes.CargarClienteComboBox();
            cmbdeCliente.DataSource = (clientes.CargarClienteComboBox());

        }

        //METODO PARA LIMPIAR CAMPOS DEL 
        private void limpiarCampos()
        {
            cmbdeCliente.Text = "";
            fechaDePedidoDTP.Text = "";
            lblfechaEstimadaEntrega.Text = "";
            textDescripcion.Text = "";
            textEstado.Text = "";
            txtIdBuscar.Text = "";
            cmbdeCliente.Focus();
        }

        
        //boton agregar pedidos
        private void btnAgregar_Click(object sender, EventArgs e)
        {
           // if (!string.IsNullOrEmpty(cmbdeCliente.Text) && !string.IsNullOrEmpty(lblfechaEstimadaEntrega.Text) && !string.IsNullOrEmpty(textDescripcion.Text) && !string.IsNullOrEmpty(textEstado.Text))
          //  {
                pedido.IdCliente = Convert.ToInt64(cmbdeCliente.Text);
                pedido.FechaPedido = Convert.ToDateTime(fechaDePedidoDTP.Text);
                pedido.FechaEsperada = Convert.ToDateTime(lblfechaEstimadaEntrega.Text);
                pedido.Comentarios = textDescripcion.Text;
                pedido.Estado = "A";
               

                long id = pedido.AgregarPedidos();
                if (id > 0)
                {
                MessageBox.Show(" Pedido Agregado Correctamente");
                refrescar();
                limpiarCampos();
                
                    
                }
           // }



        }
        //METODO PARA LIMPIAR COMPONENTES DE LA FORMA
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
        }
        //CODIFICANDO EL EVENTO SELECTION CHANGED DEL DATAGRID
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //CREAMOS LA VARIABLE PARA CAPTURAR LA FILA ACTUAL
            var row = dataGridView1.CurrentRow;
            //asignamos el valor de idPedido
            txtIdBuscar.Text = row.Cells[0].Value.ToString();
            cmbdeCliente.Text = row.Cells["idCliente"].Value.ToString();
            lblfechaEstimadaEntrega.Text = row.Cells["fechaEsperada"].Value.ToString();
            textDescripcion.Text = row.Cells["comentarios"].Value.ToString();
            textEstado.Text = row.Cells["estado"].Value.ToString();

        }

        //Metodo para actualizar pedido del boton actualizar en el formulario Realizar pedido Vista
        private void btnActualizarPedido_Click(object sender, EventArgs e)
        {
            //validando que no este vacio
            if (!txtIdBuscar.Text.Equals(""))
            {
                //obteniendo identificado unico de registro
                string id = dataGridView1.CurrentRow.Cells["idPedido"].Value.ToString();
                if (!string.IsNullOrEmpty(id))
                {
                    pedido.IdPedido = Convert.ToInt64(id);
                    pedido.IdCliente = Convert.ToInt64(cmbdeCliente.Text);
                    pedido.FechaPedido = Convert.ToDateTime(fechaDePedidoDTP.Text);
                    pedido.FechaEsperada = Convert.ToDateTime(lblfechaEstimadaEntrega.Text);
                    pedido.Comentarios = textDescripcion.Text.Trim();
                    pedido.Estado = "A";

                    int affected = pedido.EditarPedido();
                    if (affected > 0)
                    {
                        MessageBox.Show("Actualizado");
                        refrescar();
                        limpiarCampos();
                    }
                }
            }
        }


        //Metodo para eliminar un pedido
        private void btnEliminarPedido_Click(object sender, EventArgs e)
        {
            //validando que no este vacio
            if (!txtIdBuscar.Text.Equals(""))
            {
                //obteniendo identificado unico de registro
                string id = dataGridView1.CurrentRow.Cells["idPedido"].Value.ToString();
                if (!string.IsNullOrEmpty(id))
                {
                    pedido.IdPedido = Convert.ToInt64(id);
                    pedido.Estado = "I";

                    int affected = pedido.EliminarPedido();
                    if (affected > 0)
                    {
                        MessageBox.Show("Pedido Eliminado");
                        refrescar();
                        limpiarCampos();
                    }
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!txtIdBuscar.Text.Equals(""))
            {
                pedido.Buscar = Convert.ToInt64(txtIdBuscar.Text);

                dataGridView1.DataSource = pedido.BuscarPedido();
            }
            else
            {
                MessageBox.Show("'ERROR:' No se pudo Encontrar el ID: " + " " + txtIdBuscar + "" + " ");
            }

        }

        private void btnBuscarIdCliente_Click(object sender, EventArgs e)
        {
            if (!txtBuscarIDCliente.Text.Equals(""))
            {
                pedido.Buscar = Convert.ToInt64(txtBuscarIDCliente.Text);

                dataGridView1.DataSource = pedido.BuscarPedidoPorIdCliente();
            }
            else
            {
                MessageBox.Show("'ERROR:' No se pudo Encontrar el ID: " + " " + txtBuscarIDCliente + "" + " ");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DetalleDePedido detalles = new DetalleDePedido();
            this.Hide();
            detalles.Show();
        }

       
    }
}
