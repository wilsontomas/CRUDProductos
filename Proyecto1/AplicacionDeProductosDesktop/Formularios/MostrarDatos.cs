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
    public partial class MostrarDatos : Form
    {
        public MostrarDatos()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MostrarDatos_Load(object sender, EventArgs e)
        {
            mostrarSuplidores();
            mostrarFotosDelIdSeleccionado();
        }

        public void mostrarSuplidores()
        {
            MetodosDeCrud metodos = new MetodosDeCrud();
            //convertimos el id en un entero y con el obtenemos el registro de suplidores
            SuplidoresGrid.DataSource = metodos.ObtenerSuplidoresPorId(int.Parse(ProductoIdRecibido.Text)); 
        }

        public void mostrarFotosDelIdSeleccionado()
        {    MetodosDeCrud metodos = new MetodosDeCrud();
            var ListaDeImagenes = metodos.ObtenerImagenesPorIdWF(int.Parse(ProductoIdRecibido.Text));
                int coordenadaX = 20;
                int coordenadaY = 20;
            foreach (var img in ListaDeImagenes)
            {
               
                PictureBox picture = new PictureBox();
                //picture.Image = Image.FromFile(img.Imagen.ToString());
                //covertimos la imagen en un stream para poder agregarla al panel mediante un picturebox
                Stream stream = new MemoryStream(img.Imagen);
                picture.Image = Image.FromStream(stream);
                //establecemos la medida de la imagen
                picture.Size = new System.Drawing.Size(100, 100);              
                //la imagen se adaptara a la medida de su contenedor
                picture.SizeMode = PictureBoxSizeMode.StretchImage;
                //cordenadas de donde estara la imagen
                picture.Location = new Point(coordenadaX, coordenadaY);
                //la cordenada x se aumenta para que las imagenes esten una al lado de la otra
                coordenadaX += picture.Width + 10;
                //se agrega la imagen al panel
                PanelDeImagenes.Controls.Add(picture);
            }
            //mediante el id se buscan las imagenes que corresponden al registro y se llena el grid con ellas
           
            /*FotosGrid.RowTemplate.Height = 100;
            

            FotosGrid.DataSource = metodos.ObtenerImagenesPorIdWF(int.Parse(ProductoIdRecibido.Text));*/
        }
    }
}
