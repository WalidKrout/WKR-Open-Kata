using AccountValueCalculator.Domain;
using AccountValueCalculator.Domain.ValueObject;
using System.Globalization;

namespace AccountValueCalculator.Services
{
    public class CalculatorService : ICalculatorService
    {
        public decimal CalculateValueAccount(IEnumerable<Transaction> transactions, DateTime targetDate)
        {
            /********************************a récupérer depuis le csv a faire plus tard**********************************/
            decimal balance = 8300m;
            DateTime refDateBalance = DateTime.ParseExact("28/02/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture);            

            var targetTransactions = transactions.Where(d => d.Date > targetDate && d.Date <= refDateBalance);

            foreach (var transaction in targetTransactions)
            {
                var amountEur = ConvertToEur(transaction.Amount, transaction.Currency);
                balance -= amountEur;
            }

            return balance;
        }

        public Dictionary<string, decimal> GetTopDebitCategories(IEnumerable<Transaction> transactions, int top = 3)
        {
            var transactionsCatGroup = transactions
                .Where(t => t.Amount < 0)
                .GroupBy(t => t.Category);

            var transactionsGroupSum = transactionsCatGroup
                .Select(grpcat => new { Category = grpcat.Key, Total = grpcat.Sum(t => ConvertToEur(t.Amount, t.Currency)) })
                .OrderBy(t => t.Total)
                .Take(top);

            return transactionsGroupSum.ToDictionary(t => t.Category, t => t.Total);
        }

        private decimal ConvertToEur(decimal amount, string currency)
        {
            /********************************a récupérer depuis le csv a faire plus tard**********************************/
            var rates = new Dictionary<string, decimal> { { "EUR", 1m }, { "USD", 1.445m }, { "JPY", 0.482m } };
            return amount * rates[currency];
        }
    }
}
