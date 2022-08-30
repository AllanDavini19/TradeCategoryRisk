namespace TradeCategory.Interfaces
{
    public interface IRiskCategoryServices
    {
        string RiskCategory(ITrade trade, DateTime referenceDate);
    }
}
