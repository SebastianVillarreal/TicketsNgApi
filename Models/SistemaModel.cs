using System;
namespace reportesApi.Models{
    public class SistemaModelInsert{
        public string Sistema_Nombre {get; set;}
        public int Sistema_Estatus {get; set;}
        public int Usuario_Registra {get; set;}
        public int Tipo_Id {get; set;}
    }

    public class SistemaModelGet{
        public int Sistema_Id {get; set;}
        public string Sistema_Nombre {get; set;}
        public int Sistema_Estatus {get; set;}
        public string Usuario_Registra {get; set;}
        public string Fecha_Registro {get; set;}
        public int Tipo_Id {get; set;}
        public string Tipo_Descripcion {get; set;}
    }

    public class SisteamModelUpdate{
        public int Sistema_Id {get; set;}
        public string Sistema_Nombre {get; set;}
        public int Sistema_Estatus {get; set;}
        public int Usuario_Registra {get; set;}
        public int Tipo_Id {get; set;}
    }
}