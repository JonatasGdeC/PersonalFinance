using Microsoft.Extensions.DependencyInjection;
using UserService.Application.UseCase.User.Delete;
using UserService.Application.UseCase.User.ForgotPassword;
using UserService.Application.UseCase.User.Get;
using UserService.Application.UseCase.User.Login;
using UserService.Application.UseCase.User.Register;
using UserService.Application.UseCase.User.ResetPassword;
using UserService.Application.UseCase.User.Update;
using UserService.Application.UseCase.User.UpdatePassword;
using UserService.Application.UseCase.User.UpdateProfileImage;
using UserService.Application.UseCase.User.ValidateResetCode;

namespace UserService.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(service: services);
        AddAutoMapperApplication(services: services);
    }
    
    private static void AddAutoMapperApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(configAction: config => { }, typeof(AutoMapping.AutoMapping));
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
    }
}