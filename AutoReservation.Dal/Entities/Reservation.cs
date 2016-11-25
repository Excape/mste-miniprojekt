using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    public class Reservation
    {
        [Key]
        [Column("Id")]
        public int ReservationsNr { get; set; }
        [Required]
        public DateTime Von { get; set; }
        [Required]
        public DateTime Bis { get; set; }
        [Required]
        public int AutoId { get; set; }
        [Required]
        public int KundeId { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [ForeignKey("AutoId"), InverseProperty("Reservationen")]
        public Auto Auto { get; set; }
        [ForeignKey("KundeId"), InverseProperty("Reservationen")]
        public Kunde Kunde { get; set; }
    }
}