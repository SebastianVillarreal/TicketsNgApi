using System;
using System.IO;
namespace reportesApi.Models
{
    public class TicketEstatusModelGet
    {
        public int Ticket_Id {get; set;}
        public string Ticket_Titulo {get; set;}
        public string Ticket_Descripcion {get; set;}
        public int Tipo_Ticket_Id {get; set;}
        public string Tipo_Ticket_Nombre {get; set;}
        public string Ticket_Fecha {get; set;}
        public int Estatus_Id {get; set;}
        public string Estatus_Nombre {get; set;}
        public int Sistema_Id {get; set;}
        public string Sistema_Nombre {get; set;}
        public int Modulo_Id {get; set;}
        public string Modulo_Nombre {get; set;}
        public string Usuario_Registra {get; set;}
        public int Usuario_Asignado_Id {get; set;}
        public string Usuario_Asignado {get; set;}
        public string Ticket_Comentarios {get; set;}
        public ArchivoInfo[] Archivos {get; set;}
    }

    public class ArchivoInfo
    {
        public string FileName { get; set; }
        public string Format { get; set; }
    }
}