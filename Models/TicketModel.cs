using System;
namespace reportesApi.Models
{
    public class TicketModelInsert
    {
        public int Usuario_Registra {get; set;}
        public int Ticket_Tipo {get; set;}
        public int Modulo_Id {get; set;}
        public string Ticket_Descripcion {get; set;}
        public int Ticket_Estatus {get; set;}
    }
}