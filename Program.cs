var builder = WebApplication.CreateBuilder(args);

string? connectionStringProducao = builder.Configuration.GetConnectionString("Producao");
///string? connectionStringSimulacao = builder.Configuration.GetConnectionString("Simulacao");

string? connectionStringSimulacao = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.0.70.146)(PORT=1521)))(CONNECT_DATA=(SID=SIMULACAO))); User Id=dbamv; Password=dbati2019";

// Add services to the container.
builder.Services.AddScoped<ITotemService>(provider => new TotemService(connectionStringProducao));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();
