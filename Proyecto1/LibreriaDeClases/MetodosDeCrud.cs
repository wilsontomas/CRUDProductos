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
            string sql = "DELETE FROM Productos WHERE IdProducto=@IdProducto";
            if(BaseDeDatos.Conection.Execute(sql, new { @IdProducto = Id }) > 0)
            {
                respuesta = true;
            }
               
          /*  SqlCommand comando = BaseDeDatos.Conection.CreateCommand();
            comando.CommandText = "DELETE FROM Productos WHERE IdProducto=@Id";
            comando.Parameters.AddWithValue("@Id", Id);


            BaseDeDatos.Conection.Open();
            if (comando.ExecuteNonQuery() > 0)
            {
                respuesta = true;
            }
            BaseDeDatos.Conection.Close();*/
            return respuesta;
        }

        public Producto BuscarProducto(int Id)
        {

            string sql = "Select * from Productos WHERE IdProducto=@IdProducto";
            var producto = BaseDeDatos.Conection.QuerySingle<Producto>(sql, new { @IdProducto = Id });
            producto.Id = Id;
            return producto;
        }

        public bool EditarProducto(Producto producto)
        {

            //primero vaciamos los suplidores y las imagenes del producto a editar
            VaciarSuplidoresDeProducto(producto.Id);
            VaciarImagenesDeProducto(producto.Id);
            //creamos la variable de respuesta
            bool respuesta = false;
          
            string sql = "UPDATE Productos SET Nombre=@Nombre, PrecioM=@PrecioM, PrecioD=@PrecioD, CategoriaId=@CategoriaId, Descripcion=@Descripcion WHERE IdProducto=@IdProducto";
            int contador =  BaseDeDatos.Conection.Execute(sql, new { @Nombre= producto.Nombre, @PrecioM=producto.PrecioM, @PrecioD=producto.PrecioD, @CategoriaId=producto.CategoriaId, @Descripcion=producto.Descripcion, @IdProducto=producto.Id });
            if (contador > 0)
            {             
                //InsertarSuplidores(producto.Suplidores, producto.Id);
                InsertarSuplidoresObj(producto.Suplidores, producto.Id);
                //insertamos las imagenes con el id del producto que se esta editando             
                InsertarImagenes( producto.Id, producto.Imagenes);
                respuesta = true;
            }
          
            return respuesta;
        }







    }
}
