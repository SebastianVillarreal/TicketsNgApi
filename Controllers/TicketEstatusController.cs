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

    public class TicketEstatusController: ControllerBase
    {
        private readonly TicketEstatusService _ticketEstatusService;
        private readonly ILogger<TicketEstatusController> _logger;
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

        public TicketEstatusController(TicketEstatusService ticketestatusservice, ILogger<TicketEstatusController> logger, IJwtAuthenticationService authService)
        {
            _ticketEstatusService = ticketestatusservice;
            _logger = logger;

            _authService = authService;
        }

        [HttpGet("GetTicketEstatus")]
        public IActionResult GetTicketEstatus([FromQuery] string FechaInicial, string FechaFinal)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargada con éxito";
                var resultado = _ticketEstatusService.GetTicketEstatus(FechaInicial, FechaFinal);
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