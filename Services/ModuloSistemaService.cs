using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using reportesApi.DataContext;
using reportesApi.Models;
using System.Collections.Generic;
using System.Linq;


namespace reportesApi.Services
{
    public class ModuloSistemaService
    {
        private  string connection;
        
        
        public ModuloSistemaService(IMarcatelDatabaseSetting settings)
        {
             connection = settings.ConnectionString;
        }

        public void InsertModulo(ModuloSistemaModelInsert modulo)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            parametros.Add(new SqlParameter { ParameterName = "@IdSistema", SqlDbType = SqlDbType.Int, Value = modulo.Sistema_Id });
            parametros.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = SqlDbType.VarChar, Value = modulo.Modulo_Nombre });
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = SqlDbType.Int, Value = modulo.Modulo_Estatus});
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = modulo.Usuario_Registra });

            try
            {
                dac.ExecuteNonQuery("sp_InsertModuloSistema", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        


    }

}
