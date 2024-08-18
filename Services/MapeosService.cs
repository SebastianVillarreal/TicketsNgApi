using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using reportesApi.DataContext;
using reportesApi.Models;
using System.Collections.Generic;
using reportesApi.Models.Compras;
using OfficeOpenXml;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
namespace reportesApi.Services
{
    public class MapeosService
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        
        public MapeosService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;
             _webHostEnvironment = webHostEnvironment;
             
        }

        public string GetRutaReportes()
        {
            return @"C:\Users\User\Documents\GitHub\ApiReportes\templates";
            
        }

        public  (MemoryStream, string )ReporteDiferenciasMapeo(string Fecha, int sucursal)
        {
            var reporte = this.GetDiferenciasMapeos(Fecha, sucursal);
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            string webRootPath = _webHostEnvironment.ContentRootPath;
            //var fullPath = Server.MapPath(pathFileExcel + nameFileExcel);
            var fullPath = Path.Combine(webRootPath, "templates/DiferenciasMapeo.xlsx");

            try
            {
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                //FileInfo template = new FileInfo(Server.MapPath("~/templates/ReporteEstadoCuentaProveedorTemplate.xlsx"));
                FileInfo template = new FileInfo(Path.Combine(webRootPath, "templates/template.xlsx"));
                FileInfo newFile = new FileInfo(fullPath);
                int dias = 13;
                using (ExcelPackage excelPackage = new ExcelPackage(newFile, template))
                {
                    ExcelWorkbook myWorkbook = excelPackage.Workbook;
                    ExcelWorksheet myWorksheet = myWorkbook.Worksheets[0];
                    int i = 3;

                    myWorksheet.Cells["A1"].Value = "F. Inicial";
                    myWorksheet.Cells["B1" ].Value = Fecha;

                    myWorksheet.Cells["A2"].Value = "Articulo";
                    myWorksheet.Cells["B2" ].Value = "Descripcion";
                    myWorksheet.Cells["C2" ].Value = "Cantidad";
                    myWorksheet.Cells["D2" ].Value = "Existencia";
                    myWorksheet.Cells["E2" ].Value = "Diferencia";
                    myWorksheet.Cells["F2" ].Value = "Ultimo Costo";
                    myWorksheet.Cells["G2" ].Value = "Precio Publico";
                    myWorksheet.Cells["H2"].Value = "Diferencia $ Costo";
                    myWorksheet.Cells["I2" ].Value = "Diferencia $ PP";

                    foreach (var registro in reporte)
                    {

                        
                        myWorksheet.Cells["A" + i].Value = registro.Codigo;
                        myWorksheet.Cells["B" + i].Value = registro.Descripcion;
                        myWorksheet.Cells["C" + i].Value = registro.Cantidad;
                        myWorksheet.Cells["D" + i].Value = registro.Existencia;
                        myWorksheet.Cells["E" + i].Value = registro.Diferencia;
                        myWorksheet.Cells["F" + i].Value =registro.UltimoCosto;
                        myWorksheet.Cells["G" + i].Value = registro.PrecioPublico;
                        myWorksheet.Cells["H" + i].Value = registro.DifCosto;
                        myWorksheet.Cells["I" + i].Value = registro.DifPrecio;
                       
                        i++;
                    }

                    excelPackage.Save();
                }

                var fullPathDownload = fullPath;

                var memory = new MemoryStream();
                using (var stream = new FileStream(fullPathDownload, FileMode.Open))
                {
                    stream.CopyTo(memory);
                }
                memory.Position = 0;

                // Configura el tipo de contenido correcto según el tipo de archivo.
                // Para archivos Excel, puedes usar "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileExt = Path.GetExtension("ReporteDias.xlsx").ToLowerInvariant();
                if (fileExt == ".xlsx")
                {
                    contentType = "application/vnd.ms-excel";
                }
                return (memory, fullPathDownload);
                //return File(memory, contentType, Path.GetFileName(filePath));
                //return  ruta + "/" +   nameFileExcel;;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<DiferenciaInventariosModel> GetDiferenciasMapeos(string fecha, int sucursal)
        {
            
            List<DiferenciaInventariosModel> lista = new List<DiferenciaInventariosModel>();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            ArrayList parametros = new ArrayList();
            try
            {
                parametros.Add(new SqlParameter { ParameterName = "@pFecha", SqlDbType = SqlDbType.VarChar, Value = fecha });
                parametros.Add(new SqlParameter { ParameterName = "@pIdSucursal", SqlDbType = SqlDbType.VarChar, Value = sucursal });
                DataSet ds = dac.Fill("RPT_DiferenciasInventario", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        lista.Add(new DiferenciaInventariosModel{
                            Codigo  = row["Articulo"].ToString(),
                            Descripcion  = row["Descripcion"].ToString(),
                            Cantidad = decimal.Parse(row["Cantidad"].ToString()),
                            Existencia = decimal.Parse(row["Existencia"].ToString()),
                            Diferencia = decimal.Parse(row["Cantidad"].ToString()) -  decimal.Parse(row["Existencia"].ToString()),
                            UltimoCosto = decimal.Parse(row["UP"].ToString()),
                            PrecioPublico = decimal.Parse(row["PrecioVenta"].ToString()),
                            DifCosto = (decimal.Parse(row["Cantidad"].ToString()) -  decimal.Parse(row["Existencia"].ToString())) *  decimal.Parse(row["UP"].ToString()),
                            DifPrecio = (decimal.Parse(row["Cantidad"].ToString()) -  decimal.Parse(row["Existencia"].ToString())) * decimal.Parse(row["PrecioVenta"].ToString()),
                        });
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        

    }
}