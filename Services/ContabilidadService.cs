using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using reportesApi.DataContext;
using reportesApi.Models;
using reportesApi.Models;
using System.Collections.Generic;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace reportesApi.Services
{
    public class ContabilidadService
    {
        private  string connection;
        private string rutaReportes;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ContabilidadService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;
              _webHostEnvironment = webHostEnvironment;

        }


        public string ReporteTipo(string Fecha, string IdSucursal, string FechaFinal)
        {
            string pathFileExcel = "~/templates/";
            string nameFileExcel = "ReporteTipo.xlsx";
            var ruta = GetRutaReportes();
            try
            {
                FiltrosFechaModel filtro = new FiltrosFechaModel
                {
                    FechaInicial = Fecha,
                    FechaFinal = FechaFinal,
                    IdSucursal = int.Parse( IdSucursal)
                };
                var ListaTipo = this.RPT_TipoTipo(filtro);
                string webRootPath = _webHostEnvironment.ContentRootPath;

                ExcelPackage.LicenseContext = LicenseContext.Commercial;

                
                //var fullPath = Server.MapPath(pathFileExcel + nameFileExcel);
                var fullPath = Path.Combine(webRootPath, "templates/ReporteTipo.xlsx");
                ExcelPackage.LicenseContext = LicenseContext.Commercial;

            
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                //FileInfo template = new FileInfo(Server.MapPath("~/templates/ReporteTipoTemplate.xlsx"));
                //FileInfo newFile = new FileInfo(fullPath);

                FileInfo template = new FileInfo(Path.Combine(webRootPath, "templates/template.xlsx"));
                FileInfo newFile = new FileInfo(fullPath);

                using (ExcelPackage excelPackage = new ExcelPackage(newFile, template))
                {
                    ExcelWorkbook myWorkbook = excelPackage.Workbook;
                    ExcelWorksheet myWorksheet = myWorkbook.Worksheets[0];

                    myWorksheet.Cells["A2"].Value = "Fecha:";
                    myWorksheet.Cells["A2"].Style.Font.Bold = true;
                    myWorksheet.Cells["A2"].Style.Font.Size = 12;
                    myWorksheet.Cells["A2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    myWorksheet.Cells["A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    myWorksheet.Cells["B2"].Value = Fecha;

                    myWorksheet.Cells["A3"].Value = "Hora:";
                    myWorksheet.Cells["A3"].Style.Font.Bold = true;
                    myWorksheet.Cells["A3"].Style.Font.Size = 12;
                    myWorksheet.Cells["A3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    myWorksheet.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    myWorksheet.Cells["B3"].Value = DateTime.Now.ToLongTimeString();

                    myWorksheet.Cells["C2:K3"].Merge = true;
                    myWorksheet.Cells["C2:K3"].Value = "Supertienda Rico S.A de C.V.";
                    myWorksheet.Cells["C2:K3"].Style.Font.Bold = true;
                    myWorksheet.Cells["C2:K3"].Style.Font.Size = 12;
                    myWorksheet.Cells["C2:K3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    myWorksheet.Cells["C2:K3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    myWorksheet.Cells["A4:K5"].Merge = true;
                    myWorksheet.Cells["A4:K5"].Value = "Estadistica de Ventas por Departamento";
                    myWorksheet.Cells["A4:K5"].Style.Font.Bold = true;
                    myWorksheet.Cells["A4:K5"].Style.Font.Size = 12;
                    myWorksheet.Cells["A4:K5"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    myWorksheet.Cells["A4:K5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    myWorksheet.Cells["A6"].Value = "Departamento";
                    myWorksheet.Cells["B6"].Value = "Venta Global";
                    myWorksheet.Cells["C6"].Value = "Desc./Ventas";
                    myWorksheet.Cells["D6"].Value = "Venta Bruta c/Iva IEPS";
                    myWorksheet.Cells["E6"].Value = "Dev./Venta";
                    myWorksheet.Cells["F6"].Value = "Dev. Credito /Venta";
                    myWorksheet.Cells["G6"].Value = "Notas Cred.";
                    myWorksheet.Cells["H6"].Value = "Venta Neta";
                    myWorksheet.Cells["I6"].Value = "Iva";
                    myWorksheet.Cells["J6"].Value = "IEPS";
                    myWorksheet.Cells["K6"].Value = "Venta Total";
                    myWorksheet.Cells["A6:K6"].Style.Font.Bold = true;
                    myWorksheet.Cells["A6:K6"].Style.Font.Size = 12;
                    myWorksheet.Cells["A6:K6"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    myWorksheet.Cells["A6:K6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    int i = 7;

                    foreach (var item in ListaTipo)
                    {
                        myWorksheet.Cells["A" + i].Value = item.Departamento;
                        myWorksheet.Cells["B" + i].Style.Numberformat.Format = "$###,###,##0.00";
                        myWorksheet.Cells["B" + i].Value = item.VentaGlobal;
                        myWorksheet.Cells["C" + i].Style.Numberformat.Format = "$###,###,##0.00";
                        myWorksheet.Cells["C" + i].Formula = "=B" + i + "-D" + i;
                        myWorksheet.Cells["D" + i].Style.Numberformat.Format = "$###,###,##0.00";
                        myWorksheet.Cells["D" + i].Value = item.VentaBruta;
                        myWorksheet.Cells["E" + i].Style.Numberformat.Format = "$###,###,##0.00";
                        myWorksheet.Cells["E" + i].Value = item.DevVenta;
                        myWorksheet.Cells["F" + i].Style.Numberformat.Format = "$###,###,##0.00";
                        myWorksheet.Cells["F" + i].Value = item.DevCredito;
                        myWorksheet.Cells["G" + i].Style.Numberformat.Format = "$###,###,##0.00";
                        myWorksheet.Cells["G" + i].Value = item.NotasCred;
                        myWorksheet.Cells["H" + i].Style.Numberformat.Format = "$###,###,##0.00";
                        myWorksheet.Cells["H" + i].Value = item.VentaTotal;
                        myWorksheet.Cells["I" + i].Style.Numberformat.Format = "$###,###,##0.00";
                        myWorksheet.Cells["I" + i].Value = item.Iva;
                        myWorksheet.Cells["J" + i].Style.Numberformat.Format = "$###,###,##0.00";
                        myWorksheet.Cells["J" + i].Value = item.Ieps;
                        myWorksheet.Cells["K" + i].Style.Numberformat.Format = "$###,###,##0.00";
                        myWorksheet.Cells["K" + i].Value = item.VentaNeta;
                        myWorksheet.Cells["A" + i + ":K" + i].Style.Font.Bold = false;

                        myWorksheet.Cells["A2:K" + i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        myWorksheet.Cells["A2:K" + i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        myWorksheet.Cells["A2:K" + i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        myWorksheet.Cells["A2:K" + i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        myWorksheet.Cells["A2:K" + i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        myWorksheet.Cells["A2:K" + i].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                        i++;
                    }
                    int l = i - 1;
                    i = i + 1;
                    myWorksheet.Cells["A" + i].Value = "Total:";
                    myWorksheet.Cells["B" + i].Style.Numberformat.Format = "$###,###,##0.00";
                    myWorksheet.Cells["B" + i].Formula = "=SUM(B7:B" + l + ")";
                    myWorksheet.Cells["C" + i].Style.Numberformat.Format = "$###,###,##0.00";
                    myWorksheet.Cells["C" + i].Formula = "=SUM(C7:C" + l + ")";
                    myWorksheet.Cells["D" + i].Style.Numberformat.Format = "$###,###,##0.00";
                    myWorksheet.Cells["D" + i].Formula = "=SUM(D7:D" + l + ")";
                    myWorksheet.Cells["E" + i].Style.Numberformat.Format = "$###,###,##0.00";
                    myWorksheet.Cells["E" + i].Formula = "=SUM(E7:E" + l + ")";
                    myWorksheet.Cells["F" + i].Style.Numberformat.Format = "$###,###,##0.00";
                    myWorksheet.Cells["F" + i].Formula = "=SUM(F7:F" + l + ")";
                    myWorksheet.Cells["G" + i].Style.Numberformat.Format = "$###,###,##0.00";
                    myWorksheet.Cells["G" + i].Formula = "=SUM(G7:G" + l + ")";
                    myWorksheet.Cells["H" + i].Style.Numberformat.Format = "$###,###,##0.00";
                    myWorksheet.Cells["H" + i].Formula = "=SUM(H7:H" + l + ")";
                    myWorksheet.Cells["I" + i].Style.Numberformat.Format = "$###,###,##0.00";
                    myWorksheet.Cells["I" + i].Formula = "=SUM(I7:I" + l + ")";
                    myWorksheet.Cells["J" + i].Style.Numberformat.Format = "$###,###,##0.00";
                    myWorksheet.Cells["J" + i].Formula = "=SUM(J7:J" + l + ")";
                    myWorksheet.Cells["K" + i].Style.Numberformat.Format = "$###,###,##0.00";
                    myWorksheet.Cells["K" + i].Formula = "=SUM(K7:K" + l + ")";

                    myWorksheet.Cells["A" + i + ":K" + i].Style.Font.Bold = true;
                    myWorksheet.Cells["A" + i + ":K" + i].Style.Font.Size = 12;
                    myWorksheet.Cells["A" + i + ":K" + i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    myWorksheet.Cells["A" + i + ":K" + i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    excelPackage.Save();
                }
                var fullPathDownload = Path.Combine(pathFileExcel, nameFileExcel);
                return ruta + "/" +   nameFileExcel;// Json("correcto", JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                var fullPathDownload = Path.Combine(pathFileExcel, nameFileExcel);
                //return Json("correcto", JsonRequestBehavior.AllowGet);
                throw Ex;
            }
            return "";
           // return Json("correcto", JsonRequestBehavior.AllowGet);
        }
        public List<ReporteTipoTipo> RPT_TipoTipo(FiltrosFechaModel filtros)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            
            //filtros.FechaInicial = (filtros.FechaInicial == "") ? "0000-00-00" : filtros.FechaInicial;
            //filtros.FechaFinal = (filtros.FechaFinal == "") ? "0000-00-00" : filtros.FechaFinal;
            var lista = new List<ReporteTipoTipo>();
            parametros.Add(new SqlParameter { ParameterName = "@pSucursal", SqlDbType = SqlDbType.VarChar, Value = filtros.IdSucursal });
            parametros.Add(new SqlParameter { ParameterName = "@pFecha", SqlDbType = SqlDbType.VarChar, Value = filtros.FechaInicial });
            parametros.Add(new SqlParameter { ParameterName = "@pFechaFinal", SqlDbType = SqlDbType.VarChar, Value = filtros.FechaFinal });
            try
            {
                DataSet ds = dac.Fill("RPT_TipoTipo", parametros);
                if(ds.Tables.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new ReporteTipoTipo
                        {
                            Departamento = dr["Departamento"].ToString(),
                            VentaGlobal = decimal.Parse(dr["VentaGlobal"].ToString()),
                            DescVentas = decimal.Parse(dr["DescVentas"].ToString()),
                            VentaBruta = decimal.Parse(dr["VentaBruta"].ToString()),
                            DevVenta = decimal.Parse( dr["DevVenta"].ToString()),
                            DevCredito = decimal.Parse( dr["DevCredito"].ToString()),
                            NotasCred = decimal.Parse( dr["NotasCred"].ToString()),
                            VentaTotal =  decimal.Parse(dr["VentaTotal"].ToString()),
                            Iva = decimal.Parse(dr["Iva"].ToString()),
                            Ieps = decimal.Parse( dr["Ieps"].ToString()),
                            VentaNeta = decimal.Parse(dr["VentaNeta"].ToString())

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return lista;
        }

        public string GetRutaReportes()
        {
            return @"C:\Users\User\Documents\GitHub\ApiReportes\templates";
            // ArrayList parametros = new ArrayList();
            // ConexionDataAccess dac = new ConexionDataAccess(connection);
            
            // //filtros.FechaInicial = (filtros.FechaInicial == "") ? "0000-00-00" : filtros.FechaInicial;
            // //filtros.FechaFinal = (filtros.FechaFinal == "") ? "0000-00-00" : filtros.FechaFinal;
            // var lista = new List<RutaRepporte>();
            // parametros.Add(new SqlParameter { ParameterName = "@pSucursal", SqlDbType = SqlDbType.VarChar, Value = 2 });
            // try
            // {
            //     DataSet ds = dac.Fill("CFG_GetRutaReportes", parametros);
            //     if(ds.Tables.Count > 0)
            //     {
            //         foreach(DataRow dr in ds.Tables[0].Rows)
            //         {
            //             lista.Add(new RutaRepporte
            //             {
            //                 Ruta = dr["Ruta"].ToString()

            //             });
            //         }
            //     }
            // }
            // catch (Exception ex)
            // {
            //     Console.Write(ex.Message);
            // }
            // return lista[0].Ruta;
        }

        public string ReportesResumenEstadistico(FiltrosFechaModel filtros)
        {

            var ListaReporteEstadistico = this.RPT_ResumentEstadistico(filtros);
            string webRootPath = _webHostEnvironment.ContentRootPath;
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            string pathFileExcel = "~/templates/";
            string nameFileExcel = "ReporteResumentEstadistico.xlsx";
           //var fullPath = Server.MapPath(pathFileExcel + nameFileExcel);
            var fullPath = Path.Combine(webRootPath, "templates/ResumenEstadistico.xlsx");
            var ruta = GetRutaReportes();

            try
            {
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                //FileInfo template = new FileInfo(Server.MapPath("~/templates/ReporteResumentEstadisticoTemplate.xlsx"));
                FileInfo template = new FileInfo(Path.Combine(webRootPath, "templates/template.xlsx"));
                FileInfo newFile = new FileInfo(fullPath);

                using (ExcelPackage excelPackage = new ExcelPackage(newFile, template))
                {
                    ExcelWorkbook myWorkbook = excelPackage.Workbook;
                    ExcelWorksheet myWorksheet = myWorkbook.Worksheets[0];

                    myWorksheet.Cells["A2"].Value = "Fecha:";
                    myWorksheet.Cells["A2"].Style.Font.Bold = true;
                    myWorksheet.Cells["A2"].Style.Font.Size = 12;
                    myWorksheet.Cells["A2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    myWorksheet.Cells["A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    myWorksheet.Cells["B2"].Value = DateTime.Now.ToShortDateString();

                    myWorksheet.Cells["A3"].Value = "Hora:";
                    myWorksheet.Cells["A3"].Style.Font.Bold = true;
                    myWorksheet.Cells["A3"].Style.Font.Size = 12;
                    myWorksheet.Cells["A3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    myWorksheet.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    myWorksheet.Cells["B3"].Value = DateTime.Now.ToLongTimeString();

                    myWorksheet.Cells["C2:H3"].Merge = true;
                    myWorksheet.Cells["C2:H3"].Value = "Supertienda Rico S.A de C.V.";
                    myWorksheet.Cells["C2:H3"].Style.Font.Bold = true;
                    myWorksheet.Cells["C2:H3"].Style.Font.Size = 12;
                    myWorksheet.Cells["C2:H3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    myWorksheet.Cells["C2:H3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    myWorksheet.Cells["A4:H5"].Merge = true;
                    myWorksheet.Cells["A4:H5"].Value = "Reporte Resumen Estadistico del " + filtros.FechaInicial + " a " + filtros.FechaFinal;
                    myWorksheet.Cells["A4:H5"].Style.Font.Bold = true;
                    myWorksheet.Cells["A4:H5"].Style.Font.Size = 12;
                    myWorksheet.Cells["A4:H5"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    myWorksheet.Cells["A4:H5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    myWorksheet.Cells["A6"].Value = "Fecha";
                    myWorksheet.Cells["B6"].Value = "Venta";
                    myWorksheet.Cells["C6"].Value = "Efectivo";
                    myWorksheet.Cells["D6"].Value = "Tarjeta";
                    myWorksheet.Cells["E6"].Value = "Credito";
                    myWorksheet.Cells["F6"].Value = "Devolucion";
                    myWorksheet.Cells["G6"].Value = "Datalogic";
             //       myWorksheet.Cells["H6"].Value = "Total";
                    myWorksheet.Cells["A6:G6"].Style.Font.Bold = true;
                    myWorksheet.Cells["A6:G6"].Style.Font.Size = 12;
                    myWorksheet.Cells["A6:G6"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    myWorksheet.Cells["A6:G6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    int i = 7;

                    foreach (var item in ListaReporteEstadistico)
                    {
                        myWorksheet.Cells["A" + i].Value = item.Fecha;
                        myWorksheet.Cells["B" + i].Value = item.Venta;
                        myWorksheet.Cells["C" + i].Value = item.Efectivo;
                        myWorksheet.Cells["D" + i].Value = item.Tarjeta;
                        myWorksheet.Cells["E" + i].Value = item.Credito;
                        myWorksheet.Cells["F" + i].Value = item.Devolucion;
                        myWorksheet.Cells["G" + i].Value = item.Datalogic;
                  //      myWorksheet.Cells["H" + i].Formula = "=B"+i+"-C"+i+"-D"+i+"-E"+i+"-F"+i;
                        myWorksheet.Cells["B" + i + ":H" + i].Style.Numberformat.Format = "$###,###,##0.00";
                        i++;
                    }
                    i = i - 1;

                    myWorksheet.Cells["A2:H" + i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    myWorksheet.Cells["A2:H" + i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    myWorksheet.Cells["A2:H" + i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    myWorksheet.Cells["A2:H" + i].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                    int l = i;
                    i = i + 1;
                    myWorksheet.Cells["A" + i].Value = "Total:";
                    myWorksheet.Cells["B" + i].Style.Numberformat.Format = "$###,###,##0.00";
                    myWorksheet.Cells["B" + i].Formula = "=SUM(B7:B" + l + ")";
                    myWorksheet.Cells["C" + i].Style.Numberformat.Format = "$###,###,##0.00";
                    myWorksheet.Cells["C" + i].Formula = "=SUM(C7:C" + l + ")";
                    myWorksheet.Cells["D" + i].Style.Numberformat.Format = "$###,###,##0.00";
                    myWorksheet.Cells["D" + i].Formula = "=SUM(D7:D" + l + ")";
                    myWorksheet.Cells["E" + i].Style.Numberformat.Format = "$###,###,##0.00";
                    myWorksheet.Cells["E" + i].Formula = "=SUM(E7:E" + l + ")";
                    myWorksheet.Cells["F" + i].Style.Numberformat.Format = "$###,###,##0.00";
                    myWorksheet.Cells["F" + i].Formula = "=SUM(F7:F" + l + ")";
                    myWorksheet.Cells["G" + i].Style.Numberformat.Format = "$###,###,##0.00";
                    myWorksheet.Cells["G" + i].Formula = "=SUM(G7:G" + l + ")";
              //      myWorksheet.Cells["H" + i].Style.Numberformat.Format = "$###,###,##0.00";
               //     myWorksheet.Cells["H" + i].Formula = "=SUM(H7:H" + l + ")";
                    myWorksheet.Cells["A2:G" + i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    myWorksheet.Cells["A2:G" + i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    excelPackage.Save();
                }
                var fullPathDownload = Path.Combine(pathFileExcel, nameFileExcel);
                return  ruta + "/" +   nameFileExcel;;
            }
            catch (Exception Ex)
            {
                var fullPathDownload = Path.Combine(pathFileExcel, nameFileExcel);
                //return Json("correcto", JsonRequestBehavior.AllowGet);
                throw;
            }

        }

        public List<ReporteResumenEstadistico> RPT_ResumentEstadistico(FiltrosFechaModel filtros)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            
            //filtros.FechaInicial = (filtros.FechaInicial == "") ? "0000-00-00" : filtros.FechaInicial;
            //filtros.FechaFinal = (filtros.FechaFinal == "") ? "0000-00-00" : filtros.FechaFinal;
            var lista = new List<ReporteResumenEstadistico>();
            parametros.Add(new SqlParameter { ParameterName = "@pIdSucursal", SqlDbType = SqlDbType.VarChar, Value = filtros.IdSucursal });
            parametros.Add(new SqlParameter { ParameterName = "@pFechaInicial", SqlDbType = SqlDbType.VarChar, Value = filtros.FechaInicial });
            parametros.Add(new SqlParameter { ParameterName = "@pFechaFinal", SqlDbType = SqlDbType.VarChar, Value = filtros.FechaFinal });
            try
            {
                DataSet ds = dac.Fill("RPT_ResumenEstadistico", parametros);
                if(ds.Tables.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new ReporteResumenEstadistico
                        {
                            Fecha = dr["ticn_aaaammddventa"].ToString(),
                            Venta = decimal.Parse(dr["Venta"].ToString()),
                            Efectivo = decimal.Parse(dr["Efectivo"].ToString()),
                            Credito = decimal.Parse(dr["Credito"].ToString()),
                            Tarjeta = decimal.Parse( dr["TE"].ToString()),
                            Datalogic = decimal.Parse( dr["DL"].ToString()),
                            Devolucion = decimal.Parse(dr["Devolucion"].ToString())


                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            
            
            return lista;
        }

        public string ReporteEstadoCuentaProveedor(int IdProveedor, string Fecha)
        {

            var ListaEstadoCuentaProveedor = this.GetEstadoCuentaProveedorApi(IdProveedor, Fecha);
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            string webRootPath = _webHostEnvironment.ContentRootPath;

            string pathFileExcel = "~/templates/";
            string nameFileExcel = "ReporteEstadoCuentaProveedor.xlsx";
            //var fullPath = Server.MapPath(pathFileExcel + nameFileExcel);
            var fullPath = Path.Combine(webRootPath, "templates/ReporteEstadoCuentaProveedor.xlsx");
            var ruta = GetRutaReportes();

            try
            {
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                //FileInfo template = new FileInfo(Server.MapPath("~/templates/ReporteEstadoCuentaProveedorTemplate.xlsx"));
                FileInfo template = new FileInfo(Path.Combine(webRootPath, "templates/ReporteEstadoCuentaProveedorTemplate.xlsx"));
                FileInfo newFile = new FileInfo(fullPath);

                using (ExcelPackage excelPackage = new ExcelPackage(newFile, template))
                {
                    ExcelWorkbook myWorkbook = excelPackage.Workbook;
                    ExcelWorksheet myWorksheet = myWorkbook.Worksheets[0];

                    myWorksheet.Cells["A2"].Value = "Fecha:";
                    myWorksheet.Cells["A2"].Style.Font.Bold = true;
                    myWorksheet.Cells["A2"].Style.Font.Size = 12;
                    myWorksheet.Cells["A2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    myWorksheet.Cells["A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    myWorksheet.Cells["B2"].Value = DateTime.Now.ToShortDateString();

                    myWorksheet.Cells["A3"].Value = "Hora:";
                    myWorksheet.Cells["A3"].Style.Font.Bold = true;
                    myWorksheet.Cells["A3"].Style.Font.Size = 12;
                    myWorksheet.Cells["A3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    myWorksheet.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    myWorksheet.Cells["B3"].Value = DateTime.Now.ToLongTimeString();

                    myWorksheet.Cells["C2:J3"].Merge = true;
                    myWorksheet.Cells["C2"].Value = "Supertienda Rico S.A de C.V.";
                    myWorksheet.Cells["C2:J3"].Style.Font.Bold = true;
                    myWorksheet.Cells["C2:J3"].Style.Font.Size = 12;
                    myWorksheet.Cells["C2:J3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    myWorksheet.Cells["C2:J3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    myWorksheet.Cells["A4:J5"].Merge = true;
                    myWorksheet.Cells["A4"].Value = "Reporte Estado Cuenta Proveedor del " + Fecha;
                    myWorksheet.Cells["A4:J5"].Style.Font.Bold = true;
                    myWorksheet.Cells["A4:J5"].Style.Font.Size = 12;
                    myWorksheet.Cells["A4:J5"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    myWorksheet.Cells["A4:J5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //myWorksheet.Cells["A6"].Value = "Id";
                    myWorksheet.Cells["A6"].Value = "Factura";
                    myWorksheet.Cells["B6"].Value = "Fecha";
                    myWorksheet.Cells["C6"].Value = "Fecha Vencimiento";
                    myWorksheet.Cells["D6"].Value = "Número Nota";
                    myWorksheet.Cells["E6"].Value = "Cargo";
                    myWorksheet.Cells["F6"].Value = "Abono";
                    myWorksheet.Cells["G6"].Value = "Saldo";
                    myWorksheet.Cells["H6"].Value = "Importe";
                    myWorksheet.Cells["I6"].Value = "Iva";
                    myWorksheet.Cells["J6"].Value = "Ieps";
                    myWorksheet.Cells["A6:J6"].Style.Font.Bold = true;
                    myWorksheet.Cells["A6:J6"].Style.Font.Size = 12;
                    myWorksheet.Cells["A6:J6"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    myWorksheet.Cells["A6:J6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    int i = 7;
                    int start = 0;
                    int cantProv = 0;
                    string idProveedor = "";
                    int cantReg = 0;
                    decimal totalME = 0m;
                    decimal totalMP = 0m;
                    decimal totalImp = 0m;
                    decimal totalMIv = 0m;
                    decimal totalMIe = 0m;

                    foreach (var item in ListaEstadoCuentaProveedor)
                    {
                        cantReg++;
                        if (idProveedor == "")
                        {
                            decimal importe = item.MontoPagar - (item.Iva + item.Ieps);
                            idProveedor = item.IdProveedor.ToString();
                            myWorksheet.Cells["A" + i].Value = item.NombreProveedor;
                            myWorksheet.Cells["A" + i + ":J" + i].Merge = true;
                            myWorksheet.Cells["A" + i + ":J" + i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            myWorksheet.Cells["A" + i + ":J" + i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            i++;
                            start = i;
                            myWorksheet.Cells["A" + i].Value = item.Factura;
                            myWorksheet.Cells["B" + i].Value = item.Fecha;
                            myWorksheet.Cells["C" + i].Value = item.FechaVencimiento;
                            myWorksheet.Cells["D" + i].Value = item.NumeroNota;
                            //myWorksheet.Cells["D" + i].Value = "";
                            myWorksheet.Cells["E" + i].Value = item.MontoEntrada;
                            myWorksheet.Cells["E" + i].Style.Numberformat.Format = "$###,###,##0.00";
                            myWorksheet.Cells["F" + i].Value = 0;
                            myWorksheet.Cells["F" + i].Style.Numberformat.Format = "$###,###,##0.00";
                            myWorksheet.Cells["G" + i].Value = item.MontoPagar;
                            myWorksheet.Cells["G" + i].Style.Numberformat.Format = "$###,###,##0.00";
                            myWorksheet.Cells["H" + i].Value = importe;
                            myWorksheet.Cells["H" + i].Style.Numberformat.Format = "$###,###,##0.00";
                            myWorksheet.Cells["I" + i].Value = item.Iva;
                            myWorksheet.Cells["I" + i].Style.Numberformat.Format = "$###,###,##0.00";
                            myWorksheet.Cells["J" + i].Value = item.Ieps;
                            myWorksheet.Cells["J" + i].Style.Numberformat.Format = "$###,###,##0.00";

                            totalME = totalME + item.MontoEntrada;
                            totalMP = totalMP + item.MontoPagar;
                            totalImp = totalImp + importe;
                            totalMIv = totalMIv + item.Iva;
                            totalMIe = totalMIe + item.Ieps;
                        }
                        else if (idProveedor != item.IdProveedor.ToString())
                        {
                            decimal importe = item.MontoPagar - (item.Iva + item.Ieps);
                            myWorksheet.Cells["A" + i].Value = "Total:";
                            myWorksheet.Cells["A" + i + ":D" + i].Merge = true;
                            myWorksheet.Cells["A" + i + ":D" + i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            myWorksheet.Cells["A" + i + ":D" + i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            int m = i - 1;
                            //i = i + 1;
                            myWorksheet.Cells["E" + i].Style.Numberformat.Format = "$###,###,##0.00";
                            myWorksheet.Cells["E" + i].Formula = "=SUM(E"+ start +":E" + m + ")";
                            myWorksheet.Cells["F" + i].Style.Numberformat.Format = "$###,###,##0.00";
                            myWorksheet.Cells["F" + i].Formula = "=SUM(F" + start + ":F" + m + ")";
                            myWorksheet.Cells["G" + i].Style.Numberformat.Format = "$###,###,##0.00";
                            myWorksheet.Cells["G" + i].Formula = "=SUM(G" + start + ":G" + m + ")";
                            myWorksheet.Cells["G" + i].Style.Numberformat.Format = "$###,###,##0.00";
                            myWorksheet.Cells["H" + i].Formula = "=SUM(H" + start + ":H" + m + ")";
                            myWorksheet.Cells["H" + i].Style.Numberformat.Format = "$###,###,##0.00";
                            myWorksheet.Cells["I" + i].Formula = "=SUM(I" + start + ":I" + m + ")";
                            myWorksheet.Cells["I" + i].Style.Numberformat.Format = "$###,###,##0.00";
                            myWorksheet.Cells["J" + i].Formula = "=SUM(J" + start + ":J" + m + ")";
                            myWorksheet.Cells["J" + i].Style.Numberformat.Format = "$###,###,##0.00";
                            i++;
                            idProveedor = item.IdProveedor.ToString();
                            cantProv++;
                            myWorksheet.Cells["A" + i].Value = item.NombreProveedor;
                            myWorksheet.Cells["A" + i + ":J" + i].Merge = true;
                            myWorksheet.Cells["A" + i + ":J" + i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            myWorksheet.Cells["A" + i + ":J" + i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            i++;
                            start = i;
                            myWorksheet.Cells["A" + i].Value = item.Factura;
                            myWorksheet.Cells["B" + i].Value = item.Fecha;
                            myWorksheet.Cells["C" + i].Value = item.FechaVencimiento;
                            myWorksheet.Cells["D" + i].Value = item.NumeroNota;
                            //myWorksheet.Cells["D" + i].Value = "";
                            myWorksheet.Cells["E" + i].Value = item.MontoEntrada;
                            myWorksheet.Cells["E" + i].Style.Numberformat.Format = "$###,###,##0.00";
                            myWorksheet.Cells["F" + i].Value = 0;
                            myWorksheet.Cells["F" + i].Style.Numberformat.Format = "$###,###,##0.00";
                            myWorksheet.Cells["G" + i].Value = item.MontoPagar;
                            myWorksheet.Cells["G" + i].Style.Numberformat.Format = "$###,###,##0.00";
                            myWorksheet.Cells["H" + i].Value = importe;
                            myWorksheet.Cells["H" + i].Style.Numberformat.Format = "$###,###,##0.00";
                            myWorksheet.Cells["I" + i].Value = item.Iva;
                            myWorksheet.Cells["I" + i].Style.Numberformat.Format = "$###,###,##0.00";
                            myWorksheet.Cells["J" + i].Value = item.Ieps;
                            myWorksheet.Cells["J" + i].Style.Numberformat.Format = "$###,###,##0.00";

                            totalME = totalME + item.MontoEntrada;
                            totalMP = totalMP + item.MontoPagar;
                            totalImp = totalImp + importe;
                            totalMIv = totalMIv + item.Iva;
                            totalMIe = totalMIe + item.Ieps;

                            if (cantReg == ListaEstadoCuentaProveedor.Count)
                            {
                                i++;
                                myWorksheet.Cells["A" + i].Value = "Total:";
                                myWorksheet.Cells["A" + i + ":D" + i].Merge = true;
                                myWorksheet.Cells["A" + i + ":D" + i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                myWorksheet.Cells["A" + i + ":D" + i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                int n = i - 1;
                                //i = i + 1;
                                myWorksheet.Cells["E" + i].Style.Numberformat.Format = "$###,###,##0.00";
                                myWorksheet.Cells["E" + i].Formula = "=SUM(E" + start + ":E" + n + ")";
                                myWorksheet.Cells["F" + i].Style.Numberformat.Format = "$###,###,##0.00";
                                myWorksheet.Cells["F" + i].Formula = "=SUM(F" + start + ":F" + n + ")";
                                myWorksheet.Cells["G" + i].Style.Numberformat.Format = "$###,###,##0.00";
                                myWorksheet.Cells["G" + i].Formula = "=SUM(G" + start + ":G" + n + ")";
                                myWorksheet.Cells["H" + i].Style.Numberformat.Format = "$###,###,##0.00";
                                myWorksheet.Cells["H" + i].Formula = "=SUM(H" + start + ":H" + n + ")";
                                myWorksheet.Cells["I" + i].Style.Numberformat.Format = "$###,###,##0.00";
                                myWorksheet.Cells["I" + i].Formula = "=SUM(I" + start + ":I" + n + ")";
                                myWorksheet.Cells["J" + i].Style.Numberformat.Format = "$###,###,##0.00";
                                myWorksheet.Cells["J" + i].Formula = "=SUM(J" + start + ":J" + n + ")";

                            }
                        }
                        else
                        {
                            decimal importe = item.MontoPagar - (item.Iva + item.Ieps);
                            myWorksheet.Cells["A" + i].Value = item.Factura;
                            myWorksheet.Cells["B" + i].Value = item.Fecha;
                            myWorksheet.Cells["C" + i].Value = item.FechaVencimiento;
                            myWorksheet.Cells["D" + i].Value = item.NumeroNota;
                            //myWorksheet.Cells["D" + i].Value = "";
                            myWorksheet.Cells["E" + i].Value = item.MontoEntrada;
                            myWorksheet.Cells["E" + i].Style.Numberformat.Format = "$###,###,##0.00";
                            myWorksheet.Cells["F" + i].Value = 0;
                            myWorksheet.Cells["F" + i].Style.Numberformat.Format = "$###,###,##0.00";
                            myWorksheet.Cells["G" + i].Value = item.MontoPagar;
                            myWorksheet.Cells["G" + i].Style.Numberformat.Format = "$###,###,##0.00";
                            myWorksheet.Cells["H" + i].Value = importe;
                            myWorksheet.Cells["H" + i].Style.Numberformat.Format = "$###,###,##0.00";
                            myWorksheet.Cells["I" + i].Value = item.Iva;
                            myWorksheet.Cells["I" + i].Style.Numberformat.Format = "$###,###,##0.00";
                            myWorksheet.Cells["J" + i].Value = item.Ieps;
                            myWorksheet.Cells["J" + i].Style.Numberformat.Format = "$###,###,##0.00";
                            

                            totalME = totalME + item.MontoEntrada;
                            totalMP = totalMP + item.MontoPagar;
                            totalImp = totalImp + importe;
                            totalMIv = totalMIv + item.Iva;
                            totalMIe = totalMIe + item.Ieps;

                            if (cantReg == ListaEstadoCuentaProveedor.Count)
                            {
                                myWorksheet.Cells["A" + i].Value = "Total:";
                                myWorksheet.Cells["A" + i + ":D" + i].Merge = true;
                                myWorksheet.Cells["A" + i + ":D" + i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                myWorksheet.Cells["A" + i + ":D" + i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                int m = i - 1;
                                //i = i + 1;
                                
                                myWorksheet.Cells["E" + i].Style.Numberformat.Format = "$###,###,##0.00";
                                myWorksheet.Cells["E" + i].Formula = "=SUM(E" + start + ":E" + m + ")";
                                myWorksheet.Cells["F" + i].Style.Numberformat.Format = "$###,###,##0.00";
                                myWorksheet.Cells["F" + i].Formula = "=SUM(F" + start + ":F" + m + ")";
                                myWorksheet.Cells["G" + i].Style.Numberformat.Format = "$###,###,##0.00";
                                myWorksheet.Cells["G" + i].Formula = "=SUM(G" + start + ":G" + m + ")";
                                myWorksheet.Cells["H" + i].Style.Numberformat.Format = "$###,###,##0.00";
                                myWorksheet.Cells["H" + i].Formula = "=SUM(H" + start + ":H" + m + ")";
                                myWorksheet.Cells["I" + i].Style.Numberformat.Format = "$###,###,##0.00";
                                myWorksheet.Cells["I" + i].Formula = "=SUM(I" + start + ":I" + m + ")";
                                myWorksheet.Cells["J" + i].Style.Numberformat.Format = "$###,###,##0.00";
                                myWorksheet.Cells["J" + i].Formula = "=SUM(J" + start + ":J" + m + ")";

                            }
                        }
                        i++;
                    }
                    i = i - 1;

                    myWorksheet.Cells["A2:J" + i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    myWorksheet.Cells["A2:J" + i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    myWorksheet.Cells["A2:J" + i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    myWorksheet.Cells["A2:J" + i].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                    i++;
                    myWorksheet.Cells["A" + i].Value = "Totales:";
                    myWorksheet.Cells["A" + i + ":D" + i].Merge = true;
                    myWorksheet.Cells["A" + i + ":D" + i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    myWorksheet.Cells["A" + i + ":D" + i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    myWorksheet.Cells["E" + i].Value = totalME;
                    myWorksheet.Cells["E" + i].Style.Numberformat.Format = "$###,###,##0.00";
                    myWorksheet.Cells["F" + i].Value = 0;
                    myWorksheet.Cells["F" + i].Style.Numberformat.Format = "$###,###,##0.00";
                    myWorksheet.Cells["G" + i].Value = totalMP;
                    myWorksheet.Cells["G" + i].Style.Numberformat.Format = "$###,###,##0.00";
                    myWorksheet.Cells["H" + i].Value = totalImp;
                    myWorksheet.Cells["H" + i].Style.Numberformat.Format = "$###,###,##0.00";
                    myWorksheet.Cells["I" + i].Value = totalMIv;
                    myWorksheet.Cells["I" + i].Style.Numberformat.Format = "$###,###,##0.00";
                    myWorksheet.Cells["J" + i].Value = totalMIe;
                    myWorksheet.Cells["J" + i].Style.Numberformat.Format = "$###,###,##0.00";

                    if (cantProv == 0)
                    {
                        int l = i;
                        i = i + 1;
                        myWorksheet.Cells["A" + i].Value = "Total:";
                        myWorksheet.Cells["A" + i + ":D" + i].Merge = true;
                        myWorksheet.Cells["A" + i + ":D" + i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        myWorksheet.Cells["A" + i + ":D" + i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        myWorksheet.Cells["D" + i].Style.Numberformat.Format = "$###,###,##0.00";
                        myWorksheet.Cells["D" + i].Formula = "=SUM(D7:D" + l + ")";
                        myWorksheet.Cells["E" + i].Style.Numberformat.Format = "$###,###,##0.00";
                        myWorksheet.Cells["E" + i].Formula = "=SUM(E7:E" + l + ")";
                        myWorksheet.Cells["F" + i].Style.Numberformat.Format = "$###,###,##0.00";
                        myWorksheet.Cells["F" + i].Formula = "=SUM(F7:F" + l + ")";
                        myWorksheet.Cells["G" + i].Style.Numberformat.Format = "$###,###,##0.00";
                        myWorksheet.Cells["G" + i].Formula = "=SUM(G7:G" + l + ")";
                        myWorksheet.Cells["H" + i].Style.Numberformat.Format = "$###,###,##0.00";
                        myWorksheet.Cells["H" + i].Formula = "=SUM(H7:H" + l + ")";
                        myWorksheet.Cells["I" + i].Style.Numberformat.Format = "$###,###,##0.00";
                        myWorksheet.Cells["I" + i].Formula = "=SUM(I7:I" + l + ")";
                        myWorksheet.Cells["J" + i].Style.Numberformat.Format = "$###,###,##0.00";
                        myWorksheet.Cells["J" + i].Formula = "=SUM(J7:J" + l + ")";
                    }
                    
                    excelPackage.Save();
                }
                var fullPathDownload = Path.Combine(pathFileExcel, nameFileExcel);
                return  ruta + "/" +   nameFileExcel;;
            }
            catch (Exception Ex)
            {
                var fullPathDownload = Path.Combine(pathFileExcel, nameFileExcel);
                //return Json("correcto", JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        public List<EstadoCuentaProveedor> GetEstadoCuentaProveedorApi(int id_proveedor, string fecha)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            
            //filtros.FechaInicial = (filtros.FechaInicial == "") ? "0000-00-00" : filtros.FechaInicial;
            //filtros.FechaFinal = (filtros.FechaFinal == "") ? "0000-00-00" : filtros.FechaFinal;
            var lista = new List<EstadoCuentaProveedor>();
            parametros.Add(new SqlParameter { ParameterName = "@pProveedor", SqlDbType = SqlDbType.VarChar, Value = id_proveedor });
            parametros.Add(new SqlParameter { ParameterName = "@pFecha", SqlDbType = SqlDbType.VarChar, Value = fecha });
            try
            {
                DataSet ds = dac.Fill("RPT_GetEstadoCuentaProveedor", parametros);
                if(ds.Tables.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new EstadoCuentaProveedor
                        {
                            Id = int.Parse( dr["Id"].ToString()),
                            Factura = dr["Factura"].ToString(),
                            Fecha = dr["Fecha"].ToString().Substring(0,10),
                            FechaVencimiento = dr["FechaVencimiento"].ToString().Substring(0,10),
                            IdProveedor = int.Parse(dr["IdProveedor"].ToString()),
                            MontoEntrada = decimal.Parse(dr["MontoEntrada"].ToString()),
                            MontoPagar = decimal.Parse(dr["MontoEntrada"].ToString()),
                            NombreProveedor = dr["NombreProveedor"].ToString(),
                            NumeroNota = dr["NumeroNota"].ToString(),
                            Ieps =decimal.Parse( dr["ieps"].ToString()),
                            Iva = decimal.Parse(dr["Iva"].ToString())
                            
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return lista;
        }

        public string ReporteVentasInsecticidas(string FechaInicial, string FechaFinal, int IdSucursal)
        {

            var ListaVentasInsecticidas = this.RPT_VentasInsecticidas(FechaInicial, FechaFinal, IdSucursal);
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            string webRootPath = _webHostEnvironment.ContentRootPath;

            string pathFileExcel = "~/templates/";
            string nameFileExcel = "ReporteVentasInsecticidas.xlsx";
            //var fullPath = Server.MapPath(pathFileExcel + nameFileExcel);
            var fullPath = Path.Combine(webRootPath, "templates/ReporteVentasInsecticidas.xlsx");
            var ruta = GetRutaReportes();

            try
            {
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                //FileInfo template = new FileInfo(Server.MapPath("~/templates/ReporteEstadoCuentaProveedorTemplate.xlsx"));
                FileInfo template = new FileInfo(Path.Combine(webRootPath, "templates/ReporteVentasInsecticidasTemplate.xlsx"));
                FileInfo newFile = new FileInfo(fullPath);

                using (ExcelPackage excelPackage = new ExcelPackage(newFile, template))
                {
                    ExcelWorkbook myWorkbook = excelPackage.Workbook;
                    ExcelWorksheet myWorksheet = myWorkbook.Worksheets[0];

                    myWorksheet.Cells["A2"].Value = "Fecha:";
                    myWorksheet.Cells["A2"].Style.Font.Bold = true;
                    myWorksheet.Cells["A2"].Style.Font.Size = 12;
                    myWorksheet.Cells["A2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    myWorksheet.Cells["A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    myWorksheet.Cells["B2"].Value = DateTime.Now.ToShortDateString();

                    myWorksheet.Cells["A3"].Value = "Hora:";
                    myWorksheet.Cells["A3"].Style.Font.Bold = true;
                    myWorksheet.Cells["A3"].Style.Font.Size = 12;
                    myWorksheet.Cells["A3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    myWorksheet.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    myWorksheet.Cells["B3"].Value = DateTime.Now.ToLongTimeString();

                    myWorksheet.Cells["C2:H3"].Merge = true;
                    myWorksheet.Cells["C2"].Value = "Supertienda Rico S.A de C.V.";
                    myWorksheet.Cells["C2:H3"].Style.Font.Bold = true;
                    myWorksheet.Cells["C2:H3"].Style.Font.Size = 12;
                    myWorksheet.Cells["C2:H3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    myWorksheet.Cells["C2:H3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    myWorksheet.Cells["A4:H5"].Merge = true;
                    myWorksheet.Cells["A4"].Value = "Reporte Ventas de Insecticidas del " + FechaInicial + " al " + FechaFinal;
                    myWorksheet.Cells["A4:H5"].Style.Font.Bold = true;
                    myWorksheet.Cells["A4:H5"].Style.Font.Size = 12;
                    myWorksheet.Cells["A4:H5"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    myWorksheet.Cells["A4:H5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //myWorksheet.Cells["A6"].Value = "Id";
                    myWorksheet.Cells["A6"].Value = "Código";
                    myWorksheet.Cells["B6"].Value = "Descripción";
                    myWorksheet.Cells["C6"].Value = "Familia";
                    myWorksheet.Cells["D6"].Value = "Cantidad";
                    myWorksheet.Cells["E6"].Value = "Costo";
                    myWorksheet.Cells["F6"].Value = "Iva";
                    myWorksheet.Cells["G6"].Value = "Ieps";
                    myWorksheet.Cells["H6"].Value = "Total";
                    myWorksheet.Cells["A6:H6"].Style.Font.Bold = true;
                    myWorksheet.Cells["A6:H6"].Style.Font.Size = 12;
                    myWorksheet.Cells["A6:H6"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    myWorksheet.Cells["A6:H6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    int i = 7;

                    foreach (var item in ListaVentasInsecticidas)
                    {
                        myWorksheet.Cells["A" + i].Value = item.Codigo;
                        myWorksheet.Cells["B" + i].Value = item.Descripcion;
                        myWorksheet.Cells["C" + i].Value = item.Familia;
                        myWorksheet.Cells["D" + i].Value = item.Cantidad;
                        myWorksheet.Cells["E" + i].Value = item.Costo;
                        myWorksheet.Cells["E" + i].Style.Numberformat.Format = "$###,###,##0.00";
                        myWorksheet.Cells["F" + i].Value = item.Iva;
                        myWorksheet.Cells["G" + i].Value = item.Ieps;
                 //       myWorksheet.Cells["G" + i].Style.Numberformat.Format = "$###,###,##0.00";
                        myWorksheet.Cells["H" + i].Value = item.Total;
                        myWorksheet.Cells["H" + i].Style.Numberformat.Format = "$###,###,##0.00";
                        i++;
                    }
                    i = i - 1;
                    myWorksheet.Cells["A2:H" + i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    myWorksheet.Cells["A2:H" + i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    myWorksheet.Cells["A2:H" + i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    myWorksheet.Cells["A2:H" + i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    int l = i;
                    i = i + 1;

                    myWorksheet.Cells["A" + i].Value = "Total:";
                    myWorksheet.Cells["A" + i + ":C" + i].Merge = true;
                    myWorksheet.Cells["A" + i + ":C" + i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    myWorksheet.Cells["A" + i + ":C" + i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    myWorksheet.Cells["D" + i].Formula = "=SUM(D7:D" + l + ")";
                    myWorksheet.Cells["E" + i].Formula = "=SUM(E7:E" + l + ")";
                    myWorksheet.Cells["E" + i].Style.Numberformat.Format = "$###,###,##0.00";
                    myWorksheet.Cells["H" + i].Style.Numberformat.Format = "$###,###,##0.00";
                    myWorksheet.Cells["H" + i].Formula = "=SUM(H7:H" + l + ")";

                    excelPackage.Save();
                }

                var fullPathDownload = Path.Combine(pathFileExcel, nameFileExcel);
                return  ruta + "/" +   nameFileExcel;;
            }
            catch (Exception Ex)
            {
                var fullPathDownload = Path.Combine(pathFileExcel, nameFileExcel);
                throw;
            }
        }

        public List<VentasInsecticidas> RPT_VentasInsecticidas(string fecha_inicial, string fecha_final, int sucursal)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            
            //filtros.FechaInicial = (filtros.FechaInicial == "") ? "0000-00-00" : filtros.FechaInicial;
            //filtros.FechaFinal = (filtros.FechaFinal == "") ? "0000-00-00" : filtros.FechaFinal;
            var lista = new List<VentasInsecticidas>();
            parametros.Add(new SqlParameter { ParameterName = "@pFechaInicial", SqlDbType = SqlDbType.VarChar, Value = fecha_inicial });
            parametros.Add(new SqlParameter { ParameterName = "@pFechaFinal", SqlDbType = SqlDbType.VarChar, Value = fecha_final });
            parametros.Add(new SqlParameter { ParameterName = "@pIdSucursal", SqlDbType = SqlDbType.VarChar, Value = sucursal });
            try
            {
                DataSet ds = dac.Fill("RPT_VentasInsecticidas", parametros);
                if(ds.Tables.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new VentasInsecticidas
                        {
                            Codigo = dr["ARTC_ARTICULO"].ToString(),
                            Descripcion = dr["ARTC_DESCRIPCION"].ToString(),
                            Cantidad = decimal.Parse(dr["Unidades"].ToString()),
                            Total = decimal.Parse(dr["Total"].ToString()),
                            Iva = int.Parse(dr["iva"].ToString()),
                            Ieps = int.Parse( dr["ieps"].ToString()),
                            Familia = dr["Nombre"].ToString(),
                            Costo = decimal.Parse( dr["Costo"].ToString()),

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return lista;
        }




    }
}