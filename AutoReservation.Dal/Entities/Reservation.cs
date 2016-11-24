using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    public class Reservation
    {
        // ID ist nicht in Vorgabe
        public int Id { get; set; }
        [Required]
        public DateTime Von { get; set; }
        [Required]
        public DateTime Bis { get; set; }
        [Required]
        public int AutoId { get; set; }
        [Required]
        public int KundeId { get; set; }
        [Required]
        public int ReservationsNr { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual Auto Auto { get; set; }
        public virtual Kunde Kunde { get; set; }
    }
}
