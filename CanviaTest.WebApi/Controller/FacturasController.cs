using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CanviaTest.Data.Contratos;
using CanviaTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CanviaTest.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private IFacturaRepositorio _facturaRepositorio;
        private readonly ILogger<FacturasController> _logger;

        public FacturasController(IFacturaRepositorio facturaRepositorio, ILogger<FacturasController> logger)
        {
            _facturaRepositorio = facturaRepositorio;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Factura>> Get()
        {
            try
            {
                var res = _facturaRepositorio.Listar();
                if (res == null)
                {
                    return NotFound();
                }

                return res.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Get)}: {ex.Message}");
                return BadRequest();
            }
            
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Factura> Post(Factura factura)
        {
            try
            {
                var nuevaFactura = _facturaRepositorio.Insertar(factura);
                if (nuevaFactura == null)
                {
                    return BadRequest();
                }

                return CreatedAtAction(nameof(Post), new { id = nuevaFactura.IdFactura }, nuevaFactura);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Post)}: {ex.Message}");
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Factura> Put(int id, [FromBody] Factura factura)
        {
            if (factura == null)
                return NotFound();


            var resultado = _facturaRepositorio.Actualizar(factura);



            return factura;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete(int id)
        {

            try
            {
                var resultado = _facturaRepositorio.Eliminar(id);
                if (!resultado)
                {
                    return BadRequest();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Delete)}: {ex.Message}");
                return BadRequest();
            }
        }

    }
}
