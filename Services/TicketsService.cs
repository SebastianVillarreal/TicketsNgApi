using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using reportesApi.DataContext;
using reportesApi.Models;
using System.Collections.Generic;


namespace reportesApi.Services
{
    public class TicketService
    {
        private  string connection;
        
        
        public TicketService(IMarcatelDatabaseSetting settings)
        {
             connection = settings.ConnectionString;
        }

        public TicketModel InsertTicket(string fecha, int sucursal, int usuario, string folio_interno, int cliente, int tipo)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            if (cliente == 0)
                cliente = 1;
            TicketModel ticket = new TicketModel();
            try
            {
                parametros.Add(new SqlParameter { ParameterName = "@pFecha", SqlDbType = SqlDbType.VarChar, Value = fecha });
                parametros.Add(new SqlParameter { ParameterName = "@pSucursal", SqlDbType = SqlDbType.VarChar, Value = sucursal });
                parametros.Add(new SqlParameter { ParameterName = "@pUsuario", SqlDbType = SqlDbType.VarChar, Value = usuario });
                parametros.Add(new SqlParameter { ParameterName = "@pFolioInterno", SqlDbType = SqlDbType.VarChar, Value = folio_interno });
                parametros.Add(new SqlParameter { ParameterName = "@pIdCliente", SqlDbType = SqlDbType.VarChar, Value = cliente });
                parametros.Add(new SqlParameter { ParameterName = "@pTipo", SqlDbType = SqlDbType.VarChar, Value = tipo });
                DataSet ds = dac.Fill("sp_insert_ticket", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ticket.Fecha = row["Folio"].ToString();
                        ticket.Folio = row["IntFolio"].ToString();
                        ticket.Id = int.Parse(row["Id"].ToString());
                    }
                }
                return ticket;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertTicketFull(TicketModel ticket)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros.Add(new SqlParameter { ParameterName = "@pSucursal", SqlDbType = SqlDbType.VarChar, Value = ticket.IdSucursal });
            parametros.Add(new SqlParameter { ParameterName = "@pFecha", SqlDbType = SqlDbType.VarChar, Value = ticket.Fecha });
            parametros.Add(new SqlParameter { ParameterName = "@pTipo", SqlDbType = SqlDbType.VarChar, Value = 1 });
            parametros.Add(new SqlParameter { ParameterName = "@pCajero", SqlDbType = SqlDbType.VarChar, Value = ticket.IdUsuario });
            parametros.Add(new SqlParameter { ParameterName = "@pDescuento", SqlDbType = SqlDbType.VarChar, Value = ticket.Descuento });
            parametros.Add(new SqlParameter { ParameterName = "@pVenta", SqlDbType = SqlDbType.VarChar, Value = ticket.Total });
            parametros.Add(new SqlParameter { ParameterName = "@pEstatus", SqlDbType = SqlDbType.VarChar, Value = ticket.Estatus });
            parametros.Add(new SqlParameter { ParameterName = "@FolioInterno", SqlDbType = SqlDbType.VarChar, Value = ticket.FolioInterno });
            try
            {
                dac.ExecuteNonQuery("sp_insert_ticket_full", parametros);
                return 1;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return 0;
            }
            
            
        }

        public int UpdateTicket(string fecha, int folio, int id_sucursal)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            try
            {
                parametros.Add(new SqlParameter { ParameterName = "@pIdSucursal", SqlDbType = SqlDbType.VarChar, Value = id_sucursal });
                parametros.Add(new SqlParameter { ParameterName = "@pFolio", SqlDbType = SqlDbType.VarChar, Value = folio });
                parametros.Add(new SqlParameter { ParameterName = "@pFecha", SqlDbType = SqlDbType.VarChar, Value = fecha });
                dac.ExecuteNonQuery("sp_update_ticket", parametros);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }

