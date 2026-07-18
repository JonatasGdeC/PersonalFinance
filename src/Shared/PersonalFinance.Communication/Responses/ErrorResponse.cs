using System.Text.Json.Serialization;

namespace PersonalFinance.Communication.Responses;

public record ErrorResponse
{
    public List<string> ErrorMessages { get; init; }

    public ErrorResponse(string errorMessage) => ErrorMessages = [errorMessage];

    [JsonConstructor]
    public ErrorResponse(List<string> errorMessages) => ErrorMessages = errorMessages;
}