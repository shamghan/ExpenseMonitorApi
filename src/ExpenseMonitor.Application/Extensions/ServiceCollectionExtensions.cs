using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ExpenseMonitor.Application.Restaurants;
using ExpenseMonitor.Application.User;
using static System.Net.Mime.MediaTypeNames;

namespace ExpenseMonitor.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(applicationAssembly));
        services.AddAutoMapper(applicationAssembly);
        services.AddValidatorsFromAssembly(applicationAssembly).AddFluentValidationAutoValidation();
        //start
        services.AddScoped<IUserContext, UserContext>();
        services.AddHttpContextAccessor();// allow us to inject context accessor to user context service class
        //end

    }
}

