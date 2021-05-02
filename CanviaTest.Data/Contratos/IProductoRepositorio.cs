
using System;
using System.Collections.Generic;
using System.Text;
using CanviaTest.Models;
namespace CanviaTest.Data.Contratos
{
    public interface IProductoRepositorio : IBase<Producto>
    {
        (int totalRegistros, IEnumerable<Producto> registros) ObtenerPaginas(int paginaActual, int registrosPorPagina);
    }
}
