namespace PersonalFinance.Communication.Dtos;

public record BillDto
{
    public Guid Id { get; init; }
    public DateTime DueDate { get; init; }
    public double Amount { get; init; }
    public int InstallmentsTotal { get; init; }
    public int InstallmentsPaid { get; init; }
    public CategoryDto? Category { get; init; }
    public required ParticipantDto Participant { get; init; }
}