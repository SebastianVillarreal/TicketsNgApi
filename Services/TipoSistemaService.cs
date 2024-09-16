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

        public List<TipoSistemaModel> GetAllTiposSistemas()
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            List<TipoSistemaModel> lista = new List<TipoSistemaModel>();

            try
            {
                DataSet ds = dac.Fill("sp_GetAllTiposSistemas", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new TipoSistemaModel
                        {
                            TipoSistema_Id = int.Parse(dr["Id"].ToString()),
                            TipoSistema_Descripcion = dr["Descripcion"].ToString()
                        });
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return lista;

        }

        public List<TipoSistemaModel> GetTipoSistemaById(int id)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            List<TipoSistemaModel> lista = new List<TipoSistemaModel>();

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id });

            try
            {
                DataSet ds = dac.Fill("sp_GetTipoSistemaById", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new TipoSistemaModel
                        {
                            TipoSistema_Id = int.Parse(dr["Id"].ToString()),
                            TipoSistema_Descripcion = dr["Descripcion"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return lista;
        }

        public void UpdateTipoSistema (TipoSistemaModel tipo)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = tipo.TipoSistema_Id });
            parametros.Add(new SqlParameter { ParameterName = "@Descripcion", SqlDbType = SqlDbType.VarChar, Value = tipo.TipoSistema_Descripcion});

            try
            {
                dac.ExecuteNonQuery("sp_UpdateTipoSistema", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        
    }
}