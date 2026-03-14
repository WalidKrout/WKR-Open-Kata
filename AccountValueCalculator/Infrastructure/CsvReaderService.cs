using AccountValueCalculator.Domain;
using AccountValueCalculator.Domain.ValueObject;
using System.Globalization;

namespace AccountValueCalculator.Infrastructure
{
    public class CsvReaderService : ICsvReaderService
    {
        public IEnumerable<Transaction> ReadTransactions(string path)
        {
            var transactions = new List<Transaction>();
            var linesTransactions = File.ReadLines(path).Skip(4);
            foreach (var line in linesTransactions)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                var columns = line.Split(';');
                var date = DateTime.ParseExact(columns[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var amount = decimal.Parse(columns[1], CultureInfo.InvariantCulture);
                transactions.Add(new Transaction(
                    date,
                    amount,
                    columns[2],
                    columns[3]));
            }
            return transactions;
        }

    }
}
