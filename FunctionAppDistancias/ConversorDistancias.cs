using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FunctionAppDistancias
{
    public static class ConversorDistancias
    {
        [FunctionName("ConversorDistancias")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "milhasparakm/{milhas}")] HttpRequest req,
            double milhas,
            ILogger log)
        {
            log.LogInformation("Acionada a Function ConversorDistancias - HTTP Trigger");
            log.LogInformation($"Distancia recebida para conversao: {milhas}");

            if (milhas <= 0.0)
            {
                var mensagem =
                    $"A distancia informada ({milhas}) deve ser maior do que zero!";
                log.LogError(mensagem);
                return new BadRequestObjectResult(
                    new
                    {
                        Sucesso = false,
                        Mensagem = mensagem
                    });
            }
                        
            return new OkObjectResult(new Distancia(milhas));
        }
    }
}