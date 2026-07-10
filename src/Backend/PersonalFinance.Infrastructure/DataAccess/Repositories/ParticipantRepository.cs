using Microsoft.EntityFrameworkCore;
using PersonalFinance.Domain.Entities;
using PersonalFinance.Domain.Repositories;
using PersonalFinance.Domain.Repositories.Participant;
using PersonalFinance.Domain.Requests;
using PersonalFinance.Domain.Requests.Participant;
using PersonalFinance.Domain.Response;
using PersonalFinance.Infrastructure.DataAccess.Utils;

namespace PersonalFinance.Infrastructure.DataAccess.Repositories;

internal class ParticipantRepository(PersonalFinanceDbContext context) : IParticipantReadRepository, IParticipantWriteRepository
{
    public async Task<PagedListResponse<Participant>> GetAll(Guid userId, GetAllParticipantRequest request)
    {
        IQueryable<Participant> query = context.Participants
            .AsNoTracking()
            .Where(predicate: participant => participant.UserId == userId);

        if (!string.IsNullOrWhiteSpace(value: request.Name))
        {
            string search = request.Name.ToLower();
            query = query.Where(predicate: participant => participant.Name.ToLower().Contains(search));
        }

        query = query.OrderBy(keySelector: participant => participant.Name);

        return await CreatePageList<Participant>.Execute(query: query, page: request.PageRequest);
    }

    public async Task Add(Participant participant)
    {
        await context.Participants.AddAsync(entity: participant);
    }

    public void Update(Participant participant)
    {
        context.Participants.Update(entity: participant);
    }

    public void Delete(Participant participant)
    {
        context.Participants.Remove(entity: participant);
    }

    public async Task<Participant?> GetById(long participantId, Guid userId)
    {
        return await context.Participants.AsTracking()
            .FirstOrDefaultAsync(predicate: participant =>
                participant.Id == participantId && participant.UserId == userId);
    }
}