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
    public class BitacoraCambioTicketController: ControllerBase
    {
        private readonly BitacoraCambioTicketService _bitacoraService;
        private readonly ILogger<BitacoraCambioTicketController> _logger;
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

        public BitacoraCambioTicketController(BitacoraCambioTicketService bitacoraservice, ILogger<BitacoraCambioTicketController> logger, IJwtAuthenticationService authService) {
            _bitacoraService = bitacoraservice;
            _logger = logger;
       
            _authService = authService;
            
        }

        [HttpPost("InsertCambioTicket")]
        public IActionResult InsertCambioTicket([FromBody] CambioTicketModelInsert req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargada con éxito";
                _bitacoraService.InsertCambioTicket(req);

            }

            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetBitacoraCambiosTickets")]
        public IActionResult GetBitacoraCambiosTickets()
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargada con éxito";
                var resultado = _bitacoraService.GetAllBitacoraCambiosTickets();
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
        [HttpGet("GetCambioTicketById")]
        public IActionResult GetCambioTicketById([FromQuery] int Id)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargada con éxito";
                var resultado = _bitacoraService.GetCambioTicketById(Id);
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

        [HttpPut("UpdateCambioTicket")]
        public IActionResult UpdateCambioTicket([FromBody] CambioTicketModelUpdate req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargada con éxito";
                _bitacoraService.UpdateCambioTicket(req);

            }

            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteCambioTicket")]
        public IActionResult DeleteCambioTicket([FromQuery] int Id )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargada con éxito";
                _bitacoraService.DeleteCambioTicket(Id);

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