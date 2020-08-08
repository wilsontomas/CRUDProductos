using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibreriaDeClases;
namespace AplicacionDeProductosDesktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefrescarLista();
        }
        public void RefrescarLista()
        {
            MetodosDeCrud metodos = new MetodosDeCrud();
            dataGridView1.DataSource = metodos.ObtenerProductosCategorias().ToList();
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Formularios.AgregarProducto agregarProducto = new Formularios.AgregarProducto();
            agregarProducto.ShowDialog();

            RefrescarLista();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            int CeldaSeleccionada = int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
           // MessageBox.Show("El id es: "+CeldaSeleccionada);

            
            if(MessageBox.Show("Desea eliminar el producto "+ CeldaSeleccionada+ "?", "Confirmacion",MessageBoxButtons.YesNo)== System.Windows.Forms.DialogResult.Yes)
            {
               
                MetodosDeCrud metodos = new MetodosDeCrud();
                //se llama el metodo que eliminara el producto por su id
                metodos.EliminarProducto(CeldaSeleccionada);
                //se refresca la lista de productos
                RefrescarLista();
            }
            else
            {
                MessageBox.Show("Se cancelo");
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            int CeldaSeleccionada = int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
            Formularios.MostrarDatos mostrarDatos = new Formularios.MostrarDatos();
            mostrarDatos.ProductoIdRecibido.Text = CeldaSeleccionada.ToString();
            mostrarDatos.ShowDialog();

           
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            int CeldaSeleccionada = int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
            Formularios.FormularioEditar formularioEditar = new Formularios.FormularioEditar();
            formularioEditar.EdicionId.Text = CeldaSeleccionada.ToString();
            formularioEditar.ShowDialog();

            RefrescarLista();
        }
    }

    
}
