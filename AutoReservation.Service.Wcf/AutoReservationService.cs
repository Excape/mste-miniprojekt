using AutoReservation.BusinessLayer;
using AutoReservation.Common.Interfaces;
using System;
using System.Diagnostics;
using AutoReservation.Common.DataTransferObjects;
using System.Collections.Generic;
using AutoReservation.Dal.Entities;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {

        private AutoReservationBusinessComponent autoReservationBusinessComponent = new AutoReservationBusinessComponent();

        public List<AutoDto> Autos
        {
            get
            {
                List<Auto> autoList = autoReservationBusinessComponent.LoadAllAutos();
                return autoList.ConvertToDtos();
            }
        }

        public List<KundeDto> Kunden
        {
            get
            {
                List<Kunde> kundeList = autoReservationBusinessComponent.LoadAllKunden();
                return kundeList.ConvertToDtos();
            }
        }

        public List<ReservationDto> Reservationen
        {
            get
            {
                List<Reservation> reservationList = autoReservationBusinessComponent.LoadAllReservations();
                return reservationList.ConvertToDtos();
            }
        }

        public void DeleteAuto(AutoDto autoDto)
        {
            autoReservationBusinessComponent.DeleteAuto(autoDto.ConvertToEntity());
        }

        public void DeleteKunde(KundeDto kundeDto)
        {
            autoReservationBusinessComponent.DeleteKunde(kundeDto.ConvertToEntity());
        }

        public void DeleteReservation(ReservationDto reservationDto)
        {
            autoReservationBusinessComponent.DeleteReservation(reservationDto.ConvertToEntity());
        }

        public AutoDto InsertAuto(AutoDto autoDto)
        {
            Auto auto = autoDto.ConvertToEntity();
            autoReservationBusinessComponent.InsertAuto(auto);
            return auto.ConvertToDto();
        }

        public KundeDto InsertKunde(KundeDto kundeDto)
        {
            Kunde kunde = kundeDto.ConvertToEntity();
            autoReservationBusinessComponent.InsertKunde(kunde);
            return kunde.ConvertToDto();
        }

        public ReservationDto InsertReservation(ReservationDto reservationDto)
        {
            Reservation reservation = reservationDto.ConvertToEntity();
            autoReservationBusinessComponent.InsertReservation(reservation);
            return reservation.ConvertToDto();
        }

        public AutoDto UpdateAuto(AutoDto autoDto)
        {
            Auto auto = autoDto.ConvertToEntity();
            autoReservationBusinessComponent.UpdateAuto(auto);
            return auto.ConvertToDto();
        }

        public KundeDto UpdateKunde(KundeDto kundeDto)
        {
            Kunde kunde = kundeDto.ConvertToEntity();
            autoReservationBusinessComponent.UpdateKunde(kunde);
            return kunde.ConvertToDto();
        }

        public ReservationDto UpdateReservation(ReservationDto reservationDto)
        {
            Reservation reservation = reservationDto.ConvertToEntity();
            autoReservationBusinessComponent.UpdateReservation(reservation);
            return reservation.ConvertToDto();
        }
    }
}