using System;
using System.Collections.Generic;
using System.Text;

namespace CanviaTest.Models
{
    public class Cliente
    {
        public Cliente()
        {
            Facturas = new List<Factura>();
        }
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string DNI { get; set; }

        public List<Factura> Facturas { get; set; }

    }
}
