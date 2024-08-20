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
        public IActionResult InsertGrupoMateria([FromBody] SistemaModelInsert req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargada con éxito";
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

    }
}