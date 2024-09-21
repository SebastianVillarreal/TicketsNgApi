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
    public class TicketEstatusService
    {
        private string connection;

        public TicketEstatusService(IMarcatelDatabaseSetting settings)
        {
            connection = settings.ConnectionString;
        }

        public List<TicketEstatusModelGet> GetTicketEstatus(string FechaInicial, string FechaFinal)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            List<TicketEstatusModelGet> lista = new List<TicketEstatusModelGet>();

            parametros.Add(new SqlParameter { ParameterName = "@pFechaInicial", SqlDbType = SqlDbType.VarChar, Value = FechaInicial});
            parametros.Add(new SqlParameter { ParameterName = "@pFechaFinal", SqlDbType = SqlDbType.VarChar, Value = FechaFinal});
            try
            {
                DataSet ds = dac.Fill("GetTicketsEstatus", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new TicketEstatusModelGet
                        {
                            Ticket_Id = int.Parse(dr["Id"].ToString()),
                            Ticket_Titulo = dr["Titulo"].ToString(),
                            Ticket_Descripcion = dr["Descripcion"].ToString(),
                            Comentarios = dr["Comentarios"].ToString(),
                            Ticket_Fecha = dr["Fecha"].ToString(),
                            Tipo_Ticket_Id = int.Parse(dr["TipoId"].ToString()),
                            Tipo_Ticket_Nombre = dr["Tipo"].ToString(),
                            Estatus_Id = int.Parse(dr["EstatusId"].ToString()),
                            Estatus_Nombre = dr["Estatus"].ToString(),
                            Modulo_Id = int.Parse(dr["ModuloId"].ToString()),
                            Modulo_Nombre = dr["Modulo"].ToString(),
                            Sistema_Id = int.Parse(dr["SistemaId"].ToString()),
                            Sistema_Nombre = dr["Sistema"].ToString(),
                            Usuario_Registra = dr["UsuarioRegistra"].ToString(),
                            Usuario_Asignado = dr["UsuarioAsignado"].ToString()
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