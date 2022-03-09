namespace Ecommerce.Shared.Abstractions.DDD;

public class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }
}