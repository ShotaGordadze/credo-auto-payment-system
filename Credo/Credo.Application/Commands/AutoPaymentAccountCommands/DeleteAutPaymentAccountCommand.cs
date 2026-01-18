using Credo.Infrastructure.Entities;
using Credo.Infrastructure.Repository.Abstraction;
using Credo.Infrastructure.UnitOfWork.Abstraction;
using MediatR;

namespace Credo.Application.Commands.AutoPaymentAccountCommands;

public record DeleteAutPaymentAccountCommand(int Id) : IRequest<bool> ;

public class DeleteAutPaymentAccountCommandHandler : IRequestHandler<DeleteAutPaymentAccountCommand, bool>
{
    private readonly IRepository<AutoPaymentAccount> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAutPaymentAccountCommandHandler(IRepository<AutoPaymentAccount> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<bool> Handle(DeleteAutPaymentAccountCommand request, CancellationToken cancellationToken)
    {
        var apc = await _repository.Find(request.Id);
        if (apc is null)
            throw new ArgumentNullException("APC Not found");

        var deletionCheck = _repository.Delete(apc);

        if (!deletionCheck) throw new NullReferenceException("APC Not found");
        await _unitOfWork.SaveAsync(cancellationToken);
        
        return true;

    }
}