using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoLab
{
    public partial class Clientes : Form
    {
        private readonly CClientes clientes = new CClientes();
        public Clientes()
        {
            InitializeComponent();
        }

        private void refrescar()
        {
            dataGridView1.DataSource = clientes.GetClientes();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //Boton Actualizar 
            if (!textIdCliente.Text.Equals(""))//Valida que el campo no este vacio
            {
                string id = dataGridView1.CurrentRow.Cells["idCliente"].Value.ToString();
                if (!string.IsNullOrEmpty(id))
                {
                    //Instancia los datos 
                    clientes.IdClientes = Convert.ToInt64(id);
                    clientes.Nombres = textNombre.Text.Trim();
                    clientes.Apellidos = textApellido.Text.Trim();
                    clientes.Telefono = textTelefono.Text.Trim();
                    clientes.Direccion = textDireccion.Text.Trim();
                    clientes.Ciudad = textCiudad.Text.Trim();
                    clientes.Departamento = textDepartamento.Text.Trim();
                    clientes.Estado = textEstado.Text.Trim();

                    //Almacena el dato devuelto
                    int Afectados = clientes.ActualizarClientes();

                    if (Afectados >= 1)
                    {
                        //Muetsra mensaje que se completo
                        MessageBox.Show("Registro Modificado Corectamente");
                        //Refresca el grid
                        refrescar();
                    }

                }
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            LoginVista loggin = new LoginVista();
            this.Hide();
            loggin.Show();
        }

        private void btnProducto_Click(object sender, EventArgs e)
        {
            Productos product = new Productos();
            this.Hide();
            product.Show();
        }
        private void Clientes_Load(object sender, EventArgs e)
        {
            //Load 
            //Metodo que recarga el load con los datos de la base
            refrescar();
            limpiarCampos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Se valida que los campos no esten vacios
            if (!string.IsNullOrEmpty(textNombre.Text) && !string.IsNullOrEmpty(textApellido.Text) && !string.IsNullOrEmpty(textTelefono.Text) && !string.IsNullOrEmpty(textTelefono.Text) && !string.IsNullOrEmpty(textDireccion.Text) && !string.IsNullOrEmpty(textCiudad.Text) && !string.IsNullOrEmpty(textDepartamento.Text))
            {
                //Se instancian los datos que se ingresam
                clientes.Nombres = textNombre.Text;
                clientes.Apellidos = textApellido.Text;
                clientes.Telefono = textTelefono.Text;
                clientes.Direccion = textDireccion.Text;
                clientes.Ciudad = textCiudad.Text;
                clientes.Departamento = textDepartamento.Text;
                clientes.Estado = "A";

                //Se alamacena en la variable id lo que devuleva el metodo 
                long id = clientes.AgregarClientes();
                if (id > 0)
                {
                    //Muestra el mensaje si funciono el ingreso
                    MessageBox.Show(" Cliente Agregado Correctamente");
                    //Refresca el grid
                    refrescar();
                    //Limpia los campos
                    limpiarCampos();
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!textIdCliente.Text.Equals(""))//Se valida que textID no este vacio
            {
                clientes.Borrar = textIdDelete.Text;//Se intancia el dato
                string id = dataGridView1.CurrentRow.Cells["idCliente"].Value.ToString();
                if (!string.IsNullOrEmpty(id))
                {
                    //Se instancia el dato
                    clientes.IdClientes = Convert.ToInt64(id);

                    //SE almacena el dato devuelto del metodo
                    int affected = clientes.Eliminar();
                    if (affected >= 1)
                    {
                        MessageBox.Show("Registro "+ textIdCliente.Text + " Eliminado");
                        textIdDelete.Text = "";
                        refrescar();

                    }
                }
            }
            else
            {
                MessageBox.Show("error");
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
           
            //Se declara una variable con la fila actual
                var fila = dataGridView1.CurrentRow;
                textIdCliente.Text = fila.Cells[0].Value.ToString();
                textNombre.Text = fila.Cells["nombres"].Value.ToString();
                textApellido.Text = fila.Cells["apellidos"].Value.ToString();
                textTelefono.Text = fila.Cells["telefono"].Value.ToString();
                textDireccion.Text = fila.Cells["direccion"].Value.ToString();
                textCiudad.Text = fila.Cells["ciudad"].Value.ToString();
                textDepartamento.Text = fila.Cells["departamento"].Value.ToString();
                textEstado.Text = fila.Cells["estado"].Value.ToString();
            
        }

        private void limpiarCampos()
        {
            //Se limpian los campos 
            textIdCliente.Text = "";
            textNombre.Text = "";
            textApellido.Text = "";
            textCiudad.Text = "";
            textDepartamento.Text = "";
            textDireccion.Text = "";
            textEstado.Text = "";
            textTelefono.Text = "";
            textIdDelete.Text = "";
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            //Se llama al metodo para limpiar campos
            limpiarCampos();
        }



        private void btnRealizarPedidios_Click(object sender, EventArgs e)
        {
            //Se instacia el form de realizar pedido
            RealizarPedidoVista detalles = new RealizarPedidoVista();
            //Se esconde este
            this.Hide();
            //Se muestra el otro
            detalles.Show();
        }

        private void btnDetallePedido_Click(object sender, EventArgs e)
        {
            //Se instacia el form de detalle de pedido
            DetalleDePedido detalles = new DetalleDePedido();
            //Se esconde este
            this.Hide();
            //Se muestra el otro
            detalles.Show();
        }

        private void btnIntegrantes_Click(object sender, EventArgs e)
        {
            //Se instacia el form de detalle de pedido
            Integrantes inte = new Integrantes();
            //Se esconde este
            inte.Show();
            //Se esconde este
            this.Hide();
        }
    }
}
