using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AutoReservation.Dal.Entities
{
  public class Kunde
    {
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string Vorname { get; set; }
        [Required, MaxLength(20)]
        public string Nachname { get; set; }
        [Required]
        public DateTime Geburtsdatum { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
