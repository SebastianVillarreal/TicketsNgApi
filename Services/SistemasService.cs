using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using reportesApi.DataContext;
using reportesApi.Models;
using System.Collections.Generic;


namespace reportesApi.Services
{
    public class SistemaService
    {
        private  string connection;
        
        
        public SistemaService(IMarcatelDatabaseSetting settings)
        {
             connection = settings.ConnectionString;
        }

        public void InsertSistema(SistemaModelInsert sistema)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            parametros.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = SqlDbType.VarChar, Value = sistema.Sistema_Nombre });
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = SqlDbType.Int, Value = sistema.Sistema_Estatus});
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = sistema.Usuario_Registra });
            parametros.Add(new SqlParameter { ParameterName = "@Tipo", SqlDbType = SqlDbType.Int, Value = sistema.Sistema_Tipo });

            try
            {
                dac.ExecuteNonQuery("sp_InsertSistema", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        



    }

}
