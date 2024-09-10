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
    public class TipoTicketController: ControllerBase
    {
        private readonly TipoTicketService _tipoService;
        private readonly ILogger<TipoTicketController> _logger;
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

        public TipoTicketController(TipoTicketService tiposervice, ILogger<TipoTicketController> logger, IJwtAuthenticationService authService) {
            _tipoService = tiposervice;
            _logger = logger;
       
            _authService = authService;
            
        } 

        [HttpPost("InsertTipo")]
        public IActionResult InsertTipo([FromBody] TipoTicketModelInsert req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargada con éxito";
                _tipoService.InsertTipo(req);

            }

            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        } 

        [HttpGet("GetAllTipos")]
        public IActionResult GetAllTipos()
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargada con éxito";
                var resultado = _tipoService.GetAllTipos();
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

        [HttpGet("GetTipoById")]
        public IActionResult GetTipoById([FromQuery] int Id)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargada con éxito";
                var resultado = _tipoService.GetTipoById(Id);
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

        [HttpPut("UpdateTipo")]
        public IActionResult UpdateTipo([FromBody] TipoTicketModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Tipo Actualizado Correctamente";
                _tipoService.UpdateTipo(req);

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