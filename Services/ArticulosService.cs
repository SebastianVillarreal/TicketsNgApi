using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using reportesApi.DataContext;
using reportesApi.Models;
using System.Collections.Generic;

namespace reportesApi.Services
{
    public class ArticulosService
    {
        private  string connection;
        
        
        public ArticulosService(IMarcatelDatabaseSetting settings)
        {
             connection = settings.ConnectionString;
        }

        public ArticuloModel GetDatosArticulo(string codigo, int cliente)
        {
            
            ArticuloModel articulo = new ArticuloModel();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            ArrayList parametros = new ArrayList();
            try
            {
                parametros.Add(new SqlParameter { ParameterName = "@pCodigo", SqlDbType = SqlDbType.VarChar, Value = codigo });
                parametros.Add(new SqlParameter { ParameterName = "@pIdCliente", SqlDbType = SqlDbType.VarChar, Value = cliente });
                DataSet ds = dac.Fill("sp_get_datos_articulo_cliente", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        articulo.Codigo = row["Codigo"].ToString();
                        articulo.Descripcion = row["Descripcion"].ToString();
                        articulo.UMedida = row["UnidadMedida"].ToString();
                        articulo.PrecioFinal = float.Parse(row["PrecioFinal"].ToString());
                        articulo.PrecioOriginal = float.Parse(row["PrecioOriginal"].ToString());
                        articulo.PrecioFinalImpuestos = float.Parse(row["PrecioFinalImpuestos"].ToString());
                        articulo.PrecioOriginalImpuestos = float.Parse(row["PrecioOriginalImpuestos"].ToString());
                        articulo.Iva = float.Parse(row["Iva"].ToString());
                        articulo.Ieps = float.Parse(row["Ieps"].ToString());
                    }
                }
                return articulo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public DataSet BuscarArticulos(string nombre)
        {
            DataSet ds = new DataSet();
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            try
            {
                parametros.Add(new SqlParameter { ParameterName = "@pNombre", SqlDbType = SqlDbType.VarChar, Value = nombre });
                ds = dac.Fill("sp_busqueda_articulos", parametros);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
            return ds;
        }

        public DataSet BuscarServicio(string nombre)
        {
            DataSet ds = new DataSet();
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            List<ServiciosModel> lista = new List<ServiciosModel>();
            try
            {
                parametros.Add(new SqlParameter { ParameterName = "@pNombre", SqlDbType = SqlDbType.VarChar, Value = nombre });
                ds = dac.Fill("sp_busqueda_servicios", parametros);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
            return ds;
        }
    }
}
