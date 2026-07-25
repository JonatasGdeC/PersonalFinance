using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using PersonalFinance.Adapter.Exceptions;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Budget;
using PersonalFinance.Communication.Responses.Category;
using PersonalFinance.Communication.Validators;
using PersonalFinance.Web.Components.AddInput;
using PersonalFinance.Web.Resources.Budgets;
using PersonalFinance.Web.Services.SnackbarService;
using PersonalFinance.Web.UseState.Budget;
using PersonalFinance.Web.UseState.Category;
using PersonalFinance.Web.UseState.Modal;

namespace PersonalFinance.Web.Pages.Budgets.Components.ModalRegisterBudget;

public partial class ModalRegisterBudget
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
            HashSet<string> usedColors = BudgetListState.Value.Budgets.Select(selector: budget => budget.Color).ToHashSet();

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

    protected override async Task OnInitializedAsync()
    {
        if (!CategoryListState.Value.Categories.Any())
        {
            Dispatcher.Dispatch(action: new CategoryActions.GetAllCategoriesAction());
            GetAllCategoryResponse? response = await PersonalFinanceApi.Category.GetAll();
            if (response != null)
            {
                Dispatcher.Dispatch(action: new CategoryActions.GetAllCategoriesSuccessAction(Categories: response.ListCategories));
            }
        }

        await base.OnInitializedAsync();
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

    private async Task HandleRegisterBudget()
    {
        _errorMessages = [];

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

        _isSubmitting = true;

        try
        {
            BudgetDto budget = await PersonalFinanceApi.Budget.Register(request: request);
            Dispatcher.Dispatch(action: new BudgetActions.RegisterBudgetSuccessAction(Budget: budget));
            ResetForm();
            HandleClose();
            SnackbarService.Show(message: BudgetsResources.BudgetRegisteredSuccessMessage, severity: SnackbarSeverity.Success);
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

    private void ResetForm()
    {
        _categoryValue = string.Empty;
        _colorValue = string.Empty;
        _maximumSpendText = string.Empty;
        CancelAddCustomColor();
    }

    private void HandleClose()
    {
        _errorMessages = [];
        Dispatcher.Dispatch(action: new ModalActions.CloseModalAction(Modal: ModalType.AddBudget));
    }
}
