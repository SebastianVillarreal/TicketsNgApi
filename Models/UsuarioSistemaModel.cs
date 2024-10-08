using System;
namespace reportesApi.Models{
    public class UsuarioSistemaModelInsert{
        public int Sistema_Id {get; set;}
        public int Usuario_Id {get; set;}
        public int Estatus {get; set;}
    }

    public class UsuarioSistemaModelGet{
        public int Id {get; set;}
        public string Sistema {get; set;}
        public string Usuario {get; set;}
        public int Estatus {get; set;}
    }

    public class UsuarioSistemaModelUpdate{
        public int Id {get; set;}
        public int Sistema_Id {get; set;}
        public int Usuario_Id {get; set;}
        public int Estatus {get; set;}
    }
}