using EventsApi.EventContext;
using EventsApi.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("MvcContext");

//builder.Services.AddDbContext<EventContextDb>(u => u.UseInMemoryDatabase("EventsDb")); // vai funcionar como se fosse um banco de dados, usando dados em memoria
//builder.Services.AddDbContext<EventContextDb>(o => o.UseSqlServer(connectionString)); //Conexão com Sql
builder.Services.AddDbContext<EventContextDb>(options =>
	options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(EventProfile).Assembly);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s => {
	s.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "Events.Api",
		Version = "v1",
		Contact = new OpenApiContact
		{
			Name = "Sidartha",
			Email = "Galtamasmoraog@gmail.com",
			Url = new Uri("https://www.linkedin.com/in/sidartha-smoraog/")

		}


	});

	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	s.IncludeXmlComments(xmlPath);

});

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
