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
    ConnectionString = "server=.;uid=sa;pwd=123456,database=BlogDataBase",
    DbType = IocDbType.SqlServer,
    IsAutoCloseConnection = true
});

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
