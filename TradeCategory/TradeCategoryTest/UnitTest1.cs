using System.Globalization;
using TradeCategory.Class;
using TradeCategory.Extensions;
using TradeCategory.Interfaces;

namespace TradeCategoryTest
{
    [TestClass]
    public class UnitTest1
    {
        private DateTime referenceDate = DateTime.ParseExact("08/30/2022", "MM/dd/yyyy", CultureInfo.InvariantCulture);

        [TestMethod]
        public void TradeAsPep()
        {
            string record = "1000000 Private 10/21/2023 1";

            ITrade trade = new Trade(record);

            Assert.AreEqual(1000000, trade.Value);
            Assert.AreEqual(Constants.PRIVATE, trade.ClientSector);
            Assert.AreEqual(DateTime.ParseExact("10/21/2023", "MM/dd/yyyy", CultureInfo.InvariantCulture), trade.PaymentDate);
            Assert.AreEqual(true, trade.IsPoliticallyExposed);
            Assert.AreEqual(Constants.POLITYCALLY_EXPOSED, trade.RiskCategory(referenceDate).ToString());
        }

        [TestMethod]
        public void TradeAsHighRisk()
        {
            string record = "1000000 Private 10/21/2023 0";

            ITrade trade = new Trade(record);

            Assert.AreEqual(1000000, trade.Value);
            Assert.AreEqual(Constants.PRIVATE, trade.ClientSector);
            Assert.AreEqual(DateTime.ParseExact("10/21/2023", "MM/dd/yyyy", CultureInfo.InvariantCulture), trade.PaymentDate);
            Assert.AreEqual(false, trade.IsPoliticallyExposed);
            Assert.AreEqual(Constants.HIGH_RISK, trade.RiskCategory(referenceDate).ToString());
        }

        [TestMethod]
        public void InvalidNextPaymentValue()
        {
            string record = "1000000 Public 30/12/2024";
            void recordParse() => new Trade(record);
            Assert.ThrowsException<Exception>(recordParse);
        }

        [TestMethod]
        public void TradeAsNotCategorized()
        {
            string record = "100000 Public 09/15/2022 0";

            ITrade trade = new Trade(record);

            Assert.AreEqual(100000, trade.Value);
            Assert.AreEqual(Constants.PUBLIC, trade.ClientSector);
            Assert.AreEqual(DateTime.ParseExact("09/15/2022", "MM/dd/yyyy", CultureInfo.InvariantCulture), trade.PaymentDate);
            Assert.AreEqual(Constants.NOT_CATEGORIZED, trade.RiskCategory(referenceDate).ToString());
        }

        [TestMethod]
        public void TradeAsMediumRisk()
        {
            string record = "3000000 Public 10/21/2023 0";

            ITrade trade = new Trade(record);

            Assert.AreEqual(3000000, trade.Value);
            Assert.AreEqual(Constants.PUBLIC, trade.ClientSector);
            Assert.AreEqual(DateTime.ParseExact("10/21/2023", "MM/dd/yyyy", CultureInfo.InvariantCulture), trade.PaymentDate);
            Assert.AreEqual(false, trade.IsPoliticallyExposed);
            Assert.AreEqual(Constants.MEDIUM_RISK, trade.RiskCategory(referenceDate).ToString());
        }

        [TestMethod]
        public void TradeAsExpired()
        {
            string record = "1000000 Public 06/10/2022 0";

            ITrade trade = new Trade(record);

            Assert.AreEqual(1000000, trade.Value);
            Assert.AreEqual(Constants.PUBLIC, trade.ClientSector);
            Assert.AreEqual(DateTime.ParseExact("06/10/2022", "MM/dd/yyyy", CultureInfo.InvariantCulture), trade.PaymentDate);
            Assert.AreEqual(false, trade.IsPoliticallyExposed);
            Assert.AreEqual(Constants.EXPIRED, trade.RiskCategory(referenceDate).ToString());
        }
    }
}