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
    public class FacturaDetallesController : ControllerBase
    {
        private IFacturaDetalleRepositorio _facturaDetalleRepositorio;
        private readonly ILogger<Factura_Detalle> _logger;
        public FacturaDetallesController(IFacturaDetalleRepositorio facturaDetalleRepositorio, ILogger<Factura_Detalle> logger)
        {
            _facturaDetalleRepositorio = facturaDetalleRepositorio;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Factura_Detalle>> Get()
        {
            var res = _facturaDetalleRepositorio.Listar();
            if (res == null)
            {
                return NotFound();
            }

            return res.ToList();
        }

        [HttpGet("{id}/Factura")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Factura_Detalle>> GetPorFactura(int id)
        {
            var res = _facturaDetalleRepositorio.Listar_x_Factura(id);
            if (res == null)
            {
                return NotFound();
            }

            return res;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Factura_Detalle> Post(Factura_Detalle facturaDetalle)
        {
            try
            {
                var nuevaFacturaDetalle = _facturaDetalleRepositorio.Insertar(facturaDetalle);
                if (nuevaFacturaDetalle == null)
                {
                    return BadRequest();
                }

                return CreatedAtAction(nameof(Post), new { idFactura = nuevaFacturaDetalle.IdFactura, idProducto = nuevaFacturaDetalle.IdProducto }, nuevaFacturaDetalle);
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
        public ActionResult<Factura_Detalle> Put(int id, [FromBody] Factura_Detalle facturaDetalle)
        {
            if (facturaDetalle == null)
                return NotFound();


            var resultado = _facturaDetalleRepositorio.Actualizar(facturaDetalle);



            return facturaDetalle;
        }


        [HttpDelete("{idFactura}/{idProducto}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete(int idFactura, int idProducto)
        {

            try
            {
                var resultado = _facturaDetalleRepositorio.Eliminar(idFactura,idProducto);
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
