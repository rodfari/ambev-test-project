using pgSQL;
using Application;
using MongoData;
using Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string pgConn = builder.Configuration.GetConnectionString("pgConnection")!;

var mongoSettings = builder.Configuration.GetSection("MongoSettings").Get<MongoSettings>();

builder.Services.AddPostgresDatabase(pgConn);

builder.Services.AddMongoServices(setting => {
    setting.ConnectionString = mongoSettings!.ConnectionString;
    setting.DatabaseName = mongoSettings.DatabaseName;
});

builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

DatabaseInitialization
.CreateAndSeed(app);


app.Run();
