using ecommerce.Infraestrutura.Data;
using ecommerce.Infraestrutura.Repositorios;
using ecommerce.Dominio.Interfaces;
using ecommerce.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// ==================================================================
// 1. CONFIGURAÇÃO DA API
// ==================================================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ==================================================================
// 2. PERSISTÊNCIA (SIMULAÇÃO)
// ==================================================================
// AddSingleton é CRÍTICO aqui. Ele garante que a lista de dados
// fique viva na memória RAM enquanto a API estiver rodando.
builder.Services.AddSingleton<AppDbContext>();

// ==================================================================
// 3. INJEÇÃO DE DEPENDÊNCIA - REPOSITÓRIOS (INFRASTRUCTURE)
// ==================================================================
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

// ==================================================================
// 4. INJEÇÃO DE DEPENDÊNCIA - SERVICES (APPLICATION)
// ==================================================================
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<CarrinhoService>();
builder.Services.AddScoped<PedidoService>();

// ==================================================================
// 5. CONSTRUÇÃO E PIPELINE
// ==================================================================
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();