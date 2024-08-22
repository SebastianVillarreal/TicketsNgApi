using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using reportesApi.DataContext;
using reportesApi.Models;
using System.Collections.Generic;

namespace reportesApi.Services
{
    public class TicketUsuarioService
    {
        private  string connection;
        
        
        public TicketUsuarioService(IMarcatelDatabaseSetting settings)
        {
             connection = settings.ConnectionString;
        }

        public void InsertTicketUsuario(TicketUsuarioModelInsert ticketUsuario)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            parametros.Add(new SqlParameter { ParameterName = "@IdTicket", SqlDbType = SqlDbType.Int, Value = ticketUsuario.Ticket_Id });
            parametros.Add(new SqlParameter { ParameterName = "@IdUsuarioAsignado", SqlDbType = SqlDbType.Int, Value = ticketUsuario.Usuario_Id});

            try
            {
                dac.ExecuteNonQuery("sp_InsertTicketUsuario", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

    }
}