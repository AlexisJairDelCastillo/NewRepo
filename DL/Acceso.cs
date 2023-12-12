using System;
using System.Collections.Generic;

namespace DL;

public partial class Acceso
{
    public int IdAcceso { get; set; }

    public string? Tipo { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
