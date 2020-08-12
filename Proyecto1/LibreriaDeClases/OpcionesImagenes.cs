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
        public List<Imagenes> ObtenerImagenesPorIdWF(int Id)
        {
            List<Imagenes> imagenes = new List<Imagenes>();
            SqlCommand Comand = BaseDeDatos.Conection.CreateCommand();
            Comand.CommandText = "SELECT * FROM ImagenesProducto WHERE IdProducto=@Id";
            Comand.Parameters.AddWithValue("@Id", Id);
            BaseDeDatos.Conection.Open();
            SqlDataReader reader = Comand.ExecuteReader();
            while (reader.Read())
            {
                Imagenes img = new Imagenes();
                //img.IdProducto = (int)reader["IdProducto"];
                // img.IdImagenes = (int)reader["IdImagenes"];
                img.Imagen = (byte[])reader["Imagen"];
                imagenes.Add(img);

            }
            BaseDeDatos.Conection.Close();

            return imagenes;
        }

        public void VaciarImagenesDeProducto(int Id)
        {
            SqlCommand comand = BaseDeDatos.Conection.CreateCommand();
            comand.CommandText = "DELETE FROM ImagenesProducto WHERE IdProducto=@Id";
            comand.Parameters.AddWithValue("@Id", Id);
            BaseDeDatos.Conection.Open();
            comand.ExecuteNonQuery();
            BaseDeDatos.Conection.Close();
        }

        public List<byte[]> ObtenerImagenesPorId(int Id)
        {
            List<byte[]> ListaDeImagenes = new List<byte[]>();
            SqlCommand Comand = BaseDeDatos.Conection.CreateCommand();
            Comand.CommandText = "SELECT * FROM ImagenesProducto WHERE IdProducto=@Id";
            Comand.Parameters.AddWithValue("@Id", Id);
            BaseDeDatos.Conection.Open();
            SqlDataReader reader = Comand.ExecuteReader();
            while (reader.Read())
            {

                byte[] ImagenBytes = (byte[])reader["Imagen"];
                ListaDeImagenes.Add(ImagenBytes);
            }
            BaseDeDatos.Conection.Close();
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