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
    public class TipoSistemaController: ControllerBase
    {
        private readonly TipoSistemaService _tipoService;
        private readonly ILogger<TipoSistemaController> _logger;
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

        public TipoSistemaController(TipoSistemaService tiposervice, ILogger<TipoSistemaController> logger, IJwtAuthenticationService authService)
        {
            _tipoService = tiposervice;
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("InsertTipoSistema")]
        public IActionResult InsertTipoSistema([FromBody] TipoSistemaModelInsert req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Tipo de sistema insertado correctamente";
                _tipoService.InsertTipoSistema(req);
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetAllTiposSistemas")]
        public IActionResult GetAllTiposSistemas()
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Tipos de sistemas cargados con éxito";
                var resultado = _tipoService.GetAllTiposSistemas();
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

        [HttpGet("GetTipoSistemaById")]
        public IActionResult GetTipoSistemaById([FromQuery] int id)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Tipo de sistema cargado con éxito";
                var resultado = _tipoService.GetTipoSistemaById(id);
                objectResponse.response = resultado;
            }
            catch(Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpPut("UpdateTipoSistema")]
        public IActionResult UpdateTipoSistema([FromBody] TipoSistemaModel req)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Tipo de sistema actualizado correctamente";
                _tipoService.UpdateTipoSistema(req);

            }
            catch(Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteTipoSistema")]
        public IActionResult DeleteTipoSistema([FromQuery] int Id)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Tipo de sistema eliminado correctamente";
                _tipoService.DeleteTipoSistema(Id);
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