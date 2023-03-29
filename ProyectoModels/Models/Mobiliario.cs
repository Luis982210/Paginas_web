using System;
using System.Collections.Generic;

namespace ProyectoModels;

public partial class Mobiliario
{
    public int Id { get; set; }

    public int IdSala { get; set; }

    public string Mobiliario1 { get; set; } = null!;

    public decimal Precio { get; set; }

    public int Cantidad { get; set; }

    public string Descripcion { get; set; } = null!;

    public int CodigoMobiliario { get; set; }
}
