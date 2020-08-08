using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaDeClases
{
  public class ProductoCategoria
    { 
        public int IdProducto { get; set; }
       
        public string Nombre { get; set; }
       
        public decimal PrecioM { get; set; }
      
        public decimal PrecioD { get; set; }

         
        public string Descripcion { get; set; }       
        public string NombreCategoria { get; set; }
        public List<Suplidor> Suplidores { get; set; }
        public List<byte[]> Imagenes { get; set; }
    }
}
