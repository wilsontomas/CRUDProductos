using System.Collections.Generic;

namespace LibreriaDeClases
{
    public interface IMetodosDeCrud: IOpcionesSuplidores
    {
        bool AgregarProducto(Producto producto, SuplidoresSeleccionados suplidores);
        Producto BuscarProducto(int Id);
        bool EditarProducto(Producto producto);
        bool EliminarProducto(int Id);
        List<Categoria> ObtenerCategorias();
        List<Categoria> ObtenerCategoriasADO();
        List<ProductoCategoria> ObtenerProductosCategorias();
    }
}