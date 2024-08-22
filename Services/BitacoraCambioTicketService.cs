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

        public List<BitacoraCambiosTicketsModelGet> GetAllBitacoraCambiosTickets()
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            List<BitacoraCambiosTicketsModelGet> lista = new List<BitacoraCambiosTicketsModelGet>();

            try
            {
                DataSet ds = dac.Fill("sp_GetAllBitacoraCambiosTickets", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new BitacoraCambiosTicketsModelGet
                        {
                            CambioTicket_Id = int.Parse(dr["Id"].ToString()),
                            Ticket_Id = int.Parse(dr["IdTicket"].ToString()),
                            CambioTicket_Accion = dr["Accion"].ToString(),
                            CambioTicket_Estatus = int.Parse(dr["Estatus"].ToString()),
                            CambioTicket_Comentario = dr["Comentarios"].ToString(),
                            Usuario_Id = int.Parse(dr["IdUsuario"].ToString()),
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

        public List<BitacoraCambiosTicketsModelGet> GetCambioTicketById(int id)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            List<BitacoraCambiosTicketsModelGet> lista = new List<BitacoraCambiosTicketsModelGet>();

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id });


            try
            {
                DataSet ds = dac.Fill("sp_GetBitacoraCambioTicketById", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new BitacoraCambiosTicketsModelGet
                        {
                            CambioTicket_Id = int.Parse(dr["Id"].ToString()),
                            Ticket_Id = int.Parse(dr["IdTicket"].ToString()),
                            CambioTicket_Accion = dr["Accion"].ToString(),
                            CambioTicket_Estatus = int.Parse(dr["Estatus"].ToString()),
                            CambioTicket_Comentario = dr["Comentarios"].ToString(),
                            Usuario_Id = int.Parse(dr["IdUsuario"].ToString()),
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
    }
}