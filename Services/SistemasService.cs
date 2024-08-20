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

        public List<SistemaModelGet> GetAllSistemas()
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            List<SistemaModelGet> lista = new List<SistemaModelGet>();

            try
            {
                DataSet ds = dac.Fill("sp_GetAllSistemas", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new SistemaModelGet
                        {
                            Sistema_Id = int.Parse(dr["Id"].ToString()),
                            Sistema_Nombre = dr["Nombre"].ToString(),
                            Sistema_Estatus = int.Parse(dr["Estatus"].ToString()),
                            Usuario_Registra = int.Parse(dr["UsuarioRegistra"].ToString()),
                            Fecha_Registro = dr["FechaRegistro"].ToString(),
                            Sistema_Tipo = int.Parse(dr["Tipo"].ToString())
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

        public List<SistemaModelGet> GetSistemaById(int id)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            List<SistemaModelGet> lista = new List<SistemaModelGet>();

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id });


            try
            {
                DataSet ds = dac.Fill("sp_GetSistemaById", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new SistemaModelGet
                        {
                            Sistema_Id = int.Parse(dr["Id"].ToString()),
                            Sistema_Nombre = dr["Nombre"].ToString(),
                            Sistema_Estatus = int.Parse(dr["Estatus"].ToString()),
                            Usuario_Registra = int.Parse(dr["UsuarioRegistra"].ToString()),
                            Fecha_Registro = dr["FechaRegistro"].ToString(),
                            Sistema_Tipo = int.Parse(dr["Tipo"].ToString())
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
        
        public void UpdateSistema(SisteamModelUpdate sistema)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = sistema.Sistema_Id});
            parametros.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = SqlDbType.VarChar, Value = sistema.Sistema_Nombre });
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = SqlDbType.Int, Value = sistema.Sistema_Estatus});
            parametros.Add(new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = SqlDbType.Int, Value = sistema.Usuario_Registra });
            parametros.Add(new SqlParameter { ParameterName = "@Tipo", SqlDbType = SqlDbType.Int, Value = sistema.Sistema_Tipo });

            try
            {
                dac.ExecuteNonQuery("sp_UpdateSistema", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        
        public void DeleteSistema(int Id)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = Id});

            try
            {
                dac.ExecuteNonQuery("sp_DeleteSistema", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }



    }

}
