using System;
using System.Collections.Generic;
using Aqovia.Interview.Data.InMemory.Repositories;
using Aqovia.Interview.Data.Repositories;
using Aqovia.Interview.Todo.Data.Models;
using Aqovia.Interview.Todo.Data.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Aqovia.Interview.Todo.Function.Startup))]

namespace Aqovia.Interview.Todo.Function {
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var todoSeed = new List<TodoModel>() {
                new TodoModel() {
                    Id = Guid.NewGuid(),
                    Completed = false,
                    Description = "Buy milk"
                },
                new TodoModel() {
                    Id = Guid.NewGuid(),
                    Completed = true,
                    Description = "Write interview test questions"
                }
            };

            builder.Services.AddSingleton<IBasicRepository<Guid, TodoModel>>(sp => {
                return new BasicInMemoryRepository<TodoModel>(todoSeed);
            });

            builder.Services.AddScoped<TodoService>();
            
        }
    }
}