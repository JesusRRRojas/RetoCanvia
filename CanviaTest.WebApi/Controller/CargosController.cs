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
    public class CargosController : ControllerBase
    {
        private ICargoRepositorio _cargoRepositorio;
        private readonly ILogger<CargosController> _logger;

        public CargosController(ICargoRepositorio cargoRepositorio, ILogger<CargosController> logger)
        {
            _cargoRepositorio = cargoRepositorio;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Cargo>> Get()
        {
            var res = _cargoRepositorio.Listar();
            if (res == null)
            {
                return NotFound();
            }

            return res.ToList();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public  ActionResult<Cargo> Post(Cargo cargo)
        {
            try
            {
                var nuevoCargo = _cargoRepositorio.Insertar(cargo);
                if (nuevoCargo == null)
                {
                    return BadRequest();
                }

                return CreatedAtAction(nameof(Post), new { id = nuevoCargo.IdCargo }, nuevoCargo);
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
        public ActionResult<Cargo> Put(int id, [FromBody] Cargo cargo)
        {
            if (cargo == null)
                return NotFound();


            var resultado =  _cargoRepositorio.Actualizar(cargo);

           

            return cargo;
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete(int id)
        {

            try
            {
                var resultado = _cargoRepositorio.Eliminar(id);
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
