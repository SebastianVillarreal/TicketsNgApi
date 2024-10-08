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
    public class TicketController: ControllerBase
    {
        private readonly TicketService _ticketService;
        private readonly ILogger<TicketController> _logger;
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        
        Encrypt enc = new Encrypt();

        public TicketController(TicketService ticketservice, ILogger<TicketController> logger, IJwtAuthenticationService authService) {
            _ticketService = ticketservice;
            _logger = logger;
       
            _authService = authService;
            
        }

        [HttpPost("InsertTicket")]
        public IActionResult InsertTicket([FromForm] TicketModelInsert req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Ticket insertado correctamente";
                _ticketService.InsertTicket(req);

            }

            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetAllTickets")]
        public IActionResult GetAllTickets()
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Tickets cargados con éxito";
                var resultado = _ticketService.GetAllTickets();
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

        [HttpGet("GetTicketById")]
        public IActionResult GetTicketById([FromQuery] int Id)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Ticket cargado con éxito";
                var resultado = _ticketService.GetTicketById(Id);
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

        [HttpPut("UpdateTicket")]
        public IActionResult UpdateTicket([FromBody] TicketModelUpdate req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Ticket actualizado correctamente";
                _ticketService.UpdateTicket(req);
            }

            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Conflict;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }
            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteTicket")]
        public IActionResult DeleteTicket([FromQuery] int Id )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Ticket eliminado correctamente";
                _ticketService.DeleteTicket(Id);

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