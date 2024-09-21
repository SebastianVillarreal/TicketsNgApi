using System;
using System.IO;
namespace reportesApi.Models
{
    public class TicketEstatusModelGet
    {
        public int Ticket_Id {get; set;}
        public string Ticket_Descripcion {get; set;}
        public int Tipo_Ticket_Id {get; set;}
        public string Tipo_Ticket_Nombre {get; set;}
        public string Ticket_Fecha {get; set;}
        public int Estatus_Id {get; set;}
        public string Estatus_Nombre {get; set;}
        public string Usuario_Registra {get; set;}
        public string Usuario_Asignado {get; set;}
        public string Comentarios {get; set;}
        public fileModel Adjuntos {get; set;}

    }
}