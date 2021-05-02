using System;
using System.Collections.Generic;
using System.Text;
using CanviaTest.Models;
namespace CanviaTest.Data.Contratos
{
    public interface IFacturaDetalleRepositorio : IBase<Factura_Detalle>
    {
        List<Factura_Detalle> Listar_x_Factura(int idFactura);
        bool Eliminar(int idFactura, int idProducto);

    }
}
