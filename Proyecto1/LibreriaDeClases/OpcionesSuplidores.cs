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
            string sql = "SELECT * FROM Suplidores WHERE ProductoId=@IdProducto";
            var DatosSuplidores = BaseDeDatos.Conection.Query(sql, new { @IdProducto=Id });
            foreach (var item in DatosSuplidores)
            {
                Suplidor S = new Suplidor();
                S.ProductoId = (int)item.ProductoId;
                S.NombreSuplidor = (string)item.NombreSuplidor;              
                suplidors.Add(S);
            }
            return suplidors;
        }

        public void VaciarSuplidoresDeProducto(int Id)
        {
           
            string sql = "DELETE FROM Suplidores WHERE ProductoId=@IdProducto";
            BaseDeDatos.Conection.Execute(sql, new { @IdProducto=Id });
        }
       public void InsertarSuplidores(SuplidoresSeleccionados datos, int Id)
        {
            List<Suplidor> suplidors = ObtenerListaSuplidores(datos, Id);
            if (suplidors.Count > 0)
            {
                //insertamos los suplidores con el id del producto           
                 string sql = "INSERT INTO Suplidores(ProductoId,NombreSuplidor) VALUES (@ProductoId,@NombreSuplidor)";
                foreach (var item in suplidors)
                {
                    BaseDeDatos.Conection.Query(sql, new { ProductoId = item.ProductoId, NombreSuplidor = item.NombreSuplidor });
                }           
            }
        }

        public void InsertarSuplidoresObj(List<Suplidor> lista, int Id)
        {
            if(lista.Count > 0)
            {
                string sql = "INSERT INTO Suplidores(ProductoId,NombreSuplidor) VALUES (@ProductoId,@NombreSuplidor)";
                    foreach(var item in lista)
                    {
                        BaseDeDatos.Conection.Execute(sql, new { @ProductoId = Id, @NombreSuplidor=item.NombreSuplidor });
                    }
            }
           
           
        }
    }
}