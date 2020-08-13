using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace LibreriaDeClases
{
    public class OpcionesImagenes : IOpcionesImagenes
    {
        public BaseDeDatos BaseDeDatos = new BaseDeDatos();

        public void VaciarImagenesDeProducto(int Id)
        {         
            string sql = "DELETE FROM ImagenesProducto WHERE IdProducto=@IdProducto";
            BaseDeDatos.Conection.Execute(sql, new { @IdProducto=Id });
        }

        public List<byte[]> ObtenerImagenesPorId(int Id)
        {
     
            List<byte[]> ListaDeImagenes = new List<byte[]>();
            string sql = "SELECT * FROM ImagenesProducto WHERE IdProducto=@IdProducto";
            var ListaImg = BaseDeDatos.Conection.Query(sql, new { @IdProducto = Id }).ToList();

            foreach (var item in ListaImg)
            {      
                ListaDeImagenes.Add((byte[])item.Imagen);
            }
            return ListaDeImagenes;
        }

        public void InsertarImagenes(int Id, List<byte[]> imagenes)
        {
            if(imagenes.Count > 0)
            {
                foreach (var item in imagenes)
                {
                    string sql = "INSERT INTO ImagenesProducto(IdProducto,Imagen) VALUES (@IdProducto,@Imagen)";
                    BaseDeDatos.Conection.Query(sql, new { IdProducto=Id, Imagen=item });
                }
            }
            
        }
    }
}