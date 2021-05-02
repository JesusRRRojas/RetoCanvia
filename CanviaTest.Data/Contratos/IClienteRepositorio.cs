using CanviaTest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CanviaTest.Data.Contratos
{
    public interface IClienteRepositorio : IBase<Cliente>
    {
        Cliente ListarCliente(int Id);
        Cliente ListarClienteConDetalle(int Id);
    }
}
