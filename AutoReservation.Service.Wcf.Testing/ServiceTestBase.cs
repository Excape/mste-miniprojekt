using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.FaultExceptions;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace AutoReservation.Service.Wcf.Testing
{
    [TestClass]
    public abstract class ServiceTestBase
    {
        protected abstract IAutoReservationService Target { get; }
        KundeDto kundeDto;
        AutoDto autoDto;
        ReservationDto reservationDto;

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
            autoDto = new AutoDto { Id = 1, Marke = "Fiat Punto", Tagestarif = 50, AutoKlasse = AutoKlasse.Standard };
            kundeDto = new KundeDto { Id = 1, Nachname = "Nass", Vorname = "Anna", Geburtsdatum = new DateTime(1981, 05, 05) };
            reservationDto = new ReservationDto { ReservationsNr = 1, Auto = autoDto, Kunde = kundeDto, Von = new DateTime(2020, 01, 10), Bis = new DateTime(2020, 01, 20) };
        }

        #region Read all entities

        [TestMethod]
        public void GetAutosTest()
        {
            Assert.AreEqual(3, Target.Autos.Count);
            Assert.AreEqual("Fiat Punto", Target.Autos[0].Marke);
        }

        [TestMethod]
        public void GetKundenTest()
        {
            Assert.AreEqual(4, Target.Kunden.Count);
            Assert.AreEqual("Anna", Target.Kunden[0].Vorname);
        }

        [TestMethod]
        public void GetReservationenTest()
        {
            Assert.AreEqual(3, Target.Reservationen.Count);
            Assert.AreEqual(Target.GetAutoById(1).ToString(), Target.Reservationen[0].Auto.ToString());
            Assert.AreEqual(Target.GetKundeById(1).ToString(), Target.Reservationen[0].Kunde.ToString());
        }

        #endregion

        #region Get by existing ID

        [TestMethod]
        public void GetAutoByIdTest()
        {
            Assert.AreEqual(autoDto.ToString(), Target.GetAutoById(1).ToString());
        }

        [TestMethod]
        public void GetKundeByIdTest()
        {
            Assert.AreEqual(kundeDto.ToString(), Target.GetKundeById(1).ToString());
        }

        [TestMethod]
        public void GetReservationByNrTest()
        {
            Assert.AreEqual(reservationDto.ToString(), Target.GetReservationByNr(1).ToString());
        }

        #endregion

        #region Get by not existing ID

        [TestMethod]
        public void GetAutoByIdWithIllegalIdTest()
        {
            Assert.IsNull(Target.GetAutoById(1000));
        }

        [TestMethod]
        public void GetKundeByIdWithIllegalIdTest()
        {
            Assert.IsNull(Target.GetKundeById(1000));
        }

        [TestMethod]
        public void GetReservationByNrWithIllegalIdTest()
        {
            Assert.IsNull(Target.GetReservationByNr(1000));
        }

        #endregion

        #region Insert

        [TestMethod]
        public void InsertAutoTest()
        {
            autoDto.Id = 4;
            AutoDto newAutoDto = Target.InsertAuto(autoDto);
            Assert.AreEqual(4, newAutoDto.Id);
            Assert.IsNotNull(newAutoDto.RowVersion);
            Assert.IsNotNull(Target.GetAutoById(4));
        }

        [TestMethod]
        public void InsertKundeTest()
        {
            kundeDto.Id = 5;
            KundeDto newKundeDto = Target.InsertKunde(kundeDto);
            Assert.AreEqual(5, newKundeDto.Id);
            Assert.IsNotNull(newKundeDto.RowVersion);
            Assert.IsNotNull(Target.GetKundeById(5));
        }

        [TestMethod]
        public void InsertReservationTest()
        {
            reservationDto.ReservationsNr = 4;
            ReservationDto newReservationDto = Target.InsertReservation(reservationDto);
            Assert.AreEqual(4, newReservationDto.ReservationsNr);
            Assert.IsNotNull(newReservationDto.RowVersion);
            Assert.IsNotNull(Target.GetReservationByNr(4));
        }

        #endregion

        #region Delete  

        [TestMethod]
        public void DeleteAutoTest()
        {
            Target.DeleteAuto(Target.GetAutoById(3));
            Assert.AreEqual(2, Target.Autos.Count);
        }

        [TestMethod]
        public void DeleteKundeTest()
        {
            Target.DeleteKunde(Target.GetKundeById(4));
            Assert.AreEqual(3, Target.Kunden.Count);
        }

        [TestMethod]
        public void DeleteReservationTest()
        {
            Target.DeleteReservation(Target.GetReservationByNr(3));
            Assert.AreEqual(2, Target.Reservationen.Count);
        }

        #endregion

        #region Update

        [TestMethod]
        public void UpdateAutoTest()
        {
            AutoDto tempAutoDto = Target.GetAutoById(1);
            tempAutoDto.Marke = "Mini";
            Assert.AreEqual("Mini", Target.UpdateAuto(tempAutoDto).Marke);
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            KundeDto tempKundeDto = Target.GetKundeById(1);
            tempKundeDto.Nachname = "Muster";
            Assert.AreEqual("Muster", Target.UpdateKunde(tempKundeDto).Nachname);
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            ReservationDto tempReservationDto = Target.GetReservationByNr(1);
            AutoDto tempAutoDto = Target.GetAutoById(3);
            tempReservationDto.Auto = tempAutoDto;
            Assert.AreEqual(3, Target.UpdateReservation(tempReservationDto).Auto.Id);
        }

        #endregion

        #region Update with optimistic concurrency violation

        [TestMethod]
        [ExpectedException(typeof(FaultException<OptimisticConcurrencyFaultContract>))]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            AutoDto tempAutoDto = Target.GetAutoById(1);
            AutoDto tempAutoDtoTwo = Target.GetAutoById(1);
            tempAutoDto.Marke = "Mini";
            Target.UpdateAuto(tempAutoDto);
            tempAutoDtoTwo.Marke = "Ferrari";
            Target.UpdateAuto(tempAutoDtoTwo);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<OptimisticConcurrencyFaultContract>))]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            KundeDto tempKundeDto = Target.GetKundeById(1);
            KundeDto tempKundeDtoTwo = Target.GetKundeById(1);
            tempKundeDto.Nachname = "Muster";
            Target.UpdateKunde(tempKundeDto);
            tempKundeDtoTwo.Nachname = "Meier";
            Target.UpdateKunde(tempKundeDtoTwo);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<OptimisticConcurrencyFaultContract>))]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            ReservationDto tempReservationDto = Target.GetReservationByNr(1);
            ReservationDto tempReservationDtoTwo = Target.GetReservationByNr(1);
            tempReservationDto.Auto = Target.GetAutoById(2);
            Target.UpdateReservation(tempReservationDto);
            tempReservationDtoTwo.Auto = Target.GetAutoById(3);
            Target.UpdateReservation(tempReservationDtoTwo);
            Assert.Fail();
        }

        #endregion
    }
}
