using System;
using System.Collections.Generic;

namespace GrpcServiceRentMovie.Models.DB
{
    public partial class Pelicula
    {
        public Pelicula()
        {
            Reserva = new HashSet<Reserva>();
        }

        public int Idpelicula { get; set; }
        public string Nombre { get; set; }
        public int? Rating { get; set; }
        public DateTime? FechaEstreno { get; set; }
        public int? Idcategoria { get; set; }
        public string Director { get; set; }
        public int? Estado { get; set; }

        public virtual Categoria IdcategoriaNavigation { get; set; }
        public virtual ICollection<Reserva> Reserva { get; set; }
    }
}
