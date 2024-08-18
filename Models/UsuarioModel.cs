using System;
namespace reportesApi.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string NombrePersona { get; set; }
        public int IdSucursal { get; set; }        
        public string NombreSucursal { get; set; }
        public int IdPerfil { get; set; }
    }
}
