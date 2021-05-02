using System;
using System.Collections.Generic;
using System.Text;

namespace CanviaTest.Models
{
    public class Factura_Detalle
    {
        public int IdFactura { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_Unidad { get; set; }
    }
}
