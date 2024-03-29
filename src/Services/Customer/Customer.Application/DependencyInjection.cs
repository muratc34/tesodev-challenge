﻿using Customer.Application.Services;
using Customer.Application.ValidationRules;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Customer.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateCustomerValidator>());
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UpdateCustomerValidator>());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ICustomerService, CustomerService>();
        }
    }
}
