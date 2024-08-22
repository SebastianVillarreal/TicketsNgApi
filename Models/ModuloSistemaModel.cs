using System;
namespace reportesApi.Models{
    public class ModuloSistemaModelInsert{

        public int Sistema_Id {get; set;}
        public string Modulo_Nombre {get; set;}
        public int Modulo_Estatus {get; set;}
        public int Usuario_Registra {get; set;}
    }

        public class ModuloSistemaModelGet
        {
            public int Modulo_Id {get; set;}
            public int Sistema_Id {get; set;}
            public string Modulo_Nombre {get; set;}
            public int Modulo_Estatus {get; set;}
            public int Usuario_Registra {get; set;}
            public string Fecha_Registro {get; set;}
        }

}