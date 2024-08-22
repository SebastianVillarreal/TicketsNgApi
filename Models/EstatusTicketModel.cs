using System;
namespace reportesApi.Models
{
    public class EstatusTicketModelInsert
    {
        public string Estatus_Nombre {get; set;}
        public int Estatus_Activo {get; set;}
    }

    public class EstatusTicketModelGet
    {
        public int Estatus_Id {get; set;}
        public string Estatus_Nombre {get; set;}
        public int Estatus_Activo {get; set;}
    }
}