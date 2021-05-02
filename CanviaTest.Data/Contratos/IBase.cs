using System;
using System.Collections.Generic;
using System.Text;

namespace CanviaTest.Data.Contratos
{
    public interface IBase<T> where T: class
    {
        List<T> Listar();
        T Insertar(T entidad);
        T Actualizar(T entidad);
        bool Eliminar(int id);
    }
}
