using PersonalFinance.Domain.Requests.Participant;

namespace PersonalFinance.Domain.Repositories.Participant;
using Entities;

public interface IParticipantReadRepository
{
    Task<PagedList<Participant>> GetAll(Guid userId, GetAllParticipantRequest request);
}