namespace reportesApi.Models
{
    public class CambioTicketModelInsert
    {
        public int Ticket_Id {get; set;}
        public string CambioTicket_Accion {get; set;}
        public int CambioTicket_Estatus {get; set;}
        public string CambioTicket_Comentario {get; set;}
        public int Usuario_Id {get; set;}
    }
}