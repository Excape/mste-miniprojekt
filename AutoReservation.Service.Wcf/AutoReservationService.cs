﻿using AutoReservation.BusinessLayer;
using AutoReservation.Common.Interfaces;
using System;
using System.Diagnostics;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.FaultExceptions;
using System.Collections.Generic;
using AutoReservation.Dal.Entities;
using System.ServiceModel;

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

        public AutoDto GetAutoById(int id)
        {
            return autoReservationBusinessComponent.LoadAuto(id).ConvertToDto();
        }

        public KundeDto GetKundeById(int id)
        {
            return autoReservationBusinessComponent.LoadKunde(id).ConvertToDto();
        }

        public ReservationDto GetReservationByNr(int reservationsNr)
        {
            return autoReservationBusinessComponent.LoadReservation(reservationsNr).ConvertToDto();
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
            try
            {
                Auto auto = autoDto.ConvertToEntity();
                autoReservationBusinessComponent.UpdateAuto(auto);
                return auto.ConvertToDto();
            } catch (LocalOptimisticConcurrencyException<Auto> ex)
            {
                OptimisticConcurrencyFaultContract ocfc = new OptimisticConcurrencyFaultContract
                {
                    Operation = "UpdateAuto",
                    Message = ex.Message
                };
                throw new FaultException<OptimisticConcurrencyFaultContract>(ocfc);
            }
        }

        public KundeDto UpdateKunde(KundeDto kundeDto)
        {
            try { 
                Kunde kunde = kundeDto.ConvertToEntity();
                autoReservationBusinessComponent.UpdateKunde(kunde);
                return kunde.ConvertToDto();
            } catch (LocalOptimisticConcurrencyException<Kunde> ex)
            {
                OptimisticConcurrencyFaultContract ocfc = new OptimisticConcurrencyFaultContract
                {
                    Operation = "UpdateKunde",
                    Message = ex.Message
                };
                throw new FaultException<OptimisticConcurrencyFaultContract>(ocfc);
            }
}

        public ReservationDto UpdateReservation(ReservationDto reservationDto)
        {
            try
            {
                Reservation reservation = reservationDto.ConvertToEntity();
                return autoReservationBusinessComponent.UpdateReservation(reservation).ConvertToDto();
            } catch (LocalOptimisticConcurrencyException<Reservation> ex)
            {
                OptimisticConcurrencyFaultContract ocfc = new OptimisticConcurrencyFaultContract
                {
                    Operation = "UpdateReservation",
                    Message = ex.Message
                };
                throw new FaultException<OptimisticConcurrencyFaultContract>(ocfc);
            }
        }
    }
}