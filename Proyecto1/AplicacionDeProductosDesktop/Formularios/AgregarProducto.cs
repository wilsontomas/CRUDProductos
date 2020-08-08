using LibreriaDeClases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace AplicacionDeProductosDesktop.Formularios
{
    public partial class AgregarProducto : Form
    {

        public AgregarProducto()
        {
            InitializeComponent();
        }

        private void AgregarProducto_Load(object sender, EventArgs e)
        {
            RefrescarCategoria();
        }

         public void  RefrescarCategoria()
        {
            MetodosDeCrud metodos = new MetodosDeCrud();

            Dictionary<string, string> CategoriaSource = new Dictionary<string, string>();
           var Categorias = metodos.ObtenerCategorias();
            foreach (var item in Categorias)
            {
                CategoriaSource.Add(item.IdCategoria.ToString(),item.NombreCategoria);
            }

            CategoriaBox.DataSource = new BindingSource(CategoriaSource,null);
            CategoriaBox.DisplayMember="value";
            CategoriaBox.ValueMember="key";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //obtener los suplidores seleccionados
            /*  if (ListaSuplidores.CheckedItems.Count>0)
              {
                  List<string> lista = new List<string>();
                  foreach (string nombres in ListaSuplidores.CheckedItems)
                  {
                     lista.Add(nombres);
                      MessageBox.Show(nombres);
                  }

              }
              else
              {
                  MessageBox.Show("No se lesecciono nada");
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
                if (ListaSuplidores.CheckedItems.Count > 0)
                {
                   
                    foreach (string nombres in ListaSuplidores.CheckedItems)
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
                producto.Nombre = Nombre.Text;
                producto.Descripcion = Descripcion.Text;
                producto.PrecioM = decimal.Parse(PrecioM.Text.ToString());
                producto.PrecioD = decimal.Parse(PrecioD.Text.ToString());
                producto.CategoriaId = CategoriaBox.SelectedIndex + 1;
                producto.Imagenes = ListaDeImagenes;

                MetodosDeCrud metodos = new MetodosDeCrud();
                metodos.AgregarProductoEscritorio(producto, listaSuplidores);
                RefrescarCategoria();
                MessageBox.Show("El producto ha sido agregado");
                this.Close();

            }
          
            /* var selected = CategoriaBox.GetItemText(CategoriaBox.SelectedValue);
               MessageBox.Show(selected);*/
            //MessageBox.Show("imagen 1: "+imagen1.Length+" imagen 2: ");
        }
        
        List<byte[]> ListaDeImagenes = new List<byte[]>();
        private void Imagen1Btn_Click(object sender, EventArgs e)
        {
            try
            {
                //inicializamos la ventanilla de seleccion
                OpenFileDialog Imagenes = new OpenFileDialog();
                //establecemos que la ventanilla puede seleccionar mas de un archivo
                Imagenes.Multiselect = true;
                //establecemos el filtro de solo imagenes
                Imagenes.Filter = "Files|*.jpg;*.jpeg;*.png;";
                if (Imagenes.ShowDialog()==System.Windows.Forms.DialogResult.OK)
                {
                    //obtenemos los nombres de las fotos seleccionadas
                    var nombres = Imagenes.FileNames.ToList();

                    foreach(var nombre in nombres)
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
                        Imagen1Btn.Text = "Guardadas";
                    }
                    else
                    {
                        Imagen1Btn.Text = "No se guardo";
                    }
                  

                }
            }
            catch (Exception error)
            {
                MessageBox.Show("el error es: "+ error);
            }
        }
        //un metodo para ver si los campos fueron llenados
        public bool ComprobarCamposVacios()
        {
            bool respuesta = true;
            //comprobar si el nombre esta lleno
            if (Nombre.Text.Length == 0)
            {
                respuesta = false;
            }
            //comprobar si el precio al por mayor esta lleno y es entero
            if (PrecioM.Text.Length==0)
            {
                respuesta = false;
            }
            else
            {
                decimal number = 0;
                if (!decimal.TryParse(PrecioM.Text,out number))
                {
                    MessageBox.Show("Precio al por mayor no es numerico");
                    respuesta = false;
                }
            }
            //comprobar si el precio al por menor esta lleno y es entero
            if (PrecioD.Text.Length == 0)
            {
                respuesta = false;
            }
            else
            {
                decimal number = 0;
                if (!decimal.TryParse(PrecioD.Text, out number))
                {
                    MessageBox.Show("Precio al por menor no es numerico");
                    respuesta = false;
                }
            }
            //comprobar la descripcion
            if (Descripcion.Text.Length ==0)
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
       
    }
}
