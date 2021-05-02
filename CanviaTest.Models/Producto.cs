using System;
using System.Collections.Generic;
using System.Text;

namespace CanviaTest.Models
{
    public class Producto
    {
        public int IdProducto {get;set;}
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int Cantidad_Stock { get; set; }
        public decimal Precio_Compra { get; set; }
        public decimal Precio_Venta { get; set; }
        public DateTime Fecha_Registro { get; set; }
    }
}
