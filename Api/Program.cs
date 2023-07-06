using Application.Messages;
using MessageBrokerDomain.Interfaces;
using MessageBrokerInfrastructure;
using Application;
using Application.MessageHandlers;
using Database;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Cosmos;

namespace CalculationsApi
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

            
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            builder.Services.MessageBrokerInfrastructureRegisterServices();

            builder.Services.ClientApplicationRegisterServices();

            //this should not be here !!!! client must not reference DB

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
            //this should not be here
            builder.Services.DatabaseRegisterServices();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "DefaultPolicy",
                    policy =>
                    {
                        policy
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("DefaultPolicy");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            ConfigureEventBus(app);

            app.Run();
        }

        private static void ConfigureEventBus(WebApplication app)
        {
            var messageBroker = app.Services.GetRequiredService<IMessageBroker>();
            messageBroker.DeclareQueue("CalculationRequestReplyQueue");
            messageBroker.SubscribeRPC<CalculationResponseMessage, CalculationResponseMessageHandler>("CalculationRequestReplyQueue");

            //login

            messageBroker.DeclareQueue("UserValidationReplyQueue");
            messageBroker.SubscribeRPC<UserValidationResponseMessage,UserValidationResponseMessageHandler>("UserValidationReplyQueue");


            
        }
    }
}