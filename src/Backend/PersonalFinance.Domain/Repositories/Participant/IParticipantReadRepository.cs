using PersonalFinance.Communication.Requests;

namespace PersonalFinance.Domain.Repositories.Participant;
using Entities;

public interface IParticipantReadRepository
{
    Task<PagedList<Participant>> GetAll(Guid userId, PageRequest page, string? name = null);
}