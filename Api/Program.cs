using Application.Messages;
using MessageBrokerDomain.Interfaces;
using MessageBrokerInfrastructure;
using Application;
using Application.MessageHandlers;

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




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

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
            messageBroker.SubscribeRPC<CalculationRequestMessage, CalculationRequestMessageHandler>("CalculationRequestReplyQueue");


            
        }
    }
}