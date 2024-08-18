using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using reportesApi.DataContext;
using reportesApi.Models;


namespace reportesApi.Services
{
    public class OperationService
    {
        private  string connection;
        
        
        public OperationService(IMarcatelDatabaseSetting settings)
        {
             connection = settings.ConnectionString;
        }



        public DescuentosUsuarioModel GetDescuentos(int id_usuario)
        {
            var descuento = new DescuentosUsuarioModel();
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            try
            {
                DataSet ds = new DataSet();
                parametros.Add(new SqlParameter { ParameterName = "@pId", SqlDbType = SqlDbType.VarChar, Value = id_usuario });
                ds = dac.Fill("sp_get_descuento_usuario", parametros);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    descuento.Descuento = int.Parse(dr["Descuento"].ToString());
                    descuento.IdUsuario = int.Parse(dr["IdUsuario"].ToString());
                    descuento.Nombre = dr["Nombre"].ToString();
                }
                return descuento;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);

                throw;
            }
        }
    }
}