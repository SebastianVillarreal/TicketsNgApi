using System;

public class SistemaModelGet{
    public int Sistema_Id {get; set;}
    public string Sistema_Nombre {get; set;}
    public int Sistema_Estatus {get; set;}
    public int Usuario_Registra {get; set;}
    public string Fecha_Registro {get; set;}
    public int Sistema_Tipo {get; set;}
}

public class SistemaModelInsert{
    public string Sistema_Nombre {get; set;}
    public int Sistema_Estatus {get; set;}
    public int Usuario_Registra {get; set;}
    public int Sistema_Tipo {get; set;}
}

public class SistemaModelUpdate{
    public int Sistema_Id {get; set;}
    public string Sistema_Nombre {get; set;}
    public int Sistema_Estatus {get; set;}
    public int Usuario_Registra {get; set;}
    public int Sistema_Tipo {get; set;}
}