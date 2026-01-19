using Credo.Infrastructure.Entities;
using Credo.Infrastructure.Repository.Abstraction;
using Credo.Infrastructure.UnitOfWork.Abstraction;
using MediatR;

namespace Credo.Application.Commands.AutoPaymentAccountCommands;

public record UpdateAutoPaymentAccountAmount(int Id, decimal Amount) : IRequest<AutoPaymentAccount>;

public class EditAutoPaymentAccountHandler : IRequestHandler<UpdateAutoPaymentAccountAmount, AutoPaymentAccount>
{
    private readonly IRepository<AutoPaymentAccount> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public EditAutoPaymentAccountHandler(IRepository<AutoPaymentAccount> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<AutoPaymentAccount> Handle(UpdateAutoPaymentAccountAmount request, CancellationToken cancellationToken)
    {
        var apc = await _repository.Find(request.Id);
        if (apc == null)
            throw new ArgumentNullException("APC Not found");
        
        apc.Amount = request.Amount;
        await _unitOfWork.SaveAsync(cancellationToken);

        return apc;
    }
}