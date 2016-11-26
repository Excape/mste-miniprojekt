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

        private void insert<T>(T obj) where T : class
        {
            using (var db = new AutoReservationContext())
            {
                db.Entry(obj).State = EntityState.Added;
                db.SaveChanges();
            }
        }

        public void update<T>(T obj) where T : class
        {
            using (var db = new AutoReservationContext())
            {
                try
                {
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw CreateLocalOptimisticConcurrencyException(db, obj);
                }
            }
        }

        public void delete<T>(T obj) where T : class
        {
            using (var db = new AutoReservationContext())
            {
                db.Entry(obj).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public Auto loadAuto(int index)
        {
            using (var db = new AutoReservationContext())
            {
                return db.Autos.SingleOrDefault(auto => auto.Id == index);
            }
        }
        public Kunde loadKunde(int index)
        {
            using (var db = new AutoReservationContext())
            {
                return db.Kunden.SingleOrDefault(kunde => kunde.Id == index);
            }
        }

        public Reservation loadReservation(int index)
        {
            using (var db = new AutoReservationContext())
            {
                return db.Reservationen.SingleOrDefault(reservation => reservation.ReservationsNr == index);
            }
        }


        public void insertAuto(Auto auto)
        {
            insert(auto);
        }

        public void updateAuto(Auto auto)
        {
            update(auto);
        }

        public void deleteAuto(Auto auto)
        {
            delete(auto);
        }

        public void insertKunde(Kunde customer)
        {
            insert(customer);
        }

        public void updateKunde(Kunde customer)
        {
            update(customer);
        }

        public void deleteKunde(Kunde customer)
        {
            delete(customer);
        }


        public void insertReservation(Reservation reservation)
        {
            insert(reservation);
        }

        public void updateReservation( Reservation reservation)
        {
            update(reservation);
        }

        public void deleteReservation(Reservation reservation)
        {
            delete(reservation);
        }

        private static LocalOptimisticConcurrencyException<T> CreateLocalOptimisticConcurrencyException<T>(AutoReservationContext context, T entity)
            where T : class
        {
            var dbEntity = (T)context.Entry(entity)
                .GetDatabaseValues()
                .ToObject();

            return new LocalOptimisticConcurrencyException<T>($"Update {typeof(Auto).Name}: Concurrency-Fehler", dbEntity);
        }
    }
}