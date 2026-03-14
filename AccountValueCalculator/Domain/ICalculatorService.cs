using AccountValueCalculator.Domain.ValueObject;

namespace AccountValueCalculator.Domain
{
    public interface ICalculatorService
    {
        decimal CalculateValueAccount(IEnumerable<Transaction> transactions, DateTime targetDate);
        Dictionary<string, decimal> GetTopDebitCategories(IEnumerable<Transaction> transactions, int top = 3);
    }
}
