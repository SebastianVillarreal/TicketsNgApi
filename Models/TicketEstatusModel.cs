using System;
namespace reportesApi.Models
{
    public class TicketEstatusModelGet
    {
        public int Ticket_Id {get; set;}
        public string Ticket_Descripcion {get; set;}
        public string Ticket_Fecha {get; set;}
        public int Tipo_Ticket_Id {get; set;}
        public string Tipo_Ticket_Nombre {get; set;}
        public string Modulo_Nombre {get; set;}
        public string Sistema_Nombre {get; set;}
        public int Estatus_Id {get; set;}
        public string Estatus_Nombre {get; set;}
        public string Usuario_Registra {get; set;}
        public string Usuario_Asignado {get; set;}
    }
}