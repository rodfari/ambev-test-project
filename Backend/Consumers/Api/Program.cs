using pgSQL;
using Application;
using MongoData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string pgConn = builder.Configuration.GetConnectionString("pgConnection")!;

var mongoSettings = builder.Configuration.GetSection("MongoSettings").Get<MongoSettings>();
string mongoConn = mongoSettings.ConnectionString!;

builder.Services.AddPostgresDatabase(pgConn);
builder.Services.AddMongoServices(new MongoSettings { ConnectionString = mongoConn, DatabaseName = "Sales" });
builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
