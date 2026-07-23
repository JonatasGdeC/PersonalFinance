namespace PersonalFinance.Domain.Repositories.Participant;
using Entities;

public interface IParticipantWriteRepository
{
    Task Add(Participant participant);
    void Update(Participant participant);
    void Delete(Participant participant);
    Task<Participant?> GetById(Guid participantId, Guid userId);
}