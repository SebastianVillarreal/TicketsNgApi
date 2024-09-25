using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using reportesApi.DataContext;
using reportesApi.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace reportesApi.Services
{
    public class TicketService
    {
        private  string connection;
        private readonly IWebHostEnvironment _hostingEnvironment;


        public TicketService(IMarcatelDatabaseSetting settings, IWebHostEnvironment hostingEnviroment)
        {
             connection = settings.ConnectionString;
             _hostingEnvironment = hostingEnviroment;
        }

        
        public void InsertTicket(TicketModelInsert ticket)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            parametros.Add(new SqlParameter { ParameterName = "@IdUsuarioRegistra", SqlDbType = SqlDbType.Int, Value = ticket.Usuario_Registra });
            parametros.Add(new SqlParameter { ParameterName = "@TipoTicket", SqlDbType = SqlDbType.Int, Value = ticket.Ticket_Tipo });
            parametros.Add(new SqlParameter { ParameterName = "@IdModulo", SqlDbType = SqlDbType.Int, Value = ticket.Modulo_Id });
            parametros.Add(new SqlParameter { ParameterName = "@Descripcion", SqlDbType = SqlDbType.VarChar, Value = ticket.Ticket_Descripcion });
            parametros.Add(new SqlParameter { ParameterName = "@Comentarios", SqlDbType = SqlDbType.VarChar, Value = ticket.Ticket_Comentarios  });
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = SqlDbType.Int, Value = ticket.Ticket_Estatus});
            parametros.Add(new SqlParameter {ParameterName = "@Titulo", SqlDbType = SqlDbType.VarChar, Value = ticket.Ticket_Titulo});

            try
            {

                DataSet ds = dac.Fill("sp_InsertTicket", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    foreach (IFormFile file in ticket.Ticket_Archivos)
                    {
                        if (file.Length > 0)
                        {
                            FileInfo fi = new FileInfo(file.FileName);
                            string id = ds.Tables[0].Rows[0]["Id"].ToString();
                            string uploads = Path.Combine(Directory.GetCurrentDirectory(), "uploads", id);
                            if (!Directory.Exists(uploads))
                            {
                                Directory.CreateDirectory(uploads);
                            }
                            string filePath = Path.Combine(uploads, file.FileName);
                            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                file.CopyToAsync(fileStream);
                            }
                        }
                    }
                }
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
                            Ticket_Comentarios = dr["Comentarios"].ToString(),
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

        public List<TicketModelGet> GetTicketById(int id)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            List<TicketModelGet> lista = new List<TicketModelGet>();

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id });


            try
            {
                DataSet ds = dac.Fill("sp_GetTicketById", parametros);
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
                            Ticket_Comentarios = dr["Comentarios"].ToString(),
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
        public void UpdateTicket(TicketModelUpdate ticket)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = ticket.Ticket_Id });
            parametros.Add(new SqlParameter { ParameterName = "@IdUsuarioRegistra", SqlDbType = SqlDbType.Int, Value = ticket.Usuario_Registra });
            parametros.Add(new SqlParameter { ParameterName = "@TipoTicket", SqlDbType = SqlDbType.Int, Value = ticket.Ticket_Tipo });
            parametros.Add(new SqlParameter { ParameterName = "@IdModulo", SqlDbType = SqlDbType.Int, Value = ticket.Modulo_Id });
            parametros.Add(new SqlParameter { ParameterName = "@Descripcion", SqlDbType = SqlDbType.VarChar, Value = ticket.Ticket_Descripcion });
            parametros.Add(new SqlParameter { ParameterName = "@Comentarios", SqlDbType = SqlDbType.VarChar, Value = ticket.Ticket_Comentarios });
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = SqlDbType.Int, Value = ticket.Ticket_Estatus});
            parametros.Add(new SqlParameter {ParameterName = "@Titulo", SqlDbType = SqlDbType.VarChar, Value = ticket.Ticket_Titulo});


            try
            {
                dac.ExecuteNonQuery("sp_UpdateTicket", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        
        public void DeleteTicket(int Id)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = Id});

            try
            {
                dac.ExecuteNonQuery("sp_DeleteTicket", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

    }
}