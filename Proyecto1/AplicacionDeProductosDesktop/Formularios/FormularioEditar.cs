using LibreriaDeClases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplicacionDeProductosDesktop.Formularios
{
    public partial class FormularioEditar : Form
    {
        public FormularioEditar()
        {
            InitializeComponent();
        }
        //boton para cerrar la ventana
        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //aqui se guardaran la lista de imagenes
        List<byte[]> ListaDeImagenes = new List<byte[]>();
        public bool ComprobarCamposVacios()
        {
            bool respuesta = true;
            //comprobar si el nombre esta lleno
            if (NombreEdit.Text.Length == 0)
            {
                respuesta = false;
            }
            //comprobar si el precio al por mayor esta lleno y es entero
            if (PrecioMEdit.Text.Length == 0)
            {
                respuesta = false;
            }
            else
            {
                decimal number = 0;
                if (!decimal.TryParse(PrecioMEdit.Text, out number))
                {
                    MessageBox.Show("Precio al por mayor no es numerico");
                    respuesta = false;
                }
            }
            //comprobar si el precio al por menor esta lleno y es entero
            if (PrecioDEdit.Text.Length == 0)
            {
                respuesta = false;
            }
            else
            {
                decimal number = 0;
                if (!decimal.TryParse(PrecioDEdit.Text, out number))
                {
                    MessageBox.Show("Precio al por menor no es numerico");
                    respuesta = false;
                }
            }
            //comprobar la descripcion
            if (DescripcionEdit.Text.Length == 0)
            {
                respuesta = false;
            }
            //respuesta final de la verificacion

            //comprobamos que se seleccionaro imagenes
            int CantidadImagenes = ListaDeImagenes.Count();
            if (CantidadImagenes == 0)
            {
                respuesta = false;
            }

            return respuesta;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            /* if (!ComprobarCamposVacios())
             {
                 MessageBox.Show("Faltan campos por llenas");
             }*/

            if (ComprobarCamposVacios() == false)
            {
                MessageBox.Show("Faltan camps por llenar correctamente");
            }
            else
            {
                //aqui realizamos el proceso de insercion del producto

                //obtenemos los suplidores
                List<string> listaSuplidores = new List<string>();
                if (ListaSuplidoresEdit.CheckedItems.Count > 0)
                {

                    foreach (string nombres in ListaSuplidoresEdit.CheckedItems)
                    {
                        listaSuplidores.Add(nombres);
                        //MessageBox.Show(nombres);
                    }

                }
                else
                {
                    // MessageBox.Show("No se selecciono nada");
                }

                Producto producto = new Producto();
                producto.Id = int.Parse(EdicionId.Text);
                producto.Nombre = NombreEdit.Text;
                producto.Descripcion = DescripcionEdit.Text;
                producto.PrecioM = decimal.Parse(PrecioMEdit.Text.ToString());
                producto.PrecioD = decimal.Parse(PrecioDEdit.Text.ToString());
                producto.CategoriaId = CategoriaBoxEdit.SelectedIndex + 1;
                producto.Imagenes = ListaDeImagenes;
               /* 
                producto.Suplidores.Add*/
                  List<Suplidor> listasup = new List<Suplidor>();
                foreach (var item in listaSuplidores)
                {
                    Suplidor suplidor = new Suplidor();
                    suplidor.ProductoId = producto.Id;
                    suplidor.NombreSuplidor = item;
                    listasup.Add(suplidor);

                }
                producto.Suplidores = listasup;
                MetodosDeCrud metodos = new MetodosDeCrud();
                metodos.EditarProducto(producto);


                RefrescarCategoria();
                MessageBox.Show("El producto ha sido agregado");
                this.Close();

            }
        }

        private void FormularioEditar_Load(object sender, EventArgs e)
        {
            RefrescarCategoria();
            //se llenan los campos con la info del producto a editar
            LlenarCamposPorId(int.Parse(EdicionId.Text));
        }

        public void RefrescarCategoria()
        {
            MetodosDeCrud metodos = new MetodosDeCrud();

            Dictionary<string, string> CategoriaSource = new Dictionary<string, string>();
            var Categorias = metodos.ObtenerCategorias();
            foreach (var item in Categorias)
            {
                CategoriaSource.Add(item.IdCategoria.ToString(), item.NombreCategoria);
            }

            CategoriaBoxEdit.DataSource = new BindingSource(CategoriaSource, null);
            CategoriaBoxEdit.DisplayMember = "value";
            CategoriaBoxEdit.ValueMember = "key";
        }

        private void Imagen1BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                //inicializamos la ventanilla de seleccion
                OpenFileDialog Imagenes = new OpenFileDialog();
                //establecemos que la ventanilla puede seleccionar mas de un archivo
                Imagenes.Multiselect = true;
                //establecemos el filtro de solo imagenes
                Imagenes.Filter = "Files|*.jpg;*.jpeg;*.png;";
                if (Imagenes.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //obtenemos los nombres de las fotos seleccionadas
                    var nombres = Imagenes.FileNames.ToList();

                    foreach (var nombre in nombres)
                    {
                        //obtenemos la direccion completa de la imagen
                        string DireccionImagen = System.IO.Path.GetFullPath(nombre);
                        //creamos un memorystream para guardar la imagen en memoria
                        MemoryStream ImagenEnMemoria = new MemoryStream();
                        //copiamos la informacion de la imagen al memorystream
                        using (FileStream fs = File.OpenRead(DireccionImagen))
                        {
                            fs.CopyTo(ImagenEnMemoria);
                        }
                        //guardamos la imagen en forma de bytes
                        byte[] ImagenEnByte = ImagenEnMemoria.ToArray();
                        ListaDeImagenes.Add(ImagenEnByte);

                    }
                    if (ListaDeImagenes.Count > 0)
                    {
                        //MessageBox.Show("e"+imagen1);
                        Imagen1BtnEdit.Text = "Guardadas";
                    }
                    else
                    {
                        Imagen1BtnEdit.Text = "No se guardo";
                    }


                }
            }
            catch (Exception error)
            {
                MessageBox.Show("el error es: " + error);
            }
        }
        //este metodo llenara los campos con la informacion del producto a editar
        public void LlenarCamposPorId(int Id)
        {
            Producto producto = new Producto();
            MetodosDeCrud metodos = new MetodosDeCrud();
            producto = metodos.BuscarProducto(Id);
            NombreEdit.Text = producto.Nombre;
            PrecioMEdit.Text = producto.PrecioM.ToString();
            PrecioDEdit.Text = producto.PrecioD.ToString();
            DescripcionEdit.Text = producto.Descripcion;
        }
    }
}
