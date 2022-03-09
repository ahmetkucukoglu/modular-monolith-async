using Ecommerce.Payment.Core.Repositories;
using Ecommerce.Payment.Core.Services;
using Ecommerce.Payment.Core.ValueObjects;
using Ecommerce.Payment.Shared.Events;
using Ecommerce.Shared.Abstractions.CQRS;
using Ecommerce.Shared.Abstractions.Bus;

namespace Ecommerce.Payment.Core.Commands;

public record CreatePayment(Guid OrderId, decimal TotalPrice, string Currency) : ICommand;

public class CreatePaymentHandler : ICommandHandler<CreatePayment>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentGateway _paymentGateway;
    private readonly IBusService _busService;

    public CreatePaymentHandler(IPaymentRepository paymentRepository, IPaymentGateway paymentGateway, IBusService busService)
    {
        _paymentRepository = paymentRepository;
        _paymentGateway = paymentGateway;
        _busService = busService;
    }   
    public async Task HandleAsync(CreatePayment command)
    {
        var payment = Aggregates.Payment.Create(new OrderId(command.OrderId), new Price(command.TotalPrice, command.Currency));
        
        await _paymentRepository.Create(payment);
        
        var result = _paymentGateway.Pay(new Price(command.TotalPrice, command.Currency));

        if (!result.IsSuccess)
        {
            payment.Fail(result.TransactionId, result.ErrorMessage);
            await _paymentRepository.Update(payment);
            
            await _busService.Send(new PaymentFailed(command.OrderId, result.ErrorMessage));
        }
        else
        {
            payment.Pay(result.TransactionId);
            await _paymentRepository.Update(payment);
            
            await _busService.Send(new PaymentMade(command.OrderId));
        }
    }
}