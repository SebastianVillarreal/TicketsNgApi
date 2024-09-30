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
    public class UsuariosService
    {
        private string connection;

        public UsuariosService(IMarcatelDatabaseSetting settings)
        {
            connection = settings.ConnectionString;
        }

        public List<UsuarioModelGet> GetAllUsuarios()
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            List<UsuarioModelGet> lista = new List<UsuarioModelGet>();

            try
            {
                DataSet ds = dac.Fill("sp_GetAllUsuarios", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        lista.Add(new UsuarioModelGet{
                            Id = int.Parse(row["Id"].ToString()),
                            NombreUsuario = row["NombreUsuario"].ToString()
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

    }
}