using AutoReservation.Common.DataTransferObjects;
using System.Collections.Generic;
using System.ServiceModel;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationService
    {
        List<AutoDto> Autos { [OperationContract] get; }
        List<KundeDto> Kunden { [OperationContract] get; }
        List<ReservationDto> Reservationen { [OperationContract] get; }

        [OperationContract]
        AutoDto GetAutoById(int id);

        [OperationContract]
        KundeDto GetKundeById(int id);

        [OperationContract]
        ReservationDto GetReservationByNr(int reservationsNr);

        [OperationContract]
        AutoDto InsertAuto(AutoDto autoDto);

        [OperationContract]
        KundeDto InsertKunde(KundeDto kundeDto);

        [OperationContract]
        ReservationDto InsertReservation(ReservationDto reservationDto);

        [OperationContract]
        AutoDto UpdateAuto(AutoDto autoDto);

        [OperationContract]
        KundeDto UpdateKunde(KundeDto kundeDto);

        [OperationContract]
        ReservationDto UpdateReservation(ReservationDto reservationDto);

        [OperationContract]
        void DeleteAuto(AutoDto autoDto);

        [OperationContract]
        void DeleteKunde(KundeDto kundeDto);

        [OperationContract]
        void DeleteReservation(ReservationDto reservationDto);

    }
}
