using Microsoft.EntityFrameworkCore;
using Context;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
using Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<TarefaContext>(
    options => options.UseNpgsql(
        builder.Configuration.GetConnectionString("StringPg")
    )
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
		{
   		 c.SwaggerDoc("v1", new OpenApiInfo { Title = "Agenda",Version = "v1" });
         c.UseInlineDefinitionsForEnums();
         c.MapType<StatusTarefa>(() => new OpenApiSchema
         {
            Type = "string",
            Enum = Enum.GetNames(typeof(StatusTarefa)).Select(name => new OpenApiString(name)).Cast<IOpenApiAny>().ToList()
         });
        }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Agenda");
        c.RoutePrefix = "swagger";
    });

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
