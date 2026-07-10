using PersonalFinance.Domain.Filters.Participant;
using PersonalFinance.Domain.ReadModels;

namespace PersonalFinance.Domain.Repositories.Participant;
using Entities;

public interface IParticipantReadRepository
{
    Task<PagedList<Participant>> GetAll(Guid userId, ParticipantFilter filter);
}
