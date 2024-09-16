using System;

namespace reportesApi.Models
{   
    public class TipoSistemaModelInsert
    {
        public string TipoSistema_Descripcion {get; set;}
    }

    public class TipoSistemaModel
    {
        public int TipoSistema_Id {get; set;}
        public string TipoSistema_Descripcion {get; set;}
    }
}