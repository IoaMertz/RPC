using MessageBrokerDomain.Interfaces;
using MessageBrokerDomain.Messages;
using MessageBrokerInfrastructure;
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

            builder.Services.MessageBrokerInfrastructureRegisterServices();

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

            app.Run();
        }


        //private static void ConfigureEventBus(WebApplication app)
        //{
        //    var messageBroker = app.Services.GetRequiredService<IMessageBroker>();
        //    messageBroker.DeclareQueue("");
        //    messageBroker.Publish( , "CalculationRequestReplyQueue");
            

        //}
    }
}