using EnviosWebApi.Models;
using EnviosWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnviosWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnviosController : ControllerBase
    {
        private readonly IEnviosService _enviosService;
        public EnviosController (IEnviosService enviosService)
        {
            _enviosService = enviosService;
        }

        [HttpGet("/envios", Name= "Obtener todos los envios")]
        public IActionResult GetAll()
        {
            try
            {
                List<TEnvio> envios = _enviosService.GetEnvios();
                if(envios.Count > 0 )
                {
                    return Ok(envios);
                }
                return StatusCode(404, "No se encontraron envios");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error de servidor: {ex.Message}");
            }
        }

        [HttpGet("/envios/enviosByDate", Name = "Obtener por fecha")]
        public IActionResult GetByDate([FromQuery] DateTime date)
        {
            if(date != null)
            {
                try
                {
                        List<TEnvio> envios = _enviosService.GetEnviosByDate(date);
                        if(envios.Count < 1)
                        {
                            return StatusCode(404, "No se encontraron envios");
                        }
                        return Ok(envios);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error de servidor: {ex.Message}");
                }
            }
            return StatusCode(422, $"Ingrese un valor para la fecha a filtrar");
        }

        [HttpGet("/envios/enviosByState", Name = "Obtener por estado Cancelado ")]
        public IActionResult GetByState([FromQuery] string state)
        {
            if (state != null)
            {
                if(state == "Cancelado")
                {
                    try
                    {
                        List<TEnvio> envios = _enviosService.GetCancelledEnvios(state);
                        if (envios.Count < 1)
                        {
                            return StatusCode(404, "No se encontraron envios");
                        }
                        return Ok(envios);
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Error de servidor: {ex.Message}");
                    }

                }
                return StatusCode(422, "Revise que el parametro que esta pasando sea Cancelado");
            }
            return StatusCode(422, $"Ingrese un estado para filtrar");
        }

        [HttpPost("/envios/newEnvio", Name = "Cargar Envio")]
        public IActionResult NewEnvio([FromBody]TEnvio envio)
        {
            if (envio == null)
            {
                return StatusCode(422, "Ingrese los datos del envio a cargar");
            }
            if (envio.FechaEnvio == null
                || string.IsNullOrEmpty(envio.Direccion) 
                || string.IsNullOrEmpty(envio.Estado)
                || string.IsNullOrEmpty(envio.DniCliente)
                || envio.IdEmpresa == null)
            {
                return StatusCode(422, $"Todos los campos son obligatorios");
            }
            else
            {
                try
                {
                    _enviosService.NewEnvio(envio);
                        
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error de servidor: {ex.Message}");
                }

            }
            return StatusCode(201, "Envio registrado con exito");
        }

        [HttpPut("/envios/Delete", Name = "Borrado Logico")]
        public IActionResult DeleteEnvio([FromQuery]int codigo)
        {
            TEnvio spottedEnvio = _enviosService.GetEnvioByCodigo(codigo);
            if(spottedEnvio != null)
            {
                if(spottedEnvio.Estado != "Cancelado")
                {
                    try
                    {
                        spottedEnvio.Estado = "Cancelado";
                        _enviosService.DeleteEnvio(spottedEnvio);
                        return StatusCode(200, $"El envio con codigo : {spottedEnvio.Codigo} fue cancelado correctamente");
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Error de servidor: {ex.Message}");
                    }
                }
                return StatusCode(409, "El envio ya se encuentra cancelado");
            }
            return StatusCode(404, $"El codigo: {codigo} no se encuentra en nuestros registros ");

        }

    }
}
