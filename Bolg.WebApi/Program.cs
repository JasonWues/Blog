using System.Text;
using Blog.IRepository;
using Blog.IService;
using Blog.Repository;
using Blog.Service;
using Blog.WebApi.AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SqlSugar.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Blog.WebApi", Version = "v1",Description = "Metatron"});
    c.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "ֱ�����¿�������Bearer {token} ע������֮����һ���ո�",
        Name = "Authorization",
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddSqlSugar(new IocConfig()
{
    ConnectionString = "server=.;database=BlogDB;uid=sa;pwd=123456;",
    DbType = IocDbType.SqlServer,
    IsAutoCloseConnection = true
});

#region IOC����ע��
builder.Services.AddCustomIOC();
#endregion

#region JWT��Ȩ
builder.Services.AddCustomJWT();
#endregion

#region AutoMapper

builder.Services.AddAutoMapper(typeof(CustomAutoMapperProfile));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bolg.WebApi v1"));
}

app.UseAuthentication();//��Ȩ
app.UseAuthorization();//��Ȩ

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public static class Extend
{
    public static IServiceCollection AddCustomIOC(this IServiceCollection service)
    {
        service.AddScoped<IBlogNewsRepository, BlogNewsRepository>();
        service.AddScoped<IBlogNewsService, BlogNewsService>();
        service.AddScoped<ITypeInfoRepository, TypeInfoRepository>();
        service.AddScoped<ITypeInfoService, TypeInfoService>();
        service.AddScoped<IWriteInfoRepository, WriteInfoRepository>();
        service.AddScoped<IWriteInfoService, WriteInfoService>();
        return service;
    }

    public static IServiceCollection AddCustomJWT(this IServiceCollection service)
    {
        service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SXXC-PRZ5-SAD-DFSFA-METATRX-ON")),
                    ValidateIssuer = true,
                    ValidIssuer = "https://localhost:7155",
                    ValidateAudience = true,
                    ValidAudience = "https://localhost:7155",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(60)
                };
            });
        return service;
    }
}