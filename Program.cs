using AuthApi.Data;
using AuthApi.Services; // <-- Não esqueça de incluir o namespace onde está TokenService!
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configurar Banco de Dados PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar serviços personalizados
builder.Services.AddScoped<TokenService>(); // <-- Adicionado TokenService

// Carregar configurações do JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];

if (string.IsNullOrEmpty(secretKey))
{
    throw new Exception("A chave secreta do JWT (JwtSettings:SecretKey) não foi configurada no appsettings.json!");
}

var key = Encoding.UTF8.GetBytes(secretKey);

// Configurar Autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        
        ValidateIssuer = false, // Não validar issuer
        ValidateAudience = false, // Não validar audience

        ValidateLifetime = true, // Valida expiração do token
        ClockSkew = TimeSpan.Zero // Sem tolerância de expiração
    };
});

// Adicionar Controllers
builder.Services.AddControllers();

// Construir a aplicação
var app = builder.Build();

// Configurar Middlewares
app.UseAuthentication();
app.UseAuthorization();

// Mapear Endpoints
app.MapControllers();


//Usar a porta 5000
app.Urls.Add("http://localhost:5000");

// Rodar aplicação
app.Run();
