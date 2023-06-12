using Microsoft.AspNetCore.Mvc;
using MinhaApi.Extensions;
using MinhaApi.Models;
using MinhaApi.Repositories;
using MinhaApi.Services;
using MinhaApi.ViewModels;

namespace MInhaApi.Controllers

{
    [ApiController]
    public class SimulacaoController : ControllerBase
    {
        //Endpoint da API, poderiamos colocar como v1/simulacoes por exemplo
        //O caminho do post, cria uuma Task que ira retornar com respostas padronizadas ao requester, utilizo o ViewModels para validar a proposta enviado
        [HttpGet("/v1/simulacoes")]
        public async Task<IActionResult> GetAsync(
            [FromBody] EditorCategoryViewModel model,
            [FromServices] ProdutoRepository produtoRepository,
            [FromServices] SimulacaoEvent simulacaoEvent,
            [FromServices] Simulacao simulacao
        )
        {
            //Se o modelo da proposta conter algum erro, eh retornado BadRequest
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Proposta>(ModelState.GetErrors()));
            try
            {
                //Caso o modelo seja valido, criamos um objeto proposta, que eh necessario ao objeto Simulacao para gerar tanto na ModalidadeSAC e ModalidadePRICE
                var proposta = new Proposta
                {
                    valorDesejado = model.valorDesejado,
                    Prazo = model.prazo
                };

                var produto = await produtoRepository.Get(proposta);

                //Caso nao encontre nenhum produto, retorna Not Found com mensagem padronizada
                if (produto == null)
                    return StatusCode(200,new ResultViewModel<Simulacao>("Nao foi encontrado produto que atenda aos criterios solicitados"));

                //Caso encontre o produto, gera simulacao e gera o Evento para o EventHUB
                simulacao.GeraSimulacao(proposta, produto);
                simulacaoEvent.enviaEventoAsync(simulacao);

                //Retorna a Simulacao em JSON
                return Ok(simulacao);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new ResultViewModel<Simulacao>("EX5E099 - Falha interna no servidor"));
            }
            finally
            {
                //Adicionei para evitar memory leak(testar ainda), pois como o EventHub [e um Singleton, objetos transients talvez n'ao sejam removidos corretamente
                simulacao.Dispose();
            }
        }
    }
}