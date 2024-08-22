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
    public class EstatusTicketService
    {
        private  string connection;
        
        public EstatusTicketService(IMarcatelDatabaseSetting settings)
        {
             connection = settings.ConnectionString;
        }

        public void InsertEstatus(EstatusTicketModelInsert estatus)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            parametros.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = SqlDbType.VarChar, Value = estatus.Estatus_Nombre });
            parametros.Add(new SqlParameter { ParameterName = "@Activo", SqlDbType = SqlDbType.Int, Value = estatus.Estatus_Activo});

            try
            {
                dac.ExecuteNonQuery("sp_InsertEstatusTicket", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public List<EstatusTicketModelGet> GetAllEstatus()
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            List<EstatusTicketModelGet> lista = new List<EstatusTicketModelGet>();

            try
            {
                DataSet ds = dac.Fill("sp_GetAllEstatusTickets", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new EstatusTicketModelGet
                        {
                            Estatus_Id = int.Parse(dr["Id"].ToString()),
                            Estatus_Nombre = dr["Nombre"].ToString(),
                            Estatus_Activo = int.Parse(dr["Activo"].ToString()),
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