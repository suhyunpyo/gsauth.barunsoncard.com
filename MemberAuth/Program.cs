using Azure.Identity;
using MemberAuth.Data;
using MemberAuth.Extensions;
using MemberAuth.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{
	builder.Configuration.AddAzureKeyVault(
      new Uri($"https://barunsecret.vault.azure.net/"),
      new DefaultAzureCredential());
}
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

// Add services to the container.
builder.Services.AddJWTTokenServices(builder.Configuration);
// Add barunn config
builder.Services.AddSingleton<BarunnConfig>(builder.Configuration.GetSection("BarunnConfig").Get<BarunnConfig>());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
        }
    });
});

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(
    builder.Configuration.GetConnectionString("BarShopDBConn")
));

builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//웹사이트 기본파일 읽기 설정
//app.UseDefaultFiles();
//wwwroot 파일읽기
app.UseStaticFiles();
app.UseRouting();
app.MapDefaultControllerRoute();

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
	endpoints.MapHealthChecks("/health");
});

app.Run();
