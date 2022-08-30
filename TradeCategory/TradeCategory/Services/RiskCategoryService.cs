using TradeCategory.Extensions;
using TradeCategory.Interfaces;

namespace TradeCategory.Services
{
    public class RiskCategoryService : IRiskCategoryServices
    {
        const int DaysToExpire = 30;
        const double HighRiskValue = 1000000;
        const double MediumRiskValue = 1000000;

        public string RiskCategory(ITrade trade, DateTime referenceDate)
        {
            if ((referenceDate - trade.PaymentDate).Days > DaysToExpire && !trade.IsPoliticallyExposed) return Constants.EXPIRED;
            if (trade.Value >= HighRiskValue && trade.ClientSector.Equals(Constants.PRIVATE) && !trade.IsPoliticallyExposed) return Constants.HIGH_RISK;
            if (trade.Value >= MediumRiskValue && trade.ClientSector.Equals(Constants.PUBLIC) && !trade.IsPoliticallyExposed) return Constants.MEDIUM_RISK;
            if (trade.IsPoliticallyExposed) return Constants.POLITYCALLY_EXPOSED;

            return Constants.NOT_CATEGORIZED;
        }
    }
}
