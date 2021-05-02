using System;
using System.Collections.Generic;
using System.Text;

namespace CanviaTest.Models
{
    public class Factura
    {
        public int IdFactura { get; set; }
        public string Serie_Factura {get;set;}
        public int Numero_Factura { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        public int IdEmpleado { get; set; }
    }
}
