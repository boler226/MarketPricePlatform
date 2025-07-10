using FluentValidation;
using MarketPrice.Application.Commands.SyncSupportedAssets;
using MarketPrice.Application.Interfaces;
using MarketPrice.Application.Mappings;
using MarketPrice.Domain.Interfaces;
using MarketPrice.Infrastructure.DbContext;
using MarketPrice.Infrastructure.Repositories;
using MarketPrice.Infrastructure.Services;
using MarketPrice.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AssetsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));

builder.Services.AddScoped<IAssetRepository, AssetRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(AssetMappingProfile).Assembly);

builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(SyncSupportedAssetsCommandHandler).Assembly));

builder.Services.AddHttpClient("Fintacharts", client => {
    client.BaseAddress = new Uri(builder.Configuration["Fintacharts:Url"]!);
});
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IFintachartsService, FintachartsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();