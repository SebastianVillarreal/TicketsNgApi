using System;
namespace reportesApi.Models
{
    public class TipoTicketModelInsert
    {
        public string Tipo_Nombre {get; set;}
        public int Tipo_Estatus {get; set;}
    }

    public class TipoTicketModel
    {
        public int Tipo_Id {get; set;}
        public string Tipo_Nombre {get; set;}
        public int Tipo_Estatus {get; set;}
    }
}