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
    public class UsuariosController: ControllerBase
    {
        private readonly UsuariosService _usuariosService;
        private readonly ILogger<UsuariosController> _logger;
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        Encrypt enc = new Encrypt();

        public UsuariosController(UsuariosService usuariosservice, ILogger<UsuariosController> logger, IJwtAuthenticationService authService)
        {
            _usuariosService = usuariosservice;
            _logger = logger;
            _authService = authService;
        }

        [HttpGet("GetUsuarios")]
        public IActionResult GetUsuarios()
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Usuarios cargados con éxito";
                var resultado = _usuariosService.GetAllUsuarios();
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