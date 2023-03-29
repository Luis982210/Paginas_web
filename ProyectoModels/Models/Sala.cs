using System;
using System.Collections.Generic;

namespace ProyectoModels;


public partial class Sala
{
    public int Id { get; set; }

    public string NombreSala { get; set; } = null!;

    public string Ubicacion { get; set; } = null!;

    public string Encargado { get; set; } = null!;

    public int NoPersonal { get; set; }

    public string Departamento { get; set; } = null!;
}
