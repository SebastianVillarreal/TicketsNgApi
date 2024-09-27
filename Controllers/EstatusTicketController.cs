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
    public class EstatusTicketController: ControllerBase
    {
        private readonly EstatusTicketService _estatusService;
        private readonly ILogger<EstatusTicketController> _logger;
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        
        Encrypt enc = new Encrypt();

        public EstatusTicketController(EstatusTicketService estatusservice, ILogger<EstatusTicketController> logger, IJwtAuthenticationService authService) {
            _estatusService = estatusservice;
            _logger = logger;
       
            _authService = authService;
            
        }

        [HttpPost("InsertEstatus")]
        public IActionResult InsertEstatus([FromBody] EstatusTicketModelInsert req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "data cargada con éxito";
                _estatusService.InsertEstatus(req);

            }

            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetAllEstatus")]
        public IActionResult GetAllEstatus()
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargada con éxito";
                var resultado = _estatusService.GetAllEstatus();
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

        [HttpGet("GetEstatusById")]
        public IActionResult GetEstatusById([FromQuery] int Id)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargada con éxito";
                var resultado = _estatusService.GetEstatusById(Id);
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

         [HttpPut("UpdateEstatus")]
        public IActionResult UpdateEstatus([FromBody] EstatusTicketModel req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Estatus Actualizado Correctamente";
                _estatusService.UpdateEstatus(req);

            }

            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteEstatus")]
        public IActionResult DeleteEstatus([FromQuery] int Id )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "data cargada con éxito";
                _estatusService.DeleteEstatus(Id);

            }

            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetAllEstatusDetail")]
        public IActionResult GetAllEstatusDetil()
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                // Obtener el token del encabezado Authorization
                var authHeader = Request.Headers["Authorization"].ToString();
                var token = authHeader.Substring("Bearer ".Length).Trim();
                var userId = _authService.GetUserIdFromToken(token);
                Console.WriteLine("Kevin " +userId);
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargada con éxito";
                var resultado = _estatusService.GetAllEstatusDetail(userId);
                objectResponse.response = resultado;
            }
            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.ToString();
            }

            return new JsonResult(objectResponse);
        }
    }

}