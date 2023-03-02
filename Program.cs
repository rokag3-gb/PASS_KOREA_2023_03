namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.WebHost.ConfigureKestrel(opt => {
                opt.ListenAnyIP(5000);
            });
            
            var app = builder.Build();

            app.MapGet("/HelloWorld", (string name) => $"Hello World! {name}. Minimal API�� �̷��� ���°̴ϴ�.");
            
            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            // ���� ȯ���� �ƴϾ Swagger UI ��� (������� ����)
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}