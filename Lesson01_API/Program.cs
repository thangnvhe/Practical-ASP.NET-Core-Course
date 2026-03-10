using Lesson01_API.Configurations;
using Lesson01_API.Data;
using Lesson01_API.Mapper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// add DbContext
builder.Services.AddSqlServer<AppDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
// add FluentValidation
builder.Services.AddFluentValidation();
// add Dependence Injections
builder.Services.AddDependenceInjections();
// add AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMappers>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();
