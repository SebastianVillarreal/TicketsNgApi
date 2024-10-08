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
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Ticket-Usuario insertado correctamente";
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
                objectResponse.message = "Tickets-Usuarios cargados con éxito";
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

        [HttpGet("GetTicketUsuarioById")]
        public IActionResult GetTicketUsuarioId([FromQuery] int Id)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Ticket-Usuario cargado con éxito";
                var resultado = _ticketUsuarioService.GetTicketUsuarioById(Id);
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

         [HttpPut("UpdateTicketUsuario")]
        public IActionResult UpdateTicketUsuario([FromBody] TicketUsuarioModelUpdate req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Ticket-Usuario actualizado correctamente";
                _ticketUsuarioService.UpdateTicketUsuario(req);

            }

            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteTicketUsuario")]
        public IActionResult DeleteSistema([FromQuery] int Id )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Ticket-Usuario eliminado correctamente";
                _ticketUsuarioService.DeleteTicketUsuario(Id);

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