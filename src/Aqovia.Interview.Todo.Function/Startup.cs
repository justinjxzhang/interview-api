using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Aqovia.Interview.Todo.Function.Startup))]

namespace Aqovia.Interview.Todo.Function {
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // builder.AddSwashBuckle(Assembly.GetExecutingAssembly(), opts => {
            // });
        }
    }
}