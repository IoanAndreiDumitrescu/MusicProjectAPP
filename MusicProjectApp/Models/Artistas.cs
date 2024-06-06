using System;
using System.Collections.Generic;

namespace MusicProjectApp.Models;

public partial class Artistas
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Genero { get; set; }

    public DateOnly? Fecha { get; set; }

    public virtual ICollection<Canciones> Canciones { get; set; } = new List<Canciones>();

    public virtual ICollection<Festival> Festival { get; set; } = new List<Festival>();
}
