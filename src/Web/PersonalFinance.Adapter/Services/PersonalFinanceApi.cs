using PersonalFinance.Adapter.Interfaces;

namespace PersonalFinance.Adapter.Services;

public class PersonalFinanceApi(HttpClient httpClient)
{
    public IUserServiceApi User { get; } = new UserServiceApi(httpClient: httpClient);
    public IPotServiceApi Pot { get; } = new PotServiceApi(httpClient: httpClient);
    public IParticipantServiceApi Participant { get; } = new ParticipantServiceApi(httpClient: httpClient);
}