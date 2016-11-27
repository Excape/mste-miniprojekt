using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AutoReservation.Dal.Entities
{
    public abstract class Auto
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string Marke { get; set; }
        public virtual int Tagestarif { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual ICollection<Reservation> Reservationen { get; set; }
    }

    public class LuxusklasseAuto : Auto
    {
        [Required]
        public int Basistarif { get; set; }
    }

    public class MittelklasseAuto : Auto
    {
        [Required]
        public override int Tagestarif { get; set; }
    }
    public class StandardAuto : Auto
    {
        [Required]
        public override int Tagestarif { get; set; }
    }
}