        public string InsertRenglonTicket(RenglonTicketModel renglon)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            try
            {
                parametros.Add(new SqlParameter { ParameterName = "@pSucursal", SqlDbType = SqlDbType.VarChar, Value = renglon.IdSucursal });
                parametros.Add(new SqlParameter { ParameterName = "@pFecha", SqlDbType = SqlDbType.VarChar, Value = renglon.Fecha });
                parametros.Add(new SqlParameter { ParameterName = "@pFolio", SqlDbType = SqlDbType.VarChar, Value = renglon.Folio });
                parametros.Add(new SqlParameter { ParameterName = "@pConsecutivo", SqlDbType = SqlDbType.VarChar, Value = renglon.Consecutivo});
                parametros.Add(new SqlParameter { ParameterName = "@pArticulo", SqlDbType = SqlDbType.VarChar, Value = renglon.Articulo });
                parametros.Add(new SqlParameter { ParameterName = "@pCantidad", SqlDbType = SqlDbType.VarChar, Value = renglon.Cantidad });
                parametros.Add(new SqlParameter { ParameterName = "@pPrecio", SqlDbType = SqlDbType.VarChar, Value = renglon.Precio });
                parametros.Add(new SqlParameter { ParameterName = "@pUnidad", SqlDbType = SqlDbType.VarChar, Value = renglon.Unidad });
                parametros.Add(new SqlParameter { ParameterName = "@pDescuento", SqlDbType = SqlDbType.VarChar, Value = renglon.Descuento });
                parametros.Add(new SqlParameter { ParameterName = "@pPrecioOriginal", SqlDbType = SqlDbType.VarChar, Value = renglon.PrecioOriginal });
                parametros.Add(new SqlParameter { ParameterName = "@pImpuestos", SqlDbType = SqlDbType.VarChar, Value = renglon.Impuestos });
                parametros.Add(new SqlParameter { ParameterName = "@pIva", SqlDbType = SqlDbType.VarChar, Value = renglon.Iva });
                parametros.Add(new SqlParameter { ParameterName = "@pIeps", SqlDbType = SqlDbType.VarChar, Value = renglon.Ieps });
                parametros.Add(new SqlParameter { ParameterName = "@pIdTicket", SqlDbType = SqlDbType.VarChar, Value = renglon.IdTicket });
                dac.ExecuteNonQuery("sp_insert_renglon_ticket", parametros);
                return "correcto";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public int CancelarTicket(string fecha, int folio, int id_sucursal, string motivo, string folio_interno, int id_usuario)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            int valor = 0;
            parametros.Add(new SqlParameter { ParameterName = "@pFolio", SqlDbType = SqlDbType.VarChar, Value = folio });
            parametros.Add(new SqlParameter { ParameterName = "@pMotivo", SqlDbType = SqlDbType.VarChar, Value = motivo });
            parametros.Add(new SqlParameter { ParameterName = "@pSucursal", SqlDbType = SqlDbType.VarChar, Value = id_sucursal });
            parametros.Add(new SqlParameter { ParameterName = "@pFecha", SqlDbType = SqlDbType.VarChar, Value = fecha });
            parametros.Add(new SqlParameter { ParameterName = "@pFolioInterno", SqlDbType = SqlDbType.VarChar, Value = folio_interno });
            parametros.Add(new SqlParameter { ParameterName = "@pUsuario", SqlDbType = SqlDbType.VarChar, Value = id_usuario });
            try
            {
                dac.ExecuteNonQuery("sp_cancelar_ticket", parametros);
                return valor;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);

                throw;
            }
        }


        public int SuspenderVenta(string folio_interno, int usuario, int supervisor, int sucursal, string fecha, string motivo)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            try
            {
                parametros.Add(new SqlParameter { ParameterName = "@pFolioInterno", SqlDbType = SqlDbType.VarChar, Value = folio_interno });
                parametros.Add(new SqlParameter { ParameterName = "@pUsuario", SqlDbType = SqlDbType.Int, Value = usuario });
                parametros.Add(new SqlParameter { ParameterName = "@pSupervisor", SqlDbType = SqlDbType.Int, Value = supervisor });
                parametros.Add(new SqlParameter { ParameterName = "@pSucursal", SqlDbType = SqlDbType.Int, Value = sucursal });
                parametros.Add(new SqlParameter { ParameterName = "@pMotivo", SqlDbType = SqlDbType.VarChar, Value = motivo });
                parametros.Add(new SqlParameter { ParameterName = "@pFecha", SqlDbType = SqlDbType.VarChar, Value = fecha });
                dac.ExecuteNonQuery("sp_suspender_venta", parametros);
                return 1;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return 0;
                
            }
        }

        public List<DetalleModel>GetDetalle(string folio_interno)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            List<DetalleModel> lista = new List<DetalleModel>();
            try
            {
                DataSet ds = new DataSet();
                parametros.Add(new SqlParameter { ParameterName = "@pFolioTicket", SqlDbType = SqlDbType.VarChar, Value = folio_interno });
                ds = dac.Fill("sp_get_detalle", parametros);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new DetalleModel
                        {
                            Codigo = dr["Codigo"].ToString(),
                            UM = dr["UnidadMedida"].ToString(),
                            Cantidad = float.Parse(dr["Cantidad"].ToString()),
                            Descripcion = dr["Descripcion"].ToString(),
                            Iva = float.Parse(dr["Iva"].ToString()),
                            PrecioOriginal = float.Parse(dr["PrecioOriginal"].ToString()),
                            PrecioFinal = float.Parse(dr["PrecioFinal"].ToString()),
                            Ieps = float.Parse(dr["Ieps"].ToString()),
                            PrecioOriginalImpuestos = float.Parse(dr["PrecioOriginalImpuestos"].ToString()),
                            PrecioFinalImpuestos = float.Parse(dr["PrecioFinalImpuestos"].ToString()),
                            TotalRenglon = float.Parse(dr["TotalRenglon"].ToString()),
                            TotalRenglonImpuestos = float.Parse(dr["TotalRenglonImpuestos"].ToString()),
                            FolioTicket = dr["FolioTicket"].ToString(),
                            FolioInterno = dr["FolioInterno"].ToString(),
                            Folio = int.Parse(dr["Folio"].ToString()),
                            ConsecutivoLocal = int.Parse(dr["ConsecutivoLoca"].ToString())
                        });
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }

            return lista;
        }

        

        



    }

}



