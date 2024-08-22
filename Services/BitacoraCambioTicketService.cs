using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using reportesApi.DataContext;
using reportesApi.Models;
using System.Collections.Generic;

namespace reportesApi.Services
{
    public class BitacoraCambioTicketService
    {
        private  string connection;
        
        
        public BitacoraCambioTicketService(IMarcatelDatabaseSetting settings)
        {
             connection = settings.ConnectionString;
        }

        public void InsertCambioTicket(CambioTicketModelInsert cambio)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            parametros.Add(new SqlParameter { ParameterName = "@IdTicket", SqlDbType = SqlDbType.VarChar, Value = cambio.Ticket_Id });
            parametros.Add(new SqlParameter { ParameterName = "@Accion", SqlDbType = SqlDbType.VarChar, Value = cambio.CambioTicket_Accion });
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = SqlDbType.Int, Value = cambio.CambioTicket_Estatus});
            parametros.Add(new SqlParameter { ParameterName = "@Comentarios", SqlDbType = SqlDbType.VarChar, Value = cambio.CambioTicket_Comentario });
            parametros.Add(new SqlParameter { ParameterName = "@IdUsuario", SqlDbType = SqlDbType.Int, Value = cambio.Usuario_Id });

            try
            {
                dac.ExecuteNonQuery("sp_InsertBitacoraCambioTicket", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}