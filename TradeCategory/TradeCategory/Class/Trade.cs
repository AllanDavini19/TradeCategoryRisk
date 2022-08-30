using System.Globalization;
using TradeCategory.Extensions;
using TradeCategory.Interfaces;

namespace TradeCategory.Class
{
    public class Trade : ITrade
    {        
        public double Value => _value;
        public string ClientSector => _clientSector;
        public DateTime PaymentDate => _PaymentDate;
        bool ITrade.IsPoliticallyExposed => _isPoliticallyExposed;

        private readonly double _value;
        private readonly string _clientSector;
        private readonly DateTime _PaymentDate;
        private readonly bool _isPoliticallyExposed;
        public Trade(string tradeRecord)
        {
            if (string.IsNullOrWhiteSpace(tradeRecord))
            {
                throw new Exception("Empty record");
            }

            var values = tradeRecord.Split(' ');


            if (values.Length < Enum.GetNames<Positions>().Length)
            {
                throw new Exception($"Missing Arguments, please enter 4 arguments.(Amount, Sector, PaymentDate, IsPoliticallyExposed)");
            }

            if (!double.TryParse(values[(int)Positions.value], out _value))
            {
                throw new Exception($"Invalid amount value: {tradeRecord}");
            }

            _clientSector = values[(int)Positions.clientSector].ToUpper();

            if ((new List<string> { Constants.PRIVATE, Constants.PUBLIC }).IndexOf(_clientSector) == -1)
            {
                throw new Exception($"Invalid sector: {tradeRecord}. Should be {Constants.PRIVATE} or {Constants.PUBLIC}");
            }

            if (!DateTime.TryParseExact(values[(int)Positions.nextPaymentDate], "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _PaymentDate))
            {
                throw new Exception($"Invalid payment date value: {tradeRecord}");
            }

            _isPoliticallyExposed = values[(int)Positions.isPoliticallyExposed].ToString().Equals("0") ? false : true;
        }

        private enum Positions
        {
            value = 0,
            clientSector = 1,
            nextPaymentDate = 2,
            isPoliticallyExposed = 3,
        };

        public string RiskCategory(DateTime referenceDate) =>
            Class.RiskCategory.Instance.GetRiskCategory(this, referenceDate).ToString();
    }
}
