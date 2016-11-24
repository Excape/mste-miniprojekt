using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AutoReservation.Dal.Entities
{
    public abstract class Auto
    {
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string Marke { get; set; }
        public int Tagestarif { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }

    public class LuxusklasseAuto : Auto
    {
        [Required]
        public int Basistarif { get; set; }
    }

    public class MittelklasseAuto : Auto { }
    public class StandardAuto : Auto { }
}
