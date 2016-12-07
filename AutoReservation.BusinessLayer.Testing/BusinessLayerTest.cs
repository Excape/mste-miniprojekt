using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class BusinessLayerTest
    {

        private AutoReservationBusinessComponent target;
        private AutoReservationBusinessComponent Target
        {
            get
            {
                if (target == null)
                {
                    target = new AutoReservationBusinessComponent();
                }
                return target;
            }
        }
        
        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }
        
        [TestMethod]
        public void CRUDAutoTest()
        {
            // Create
            Assert.IsNull(Target.LoadAuto(4));
            var auto = new LuxusklasseAuto { Marke = "VW", Tagestarif = 150, Basistarif = 100 };
            Target.InsertAuto(auto);
            var timestamp = auto.RowVersion;
            Assert.AreEqual(4, auto.Id);

            // Read
            Assert.IsNotNull(Target.LoadAuto(4));
            
            // Update
            auto.Basistarif = 4993;
            Target.UpdateAuto(auto);
            Assert.AreNotEqual(timestamp, auto.RowVersion);

            // Delete
            Target.DeleteAuto(auto);
            Assert.IsNull(Target.LoadAuto(4));

        }

        [TestMethod]
        public void UpdateAutoTest()
        {
            var auto1 = Target.LoadAuto(1);
            var auto2 = Target.LoadAuto(1);
            Assert.AreNotEqual(auto1, auto2);

            auto1.Marke = "A";
            auto2.Marke = "B";

            Target.UpdateAuto(auto1);

            try
            {
                Target.UpdateAuto(auto2);
                Assert.Fail("Was able to override!");
            }catch(LocalOptimisticConcurrencyException<Auto>)
            {
                // Everything OK
            }

            Assert.AreEqual("A", Target.LoadAuto(1).Marke);
        }


        [TestMethod]
        public void UpdateKundeTest()
        {
            var kunde1 = Target.LoadKunde(1);
            var kunde2 = Target.LoadKunde(1);
            Assert.AreNotEqual(kunde1, kunde2);

            kunde1.Nachname = "A";
            kunde2.Nachname = "B";

            Target.UpdateKunde(kunde1);

            try
            {
                Target.UpdateKunde(kunde2);
                Assert.Fail("Was able to override!");
            }
            catch (LocalOptimisticConcurrencyException<Kunde>)
            {
                // Everything OK
            }

            Assert.AreEqual("A", Target.LoadKunde(1).Nachname);
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            var res1 = Target.LoadReservation(1);
            var res2 = Target.LoadReservation(1);
            Assert.AreNotEqual(res1, res2);

            res1.Kunde = Target.LoadKunde(2);
            res1.KundeId = 2;
            res2.Kunde = Target.LoadKunde(3);
            res2.KundeId = 3;

            Target.UpdateReservation(res1);

            try
            {
                Target.UpdateReservation(res2);
                Assert.Fail("Was able to override!");
            }
            catch (LocalOptimisticConcurrencyException<Reservation>)
            {
                // Everything OK
            }
            Assert.AreEqual(2, Target.LoadReservation(1).Kunde.Id);
        }

        [TestMethod]
        public void LoadReservationsTest()
        {
            var resList = Target.LoadAllReservations();
            Assert.IsNotNull(resList);
            Assert.AreEqual(3, resList.Count);
            foreach (var res in resList)
            {
                Assert.IsNotNull(res.Auto);
                Assert.IsNotNull(res.Kunde);
            }
        }

        [TestMethod]
        public void LoadAutosTest()
        {
            var autos = Target.LoadAllAutos();
            Assert.IsNotNull(autos);
            Assert.AreEqual(3, autos.Count);
            foreach (var auto in autos)
            {
                Assert.IsNotNull(auto.Reservationen);
            }
        }

        [TestMethod]
        public void LoadKundenTest()
        {
            var kunden = Target.LoadAllKunden();
            Assert.IsNotNull(kunden);
            Assert.AreEqual(4, kunden.Count);
            foreach (var kunde in kunden)
            {
                Assert.IsNotNull(kunde.Reservationen);
            }
        }
    }

}
