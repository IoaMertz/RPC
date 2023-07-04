using MessageBrokerDomain.Interfaces;
using ServerAplication.MessageHandlers;
using ServerAplication;
using MessageBrokerInfrastructure;
using Microsoft.Azure.Cosmos;
using Database;
using Microsoft.Azure.Cosmos.Fluent;
using ServerAplication.Messages;

namespace ServerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<CosmosClient>(sp =>
            {
                CosmosClientBuilder cosmosClientBuilder = new CosmosClientBuilder("https://localhost:8081/",
                    "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==")
                    .WithConnectionModeDirect()
                    .WithBulkExecution(true)
                    .WithSerializerOptions(new CosmosSerializationOptions()
                    {
                        PropertyNamingPolicy = CosmosPropertyNamingPolicy.Default
                    });

                return cosmosClientBuilder.Build();
            });

            builder.Services.MessageBrokerInfrastructureRegisterServices();

            builder.Services.ServerApplicationRegisterServices();

            builder.Services.DatabaseRegisterServices();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<CosmosClient>();
                CreateCosmosDatabase.Create(context);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            ConfigureMessageBroker(app);

            app.Run();
        }


        private static void ConfigureMessageBroker(WebApplication app)
        {
            var messageBroker = app.Services.GetRequiredService<IMessageBroker>();
            messageBroker.DeclareQueue("CalculationRequestQueue");
            messageBroker.SubscribeReply<CalculationRequestMessage, CalculationRequestMessageHandler>("CalculationRequestQueue");

        }
    }
}