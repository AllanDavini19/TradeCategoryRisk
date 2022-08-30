using TradeCategory.Interfaces;
using TradeCategory.Services;

namespace TradeCategory.Class
{
    public class RiskCategory
    {
        private static RiskCategory risk;

        public static RiskCategory Instance
        {
            get
            {
                if (risk == null)
                {
                    risk = new RiskCategory();
                }

                return risk;
            }
        }        

        public string GetRiskCategory(ITrade trade, DateTime referenceDate)
        {
            var service = new RiskCategoryService();

            return service.RiskCategory(trade, referenceDate);
        }        
    }
}
