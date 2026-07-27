using System.Globalization;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using PersonalFinance.Adapter.Exceptions;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Pot;
using PersonalFinance.Communication.Validators;
using PersonalFinance.Web.Components.AddInput;
using PersonalFinance.Web.Resources.Pots;
using PersonalFinance.Web.Services.SnackbarService;
using PersonalFinance.Web.UseState.Modal;
using PersonalFinance.Web.UseState.Pot;

namespace PersonalFinance.Web.Pages.Pots.Components.ModalUpdatePot;

public partial class ModalUpdatePot : IDisposable
{
    private const int NameMaxLength = 30;

    private static readonly (string Hex, Func<string> GetName)[] ColorPalette =
    [
        ("#277C78", () => PotsResources.ColorGreen),
        ("#F2CDAC", () => PotsResources.ColorYellow),
        ("#82C9D7", () => PotsResources.ColorCyan),
        ("#626070", () => PotsResources.ColorNavy),
        ("#C94736", () => PotsResources.ColorRed),
        ("#826CB0", () => PotsResources.ColorPurple),
        ("#AF81BA", () => PotsResources.ColorPurpleLight),
        ("#597C7C", () => PotsResources.ColorTurquoise),
        ("#93674F", () => PotsResources.ColorBrown),
        ("#934F6F", () => PotsResources.ColorMagenta),
        ("#3F82B2", () => PotsResources.ColorBlue),
        ("#97A0AC", () => PotsResources.ColorNavyGrey),
        ("#7F9161", () => PotsResources.ColorArmyGreen),
        ("#CAB361", () => PotsResources.ColorGold),
        ("#BE6C49", () => PotsResources.ColorOrange),
    ];

    private readonly List<AddInputOption> _customColorOptions = [];

    private List<AddInputOption> ColorOptions
    {
        get
        {
            HashSet<string> usedColors = PotListState.Value.Pots
                .Where(predicate: pot => pot.Id != PotState.Value.Pot?.Id)
                .Select(selector: pot => pot.Color)
                .ToHashSet();

            List<AddInputOption> options = ColorPalette
                .Select(selector: color => new AddInputOption
                {
                    Value = color.Hex,
                    Label = color.GetName(),
                    ColorTag = color.Hex,
                    Badge = usedColors.Contains(color.Hex) ? PotsResources.AlreadyUsedBadge : null
                })
                .ToList();

            options.AddRange(collection: _customColorOptions.Select(selector: option => new AddInputOption
            {
                Value = option.Value,
                Label = option.Label,
                ColorTag = option.ColorTag,
                Badge = usedColors.Contains(option.Value) ? PotsResources.AlreadyUsedBadge : null
            }));

            return options;
        }
    }

    private string _name = string.Empty;
    private string _colorValue = string.Empty;
    private string _targetText = string.Empty;

    private bool _isAddingCustomColor;
    private string _customColorName = string.Empty;
    private string _customColorValue = "#277C78";

    private List<string> _errorMessages = [];
    private bool _isSubmitting;

    private string NameCharactersLeftText => string.Format(format: PotsResources.CharactersLeftFormat, arg0: NameMaxLength - _name.Length);

    protected override void OnInitialized()
    {
        base.OnInitialized();
        PotState.StateChanged += HandlePotChanged;
        SeedForm();
    }

    private void HandlePotChanged(object? sender, EventArgs e)
    {
        SeedForm();
        StateHasChanged();
    }

    private void SeedForm()
    {
        PotDto? pot = PotState.Value.Pot;
        if (pot == null)
        {
            return;
        }

        _name = pot.Name;
        _colorValue = pot.Color;
        _targetText = pot.Target.ToString(format: "0.##", provider: CultureInfo.InvariantCulture);
    }

    private void OpenAddCustomColor() => _isAddingCustomColor = true;

    private void CancelAddCustomColor()
    {
        _isAddingCustomColor = false;
        _customColorName = string.Empty;
        _customColorValue = "#277C78";
    }

    private void HandleCustomColorValueChanged(ChangeEventArgs e) => _customColorValue = e.Value?.ToString() ?? _customColorValue;

    private void ConfirmAddCustomColor()
    {
        if (string.IsNullOrWhiteSpace(value: _customColorName))
        {
            _errorMessages = [PotsResources.RequiredFieldsError];
            return;
        }

        _customColorOptions.Add(item: new AddInputOption { Value = _customColorValue, Label = _customColorName, ColorTag = _customColorValue });
        _colorValue = _customColorValue;

        CancelAddCustomColor();
    }

    private async Task HandleUpdatePot()
    {
        _errorMessages = [];

        PotDto? targetPot = PotState.Value.Pot;
        if (targetPot == null)
        {
            return;
        }

        RegisterPotRequest request = new()
        {
            Name = _name,
            CurrentAmount = targetPot.CurrentAmount,
            Target = double.TryParse(s: _targetText, result: out double target) ? target : 0,
            Color = _colorValue
        };

        PotValidator validator = new();
        ValidationResult? result = await validator.ValidateAsync(instance: request);

        if (!result.IsValid)
        {
            _errorMessages = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
            return;
        }

        _isSubmitting = true;

        try
        {
            await PersonalFinanceApi.Pot.Update(potId: targetPot.Id, request: request);

            PotDto updatedPot = new()
            {
                Id = targetPot.Id,
                Name = request.Name,
                CurrentAmount = request.CurrentAmount,
                Target = request.Target,
                Color = request.Color
            };

            Dispatcher.Dispatch(action: new PotActions.UpdatePotSuccessAction(Pot: updatedPot));
            HandleClose();
            SnackbarService.Show(message: PotsResources.PotUpdatedSuccessMessage, severity: SnackbarSeverity.Success);
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _errorMessages = exception.ErrorMessages.ToList();
        }
        catch
        {
            SnackbarService.Show(message: PotsResources.UnknownError, severity: SnackbarSeverity.Error);
        }
        finally
        {
            _isSubmitting = false;
        }
    }

    private void HandleClose()
    {
        _errorMessages = [];
        CancelAddCustomColor();
        Dispatcher.Dispatch(action: new ModalActions.CloseModalAction(Modal: ModalType.EditPot));
    }

    public void Dispose() => PotState.StateChanged -= HandlePotChanged;
}
