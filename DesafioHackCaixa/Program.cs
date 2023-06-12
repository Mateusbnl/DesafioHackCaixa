using MinhaApi;
using MinhaApi.Models;
using MinhaApi.Repositories;
using MinhaApi.Services;

var builder = WebApplication.CreateBuilder(args);

//Funcao para organizar as configuracoes do Mvc, como opcao de supressao de erro no ModelState nos controladores e adicao de Cache
ConfigureMvc(builder);

//Funcao para configurar os servicos do Repositorio, EventHub e Simulacao
ConfigureServices(builder);

var app = builder.Build();
//Carrega as configuracoes contidas no appsettings, como as conexoes do EventHub e carrega a classe de Configuracao (Configuration.cs)
LoadConfiguration(app);
// Mapeia todos os controllers da aplicação, no nosso caso, somente existe um Controller que é o SimulacaoController que encontra-se na pasta Controllers
app.MapControllers();
app.Run();

void LoadConfiguration(WebApplication app) {
    Configuration.sqlConnection = app.Configuration.GetValue<string>("sqlConnection");

    var simulacaoEvent = new Configuration.EventHubConfiguration();
    app.Configuration.GetSection("EventHub").Bind(simulacaoEvent);
    Configuration.eventConfig = simulacaoEvent;
}

void ConfigureMvc(WebApplicationBuilder builder)
{
    builder.Services.AddControllers()
        .ConfigureApiBehaviorOptions(options => {
            options.SuppressModelStateInvalidFilter = true;
        });
    builder.Services.AddMemoryCache();
}
void ConfigureServices(WebApplicationBuilder builder)
{
    // Utilizei injeção de dependência para o Repository que será injetada no SimuulacaoController, assim a cada requisição, iremos possuir apenas uma instancia do ProdutoRepository
    builder.Services.AddScoped<ProdutoRepository>();
    builder.Services.AddSingleton<SimulacaoEvent>();
    builder.Services.AddTransient<Simulacao>();
}