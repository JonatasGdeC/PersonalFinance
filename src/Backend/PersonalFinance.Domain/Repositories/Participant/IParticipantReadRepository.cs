using PersonalFinance.Domain.Requests.Participant;
using PersonalFinance.Domain.Response;

namespace PersonalFinance.Domain.Repositories.Participant;
using Entities;

public interface IParticipantReadRepository
{
    Task<PagedListResponse<Participant>> GetAll(Guid userId, GetAllParticipantRequest request);
}