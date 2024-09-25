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

        public List<EstatusTicketModel> GetAllEstatus()
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            List<EstatusTicketModel> lista = new List<EstatusTicketModel>();

            try
            {
                DataSet ds = dac.Fill("sp_GetAllEstatusTickets", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new EstatusTicketModel
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

        public List<EstatusTicketModel> GetEstatusById(int id)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            List<EstatusTicketModel> lista = new List<EstatusTicketModel>();

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id });


            try
            {
                DataSet ds = dac.Fill("sp_GetEstatusTicketById", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new EstatusTicketModel
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

        public void UpdateEstatus(EstatusTicketModel estatus)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.VarChar, Value = estatus.Estatus_Id });
            parametros.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = SqlDbType.VarChar, Value = estatus.Estatus_Nombre });
            parametros.Add(new SqlParameter { ParameterName = "@Activo", SqlDbType = SqlDbType.Int, Value = estatus.Estatus_Activo});

            try
            {
                dac.ExecuteNonQuery("sp_UpdateEstatusTicket", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void DeleteEstatus(int Id)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = Id});

            try
            {
                dac.ExecuteNonQuery("sp_DeleteEstatusTicket", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }


        public List<EstatusTicketModelDetail> GetAllEstatusDetail()
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            List<EstatusTicketModelDetail> lista = new List<EstatusTicketModelDetail>();
            try
            {
                DataSet ds = dac.Fill("sp_GetAllEstatusTickets", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new EstatusTicketModelDetail
                        {
                            Estatus_Id = int.Parse(dr["Id"].ToString()),
                            Estatus_Nombre = dr["Nombre"].ToString(),
                            Estatus_Activo = int.Parse(dr["Activo"].ToString()),
                            Tickets = this.GetTicketsByEstatus(int.Parse(dr["Id"].ToString()))
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }

            return lista;
        }

        public List<TicketEstatusModelGet> GetTicketsByEstatus (int Estatus_Id)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            List<TicketEstatusModelGet> lista = new List<TicketEstatusModelGet>();
            parametros.Add(new SqlParameter { ParameterName = "@pIdEstatus", SqlDbType = SqlDbType.VarChar, Value = Estatus_Id });
            try
            {
                DataSet ds = dac.Fill("GetTicketsByEstatus", parametros);
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
                            Usuario_Asignado_Id = int.Parse(dr["UsuarioAsignadoId"].ToString()),
                            Usuario_Asignado = dr["UsuarioAsignado"].ToString()
                        });
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
    }
}