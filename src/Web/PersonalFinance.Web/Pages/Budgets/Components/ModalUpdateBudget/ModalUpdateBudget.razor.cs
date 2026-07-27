using System.Globalization;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using PersonalFinance.Adapter.Exceptions;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Budget;
using PersonalFinance.Communication.Validators;
using PersonalFinance.Web.Components.AddInput;
using PersonalFinance.Web.Resources.Budgets;
using PersonalFinance.Web.Services.SnackbarService;
using PersonalFinance.Web.UseState.Budget;
using PersonalFinance.Web.UseState.Category;
using PersonalFinance.Web.UseState.Modal;

namespace PersonalFinance.Web.Pages.Budgets.Components.ModalUpdateBudget;

public partial class ModalUpdateBudget : IDisposable
{
    private static readonly (string Hex, Func<string> GetName)[] ColorPalette =
    [
        ("#277C78", () => BudgetsResources.ColorGreen),
        ("#F2CDAC", () => BudgetsResources.ColorYellow),
        ("#82C9D7", () => BudgetsResources.ColorCyan),
        ("#626070", () => BudgetsResources.ColorNavy),
        ("#C94736", () => BudgetsResources.ColorRed),
        ("#826CB0", () => BudgetsResources.ColorPurple),
        ("#AF81BA", () => BudgetsResources.ColorPurpleLight),
        ("#597C7C", () => BudgetsResources.ColorTurquoise),
        ("#93674F", () => BudgetsResources.ColorBrown),
        ("#934F6F", () => BudgetsResources.ColorMagenta),
        ("#3F82B2", () => BudgetsResources.ColorBlue),
        ("#97A0AC", () => BudgetsResources.ColorNavyGrey),
        ("#7F9161", () => BudgetsResources.ColorArmyGreen),
        ("#CAB361", () => BudgetsResources.ColorGold),
        ("#BE6C49", () => BudgetsResources.ColorOrange),
    ];

    private readonly List<AddInputOption> _customColorOptions = [];

    private List<AddInputOption> CategoryOptions => CategoryListState.Value.Categories
        .Select(selector: category => new AddInputOption { Value = category.Id.ToString(), Label = category.Name })
        .ToList();

    private List<AddInputOption> ColorOptions
    {
        get
        {
            HashSet<string> usedColors = BudgetListState.Value.Budgets
                .Where(predicate: budget => budget.Id != BudgetState.Value.Budget?.Id)
                .Select(selector: budget => budget.Color)
                .ToHashSet();

            List<AddInputOption> options = ColorPalette
                .Select(selector: color => new AddInputOption
                {
                    Value = color.Hex,
                    Label = color.GetName(),
                    ColorTag = color.Hex,
                    Badge = usedColors.Contains(color.Hex) ? BudgetsResources.AlreadyUsedBadge : null
                })
                .ToList();

            options.AddRange(collection: _customColorOptions.Select(selector: option => new AddInputOption
            {
                Value = option.Value,
                Label = option.Label,
                ColorTag = option.ColorTag,
                Badge = usedColors.Contains(option.Value) ? BudgetsResources.AlreadyUsedBadge : null
            }));

            return options;
        }
    }

    private string _categoryValue = string.Empty;
    private string _colorValue = string.Empty;
    private string _maximumSpendText = string.Empty;

    private bool _isAddingCustomColor;
    private string _customColorName = string.Empty;
    private string _customColorValue = "#277C78";

    private List<string> _errorMessages = [];
    private bool _isSubmitting;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        BudgetState.StateChanged += HandleBudgetChanged;
        SeedForm();
    }

    private void HandleBudgetChanged(object? sender, EventArgs e)
    {
        SeedForm();
        StateHasChanged();
    }

    private void SeedForm()
    {
        BudgetDto? budget = BudgetState.Value.Budget;
        if (budget == null)
        {
            return;
        }

        _categoryValue = budget.Category.Id.ToString();
        _colorValue = budget.Color;
        _maximumSpendText = budget.MaximumSpend.ToString(format: "0.##", provider: CultureInfo.InvariantCulture);
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
            _errorMessages = [BudgetsResources.RequiredFieldsError];
            return;
        }

        _customColorOptions.Add(item: new AddInputOption { Value = _customColorValue, Label = _customColorName, ColorTag = _customColorValue });
        _colorValue = _customColorValue;

        CancelAddCustomColor();
    }

    private async Task HandleUpdateBudget()
    {
        _errorMessages = [];

        BudgetDto? targetBudget = BudgetState.Value.Budget;
        if (targetBudget == null)
        {
            return;
        }

        RegisterBudgetRequest request = new()
        {
            MaximumSpend = double.TryParse(s: _maximumSpendText, result: out double maximumSpend) ? maximumSpend : 0,
            Color = _colorValue,
            CategoryId = Guid.TryParse(input: _categoryValue, result: out Guid categoryId) ? categoryId : Guid.Empty
        };

        BudgetValidator validator = new();
        ValidationResult? result = await validator.ValidateAsync(instance: request);

        if (!result.IsValid)
        {
            _errorMessages = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
            return;
        }

        CategoryDto? category = CategoryListState.Value.Categories.FirstOrDefault(predicate: c => c.Id == request.CategoryId);
        if (category == null)
        {
            _errorMessages = [BudgetsResources.UnknownError];
            return;
        }

        _isSubmitting = true;

        try
        {
            await PersonalFinanceApi.Budget.Update(budgetId: targetBudget.Id, request: request);

            BudgetDto updatedBudget = new()
            {
                Id = targetBudget.Id,
                MaximumSpend = request.MaximumSpend,
                Color = request.Color,
                Category = category
            };

            Dispatcher.Dispatch(action: new BudgetActions.UpdateBudgetSuccessAction(Budget: updatedBudget));
            HandleClose();
            SnackbarService.Show(message: BudgetsResources.BudgetUpdatedSuccessMessage, severity: SnackbarSeverity.Success);
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _errorMessages = exception.ErrorMessages.ToList();
        }
        catch
        {
            SnackbarService.Show(message: BudgetsResources.UnknownError, severity: SnackbarSeverity.Error);
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
        Dispatcher.Dispatch(action: new ModalActions.CloseModalAction(Modal: ModalType.EditBudget));
    }

    public void Dispose() => BudgetState.StateChanged -= HandleBudgetChanged;
}
