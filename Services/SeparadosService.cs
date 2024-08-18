using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using reportesApi.DataContext;
using reportesApi.Models;
using System.Collections.Generic;


namespace reportesApi.Services
{
    public class SeparadosService
    {
        private  string connection;
        
        
        public SeparadosService(IMarcatelDatabaseSetting settings)
        {
             connection = settings.ConnectionString;
        }



        public int InsertSeparado(SeparadoModel separado, int id_usuario)
        {
            var descuento = new DescuentosUsuarioModel();
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            int folio = 0;
            parametros.Add(new SqlParameter { ParameterName = "@pIdSucursal", SqlDbType = SqlDbType.VarChar, Value = separado.IdSucursal });
            parametros.Add(new SqlParameter { ParameterName = "@pTicket", SqlDbType = SqlDbType.VarChar, Value = separado.FolioTicket });
            parametros.Add(new SqlParameter { ParameterName = "@pUsuario", SqlDbType = SqlDbType.VarChar, Value = id_usuario });
            parametros.Add(new SqlParameter { ParameterName = "@pMonto", SqlDbType = SqlDbType.VarChar, Value = separado.Monto });
            parametros.Add(new SqlParameter { ParameterName = "@pIdCliente", SqlDbType = SqlDbType.VarChar, Value = separado.IdCliente });
            try
            {
                DataSet ds = dac.Fill("sp_insert_separado", parametros);
                if(ds.Tables.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        folio = int.Parse(dr["Id"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return folio;
        }

        public DataSet GetRenglonesSeparado(int folio, int sucursal)
        {
            var lista = new List<RenglonTicketModel>();
            DataSet ds = new DataSet();
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros.Add(new SqlParameter { ParameterName = "@pFolio", SqlDbType = SqlDbType.VarChar, Value = folio });
            parametros.Add(new SqlParameter { ParameterName = "@pIdSucursal", SqlDbType = SqlDbType.VarChar, Value = sucursal });
            try
            {
                ds = dac.Fill("sp_get_renglones_separado", parametros);
                
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return ds;
            
        }

        public SeparadoModel GetDatosSeparado(string folio, int id_sucursal)
        {
            SeparadoModel separado = new SeparadoModel();
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros.Add(new SqlParameter { ParameterName = "@pFolio", SqlDbType = SqlDbType.VarChar, Value = folio });
            parametros.Add(new SqlParameter { ParameterName = "@pSucursal", SqlDbType = SqlDbType.VarChar, Value =  id_sucursal});
            try
            {
                DataSet ds = dac.Fill("sp_get_datos_separado", parametros);
                if(ds.Tables.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        separado.Abonado = float.Parse(dr["Abonado"].ToString());
                        separado.Monto = float.Parse(dr["Monto"].ToString());
                        separado.Restante = float.Parse(dr["Resto"].ToString());
                        separado.Id = int.Parse(dr["Id"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                
            }
            return separado;
        }
    }
}