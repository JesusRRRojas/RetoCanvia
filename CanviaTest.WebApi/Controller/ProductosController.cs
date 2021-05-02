using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CanviaTest.Data.Contratos;
using CanviaTest.Models;
using CanviaTest.WebApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CanviaTest.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private IProductoRepositorio _productoRepositorio;
        private readonly ILogger<ProductosController> _logger;

        public ProductosController(IProductoRepositorio productoRepositorio, ILogger<ProductosController> logger)
        {
            _productoRepositorio = productoRepositorio;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Paginador<Producto>> Get(int paginaActual =1, int registrosPorPagina=2)
        {
            try
            {
                var res = _productoRepositorio.ObtenerPaginas(paginaActual, registrosPorPagina);


                return new Paginador<Producto>(res.registros, res.totalRegistros, paginaActual, registrosPorPagina);
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
        public ActionResult<Producto> Post(Producto producto)
        {
            try
            {
                var nuevoProducto = _productoRepositorio.Insertar(producto);
                if (nuevoProducto == null)
                {
                    return BadRequest();
                }

                return CreatedAtAction(nameof(Post), new { id = nuevoProducto.IdProducto }, nuevoProducto);
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
        public ActionResult<Producto> Put(int id, [FromBody] Producto producto)
        {
            if (producto == null)
                return NotFound();


            var resultado = _productoRepositorio.Actualizar(producto);



            return producto;
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete(int id)
        {

            try
            {
                var resultado = _productoRepositorio.Eliminar(id);
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
