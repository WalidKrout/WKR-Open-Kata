using AccountValueCalculator.Domain.ValueObject;

namespace AccountValueCalculator.Domain
{
    public interface ICsvReaderService
    {
        IEnumerable<Transaction> ReadTransactions(string path);
    }
}
