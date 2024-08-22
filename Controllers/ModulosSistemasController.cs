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
    public class ModuloSistemaController: ControllerBase
    {
        private readonly ModuloSistemaService _modulosService;
        private readonly ILogger<ModuloSistemaController> _logger;
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        
        Encrypt enc = new Encrypt();

        public ModuloSistemaController(ModuloSistemaService moduloservice, ILogger<ModuloSistemaController> logger, IJwtAuthenticationService authService) {
            _modulosService = moduloservice;
            _logger = logger;
       
            _authService = authService;
            
        }

        [HttpPost("InsertModulo")]
        public IActionResult InsertModulo([FromBody] ModuloSistemaModelInsert req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargada con éxito";
                _modulosService.InsertModulo(req);

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