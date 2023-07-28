using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WsCinemaOc.Models
{
    public partial class Pelicula
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public int AñoLanzamiento { get; set; }
        public int? GeneroId { get; set; }
        [NotMapped]
        public string ImagenPortadaBase64 { get; set; }
        public byte[]? ImagenPortada { get; set; }

        public virtual Genero? Genero { get; set; }
    }
}
