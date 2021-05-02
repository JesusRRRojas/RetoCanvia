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
    public class EmpleadosController : ControllerBase
    {
        private IEmpleadoRepositorio _empleadoRepositorio;
        private readonly ILogger<Empleado> _logger;

        public EmpleadosController(IEmpleadoRepositorio empleadoRepositorio, ILogger<Empleado> logger)
        {
            _empleadoRepositorio = empleadoRepositorio;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Empleado>> Get()
        {
            var res = _empleadoRepositorio.Listar();
            if (res == null)
            {
                return NotFound();
            }

            return res.ToList();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Empleado> Post(Empleado empleado)
        {
            try
            {
                var nuevoEmpleado = _empleadoRepositorio.Insertar(empleado);
                if (nuevoEmpleado == null)
                {
                    return BadRequest();
                }

                return CreatedAtAction(nameof(Post), new { id = nuevoEmpleado.IdEmpleado }, nuevoEmpleado);
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
        public ActionResult<Empleado> Put(int id, [FromBody] Empleado empleado)
        {
            if (empleado == null)
                return NotFound();


            var resultado = _empleadoRepositorio.Actualizar(empleado);



            return empleado;
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete(int id)
        {

            try
            {
                var resultado = _empleadoRepositorio.Eliminar(id);
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
