using System;
namespace reportesApi.Models
{
    public class TicketUsuarioModelInsert
    {
        public int Ticket_Id {get; set;}
        public int Usuario_Id {get; set;}
    }

    public class TicketUsuarioModelGet
    {
        public int TicketUsuario_Id {get; set;}
        public int Ticket_Id {get; set;}
        public int Usuario_Id {get; set;}
        public string Fecha_Registro {get; set;}
    }

    public class TicketUsuarioModelUpdate
    {
        public int TicketUsuario_Id {get; set;}
        public int Ticket_Id {get; set;}
        public int Usuario_Id {get; set;}
    }
}