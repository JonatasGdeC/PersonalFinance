using Microsoft.Extensions.DependencyInjection;
using PersonalFinance.Application.UseCase.Bill.Delete;
using PersonalFinance.Application.UseCase.Bill.GetAll;
using PersonalFinance.Application.UseCase.Bill.GetDashboard;
using PersonalFinance.Application.UseCase.Bill.Register;
using PersonalFinance.Application.UseCase.Bill.Update;
using PersonalFinance.Application.UseCase.Budget.Delete;
using PersonalFinance.Application.UseCase.Budget.GetAll;
using PersonalFinance.Application.UseCase.Budget.Register;
using PersonalFinance.Application.UseCase.Budget.Update;
using PersonalFinance.Application.UseCase.Category.Delete;
using PersonalFinance.Application.UseCase.Category.GetAll;
using PersonalFinance.Application.UseCase.Category.Register;
using PersonalFinance.Application.UseCase.Category.Update;
using PersonalFinance.Application.UseCase.Participant.Delete;
using PersonalFinance.Application.UseCase.Participant.GetAll;
using PersonalFinance.Application.UseCase.Participant.GetById;
using PersonalFinance.Application.UseCase.Participant.Register;
using PersonalFinance.Application.UseCase.Participant.Update;
using PersonalFinance.Application.UseCase.Pot.Delete;
using PersonalFinance.Application.UseCase.Pot.GetAll;
using PersonalFinance.Application.UseCase.Pot.Register;
using PersonalFinance.Application.UseCase.Pot.Update;
using PersonalFinance.Application.UseCase.Transaction.Delete;
using PersonalFinance.Application.UseCase.Transaction.GetAll;
using PersonalFinance.Application.UseCase.Transaction.GetByCategory;
using PersonalFinance.Application.UseCase.Transaction.GetDashboard;
using PersonalFinance.Application.UseCase.Transaction.Register;
using PersonalFinance.Application.UseCase.Transaction.Update;
using PersonalFinance.Application.UseCase.User.Delete;
using PersonalFinance.Application.UseCase.User.ForgotPassword;
using PersonalFinance.Application.UseCase.User.Get;
using PersonalFinance.Application.UseCase.User.Login;
using PersonalFinance.Application.UseCase.User.Register;
using PersonalFinance.Application.UseCase.User.ResetPassword;
using PersonalFinance.Application.UseCase.User.Update;
using PersonalFinance.Application.UseCase.User.UpdatePassword;
using PersonalFinance.Application.UseCase.User.UpdateProfileImage;
using PersonalFinance.Application.UseCase.User.ValidateResetCode;

namespace PersonalFinance.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(service: services);
    }

    private static void AddUseCases(this IServiceCollection service)
    {
        service.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
        service.AddScoped<ILoginUseCase, LoginUseCase>();
        service.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        service.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        service.AddScoped<IUpdatePasswordUseCase, UpdatePasswordUseCase>();
        service.AddScoped<IUpdateProfileImageUseCase, UpdateProfileImageUseCase>();
        service.AddScoped<IGetUserUseCase, GetUserUseCase>();
        service.AddScoped<IForgotPassword, ForgotPassword>();
        service.AddScoped<IValidateResetCodeUseCase, ValidateResetCodeUseCase>();
        service.AddScoped<IResetPassword, ResetPassword>();
        service.AddScoped<IRegisterPotUseCase, RegisterPotUseCase>();
        service.AddScoped<IUpdatePotUseCase, UpdatePotUseCase>();
        service.AddScoped<IDeletePotUseCase, DeletePotUseCase>();
        service.AddScoped<IGetAllPotUseCase, GetAllPotUseCase>();
        service.AddScoped<IRegisterParticipantUseCase, RegisterParticipantUseCase>();
        service.AddScoped<IUpdateParticipantUseCase, UpdateParticipantUseCase>();
        service.AddScoped<IDeleteParticipantUseCase, DeleteParticipantUseCase>();
        service.AddScoped<IGetAllParticipantUseCase, GetAllParticipantUseCase>();
        service.AddScoped<IGetParticipantByIdUseCase, GetParticipantByIdUseCase>();
        service.AddScoped<IRegisterCategoryUseCase, RegisterCategoryUseCase>();
        service.AddScoped<IUpdateCategoryUseCase, UpdateCategoryUseCase>();
        service.AddScoped<IDeleteCategoryUseCase, DeleteCategoryUseCase>();
        service.AddScoped<IGetAllCategoryUseCase, GetAllCategoryUseCase>();
        service.AddScoped<IRegisterTransactionUseCase, RegisterTransactionUseCase>();
        service.AddScoped<IUpdateTransactionUseCase, UpdateTransactionUseCase>();
        service.AddScoped<IDeleteTransactionUseCase, DeleteTransactionUseCase>();
        service.AddScoped<IGetAllTransactionUseCase, GetAllTransactionUseCase>();
        service.AddScoped<IGetTransactionByCategoryIdUseCase, GetTransactionByCategoryIdUseCase>();
        service.AddScoped<IGetTransactionDashboardUseCase, GetTransactionDashboardUseCase>();
        service.AddScoped<IRegisterBudgetUseCase, RegisterBudgetUseCase>();
        service.AddScoped<IUpdateBudgetUseCase, UpdateBudgetUseCase>();
        service.AddScoped<IDeleteBudgetUseCase, DeleteBudgetUseCase>();
        service.AddScoped<IGetAllBudgetUseCase, GetAllBudgetUseCase>();
        service.AddScoped<IRegisterBillUseCase, RegisterBillUseCase>();
        service.AddScoped<IUpdateBillUseCase, UpdateBillUseCase>();
        service.AddScoped<IDeleteBillUseCase, DeleteBillUseCase>();
        service.AddScoped<IGetAllBillUseCase, GetAllBillUseCase>();
        service.AddScoped<IGetBillDashboardUseCase, GetBillDashboardUseCase>();
    }
}