using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WsCinemaOc.Models
{
    public partial class Genero
    {
        public Genero()
        {
            Peliculas = new HashSet<Pelicula>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        [NotMapped]
        public virtual ICollection<Pelicula> Peliculas { get; set; }
    }
}
