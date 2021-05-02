using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CanviaTest.Data.Contratos;
using CanviaTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CanviaTest.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private IClienteRepositorio _clienteRepositorio;
        private IFacturaRepositorio _facturaRepositorio;

        public ClientesController(IClienteRepositorio clienteRepositorio, IFacturaRepositorio facturaRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
            _facturaRepositorio = facturaRepositorio;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            var res = _clienteRepositorio.Listar();
            if (res == null)
            {
                return NotFound();
            }

            return res.ToList();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Cliente> Get(int id)
        {
            var res = _clienteRepositorio.ListarCliente(id);
            if (res == null)
            {
                return NotFound();
            }

            return res;
        }

        [HttpGet("{id}/Facturas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Cliente> GetConFacturas(int id)
        {
            var res = _clienteRepositorio.ListarCliente(id);
            res.Facturas = _facturaRepositorio.Facturas_x_Cliente(id);
            if (res == null)
            {
                return NotFound();
            }

            return res;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Cliente> Post(Cliente cliente)
        {
            try
            {
                var neuvoCliente = _clienteRepositorio.Insertar(cliente);
                if (neuvoCliente == null)
                {
                    return BadRequest();
                }

                return CreatedAtAction(nameof(Post), new { id = neuvoCliente.IdCliente }, neuvoCliente);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Error en {nameof(Post)}: {ex.Message}");
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Cliente> Put(int id, [FromBody] Cliente cliente)
        {
            if (cliente == null)
                return NotFound();


            var resultado = _clienteRepositorio.Actualizar(cliente);



            return cliente;
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete(int id)
        {

            try
            {
                var resultado = _clienteRepositorio.Eliminar(id);
                if (!resultado)
                {
                    return BadRequest();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Error en {nameof(Delete)}: {ex.Message}");
                return BadRequest();
            }
        }



    }
}
