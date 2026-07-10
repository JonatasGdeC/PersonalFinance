using Microsoft.EntityFrameworkCore;
using PersonalFinance.Domain.Entities;
using PersonalFinance.Domain.Filters.Participant;
using PersonalFinance.Domain.ReadModels;
using PersonalFinance.Domain.Repositories.Participant;
using PersonalFinance.Infrastructure.DataAccess.Utils;

namespace PersonalFinance.Infrastructure.DataAccess.Repositories;

internal class ParticipantRepository(PersonalFinanceDbContext context) : IParticipantReadRepository, IParticipantWriteRepository
{
    public async Task<PagedList<Participant>> GetAll(Guid userId, ParticipantFilter filter)
    {
        IQueryable<Participant> query = context.Participants
            .AsNoTracking()
            .Where(predicate: participant => participant.UserId == userId);

        if (!string.IsNullOrWhiteSpace(value: filter.Name))
        {
            string search = filter.Name.ToLower();
            query = query.Where(predicate: participant => participant.Name.ToLower().Contains(search));
        }

        query = query.OrderBy(keySelector: participant => participant.Name);

        return await CreatePageList<Participant>.Execute(query: query, page: filter.Pagination);
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