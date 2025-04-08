using ProductApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar Kestrel para escuchar en puertos HTTP y HTTPS
builder.WebHost.ConfigureKestrel(options =>
{
    // Puerto HTTP (por ejemplo, 5000)
    options.ListenAnyIP(5000);

    // Puerto HTTPS (por ejemplo, 5001) con certificado (en desarrollo se puede usar un certificado de desarrollo)
    options.ListenAnyIP(5001, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// Registro de la inyección de dependencias para ProductService
builder.Services.AddSingleton<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();


