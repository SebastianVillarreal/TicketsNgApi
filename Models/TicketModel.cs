using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace reportesApi.Models
{
    public class TicketModelInsert
    {
        public int Usuario_Registra {get; set;}
        public int Ticket_Tipo {get; set;}
        public int Modulo_Id {get; set;}
        public string Ticket_Descripcion {get; set;}
        public string Ticket_Comentarios {get; set;}

        public string Ticket_Titulo {get; set;}
        public int Ticket_Estatus {get; set;}
        public IEnumerable<IFormFile> Ticket_Archivos { get; set; }

    }

    public class TicketModelGet
    {
        public int Ticket_Id { get; set; }
        public int Usuario_Registra {get; set;}
        public int Ticket_Tipo {get; set;}
        public int Modulo_Id {get; set;}
        public string Ticket_Descripcion {get; set;}
        public string Ticket_Comentarios {get; set;}
        public string Fecha_Registro {get; set;}
        public int Ticket_Estatus {get; set;}
    }

    public class TicketModelUpdate
    {
        public int Ticket_Id { get; set; }
        public int Usuario_Registra {get; set;}
        public int Usuario_Asignado {get; set;}
        public int Ticket_Tipo {get; set;}
        public int Modulo_Id {get; set;}
        public string Ticket_Descripcion {get; set;}
        public string Ticket_Comentarios {get; set;}

        public string Ticket_Titulo {get; set;}
        public int Ticket_Estatus {get; set;}
    }
}