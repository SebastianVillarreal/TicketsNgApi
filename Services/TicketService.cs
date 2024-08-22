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
    public class TicketService
    {
        private  string connection;
        
        
        public TicketService(IMarcatelDatabaseSetting settings)
        {
             connection = settings.ConnectionString;
        }

        
        public void InsertTicket(TicketModelInsert ticket)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            parametros.Add(new SqlParameter { ParameterName = "@IdUsuarioRegistra", SqlDbType = SqlDbType.Int, Value = ticket.Usuario_Registra });
            parametros.Add(new SqlParameter { ParameterName = "@TipoTicket", SqlDbType = SqlDbType.Int, Value = ticket.Ticket_Tipo });
            parametros.Add(new SqlParameter { ParameterName = "@IdModulo", SqlDbType = SqlDbType.Int, Value = ticket.Modulo_Id });
            parametros.Add(new SqlParameter { ParameterName = "@Descripcion", SqlDbType = SqlDbType.VarChar, Value = ticket.Ticket_Descripcion });
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = SqlDbType.Int, Value = ticket.Ticket_Estatus});

            try
            {
                dac.ExecuteNonQuery("sp_InsertTicket", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TicketModelGet> GetAllTickets()
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            List<TicketModelGet> lista = new List<TicketModelGet>();

            try
            {
                DataSet ds = dac.Fill("sp_GetAllTickets", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new TicketModelGet
                        {
                            Ticket_Id = int.Parse(dr["Id"].ToString()),
                            Usuario_Registra = int.Parse(dr["IdUsuarioRegistra"].ToString()),
                            Ticket_Tipo = int.Parse(dr["TipoTicket"].ToString()),
                            Modulo_Id = int.Parse(dr["IdModulo"].ToString()),
                            Ticket_Descripcion = dr["Descripcion"].ToString(),
                            Fecha_Registro = dr["Fecha"].ToString(),
                            Ticket_Estatus = int.Parse(dr["Estatus"].ToString())
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