using System.Collections.Generic;

namespace LibreriaDeClases
{
    public interface IOpcionesImagenes
    {
        List<byte[]> ObtenerImagenesPorId(int Id);
        List<Imagenes> ObtenerImagenesPorIdWF(int Id);
        void VaciarImagenesDeProducto(int Id);
    }
}