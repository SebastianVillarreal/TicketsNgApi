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
    public class UsuarioSistemaService
    {
        private  string connection;
        
        
        public UsuarioSistemaService(IMarcatelDatabaseSetting settings)
        {
             connection = settings.ConnectionString;
        }

        public void InsertUsuarioSistema(UsuarioSistemaModelInsert UsuarioSistema)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            parametros.Add(new SqlParameter { ParameterName = "@IdSistema", SqlDbType = SqlDbType.Int, Value = UsuarioSistema.Sistema_Id });
            parametros.Add(new SqlParameter { ParameterName = "@IdUsuario", SqlDbType = SqlDbType.Int, Value = UsuarioSistema.Usuario_Id });
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = SqlDbType.Int, Value = UsuarioSistema.Estatus});

            try
            {
                dac.ExecuteNonQuery("sp_InsertUsuarioSistema", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UsuarioSistemaModelGet> GetAllUsuariosSistemas()
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            List<UsuarioSistemaModelGet> lista = new List<UsuarioSistemaModelGet>();

            try
            {
                DataSet ds = dac.Fill("sp_GetAllUsuariosSistemas", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new UsuarioSistemaModelGet
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            Sistema_Id = int.Parse(dr["IdSistema"].ToString()),
                            Usuario_Id = int.Parse(dr["IdUsuario"].ToString()),
                            Sistema = dr["Sistema"].ToString(),
                            Usuario = dr["Usuario"].ToString(),
                            Estatus = int.Parse(dr["Estatus"].ToString()),
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
        
        public void UpdateUsuarioSistema(UsuarioSistemaModelUpdate UsuarioSistema)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = UsuarioSistema.Id});
            parametros.Add(new SqlParameter { ParameterName = "@IdSistema", SqlDbType = SqlDbType.Int, Value = UsuarioSistema.Sistema_Id });
            parametros.Add(new SqlParameter { ParameterName = "@IdUsuario", SqlDbType = SqlDbType.Int, Value = UsuarioSistema.Usuario_Id });
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = SqlDbType.Int, Value = UsuarioSistema.Estatus});

            try
            {
                dac.ExecuteNonQuery("sp_UpdateUsuarioSistema", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        
        public void DeleteUsuarioSistema(int Id)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = Id});

            try
            {
                dac.ExecuteNonQuery("sp_DeleteUsuarioSistema", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }



    }

}
