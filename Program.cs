using Microsoft.EntityFrameworkCore;
using MyTodo.Data;

namespace MyTodo;

class Program
{
    public static void Main(string[] args)
    {
        WebApplication app = CreateApp();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapControllers();
        app.Run();
    }

    public static WebApplication CreateApp()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<PracticeContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("db")));

        return builder.Build();
    }
}
