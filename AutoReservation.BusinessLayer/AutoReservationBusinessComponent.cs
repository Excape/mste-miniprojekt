using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace AutoReservation.BusinessLayer
{
    public class AutoReservationBusinessComponent
    {

        private void Insert<T>(T obj) where T : class
        {
            using (var db = new AutoReservationContext())
            {
                db.Entry(obj).State = EntityState.Added;
                db.SaveChanges();
            }
        }

        public void Update<T>(T obj) where T : class
        {
            using (var db = new AutoReservationContext())
            {
                try
                {
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    // Wie "eager" loading?
                    // Warum passiert das automatisch beim insert?
                    db.Entry(obj).Reload();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw CreateLocalOptimisticConcurrencyException(db, obj);
                }
            }
        }

        public void Delete<T>(T obj) where T : class
        {
            using (var db = new AutoReservationContext())
            {
                db.Entry(obj).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public Auto LoadAuto(int index)
        {
            using (var db = new AutoReservationContext())
            {
                return db.Autos
                    .Include(auto => auto.Reservationen)
                    .SingleOrDefault(auto => auto.Id == index);
            }
        }
        public Kunde LoadKunde(int index)
        {
            using (var db = new AutoReservationContext())
            {
                return db.Kunden
                    .Include(kunde => kunde.Reservationen)
                    .SingleOrDefault(kunde => kunde.Id == index);
            }
        }

        public Reservation LoadReservation(int index)
        {
            using (var db = new AutoReservationContext())
            {
                return db.Reservationen
                    .Include(reservation => reservation.Kunde)
                    .Include(reservation => reservation.Auto)
                    .SingleOrDefault(reservation => reservation.ReservationsNr == index);
            }
        }

        public List<Auto> LoadAllAutos()
        {
            using (var db = new AutoReservationContext())
            {
                return db.Autos
                    .Include(auto => auto.Reservationen)
                    .ToList();
            }
        }
        public List<Kunde> LoadAllKunden()
        {
            using (var db = new AutoReservationContext())
            {
                return db.Kunden
                    .Include(kunde => kunde.Reservationen)
                    .ToList();
            }
        }
        public List<Reservation> LoadAllReservations()
        {
            using (var db = new AutoReservationContext())
            {
                return db.Reservationen
                    .Include(reservation => reservation.Kunde)
                    .Include(reservation => reservation.Auto)
                    .ToList();
            }
        }


        public void InsertAuto(Auto auto)
        {
            Insert(auto);
        }

        public void UpdateAuto(Auto auto)
        {
            Update(auto);
        }

        public void DeleteAuto(Auto auto)
        {
            Delete(auto);
        }

        public void InsertKunde(Kunde customer)
        {
            Insert(customer);
        }

        public void UpdateKunde(Kunde customer)
        {
            Update(customer);
        }

        public void DeleteKunde(Kunde customer)
        {
            Delete(customer);
        }


        public void InsertReservation(Reservation reservation)
        {
            Insert(reservation);
        }

        public Reservation UpdateReservation( Reservation reservation)
        {
            Update(reservation);
            return LoadReservation(reservation.ReservationsNr);
        }

        public void DeleteReservation(Reservation reservation)
        {
            Delete(reservation);
        }

        private static LocalOptimisticConcurrencyException<T> CreateLocalOptimisticConcurrencyException<T>(AutoReservationContext context, T entity)
            where T : class
        {
            var dbEntity = (T)context.Entry(entity)
                .GetDatabaseValues()
                .ToObject();

            return new LocalOptimisticConcurrencyException<T>($"Update {typeof(T).Name}: Concurrency-Fehler", dbEntity);
        }
    }
}