using Aqovia.Interview.Data.InMemory.Repositories;
using Aqovia.Interview.Data.Repositories;
using Aqovia.Interview.Todo.Data.Models;
using Aqovia.Interview.Todo.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(opts => {
    opts.AddDefaultPolicy(pol => {
        pol.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
