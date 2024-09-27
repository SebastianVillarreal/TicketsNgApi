using System;
using Microsoft.AspNetCore.Mvc;
using reportesApi.Services;
using reportesApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using reportesApi.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using reportesApi.Helpers;
using Microsoft.AspNetCore.Hosting;


namespace reportesApi.Controllers
{
   
    [Route("api")]
    public class SistemaController: ControllerBase
    {
        private readonly SistemaService _sistemasService;
        private readonly ILogger<SistemaController> _logger;
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        
        Encrypt enc = new Encrypt();

        public SistemaController(SistemaService sistemaservice, ILogger<SistemaController> logger, IJwtAuthenticationService authService) {
            _sistemasService = sistemaservice;
            _logger = logger;
       
            _authService = authService;
            
        }

        [HttpPost("InsertSistema")]
        public IActionResult InsertSistema([FromBody] SistemaModelInsert req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Sistema insertado correctamente";
                _sistemasService.InsertSistema(req);

            }

            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetAllSistemas")]
        public IActionResult GetAllSistemas()
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Sistemas cargados con éxito";
                var resultado = _sistemasService.GetAllSistemas();
                objectResponse.response = resultado;
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetSistemaById")]
        public IActionResult GetSistemasById([FromQuery] int Id)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Sistema cargado con éxito";
                var resultado = _sistemasService.GetSistemaById(Id);
                objectResponse.response = resultado;
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpPut("UpdateSistema")]
        public IActionResult UpdateSistema([FromBody] SisteamModelUpdate req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Sistema actualizado correctamente";
                _sistemasService.UpdateSistema(req);

            }

            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteSistema")]
        public IActionResult DeleteSistema([FromQuery] int Id )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Sistema eliminado correctamente";
                _sistemasService.DeleteSistema(Id);

            }

            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }
    }
}