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
    public class TipoSistemaService
    {
        private string connection;

        public TipoSistemaService(IMarcatelDatabaseSetting settings)
        {
            connection = settings.ConnectionString;
        }

        public void InsertTipoSistema (TipoSistemaModelInsert tipo)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            parametros.Add(new SqlParameter { ParameterName="@Descripcion", SqlDbType = SqlDbType.VarChar, Value = tipo.TipoSistema_Descripcion});

            try
            {
                dac.ExecuteNonQuery("sp_InsertTipoSistema", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        
    }
}