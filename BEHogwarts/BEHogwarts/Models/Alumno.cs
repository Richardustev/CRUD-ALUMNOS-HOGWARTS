using System;
using System.Collections.Generic;

namespace BEHogwarts.Models;

public partial class Alumno
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Identificacion { get; set; }

    public int? Edad { get; set; }

    public string? Casa { get; set; }
}
