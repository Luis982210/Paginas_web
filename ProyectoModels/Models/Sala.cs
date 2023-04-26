using System;
using System.Collections.Generic;

namespace ProyectoModels.Models;

public partial class Sala
{
    public int IdSala { get; set; }

    public string Ubicacion { get; set; } = null!;

    public string Encargado { get; set; } = null!;

    public string Departamento { get; set; } = null!;
}
