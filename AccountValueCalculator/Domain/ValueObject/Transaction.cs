namespace AccountValueCalculator.Domain.ValueObject
{
    public record Transaction(DateTime Date, decimal Amount, string Currency, string Category);
}
