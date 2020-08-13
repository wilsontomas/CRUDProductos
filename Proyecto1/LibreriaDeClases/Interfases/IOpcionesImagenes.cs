using System.Collections.Generic;

namespace LibreriaDeClases
{
    public interface IOpcionesImagenes
    {
        List<byte[]> ObtenerImagenesPorId(int Id);
        void VaciarImagenesDeProducto(int Id);

        void InsertarImagenes(int Id, List<byte[]> imagenes);
    }
}