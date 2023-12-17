using System;
using System.Collections.Generic;

namespace apibackend.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}
