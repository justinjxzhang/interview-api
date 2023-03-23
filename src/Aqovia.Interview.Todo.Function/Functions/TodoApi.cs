using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace Aqovia.Interview.Todo.Function.Functions
{
    public class TodoApi {
        public const string API_VERSION = "v1";
        public const string CONTROLLERNAME = "todos";

        [OpenApiOperation(operationId: "TodoApi_GetTodos", tags: new[] { "todo" }, Summary = "Get all todos")]
        [FunctionName("HttpTrigger_TodoApi_GetTodos")]
        public static async Task<IActionResult> GetTodos(
            [HttpTrigger(Microsoft.Azure.WebJobs.Extensions.Http.AuthorizationLevel.Anonymous, "GET", Route = $"{TodoApi.API_VERSION}/{TodoApi.CONTROLLERNAME}")]
            HttpRequest req,
            ILogger log
        ) {
            var result = await Task.FromResult(true);
            if (result) {
                return new OkObjectResult(new {
                    hello = "world"
                });
            }
            else {
                return new BadRequestResult();
            }
        }
    }
}
