using FinanceService.Domain.Filters;
using FinanceService.Domain.ReadModels;

namespace FinanceService.Domain.Repositories.Participant;
using Entities;

public interface IParticipantReadRepository
{
    Task<PagedList<Participant>> GetAll(Guid userId, ParticipantFilter filter);
}
