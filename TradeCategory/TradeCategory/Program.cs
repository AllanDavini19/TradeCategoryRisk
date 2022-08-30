using System.Globalization;
using TradeCategory.Class;

class Program
{
    public static void Main(string[] args)
    {
        try
        {
            string answer = string.Empty;
            DateTime referenceDate;
            int numberOfTrades = 0;
            Trade[] trades;

            Console.Clear();
            Console.WriteLine("Risk Calculator");
            Console.Write("Reference date (mm/dd/yyyy): ");
            answer = Console.ReadLine();

            if (!DateTime.TryParseExact(answer, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out referenceDate))
            {
                throw new ArgumentException("Invalid reference date");
            }

            Console.Write("Number of trades: ");
            answer = Console.ReadLine();

            if (!int.TryParse(answer, out numberOfTrades))
            {
                throw new ArgumentException("Invalid number of trades");
            }

            trades = new Trade[numberOfTrades];

            for (int trade = 0; trade < numberOfTrades; trade++)
            {
                Console.Write($"Trade info {trade + 1} of {numberOfTrades}: ");
                answer = Console.ReadLine();
                trades[trade] = new Trade(answer);
            }

            ShowTradeCategories(trades, referenceDate);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    
    static void ShowTradeCategories(Trade[] trades, DateTime referenceDate)
    {
        foreach (Trade trade in trades)
        {
            Console.WriteLine(trade.RiskCategory(referenceDate));
        };
    }
}