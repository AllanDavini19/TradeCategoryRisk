namespace TradeCategory.Interfaces
{
    public interface ITrade
    {
        //indicates the transaction amount in dollar
        double Value { get; }

        //indicates the client's sector which can be "Public" or "Private"
        string ClientSector { get; }

        //indicates when the next payment from the client to the bank is expected
        DateTime PaymentDate { get; }

        //indicates which category of risk for this trade
        string RiskCategory(DateTime referenceDate); 

        //indicates if is Politically Exposed person
        bool IsPoliticallyExposed { get; }
    }
}
