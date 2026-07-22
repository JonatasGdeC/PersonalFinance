using Microsoft.AspNetCore.Components;
using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.Utils.HandlerParticipant;

public partial class HandlerParticipant : ComponentBase
{
    [Parameter] public required ParticipantDto Participant { get; init; }
    [Parameter] public string? Subtitle { get; set; }

    private static string GetInitial(string name) => name.Length > 0 ? name[..1].ToUpperInvariant() : string.Empty;
}