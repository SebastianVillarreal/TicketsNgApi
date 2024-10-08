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
    public class UsuarioSistemaController: ControllerBase
    {
        private readonly UsuarioSistemaService _usuarioSistemasService;
        private readonly ILogger<UsuarioSistemaController> _logger;
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        
        Encrypt enc = new Encrypt();

        public UsuarioSistemaController(UsuarioSistemaService usuariosistemaservice, ILogger<UsuarioSistemaController> logger, IJwtAuthenticationService authService) {
            _usuarioSistemasService = usuariosistemaservice;
            _logger = logger;
       
            _authService = authService;
            
        }

        [HttpPost("InsertUsuarioSistema")]
        public IActionResult InsertUsuarioSistema([FromBody] UsuarioSistemaModelInsert req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Relación usuario-sistema insertada correctamente";
                _usuarioSistemasService.InsertUsuarioSistema(req);

            }

            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetAllUsuariosSistemas")]
        public IActionResult GetAllUsuariosSistemas()
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "Usuarios-sistemas cargados con éxito";
                var resultado = _usuarioSistemasService.GetAllUsuariosSistemas();
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


        [HttpPut("UpdateUsuarioSistema")]
        public IActionResult UpdateUsuarioSistema([FromBody] UsuarioSistemaModelUpdate req )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Relación usuario-sistema actualizada correctamente";
                _usuarioSistemasService.UpdateUsuarioSistema(req);

            }

            catch (Exception ex)
            {
                objectResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                objectResponse.success = false;
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteUsuarioSistema")]
        public IActionResult DeleteUsuarioSistema([FromQuery] int Id )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.Created;
                objectResponse.success = true;
                objectResponse.message = "Relación usuario-sistema eliminada correctamente";
                _usuarioSistemasService.DeleteUsuarioSistema(Id);

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