using System.Collections.Generic;

namespace LibreriaDeClases
{
    public interface IOpcionesSuplidores: IOpcionesImagenes
    {
        List<Suplidor> ObtenerListaSuplidores(SuplidoresSeleccionados datos, int IdProducto);
        List<Suplidor> ObtenerSuplidoresPorId(int Id);
        void VaciarSuplidoresDeProducto(int Id);

        void InsertarSuplidores(SuplidoresSeleccionados datos, int Id);
        void InsertarSuplidoresObj(List<Suplidor> lista, int Id);
    }
}