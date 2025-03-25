using RestauranteAPI.Services;

var builder = WebApplication.CreateBuilder(args);
// Registrar o serviço do Firebase
builder.Services.AddSingleton<FirebaseService>();
// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
var app = builder.Build();
// Habilitar o Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();