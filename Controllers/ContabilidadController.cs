using System;
using Microsoft.AspNetCore.Mvc;
using reportesApi.Services;
using reportesApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using reportesApi.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using reportesApi.Helpers;


namespace reportesApi.Controllers
{
   
    [Route("api")]
    public class ContabilidadController: ControllerBase
    {
        private readonly ContabilidadService _articulosService;
        private readonly ILogger<ContabilidadController> _logger;
  
        private readonly IJwtAuthenticationService _authService;
        


        Encrypt enc = new Encrypt();

        public ContabilidadController(ContabilidadService articulosservice, ILogger<ContabilidadController> logger, IJwtAuthenticationService authService) {
            _articulosService = articulosservice;
            _logger = logger;
       
            _authService = authService;
            
        }

       // [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("TipoTipo")]
        public JsonResult GetDatosArticulo([FromQuery] string FechaInicial, string FechaFinal, int IdSucursal)
        {
            FiltrosFechaModel filtro = new FiltrosFechaModel {
                FechaInicial = FechaInicial,
                FechaFinal = FechaFinal,
                IdSucursal = IdSucursal
            };
            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _articulosService.ReporteTipo(FechaInicial, IdSucursal.ToString(), FechaFinal);
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargado con exito";

                objectResponse.response = new
                {
                    data =  articulo
                };
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }


            return new JsonResult(objectResponse);

        }

        [HttpGet("ResumenEstadistico")]
        public JsonResult ReportesResumenEstadistico([FromQuery] string FechaInicial, string FechaFinal, int IdSucursal)
        {
            FiltrosFechaModel filtro = new FiltrosFechaModel {
                FechaInicial = FechaInicial,
                FechaFinal = FechaFinal,
                IdSucursal = IdSucursal
            };
            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _articulosService.ReportesResumenEstadistico(filtro);
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargado con exito";

                objectResponse.response = new
                {
                    data =  articulo
                };
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }


            return new JsonResult(objectResponse);

        }

        [HttpGet("EstadoCuentaProveedor")]
        public JsonResult EstadoCuentaProveedor([FromQuery] int proveedor , string fecha)
        {

            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _articulosService.ReporteEstadoCuentaProveedor(proveedor, fecha);
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargado con exito";

                objectResponse.response = new
                {
                    data =  articulo
                };
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }


            return new JsonResult(objectResponse);

        }

        [HttpGet("VentasInsecticidas")]
        public JsonResult VentasInsecticidas([FromQuery] int id_sucursal , string fecha_inicial, string fecha_final)
        {

            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _articulosService.ReporteVentasInsecticidas(fecha_inicial, fecha_final, id_sucursal);
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargado con exito";

                objectResponse.response = new
                {
                    data =  articulo
                };
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }


            return new JsonResult(objectResponse);

        }

    }
}