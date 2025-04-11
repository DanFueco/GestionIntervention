using GestionIntervention;
using GestionIntervention.Models.Entities;
using GestionIntervention.Seeders;
using GestionIntervention.Services.Interfaces;
using GestionIntervention.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GestionIntervention.Repositories.Interfaces;
using GestionIntervention.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
        )
    );
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<ISeeder, RoleSeeder>();
builder.Services.AddScoped<ISeeder, AdminSeeder>();
builder.Services.AddScoped<DbSeeder>();

builder.Services.AddScoped<IDataAccess<Client>, ClientDataAccess>();
builder.Services.AddScoped<IDataAccess<Intervention>, InterventionDataAccess>();
builder.Services.AddScoped<IDataAccess<TypeIntervention>, TypeInterventionDataAccess>();
builder.Services.AddScoped<TechnicienDataAccess>();

builder.Services.AddScoped<IService<Client>, ClientService>();
builder.Services.AddScoped<IService<TypeIntervention>, TypeInterventionService>();
builder.Services.AddScoped<IService<Intervention>, InterventionService>();
builder.Services.AddScoped<InterventionService>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<TechnicienService>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var dbSeeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
    await dbSeeder.SeedAsync(context);
}

app.Run();
