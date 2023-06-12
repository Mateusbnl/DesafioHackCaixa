using System.Text.Json;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using MinhaApi.Models;

namespace MinhaApi.Services
{
    //Servico do EventHUb, como foi adotado a forma Singleton, foi desabilitado o dispose do producerClient
    public class SimulacaoEvent : IDisposable
    {
        public SimulacaoEvent()
        {
            producerClient = new EventHubProducerClient(Configuration.eventConfig.connectionString, Configuration.eventConfig.eventHubName);
        }
        public Simulacao simulacao { get; set; }
        public static EventHubProducerClient producerClient { get; set; }

        public void Dispose()
        {
            if (producerClient != null)
            {
                producerClient.DisposeAsync();
            };
        }

        public async Task enviaEventoAsync(Simulacao simulacao)
        {
            using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();
            var json = JsonSerializer.Serialize(simulacao);
            eventBatch.TryAdd(new EventData(json));
            try
            {
                producerClient.SendAsync(eventBatch);
                Console.WriteLine("Evento Registrado");
            }
            finally
            {

                // await producerClient.DisposeAsync();
            }

        }
    }
}
