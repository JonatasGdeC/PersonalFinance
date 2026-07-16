using FluentValidation;
using PersonalFinance.Communication.Requests.Participant;
using PersonalFinance.Exception;

namespace PersonalFinance.Application.UseCase.Participant;

public class ParticipantValidator : AbstractValidator<RegisterParticipantRequest>
{
    public ParticipantValidator()
    {
        RuleFor(expression: request => request.Name)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty().WithMessage(errorMessage: ResourceErrorMessages.NAME_IS_REQUIRED)
            .MinimumLength(minimumLength: 3).WithMessage(errorMessage: ResourceErrorMessages.NAME_MINIMUM_LENGTH)
            .MaximumLength(maximumLength: 100).WithMessage(errorMessage: ResourceErrorMessages.NAME_MAXIMUM_LENGTH);
    }
}
