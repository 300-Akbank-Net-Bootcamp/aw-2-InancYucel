using Microsoft.EntityFrameworkCore;
using Vb.Data;

namespace Week2_Task_Controllers;

public class Startup
{
    public IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var connection = Configuration.GetConnectionString("MsSqlConnection"); //Connection String'i yakalayalım
        services.AddDbContext<VbDbContext>(options => options.UseSqlServer(connection)); //Postgre için useNpgsql

        services.AddControllers(); //Controllers klasöründeki class'ları ekler
        services.AddEndpointsApiExplorer(); // EndPointleri keşfeder. Discovers endpoints
        services.AddSwaggerGen(); //Swagger için bir dökümantasyon hazırlar. Prepares documentation for Swagger
        services.AddAutoMapper(typeof(Program).Assembly);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment()) //If we are working in a development environment, UI is enabled.
        {
            app.UseDeveloperExceptionPage(); //Herhangi bir hata olursa göstersin bize
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(x => { x.MapControllers(); }); //?
    }
}