using System;
using System.Collections.Generic;

namespace ProyectoModels.Models;

public partial class Mobiliario
{
    public int IdMobiliario { get; set; }

    public int IdSala { get; set; }

    public string Nombre { get; set; } = null!;

    public string Precio { get; set; } = null!;

    public string Descripcion { get; set; } = null!;
}
