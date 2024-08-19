using System;
using Microsoft.AspNetCore.Mvc;
using reportesApi.Services;
using reportesApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using reportesApi.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using reportesApi.Helpers;


namespace reportesApi.Controllers
{
   
    [Route("api")]
    public class SistemasController: ControllerBase
    {
        private readonly SistemaService _sistemasService;
        private readonly ILogger<ContabilidadController> _logger;
  
        private readonly IJwtAuthenticationService _authService;
        


        Encrypt enc = new Encrypt();

        public SistemasController(SistemaService sistemaService, ILogger<ContabilidadController> logger, IJwtAuthenticationService authService) {
            _sistemasService = sistemaService;
            _logger = logger;
       
            _authService = authService;
            
        }

       // [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("InsertSistema")]
        public IActionResult InsertSistema([FromBody] SistemaModelInsert req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargada con éxito";
                _sistemasService.InsertSistema(req);

            }

            catch (System.Exception ex)
            {
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetSistemas")]
        public IActionResult GetGruposMaterias()
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargado con exito";


                // Llamando a la función y recibiendo los dos valores.
                
                 var resultado = _sistemasService.GetSistemas();
                 objectResponse.response = resultado;
            }

            catch (System.Exception ex)
            {
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpPut("UpdateSistema")]
        public IActionResult UpdateSistema([FromBody] SistemaModelUpdate req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargado con exito";
                _sistemasService.UpdateSistema(req);

            }

            catch (System.Exception ex)
            {
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteSistema/{id}")]
        public IActionResult DeleteSistema([FromRoute] int id )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargado con exito";

                _sistemasService.DeleteSistema(id);

            }

            catch (System.Exception ex)
            {
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }
    }
}