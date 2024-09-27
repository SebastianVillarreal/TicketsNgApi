using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using reportesApi.DataContext;
using reportesApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.IO;

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


        public List<EstatusTicketModelDetail> GetAllEstatusDetail(string userId)
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
                            Tickets = this.GetTicketsByEstatus(int.Parse(dr["Id"].ToString()), userId)
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

        public List<TicketEstatusModelGet> GetTicketsByEstatus (int Estatus_Id, string userId)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            List<TicketEstatusModelGet> lista = new List<TicketEstatusModelGet>();
            parametros.Add(new SqlParameter { ParameterName = "@pIdEstatus", SqlDbType = SqlDbType.VarChar, Value = Estatus_Id });
            parametros.Add(new SqlParameter { ParameterName = "@pUserId", SqlDbType = SqlDbType.VarChar, Value = userId });
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
                            Ticket_Comentarios = dr["Comentarios"].ToString(),
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
                            Usuario_Asignado = dr["UsuarioAsignado"].ToString(),
                            Archivos = ObtenerArchivosPorId(dr["Id"].ToString())
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
        public ArchivoInfo[] ObtenerArchivosPorId(string id)
        {
            string carpeta = Path.Combine(Directory.GetCurrentDirectory(), "uploads", id);
            if (!Directory.Exists(carpeta))
            {
                return new ArchivoInfo[0];
            }
            var archivos = Directory.GetFiles(carpeta);
            var archivosInfo = archivos.Select(archivo =>
            {
                FileInfo fi = new FileInfo(archivo);
                return new ArchivoInfo { FileName = fi.Name, Format = fi.Extension.Split(".")[1] };
            }).ToArray();

            return archivosInfo;
        }
    }
}