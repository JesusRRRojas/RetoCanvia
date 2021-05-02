using CanviaTest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CanviaTest.Data.Contratos
{
    public  interface IFacturaRepositorio : IBase<Factura>
    {
        List<Factura> Facturas_x_Cliente(int idCliente);
    }
}
