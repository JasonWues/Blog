using Blog.IRepository;
using Blog.IService;
using Blog.Repository;
using Blog.Service;
using SqlSugar.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Bolg.WebApi", Version = "v1" });
});
builder.Services.AddSqlSugar(new IocConfig()
{
    ConnectionString = "server=.;database=BlogDB;uid=sa;pwd=123456;",
    DbType = IocDbType.SqlServer,
    IsAutoCloseConnection = true
});

#region IOCÒÀÀµ×¢Èë
builder.Services.AddCustomIOC();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bolg.WebApi v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public static class IOCExtend
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
}