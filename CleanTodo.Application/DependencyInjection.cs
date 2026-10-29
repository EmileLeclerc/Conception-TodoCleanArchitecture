using CleanTodo.Application.UseCase;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanTodo.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // cette ligne ajoute les validators
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<CreateUseCase>();
        services.AddScoped<DeleteUseCase>();
        services.AddScoped<GetAllUseCase>();
        services.AddScoped<GetUseCase>();
        services.AddScoped<EditUseCase>();


        services.AddScoped<CreateTodoUseCase>();
        services.AddScoped<DeleteTodoUseCase>();
        services.AddScoped<GetTodoUseCase>();
        services.AddScoped<GetAllTodosUseCase>();
        services.AddScoped<ToggleTodoCompleteStatusUseCase>();

        return services;
    }
}