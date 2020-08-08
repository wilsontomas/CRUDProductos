using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaDeClases
{
   public class Producto
    {
        public int Id { get; set; }
        [Display(Name ="Nombre")]
        [Required(ErrorMessage ="Se debe agregar el nombre")]
        public string Nombre { get; set; }
        [Display(Name = "Precio al por mayor")]
        [Required(ErrorMessage = "Se debe agregar el precio al por mayor")]
        public decimal PrecioM  { get; set; }
        [Display(Name = "Precio al por menor")]
        [Required(ErrorMessage = "Se debe agregar el precio al por menor")]
        public decimal PrecioD { get; set; }
       
        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "Se debe agregar la descripcion")]
        public string Descripcion { get; set; }
        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Se deben elegir una categoria")]
        public int CategoriaId { get; set; }
       
        public List<Suplidor> Suplidores { get; set; }
        public List<byte[]> Imagenes { get; set; }
    }
}
