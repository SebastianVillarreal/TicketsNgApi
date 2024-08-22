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
    public class TicketUsuarioController: ControllerBase
    {
        private readonly TicketUsuarioService _ticketUsuarioService;
        private readonly ILogger<TicketUsuarioController> _logger;
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

        public TicketUsuarioController(TicketUsuarioService ticketusuarioservice, ILogger<TicketUsuarioController> logger, IJwtAuthenticationService authService) {
            _ticketUsuarioService = ticketusuarioservice;
            _logger = logger;
       
            _authService = authService;
            
        }

        [HttpPost("InsertTicketUsuario")]
        public IActionResult InsertTicketUsuario([FromBody] TicketUsuarioModelInsert req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargada con éxito";
                _ticketUsuarioService.InsertTicketUsuario(req);

            }

            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetAllTicketsUsuarios")]
        public IActionResult GetAllTicketsUsuarios()
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargada con éxito";
                var resultado = _ticketUsuarioService.GetAllTicketsUsuarios();
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
    }
}