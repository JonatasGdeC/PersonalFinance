using Microsoft.Extensions.DependencyInjection;
using PersonalFinance.Application.UseCase.User.Delete;
using PersonalFinance.Application.UseCase.User.ForgotPassword;
using PersonalFinance.Application.UseCase.User.Get;
using PersonalFinance.Application.UseCase.User.Login;
using PersonalFinance.Application.UseCase.User.Register;
using PersonalFinance.Application.UseCase.User.ResetPassword;
using PersonalFinance.Application.UseCase.User.Update;
using PersonalFinance.Application.UseCase.User.UpdatePassword;
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
        service.AddScoped<IGetUserUseCase, GetUserUseCase>();
        service.AddScoped<IForgotPassword, ForgotPassword>();
        service.AddScoped<IValidateResetCodeUseCase, ValidateResetCodeUseCase>();
        service.AddScoped<IResetPassword, ResetPassword>();
    }
}