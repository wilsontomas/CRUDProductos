using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace LibreriaDeClases
{
    public class MetodosDeCrud : OpcionesSuplidores, IMetodosDeCrud
    {

        public List<Categoria> ObtenerCategorias()
        {
           
           string sql = "SELECT * FROM Categoria";
           var ListaCategorias = BaseDeDatos.Conection.Query<Categoria>(sql).ToList();
         
            return ListaCategorias;
        }

        public List<ProductoCategoria> ObtenerProductosCategorias()
        {
          
          string sql = "SELECT Productos.IdProducto, Productos.Nombre, Productos.PrecioM,Productos.PrecioD,Productos.Descripcion, Categoria.NombreCategoria FROM Productos INNER JOIN Categoria ON Productos.CategoriaId = Categoria.IdCategoria";
          var Productos = BaseDeDatos.Conection.Query<ProductoCategoria>(sql).ToList();

          return Productos;
        }


        public bool AgregarProducto(Producto producto, SuplidoresSeleccionados suplidores)
        {

            bool respuesta = true;

                string sql ="insert into Productos(Nombre,PrecioM,PrecioD,Descripcion,CategoriaId) VALUES (@Nombre,@PrecioM,@PrecioD,@Descripcion,@CategoriaId); SELECT SCOPE_IDENTITY()";

                int insertedID = BaseDeDatos.Conection.QuerySingle<int>(sql, new {
                @Nombre = producto.Nombre,
                @PrecioM = producto.PrecioM,
                @PrecioD = producto.PrecioD,
                @Descripcion =  producto.Descripcion,
                @CategoriaId = producto.CategoriaId
                 });

            if (insertedID > 0)
            {
                respuesta = true;
                //INSERTAR LOS SUPLIDORES
                InsertarSuplidores(suplidores, insertedID);
                //INSERTAMOS LAS IMAGENES CON EL ID DEL PRODUCTO
                 InsertarImagenes(insertedID, producto.Imagenes);
         
            }
            else { respuesta = false; }

            BaseDeDatos.Conection.Close();

            return respuesta;
        }

        public bool EliminarProducto(int Id)
        {
            //primero vaciamos los registros que poseen el id del producto
            #region
            VaciarSuplidoresDeProducto(Id);
            VaciarImagenesDeProducto(Id);
            #endregion

            bool respuesta = false;
            SqlCommand comando = BaseDeDatos.Conection.CreateCommand();
            comando.CommandText = "DELETE FROM Productos WHERE IdProducto=@Id";
            comando.Parameters.AddWithValue("@Id", Id);


            BaseDeDatos.Conection.Open();
            if (comando.ExecuteNonQuery() > 0)
            {
                respuesta = true;
            }
            BaseDeDatos.Conection.Close();
            return respuesta;
        }

        public Producto BuscarProducto(int Id)
        {

            SqlDataReader DatosProducto = null;
            Producto producto = new Producto();
            SqlCommand comando = BaseDeDatos.Conection.CreateCommand();
            comando.CommandText = "Select * from Productos WHERE IdProducto=@Id";
            comando.Parameters.AddWithValue("@Id", Id);

            BaseDeDatos.Conection.Open();
            DatosProducto = comando.ExecuteReader();
            while (DatosProducto.Read())
            {
                producto.Id = (int)DatosProducto["IdProducto"];
                producto.Nombre = (string)DatosProducto["Nombre"];
                producto.PrecioM = (decimal)DatosProducto["PrecioM"];
                producto.PrecioD = (decimal)DatosProducto["PrecioD"];
                producto.CategoriaId = (int)DatosProducto["CategoriaId"];

                producto.Descripcion = (string)DatosProducto["Descripcion"];


            }
            BaseDeDatos.Conection.Close();

            return producto;
        }

        public bool EditarProducto(Producto producto)
        {

            //primero vaciamos los suplidores y las imagenes del producto a editar
            VaciarSuplidoresDeProducto(producto.Id);
            VaciarImagenesDeProducto(producto.Id);
            //creamos la variable de respuesta
            bool respuesta = false;
            SqlCommand comando = BaseDeDatos.Conection.CreateCommand();

            comando.CommandText = "UPDATE Productos SET Nombre=@Nombre, PrecioM=@PrecioM, PrecioD=@PrecioD, CategoriaId=@CategoriaId, Descripcion=@Descripcion WHERE IdProducto=@Id";
            comando.Parameters.AddWithValue("@Id", producto.Id);
            comando.Parameters.AddWithValue("@Nombre", producto.Nombre);
            comando.Parameters.AddWithValue("@PrecioM", producto.PrecioM);
            comando.Parameters.AddWithValue("@PrecioD", producto.PrecioD);
            comando.Parameters.AddWithValue("@CategoriaId", producto.CategoriaId);

            comando.Parameters.AddWithValue("@Descripcion", producto.Descripcion);


            BaseDeDatos.Conection.Open();
            if (comando.ExecuteNonQuery() > 0)
            {
                foreach (var item in producto.Suplidores)
                {
                    //despues de editar el producto se insertan los nuevo suplidores
                    SqlCommand cmd = BaseDeDatos.Conection.CreateCommand();
                    cmd.CommandText = "INSERT INTO Suplidores(ProductoId,NombreSuplidor) VALUES (@ProductoId,@NombreSuplidor)";
                    cmd.Parameters.AddWithValue("@ProductoId", item.ProductoId);
                    cmd.Parameters.AddWithValue("@NombreSuplidor", item.NombreSuplidor);
                    cmd.ExecuteNonQuery();
                }
                //insertamos las imagenes con el id del producto que se esta editando
                foreach (var item in producto.Imagenes)
                {
                    SqlCommand comand = BaseDeDatos.Conection.CreateCommand();
                    comand.CommandText = "INSERT INTO ImagenesProducto(IdProducto,Imagen) VALUES (@IdProducto,@Imagen)";
                    comand.Parameters.AddWithValue("@IdProducto", producto.Id);
                    comand.Parameters.AddWithValue("@Imagen", item);
                    comand.ExecuteNonQuery();

                }
                respuesta = true;
            }
            BaseDeDatos.Conection.Close();
            return respuesta;
        }







    }
}
