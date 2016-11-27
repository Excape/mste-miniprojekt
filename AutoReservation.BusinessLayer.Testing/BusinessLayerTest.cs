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
            Assert.IsNull(Target.loadAuto(4));
            var auto = new LuxusklasseAuto { Marke = "VW", Tagestarif = 150, Basistarif = 100 };
            Target.insertAuto(auto);
            var timestamp = auto.RowVersion;
            Assert.AreEqual(4, auto.Id);

            // Read
            Assert.IsNotNull(Target.loadAuto(4));
            
            // Update
            auto.Basistarif = 4993;
            Target.updateAuto(auto);
            Assert.AreNotEqual(timestamp, auto.RowVersion);

            // Delete
            Target.deleteAuto(auto);
            Assert.IsNull(Target.loadAuto(4));

        }

        [TestMethod]
        public void UpdateAutoTest()
        {
            var auto1 = Target.loadAuto(1);
            var auto2 = Target.loadAuto(1);
            Assert.AreNotEqual(auto1, auto2);

            auto1.Marke = "A";
            auto2.Marke = "B";

            Target.updateAuto(auto1);

            try
            {
                Target.updateAuto(auto2);
                Assert.Fail("Was able to override!");
            }catch(LocalOptimisticConcurrencyException<Auto>)
            {
                // Everything OK
            }

            Assert.AreEqual("A", Target.loadAuto(1).Marke);
        }


        [TestMethod]
        public void UpdateKundeTest()
        {
            var kunde1 = Target.loadKunde(1);
            var kunde2 = Target.loadKunde(1);
            Assert.AreNotEqual(kunde1, kunde2);

            kunde1.Nachname = "A";
            kunde2.Nachname = "B";

            Target.updateKunde(kunde1);

            try
            {
                Target.updateKunde(kunde2);
                Assert.Fail("Was able to override!");
            }
            catch (LocalOptimisticConcurrencyException<Kunde>)
            {
                // Everything OK
            }

            Assert.AreEqual("A", Target.loadKunde(1).Nachname);
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            var res1 = Target.loadReservation(1);
            var res2 = Target.loadReservation(1);
            Assert.AreNotEqual(res1, res2);

            // TODO: Ask, why both must be updated!?
            res1.Kunde = Target.loadKunde(2);
            res1.KundeId = 2;
            res2.Kunde = Target.loadKunde(3);
            res2.KundeId = 3;

            Target.updateReservation(res1);

            try
            {
                Target.updateReservation(res2);
                Assert.Fail("Was able to override!");
            }
            catch (LocalOptimisticConcurrencyException<Reservation>)
            {
                // Everything OK
            }

            Assert.AreEqual(2, Target.loadReservation(1).Kunde.Id);
        }

    }

}
