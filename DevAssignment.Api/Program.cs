using DevAssignment.CommonLayer.Dtos;
using DevAssignment.CommonLayer.Validators;
using DevAssignment.RepositoryLayer.Implementations;
using DevAssignment.RepositoryLayer.Repositories;
using DevAssignment.ServiceLayer.Implementations;
using DevAssignment.ServiceLayer.Services;
using eKYC.Consumer.Core.Common.Constants;
using EntityLayer.DbContext;
using EntityLayer.DbContext.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins(builder.Configuration.GetValue<string>("AllowedHosts"))
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<User, IdentityRole>(option =>
{
    option.Password.RequireDigit = true;
    option.Password.RequiredLength = 8;
    option.Password.RequireNonAlphanumeric = true;
    option.Password.RequireUppercase = true;
    option.Password.RequireLowercase = true;
}).AddEntityFrameworkStores<DevAssignmentDbCOntext>()
             .AddDefaultTokenProviders();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddTransient<IValidator<RegisterUserDto>, RegisterUserValidator>();

builder.Services.AddDbContext<DevAssignmentDbCOntext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(AppSettings.CONNECTION_STRING)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
