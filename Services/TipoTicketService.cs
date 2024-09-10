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
    public class TipoTicketService
    {
        private string connection;

        public TipoTicketService(IMarcatelDatabaseSetting settings)
        {
            connection = settings.ConnectionString;
        }

        public void InsertTipo(TipoTicketModelInsert tipo)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            parametros.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = SqlDbType.VarChar, Value = tipo.Tipo_Nombre });
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = SqlDbType.Int, Value = tipo.Tipo_Estatus});        

            try
            {
                dac.ExecuteNonQuery("sp_InsertTipoTicket", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

         public List<TipoTicketModel> GetAllTipos()
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            List<TipoTicketModel> lista = new List<TipoTicketModel>();

            try
            {
                DataSet ds = dac.Fill("sp_GetAllTiposTickets", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new TipoTicketModel
                        {
                            Tipo_Id = int.Parse(dr["Id"].ToString()),
                            Tipo_Nombre = dr["Nombre"].ToString(),
                            Tipo_Estatus = int.Parse(dr["Estatus"].ToString()),
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

        public List<TipoTicketModel> GetTipoById(int id)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            List<TipoTicketModel> lista = new List<TipoTicketModel>();

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id });


            try
            {
                DataSet ds = dac.Fill("sp_GetTipoTicketById", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new TipoTicketModel
                        {
                            Tipo_Id = int.Parse(dr["Id"].ToString()),
                            Tipo_Nombre = dr["Nombre"].ToString(),
                            Tipo_Estatus = int.Parse(dr["Estatus"].ToString()),
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

        public void UpdateTipo(TipoTicketModel tipo)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.VarChar, Value = tipo.Tipo_Id });
            parametros.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = SqlDbType.VarChar, Value = tipo.Tipo_Nombre });
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = SqlDbType.Int, Value = tipo.Tipo_Estatus});

            try
            {
                dac.ExecuteNonQuery("sp_UpdateTipoTicket", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

         public void DeleteTipo(int Id)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = Id});

            try
            {
                dac.ExecuteNonQuery("sp_DeleteTipoTicket", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

    }

}