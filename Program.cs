using APIMongoDB.Services;
using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddSingleton<ProdutoService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// 1. Adiciona os serviços do Swagger ao container do projeto

builder.Services.AddEndpointsApiExplorer();


// 1. Configura o suporte a versionamento no código
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0); // Versão padrão se o cliente não informar
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true; // Retorna a versão nos headers HTTP
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // Formata como 'v1', 'v2'
    options.SubstituteApiVersionInUrl = true; // Substitui o parâmetro {version:apiVersion} na rota
});

// 2. Configura o Swagger para gerar documentações separadas
builder.Services.AddSwaggerGen(options =>
{
    // Documentação da Versão 1.0
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Minha API de Produtos - V1",
        Version = "v1",
        Description = "Versão legada da API de produtos."
    });

    // Documentação da Nova Versão 2.0
    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Minha API de Produtos - V2",
        Version = "v2",
        Description = "Nova versão otimizada da API de produtos."
    });

    // Inclui os comentários XML que você já aprendeu a fazer
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// 3. Configura a Interface do Swagger para exibir o seletor de versões
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // Cria os endpoints na interface gráfica para cada versão disponível
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Produtos V1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "API de Produtos V2");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
