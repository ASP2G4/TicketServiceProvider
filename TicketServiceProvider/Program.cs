using Microsoft.EntityFrameworkCore;
using TicketServiceProvider.Business.Services;
using TicketServiceProvider.Data.Contexts;
using TicketServiceProvider.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddGrpc();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITicketService, TicketService>();

var app = builder.Build();


app.MapOpenApi();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapGrpcService<TicketService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client.");
app.MapControllers();

app.Run();
