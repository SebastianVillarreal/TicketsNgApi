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

        public List<TicketUsuarioModelGet> GetAllTicketsUsuarios()
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            List<TicketUsuarioModelGet> lista = new List<TicketUsuarioModelGet>();

            try
            {
                DataSet ds = dac.Fill("sp_GetAllTicketsUsuarios", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new TicketUsuarioModelGet
                        {
                            TicketUsuario_Id = int.Parse(dr["Id"].ToString()),
                            Ticket_Id = int.Parse(dr["IdTicket"].ToString()),
                            Usuario_Id = int.Parse(dr["IdUsuarioAsignado"].ToString()),
                            Fecha_Registro = dr["Fecha"].ToString(),
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

        public List<TicketUsuarioModelGet> GetTicketUsuarioById(int id)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            List<TicketUsuarioModelGet> lista = new List<TicketUsuarioModelGet>();

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id });


            try
            {
                DataSet ds = dac.Fill("sp_GetTicketUsuarioById", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new TicketUsuarioModelGet
                        {
                            TicketUsuario_Id = int.Parse(dr["Id"].ToString()),
                            Ticket_Id = int.Parse(dr["IdTicket"].ToString()),
                            Usuario_Id = int.Parse(dr["IdUsuarioAsignado"].ToString()),
                            Fecha_Registro = dr["Fecha"].ToString(),
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

        public void UpdateTicketUsuario(TicketUsuarioModelUpdate ticketUsuario)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = ticketUsuario.TicketUsuario_Id});
            parametros.Add(new SqlParameter { ParameterName = "@IdTicket", SqlDbType = SqlDbType.Int, Value = ticketUsuario.Ticket_Id });
            parametros.Add(new SqlParameter { ParameterName = "@IdUsuarioAsignado", SqlDbType = SqlDbType.Int, Value = ticketUsuario.Usuario_Id});

            try
            {
                dac.ExecuteNonQuery("sp_UpdateTicketUsuario", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

    }
}