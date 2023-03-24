using System;
using System.Linq;
using System.Threading.Tasks;
using Aqovia.Interview.Todo.Data.DTO;
using Aqovia.Interview.Todo.Data.Extensions;
using Aqovia.Interview.Todo.Function.DTO;
using Aqovia.Interview.Todo.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using System.IO;
using Newtonsoft.Json;

namespace Aqovia.Interview.Todo.Function.Functions
{
    public class TodoApi {
        public const string API_VERSION = "v1";
        public const string CONTROLLERNAME = "todo";
        private readonly ILogger<TodoApi> _logger;
        private readonly TodoService _todoService;

        public TodoApi(
            TodoService todoService,
            ILogger<TodoApi> logger
        ) {
            this._logger = logger;
            this._todoService = todoService;
        }

        [FunctionName("HttpTrigger_TodoApi_GetTodos")]
        public async Task<IActionResult> GetTodos(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "v1/todo")] HttpRequest req
        ) {
            var allTodos = this._todoService.GetTodos(x => true);
            var todoDtos = allTodos.Select(tdModel => tdModel.ToTodoDto()).ToList();

            return new OkObjectResult(todoDtos);
        }


        [FunctionName("HttpTrigger_TodoApi_UpdateTodo")]
        public async Task<IActionResult> UpdateTodo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "PUT", Route = "v1/todo")] HttpRequest req,
            Guid id
        ) {
            try {
                TodoInputDto payload;
                using (var sr = new StreamReader(req.Body)) {
                    var body = await sr.ReadToEndAsync();
                    if (string.IsNullOrWhiteSpace(body)) {
                        throw new ArgumentException("Body is empty");
                    }
                    payload = JsonConvert.DeserializeObject<TodoInputDto>(body);
                }
                var success = this._todoService.UpdateTodo(id, payload.Completed, payload.Description);
                if (success) {
                    return new NoContentResult();
                }
                else {
                    return new BadRequestObjectResult(new ErrorDto() {
                        Message = "Unexpected error occurred"
                    });
                }
                
            }
            catch (JsonException jse) {
                this._logger.LogError(jse, jse.Message);
                return new BadRequestObjectResult(new ErrorDto() {
                    Message = "Malformed data"
                });
            }
            catch (KeyNotFoundException knfe) {
                this._logger.LogError(knfe, knfe.Message);
                return new BadRequestObjectResult(new ErrorDto() {
                    Message = "Todo not found"
                });
            }
            catch (ArgumentException ae) {
                this._logger.LogError(ae, ae.Message);
                return new BadRequestObjectResult(new ErrorDto() {
                    Message = "Body was empty"
                });
            }
            return new NoContentResult();
        }
    }
}
