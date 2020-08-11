using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
    namespace LibreriaDeClases
    {
    public class OpcionesSuplidores : OpcionesImagenes, IOpcionesSuplidores
    {
        public List<Suplidor> ObtenerListaSuplidores(SuplidoresSeleccionados datos, int IdProducto)
        {
            List<Suplidor> suplidors = new List<Suplidor>();
            if (datos.PrimerSuplidor != false)
            {
                suplidors.Add(new Suplidor { NombreSuplidor = "Leterago", ProductoId = IdProducto });
            }
            if (datos.SegundoSuplidor != false)
            {
                suplidors.Add(new Suplidor { NombreSuplidor = "Disfarcam", ProductoId = IdProducto });
            }
            if (datos.TercerSuplidor != false)
            {
                suplidors.Add(new Suplidor { NombreSuplidor = "Ibero", ProductoId = IdProducto });
            }
            if (datos.CuartoSuplidor != false)
            {
                suplidors.Add(new Suplidor { NombreSuplidor = "Calox", ProductoId = IdProducto });
            }

            return suplidors;
        }

        public List<Suplidor> ObtenerSuplidoresPorId(int Id)
        {
            List<Suplidor> suplidors = new List<Suplidor>();
            SqlCommand Comand = BaseDeDatos.Conection.CreateCommand();
            Comand.CommandText = "SELECT * FROM Suplidores WHERE ProductoId=@Id";
            Comand.Parameters.AddWithValue("@Id", Id);
            SqlDataReader reader = null;
            BaseDeDatos.Conection.Open();
            reader = Comand.ExecuteReader();
            while (reader.Read())
            {
                Suplidor S = new Suplidor();
                S.ProductoId = (int)reader["ProductoId"];
                S.NombreSuplidor = (string)reader["NombreSuplidor"];
                suplidors.Add(S);
            }
            BaseDeDatos.Conection.Close();
            return suplidors;
        }

        public void VaciarSuplidoresDeProducto(int Id)
        {
            SqlCommand comand = BaseDeDatos.Conection.CreateCommand();
            comand.CommandText = "DELETE FROM Suplidores WHERE ProductoId=@Id";
            comand.Parameters.AddWithValue("@Id", Id);
            BaseDeDatos.Conection.Open();
            comand.ExecuteNonQuery();
            BaseDeDatos.Conection.Close();
        }
       public void InsertarSuplidores(SuplidoresSeleccionados datos, int Id)
        {
            List<Suplidor> suplidors = ObtenerListaSuplidores(datos, Id);
            if (suplidors.Count > 0)
            {
                //insertamos los suplidores con el id del producto
              /*  foreach (var item in suplidors)
                {
                    SqlCommand cmd = BaseDeDatos.Conection.CreateCommand();
                    cmd.CommandText = "INSERT INTO Suplidores(ProductoId,NombreSuplidor) VALUES (@ProductoId,@NombreSuplidor)";
                    cmd.Parameters.AddWithValue("@ProductoId", item.ProductoId);
                    cmd.Parameters.AddWithValue("@NombreSuplidor", item.NombreSuplidor);
                    cmd.ExecuteNonQuery();
                }*/
                 string sql = "INSERT INTO Suplidores(ProductoId,NombreSuplidor) VALUES (@ProductoId,@NombreSuplidor)";
                foreach (var item in suplidors)
                {
                    BaseDeDatos.Conection.Query(sql, new { ProductoId = item.ProductoId, NombreSuplidor = item.NombreSuplidor });
                }
            
            }
        }
    }
}